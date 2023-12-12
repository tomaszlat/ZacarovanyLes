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
using Zacarovany_les.Classes.Pomocne;
using Zacarovany_les.Classes.Interface;

namespace Zacarovany_les
{
   
    public class CreateCharactersState : State
    {
        //spravce medii
        public SpravceMedii SpravceMedii;
        //promenne
        string textMajitel;
        string textLevel;
        string textPohlavi;
        string textMinulost;
        string textTrida;
        //Buttons
        Button buttonLevel;
        Button buttonHrac1Majitel;
        Button buttonHrac1Pohlavi;
        Button buttonHrac1Minulost;
        Button buttonHrac1Trida;
        Button buttonHrac2Majitel;
        Button buttonHrac2Pohlavi;
        Button buttonHrac2Minulost;
        Button buttonHrac2Trida;
        Button buttonBoj;

        public CreateCharactersState(ZacarovanyLes game, ContentManager content) : base(game, content)
        {

        }

        public override void LoadContent()
        {
            //spravce medii
            SpravceMedii = ZacarovanyLes.spravceMedii;
            //Promenne
            textMajitel = "Majitel";
            textLevel = "Level";
            textPohlavi = "Pohlaví";
            textMinulost = "Minulost";
            textTrida = "Třída";

            //Buttons
            buttonLevel = new Button(SpravceMedii.FontNadpis, new Vector2(325, 80), SpravceMedii.PrazdnaTexturaBila, 150, 40)
            {
                Text = "5"
            };
            buttonLevel.Click += ButtonLevelleftHandler;
            buttonLevel.RightClick += ButtonLevelrightHandler;
            buttonHrac1Majitel = new Button(SpravceMedii.FontNadpis, new Vector2(25, 80), SpravceMedii.PrazdnaTexturaBila, 150, 40)
            {
                Text = "Hráč"
            };
            buttonHrac1Majitel.Click += ButtonHrac1MajitelHandler;
            buttonHrac1Majitel.RightClick += ButtonHrac1MajitelHandler;

            buttonHrac1Pohlavi = new Button(SpravceMedii.FontNadpis, new Vector2(25, 180), SpravceMedii.PrazdnaTexturaBila, 150, 40)
            {
                Text = "Muž"
            };
            buttonHrac1Pohlavi.Click += ButtonHrac1PohlaviHandler;
            buttonHrac1Pohlavi.RightClick += ButtonHrac1PohlaviHandler;

            buttonHrac1Minulost = new Button(SpravceMedii.FontNadpis, new Vector2(25, 280), SpravceMedii.PrazdnaTexturaBila, 150, 40)
            {
                Text = "Rytíř"
            };
            buttonHrac1Minulost.Click += ButtonHrac1MinulostHandler;
            buttonHrac1Minulost.RightClick += ButtonHrac1MinulostHandlerRight;
            
            buttonHrac1Trida = new Button(SpravceMedii.FontNadpis, new Vector2(25, 380), SpravceMedii.PrazdnaTexturaBila, 150, 40)
            {
                Text = "Válečník"
            };
            buttonHrac1Trida.Click += ButtonHrac1TridaHandler;
            buttonHrac1Trida.RightClick += ButtonHrac1TridaHandlerRight;
            buttonHrac2Majitel = new Button(SpravceMedii.FontNadpis, new Vector2(625, 80), SpravceMedii.PrazdnaTexturaBila, 150, 40)
            {
                Text = "Hráč"
            };
            buttonHrac2Majitel.Click += ButtonHrac2MajitelHandler;
            buttonHrac2Majitel.RightClick += ButtonHrac2MajitelHandler;
            
            buttonHrac2Pohlavi = new Button(SpravceMedii.FontNadpis, new Vector2(625, 180), SpravceMedii.PrazdnaTexturaBila, 150, 40)
            {
                Text = "Muž"
            };
            buttonHrac2Pohlavi.Click += ButtonHrac2PohlaviHandler;
            buttonHrac2Pohlavi.RightClick += ButtonHrac2PohlaviHandler;
            buttonHrac2Minulost = new Button(SpravceMedii.FontNadpis, new Vector2(625, 280), SpravceMedii.PrazdnaTexturaBila, 150, 40)
            {
                Text = "Rytíř"
            };
            buttonHrac2Minulost.Click += ButtonHrac2MinulostHandler;
            buttonHrac2Minulost.RightClick += ButtonHrac2MinulostHandlerRight;
            buttonHrac2Trida = new Button(SpravceMedii.FontNadpis, new Vector2(625, 380), SpravceMedii.PrazdnaTexturaBila, 150, 40)
            {
                Text = "Válečník"
            };
            buttonHrac2Trida.Click += ButtonHrac2TridaHandler;
            buttonHrac2Trida.RightClick += ButtonHrac2TridaHandlerRight;
            buttonBoj = new Button(SpravceMedii.FontNadpis, new Vector2(325, 480), SpravceMedii.PrazdnaTexturaBila, 150, 40)
            {
                Text = "Boj!"
            };
            buttonBoj.Click += ButtonBojHandler;
            
            MediaPlayer.Play(SpravceMedii.CreateMusic);
            MediaPlayer.IsRepeating = true;
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {
            buttonLevel.Update(gameTime);
            buttonHrac1Majitel.Update(gameTime);
            buttonHrac1Pohlavi.Update(gameTime);
            buttonHrac1Minulost.Update(gameTime);
            buttonHrac1Trida.Update(gameTime);
            buttonHrac2Majitel.Update(gameTime);
            buttonHrac2Pohlavi.Update(gameTime);
            buttonHrac2Minulost.Update(gameTime);
            buttonHrac2Trida.Update(gameTime);
            buttonBoj.Update(gameTime);

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
            Vector2 velikost = SpravceMedii.FontNadpis.MeasureString(textLevel);
            spriteBatch.Begin();

            spriteBatch.DrawString(SpravceMedii.FontNadpis, textLevel, new Vector2(400 - velikost.X / 2, 50 - velikost.Y / 2), Color.White);
            buttonLevel.Draw(gameTime, spriteBatch);
            buttonHrac1Majitel.Draw(gameTime, spriteBatch);
            buttonHrac1Pohlavi.Draw(gameTime, spriteBatch);
            buttonHrac1Minulost.Draw(gameTime, spriteBatch);
            buttonHrac1Trida.Draw(gameTime, spriteBatch);

            buttonHrac2Majitel.Draw(gameTime, spriteBatch);
            buttonHrac2Pohlavi.Draw(gameTime, spriteBatch);
            buttonHrac2Minulost.Draw(gameTime, spriteBatch);
            buttonHrac2Trida.Draw(gameTime, spriteBatch);
            buttonBoj.Draw(gameTime, spriteBatch);
            velikost = SpravceMedii.FontNadpis.MeasureString(textMajitel);
            spriteBatch.DrawString(SpravceMedii.FontNadpis, textMajitel, new Vector2(100 - velikost.X / 2, 50 - velikost.Y / 2), Color.White);
            spriteBatch.DrawString(SpravceMedii.FontNadpis, textMajitel, new Vector2(700 - velikost.X / 2, 50 - velikost.Y / 2), Color.White);
            velikost = SpravceMedii.FontNadpis.MeasureString(textPohlavi);
            spriteBatch.DrawString(SpravceMedii.FontNadpis, textPohlavi, new Vector2(100 - velikost.X / 2, 150 - velikost.Y / 2), Color.White);
            spriteBatch.DrawString(SpravceMedii.FontNadpis, textPohlavi, new Vector2(700 - velikost.X / 2, 150 - velikost.Y / 2), Color.White);
            velikost = SpravceMedii.FontNadpis.MeasureString(textMinulost);
            spriteBatch.DrawString(SpravceMedii.FontNadpis, textMinulost, new Vector2(100 - velikost.X / 2, 250 - velikost.Y / 2), Color.White);
            spriteBatch.DrawString(SpravceMedii.FontNadpis, textMinulost, new Vector2(700 - velikost.X / 2, 250 - velikost.Y / 2), Color.White);
            velikost = SpravceMedii.FontNadpis.MeasureString(textTrida);
            spriteBatch.DrawString(SpravceMedii.FontNadpis, textTrida, new Vector2(100 - velikost.X / 2, 350 - velikost.Y / 2), Color.White);
            spriteBatch.DrawString(SpravceMedii.FontNadpis, textTrida, new Vector2(700 - velikost.X / 2, 350 - velikost.Y / 2), Color.White);

            spriteBatch.End();
        }
        private void ButtonLevelleftHandler(object sender, EventArgs args)
        {
            int level = int.Parse(buttonLevel.Text);
            if (level < 20)
            {
                SpravceMedii.Click.Play();
                level++;
                buttonLevel.Text = level.ToString();
            }
            
        }
        private void ButtonLevelrightHandler(object sender, EventArgs args)
        {
            int level = int.Parse(buttonLevel.Text);
            if (level > 1)
            {
                SpravceMedii.Click.Play();
                level--;
                buttonLevel.Text = level.ToString();
            }
        }
        private void ButtonHrac1MajitelHandler(object sender, EventArgs args)
        {
            SpravceMedii.Click.Play();
            switch (buttonHrac1Majitel.Text)
            {
                case "Hráč":
                    buttonHrac1Majitel.Text = "AI";
                    break;
                case "AI":
                    buttonHrac1Majitel.Text = "Hráč";
                    break;
            }
        }
        private void ButtonHrac1PohlaviHandler(object sender, EventArgs args)
        {
            SpravceMedii.Click.Play();
            switch (buttonHrac1Pohlavi.Text)
            {
                case "Muž":
                    buttonHrac1Pohlavi.Text = "Žena";
                    break;
                case "Žena":
                    buttonHrac1Pohlavi.Text = "Muž";
                    break;
            }
        }
        private void ButtonHrac1MinulostHandler(object sender, EventArgs args)
        {
            SpravceMedii.Click.Play();
            switch (buttonHrac1Minulost.Text)
            {
                case "Rytíř":
                    buttonHrac1Minulost.Text = "Lovec";
                    break;
                case "Lovec":
                    buttonHrac1Minulost.Text = "Čaroděj";
                    break;
                case "Čaroděj":
                    buttonHrac1Minulost.Text = "Rytíř";
                    break;
            }
        }
        private void ButtonHrac1MinulostHandlerRight(object sender, EventArgs args)
        {
            SpravceMedii.Click.Play();
            switch (buttonHrac1Minulost.Text)
            {
                case "Rytíř":
                    buttonHrac1Minulost.Text = "Čaroděj";
                    break;
                case "Lovec":
                    buttonHrac1Minulost.Text = "Rytíř";
                    break;
                case "Čaroděj":
                    buttonHrac1Minulost.Text = "Lovec";
                    break;
            }
        }
        private void ButtonHrac1TridaHandler(object sender, EventArgs args)
        {
            SpravceMedii.Click.Play();
            switch (buttonHrac1Trida.Text)
            {
                case "Válečník":
                    buttonHrac1Trida.Text = "Lučištník";
                    break;
                case "Lučištník":
                    buttonHrac1Trida.Text = "Kouzelník";
                    break;
                case "Kouzelník":
                    buttonHrac1Trida.Text = "Válečník";
                    break;
            }
        }

        private void ButtonHrac1TridaHandlerRight(object sender, EventArgs args)
        {
            SpravceMedii.Click.Play();
            switch (buttonHrac1Trida.Text)
            {
                case "Válečník":
                    buttonHrac1Trida.Text = "Kouzelník";
                    break;
                case "Lučištník":
                    buttonHrac1Trida.Text = "Válečník";
                    break;
                case "Kouzelník":
                    buttonHrac1Trida.Text = "Lučištník";
                    break;
            }
        }
        private void ButtonHrac2MajitelHandler(object sender, EventArgs args)
        {
            SpravceMedii.Click.Play();
            switch (buttonHrac2Majitel.Text)
            {
                case "Hráč":
                    buttonHrac2Majitel.Text = "AI";
                    break;
                case "AI":
                    buttonHrac2Majitel.Text = "Hráč";
                    break;
            }
        }
        private void ButtonHrac2PohlaviHandler(object sender, EventArgs args)
        {
            SpravceMedii.Click.Play();
            switch (buttonHrac2Pohlavi.Text)
            {
                case "Muž":
                    buttonHrac2Pohlavi.Text = "Žena";
                    break;
                case "Žena":
                    buttonHrac2Pohlavi.Text = "Muž";
                    break;
            }
        }
        private void ButtonHrac2MinulostHandler(object sender, EventArgs args)
        {
            SpravceMedii.Click.Play();
            switch (buttonHrac2Minulost.Text)
            {
                case "Rytíř":
                    buttonHrac2Minulost.Text = "Lovec";
                    break;
                case "Lovec":
                    buttonHrac2Minulost.Text = "Čaroděj";
                    break;
                case "Čaroděj":
                    buttonHrac2Minulost.Text = "Rytíř";
                    break;
            }
        }
        private void ButtonHrac2MinulostHandlerRight(object sender, EventArgs args)
        {
            SpravceMedii.Click.Play();
            switch (buttonHrac2Minulost.Text)
            {
                case "Rytíř":
                    buttonHrac2Minulost.Text = "Čaroděj";
                    break;
                case "Lovec":
                    buttonHrac2Minulost.Text = "Rytíř";
                    break;
                case "Čaroděj":
                    buttonHrac2Minulost.Text = "Lovec";
                    break;
            }
        }
        private void ButtonHrac2TridaHandler(object sender, EventArgs args)
        {
            SpravceMedii.Click.Play();
            switch (buttonHrac2Trida.Text)
            {
                case "Válečník":
                    buttonHrac2Trida.Text = "Lučištník";
                    break;
                case "Lučištník":
                    buttonHrac2Trida.Text = "Kouzelník";
                    break;
                case "Kouzelník":
                    buttonHrac2Trida.Text = "Válečník";
                    break;
            }
        }
        private void ButtonHrac2TridaHandlerRight(object sender, EventArgs args)
        {
            SpravceMedii.Click.Play();
            switch (buttonHrac2Trida.Text)
            {
                case "Válečník":
                    buttonHrac2Trida.Text = "Kouzelník";
                    break;
                case "Lučištník":
                    buttonHrac2Trida.Text = "Válečník";
                    break;
                case "Kouzelník":
                    buttonHrac2Trida.Text = "Lučištník";
                    break;
            }
        }
        private void ButtonBojHandler(object sender, EventArgs args)
        {
            SpravceMedii.Click.Play();
            ZacarovanyLes.utocnik = new Postava(PomocneMetody.StringToTrida(buttonHrac1Trida.Text), PomocneMetody.StringToPohlavi(buttonHrac1Pohlavi.Text), PomocneMetody.StringToMinulost(buttonHrac1Minulost.Text), new Inventar(1, 1), PomocneMetody.StringToMajitel(buttonHrac1Majitel.Text), int.Parse(buttonLevel.Text), Generator.DejJmeno(PomocneMetody.StringToPohlavi(buttonHrac1Pohlavi.Text)));
            ZacarovanyLes.obrance = new Postava(PomocneMetody.StringToTrida(buttonHrac2Trida.Text), PomocneMetody.StringToPohlavi(buttonHrac2Pohlavi.Text), PomocneMetody.StringToMinulost(buttonHrac2Minulost.Text), new Inventar(1, 1), PomocneMetody.StringToMajitel(buttonHrac2Majitel.Text), int.Parse(buttonLevel.Text), Generator.DejJmeno(PomocneMetody.StringToPohlavi(buttonHrac2Pohlavi.Text)));
            ZacarovanyLes.mapState = null;
            GameState gameState = new GameState(_game, _content)
            {
                Duel = true
            };
            ZacarovanyLes.gameState = gameState;
            

            _game.ChangeState(ZacarovanyLes.gameState);
        }
    }
}
