using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using Zacarovany_les.Classes;
using Zacarovany_les.Classes.Interface;

namespace Zacarovany_les
{
    enum Otazky { PRVNI, DRUHA, TRETI, CTVRTA }
    public class CreateCharacterState : State
    {
        //spravce medii
        public SpravceMedii SpravceMedii;
        //promenne
        Pohlavi pohlavi;
        Minulost minulost;
        Trida trida;
        Otazky otazky;
        string message;
        string text1;
        string text2;
        string text3;
        string text4;
        //Buttons
        Button option1;
        Button option2;
        Button option3;

        public CreateCharacterState(ZacarovanyLes game, ContentManager content) : base(game, content)
        {

        }



        public override void LoadContent()
        {
            //spravce medii
            SpravceMedii = ZacarovanyLes.spravceMedii;
            //Promenne
            otazky = Otazky.PRVNI;
            //Textury
            SpravceMedii.PrazdnaTexturaBila = new Texture2D(_game.GraphicsDevice, 1, 1);
            SpravceMedii.PrazdnaTexturaBila.SetData(new Color[] { Color.White });

            //Buttons
            option1 = new Button(SpravceMedii.FontNadpis, new Vector2(100, 300), SpravceMedii.PrazdnaTexturaBila, 150, 40);
            option1.Click += Option1Handler;
            option2 = new Button(SpravceMedii.FontNadpis, new Vector2(325, 300), SpravceMedii.PrazdnaTexturaBila, 150, 40);
            option2.Click += Option2Handler;
            option3 = new Button(SpravceMedii.FontNadpis, new Vector2(550, 300), SpravceMedii.PrazdnaTexturaBila, 150, 40);
            option3.Click += Option3Handler;
            MediaPlayer.Play(SpravceMedii.CreateMusic);
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
                        text2 = "Brneni +2";
                    }
                    if (option2._isMouseOver)
                    {
                        text1 = "Obratnost +1";
                        text2 = "Zivoty +20";
                    }
                    if (option3._isMouseOver)
                    {
                        text1 = "Inteligence +1";
                        text2 = "Mana +20";
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
                        text2 = "Mana +60";
                        text3 = "Obratnost -1";
                        text4 = "Síla -2";
                    }
                    break;
                case Otazky.CTVRTA:
                    ZacarovanyLes.utocnik = new Postava(trida, pohlavi, minulost, new Inventar(1, 1), Majitel.Hrac, 1, Generator.DejJmeno(pohlavi));
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
            Vector2 velikost = SpravceMedii.FontNadpis.MeasureString(message);
            spriteBatch.Begin();
            spriteBatch.DrawString(SpravceMedii.FontNadpis, message, new Vector2(400 - velikost.X / 2, 200 - velikost.Y / 2), Color.White);
            option1.Draw(gameTime, spriteBatch);
            option2.Draw(gameTime, spriteBatch);
            option3.Draw(gameTime, spriteBatch);
            velikost = SpravceMedii.FontNadpis.MeasureString(text1);
            spriteBatch.DrawString(SpravceMedii.FontNadpis, text1, new Vector2(400 - velikost.X / 2, 400 - velikost.Y / 2), Color.Green);
            velikost = SpravceMedii.FontNadpis.MeasureString(text2);
            spriteBatch.DrawString(SpravceMedii.FontNadpis, text2, new Vector2(400 - velikost.X / 2, 450 - velikost.Y / 2), Color.Green);
            velikost = SpravceMedii.FontNadpis.MeasureString(text3);
            spriteBatch.DrawString(SpravceMedii.FontNadpis, text3, new Vector2(400 - velikost.X / 2, 500 - velikost.Y / 2), Color.Red);
            velikost = SpravceMedii.FontNadpis.MeasureString(text4);
            spriteBatch.DrawString(SpravceMedii.FontNadpis, text4, new Vector2(400 - velikost.X / 2, 550 - velikost.Y / 2), Color.Red);

            spriteBatch.End();
        }

        private void Option1Handler(object sender, EventArgs args)
        {
            SpravceMedii.Click.Play();
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
                    trida = Trida.Valecnik;
                    otazky = Otazky.CTVRTA;
                    break;
                case Otazky.CTVRTA:
                    break;
            }
        }
        private void Option2Handler(object sender, EventArgs args)
        {
            SpravceMedii.Click.Play();
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
        private void Option3Handler(object sender, EventArgs args)
        {
            SpravceMedii.Click.Play();
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
