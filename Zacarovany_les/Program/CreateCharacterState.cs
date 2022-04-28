using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;
using Zacarovany_les.Classes;
using Zacarovany_les.Classes.Interface;

namespace Zacarovany_les
{
    enum Otazky { PRVNI, DRUHA, TRETI, CTVRTA }
    public class CreateCharacterState : State
    {
        //Hudba
        Song createMusic;
        //Zvuk
        SoundEffect click;
        //promenne
        Pohlavi pohlavi;
        Minulost minulost;
        Trida trida;
        Otazky otazky;
        string jmeno;
        string message;
        string text1;
        string text2;
        string text3;
        string text4;
        //Fonty
        SpriteFont fontNadpis;
        SpriteFont fontText;
        //Textures
        Texture2D prazdnaTextura;
        //Buttons
        Button option1;
        Button option2;
        Button option3;

        public CreateCharacterState(ZacarovanyLes game, ContentManager content) : base(game, content)
        {

        }



        public override void LoadContent()
        {
            //Music
            createMusic = _content.Load<Song>("Music\\createmusic");
            //Promenne
            otazky = Otazky.PRVNI;
            //Sound
            click = _content.Load<SoundEffect>("Sound\\click");
            //Fonty
            fontNadpis = _content.Load<SpriteFont>("Fonts\\Nadpis");
            fontText = _content.Load<SpriteFont>("Fonts\\Text");
            //Textury
            prazdnaTextura = new Texture2D(_game.GraphicsDevice, 1, 1);
            prazdnaTextura.SetData(new Color[] { Color.White });

            //Buttons
            option1 = new Button(fontNadpis, new Vector2(100, 300), prazdnaTextura, 150, 40);
            option1.Click += option1Handler;
            option2 = new Button(fontNadpis, new Vector2(325, 300), prazdnaTextura, 150, 40);
            option2.Click += option2Handler;
            option3 = new Button(fontNadpis, new Vector2(550, 300), prazdnaTextura, 150, 40);
            option3.Click += option3Handler;
            MediaPlayer.Play(createMusic);
            MediaPlayer.IsRepeating = true;
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {
            option1.Update(gameTime);
            option2.Update(gameTime);
            option3.Update(gameTime);
            text1 = "";
            text2 = "";
            text3 = "";
            text4 = "";
            switch (otazky)
            {
                case Otazky.PRVNI:
                    message = "V začarovaném lese jste se narodil/a jako?";
                    option1.Text = "Muž";
                    option2.Visible = false;
                    option3.Text = "Žena";
                    if (option1._isMouseOver)
                    {

                        text1 = "Síla +1";
                    }
                    if (option3._isMouseOver)
                    {

                        text1 = "Obratnost +1";
                    }
                    break;
                case Otazky.DRUHA:
                    message = "Během vaší cesty životem vás hodně ovlivnilo setkání s ...";
                    option1.Text = "Rytířem";
                    option2.Visible = true;
                    option2.Text = "Lovcem";
                    option3.Text = "Mágem";
                    if (option1._isMouseOver)
                    {
                        text1 = "Síla +1";
                        text2 = "Brneni +1";
                    }
                    if (option2._isMouseOver)
                    {
                        text1 = "Obratnost +1";
                        text2 = "Zivoty +10";
                    }
                    if (option3._isMouseOver)
                    {
                        text1 = "Inteligence +1";
                        text2 = "Mana +10";
                    }
                    break;
                case Otazky.TRETI:
                    message = "Po uváženém rozhodnutí se z vás stal ...";
                    option1.Text = "Válečník";
                    option2.Visible = true;
                    option2.Text = "Lučištník";
                    option3.Text = "Kouzelník";
                    if (option1._isMouseOver)
                    {
                        text1 = "Síla +3";
                        text2 = "Brnění +2";
                        text3 = "Obratnost -1";
                        text4 = "Inteligence -2";
                    }
                    if (option2._isMouseOver)
                    {
                        text1 = "Obratnost +3";
                        text2 = "Brnění +1";
                        text3 = "Síla -1";
                        text4 = "Inteligence -1";
                    }
                    if (option3._isMouseOver)
                    {
                        text1 = "Inteligence +3";
                        text2 = "Mana +80";
                        text3 = "Obratnost -1";
                        text4 = "Síla -2";
                    }
                    break;
                case Otazky.CTVRTA:
                    jmeno = "Hráč";
                    ZacarovanyLes.utocnik = new Postava(trida, pohlavi, minulost, new Inventar(1, 1), Majitel.Hrac, 1, jmeno);
                    ZacarovanyLes.mapState = new MapState(_game, _content);
                    _game.ChangeState(ZacarovanyLes.mapState);
                    break;
            }

            if (ZacarovanyLes.newState.IsKeyDown(Keys.Escape) && (!ZacarovanyLes.keyDelayed || ZacarovanyLes.oldState.IsKeyUp(Keys.Escape)))
            {
                ZacarovanyLes.keyDelayed = true;
                ZacarovanyLes.keyDelay = ZacarovanyLes.DELAY_TIME;
                _game.ChangeState(ZacarovanyLes.menuState);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            _game.GraphicsDevice.Clear(Color.Black);
            Vector2 velikost = fontNadpis.MeasureString(message);
            spriteBatch.Begin();
            spriteBatch.DrawString(fontNadpis, message, new Vector2(400 - velikost.X / 2, 200 - velikost.Y / 2), Color.White);
            option1.Draw(gameTime, spriteBatch);
            option2.Draw(gameTime, spriteBatch);
            option3.Draw(gameTime, spriteBatch);
            velikost = fontNadpis.MeasureString(text1);
            spriteBatch.DrawString(fontNadpis, text1, new Vector2(400 - velikost.X / 2, 400 - velikost.Y / 2), Color.Green);
            velikost = fontNadpis.MeasureString(text2);
            spriteBatch.DrawString(fontNadpis, text2, new Vector2(400 - velikost.X / 2, 450 - velikost.Y / 2), Color.Green);
            velikost = fontNadpis.MeasureString(text3);
            spriteBatch.DrawString(fontNadpis, text3, new Vector2(400 - velikost.X / 2, 500 - velikost.Y / 2), Color.Red);
            velikost = fontNadpis.MeasureString(text4);
            spriteBatch.DrawString(fontNadpis, text4, new Vector2(400 - velikost.X / 2, 550 - velikost.Y / 2), Color.Red);

            spriteBatch.End();
        }

        private void option1Handler(object sender, EventArgs args)
        {
            click.Play();
            switch (otazky)
            {
                case Otazky.PRVNI:
                    pohlavi = Pohlavi.Muz;
                    otazky = Otazky.DRUHA;
                    break;
                case Otazky.DRUHA:
                    minulost = Minulost.Rytir;
                    otazky = Otazky.TRETI;
                    break;
                case Otazky.TRETI:
                    trida = Trida.Bojovnik;
                    otazky = Otazky.CTVRTA;
                    break;
                case Otazky.CTVRTA:
                    break;
            }
        }
        private void option2Handler(object sender, EventArgs args)
        {
            click.Play();
            switch (otazky)
            {
                case Otazky.PRVNI:
                    otazky = Otazky.DRUHA;
                    break;
                case Otazky.DRUHA:
                    minulost = Minulost.Lovec;
                    otazky = Otazky.TRETI;
                    break;
                case Otazky.TRETI:
                    trida = Trida.Lucistnik;
                    otazky = Otazky.CTVRTA;
                    break;
                case Otazky.CTVRTA:
                    break;
            }
        }
        private void option3Handler(object sender, EventArgs args)
        {
            click.Play();
            switch (otazky)
            {
                case Otazky.PRVNI:
                    pohlavi = Pohlavi.Zena;
                    otazky = Otazky.DRUHA;
                    break;
                case Otazky.DRUHA:
                    minulost = Minulost.Carodej;
                    otazky = Otazky.TRETI;
                    break;
                case Otazky.TRETI:
                    trida = Trida.Kouzelnik;
                    otazky = Otazky.CTVRTA;
                    break;
                case Otazky.CTVRTA:
                    break;
            }
        }
    }
}
