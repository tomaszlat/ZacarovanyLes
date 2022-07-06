using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Zacarovany_les.Classes.Interface;
using Zacarovany_les.Classes.Mapy;
using Zacarovany_les.Classes.Mapy.Objekty;

namespace Zacarovany_les
{
    public class MenuState : State
    {
        //Promenne ...
        private bool isIntro = true;
        //Zvuk
        SoundEffect click;
        //Fonty
        private SpriteFont fontText;
        //Textury
        private Texture2D intro;
        private Texture2D pozadi;
        private Texture2D buttonNovaHra;
        private Texture2D button1v1Texture;
        private Texture2D buttonKonec;
        private Texture2D dvere;
        private Texture2D enemyLehky;
        private Texture2D enemyStredni;
        private Texture2D enemyTezky;
        private Texture2D kamen;
        private Texture2D lahvickaMana;
        private Texture2D lahvickaZdravi;
        private Texture2D postavaDolu;
        private Texture2D postavaLevo;
        private Texture2D postavaNahoru;
        private Texture2D postavaPravo;
        private Texture2D strom;
        private Texture2D trava;
        //Buttons
        Button buttonNewGame;
        Button button1v1;
        Button buttonEndGame;
        public MenuState(ZacarovanyLes game, ContentManager content) : base(game, content)
        {
            
        }

        public override void LoadContent()
        {
            //Sound
            click = _content.Load<SoundEffect>("Sound\\click");
            //Fonts
            fontText = _content.Load<SpriteFont>("Fonts\\Text");
            //Textury
            intro = _content.Load<Texture2D>("Sprites\\GUI\\intro");
            pozadi = _content.Load<Texture2D>("Sprites\\GUI\\hlavni_menu");
            buttonNovaHra = _content.Load<Texture2D>("Sprites\\GUI\\nova_hra");
            button1v1Texture = _content.Load<Texture2D>("Sprites\\GUI\\1_v_1");
            buttonKonec = _content.Load<Texture2D>("Sprites\\GUI\\konec");
            dvere = _content.Load<Texture2D>("Sprites\\PrvkyMapy\\dvere");
            enemyLehky = _content.Load<Texture2D>("Sprites\\PrvkyMapy\\enemy_lehky");
            enemyStredni = _content.Load<Texture2D>("Sprites\\PrvkyMapy\\enemy_stredni");
            enemyTezky = _content.Load<Texture2D>("Sprites\\PrvkyMapy\\enemy_tezky");
            kamen = _content.Load<Texture2D>("Sprites\\PrvkyMapy\\kamen");
            lahvickaMana = _content.Load<Texture2D>("Sprites\\PrvkyMapy\\lahvicka_mana");
            lahvickaZdravi = _content.Load<Texture2D>("Sprites\\PrvkyMapy\\lahvicka_zdravi");
            postavaDolu = _content.Load<Texture2D>("Sprites\\PrvkyMapy\\postava_dolu");
            postavaLevo = _content.Load<Texture2D>("Sprites\\PrvkyMapy\\postava_levo");
            postavaNahoru = _content.Load<Texture2D>("Sprites\\PrvkyMapy\\postava_nahoru");
            postavaPravo = _content.Load<Texture2D>("Sprites\\PrvkyMapy\\postava_pravo");
            strom = _content.Load<Texture2D>("Sprites\\PrvkyMapy\\strom");
            trava = _content.Load<Texture2D>("Sprites\\PrvkyMapy\\trava");
            //Buttons
            buttonNewGame = new Button(fontText, new Vector2(275, 200),buttonNovaHra,250,75,Color.White);
            buttonNewGame.Animated = false;
            buttonNewGame.Click += ButtonNewGameClickedHandler;
            button1v1 = new Button(fontText, new Vector2(275, 300), button1v1Texture, 250, 75, Color.White);
            button1v1.Animated = false;
            button1v1.Click += Button1v1ClickedHandler;
            buttonEndGame = new Button(fontText, new Vector2(275, 400), buttonKonec, 250, 75, Color.White);
            buttonEndGame.Animated = false;
            buttonEndGame.Click += ButtonEndGameClickedHandler;
        }



        public override void Update(GameTime gameTime) {
            if (gameTime.TotalGameTime.TotalSeconds > 5)
            {
                isIntro = false;
            }
            if(!isIntro)
            {
                buttonNewGame.Update(gameTime);
                button1v1.Update(gameTime);
                buttonEndGame.Update(gameTime);
            }

            if (ZacarovanyLes.newState.IsKeyDown(Keys.Escape) && (!ZacarovanyLes.keyDelayed || ZacarovanyLes.oldState.IsKeyUp(Keys.Escape)))
            {
                ZacarovanyLes.keyDelayed = true;
                ZacarovanyLes.keyDelay = ZacarovanyLes.DELAY_TIME;

                if (ZacarovanyLes.gameState != null)
                {
                    _game.ChangeCurrentState(ZacarovanyLes.gameState);
                } 
                else if (ZacarovanyLes.mapState != null)
                {
                    _game.ChangeState(ZacarovanyLes.mapState);
                    
                }
                else
                {
                    _game.Exit();
                }
                
            }
                
        }
        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            if (isIntro)
            {
                spriteBatch.Draw(intro, new Rectangle(0, 0, 800, 600), Color.White);
            }
            else
            {
                spriteBatch.Draw(pozadi, new Rectangle(0, 0, 800, 600), Color.White);
                buttonNewGame.Draw(gameTime,spriteBatch);
                button1v1.Draw(gameTime, spriteBatch);
                buttonEndGame.Draw(gameTime, spriteBatch);

            }
            spriteBatch.End();
        }
        private void ButtonNewGameClickedHandler(object sender, EventArgs args)
        {
            click.Play();
            ZacarovanyLes.gameState = null;
            ZacarovanyLes.mapState = null;
            int cislomapy = 1;
            string mapa = "Maps\\mapa";
            List<Map> maps = new List<Map>();
            while (File.Exists(mapa + cislomapy + ".csv"))
            {
                string[] lines = File.ReadAllLines(mapa + cislomapy + ".csv");
                Objekt[,] objekty = new Objekt[12, 12];
                int i = 0, j = 0;
                Vector2 pozice = new Vector2();
                bool neniHrac = true;
                foreach (string line in lines)
                {
                    string[] col = line.Split(';');
                    Objekt objekt;
                    foreach (string s in col)
                    {
                        Vector2 pos = new Vector2(i, j);
                        switch (s)
                        {
                            case "0":
                                objekt = new Trava(pos,trava);
                                break;
                            case "1":
                                objekt = new Kamen(pos, kamen);
                                break;
                            case "2":
                                objekt = new Strom(pos, strom);
                                break;
                            case "3":
                                if (neniHrac)
                                {
                                    objekt = new Hrac(pos, postavaDolu);
                                    pozice = pos;
                                    neniHrac = false;
                                    break;
                                }
                                objekt = new Trava(pos, trava);
                                break;
                            case "4":
                                objekt = new DvereDalsi(pos, dvere);
                                break;
                            case "5":
                                objekt = new DverePredchozi(pos, dvere);
                                break;
                            case "6":
                                objekt = new SouperEasy(pos, enemyLehky);
                                break;
                            case "7":
                                objekt = new SouperMedium(pos, enemyStredni);
                                break;
                            case "8":
                                objekt = new SouperHard(pos, enemyTezky);
                                break;
                            case "9":
                                objekt = new LahvickaZdravi(pos, lahvickaZdravi);
                                break;
                            case "10":
                                objekt = new LahvickaMany(pos, lahvickaMana);
                                break;
                            case "11":
                                objekt = new DverePosledni(pos, dvere);
                                break;
                            default:
                                objekt = new Trava(pos, trava);
                                break;
                        }

                        objekty[i, j] = objekt;
                        i++;
                        if (i > 11)
                        {
                            i = 0;
                            j++;
                        }
                        if (j == 12)
                            break;
                    }
                }
                Map map = new Map(objekty, pozice);
                maps.Add(map);
                cislomapy++;
            }
            ZacarovanyLes.maps = new MapManager(maps);

            _game.ChangeState(new CreateCharacterState(_game, _content));
        }
        private void Button1v1ClickedHandler(object sender, EventArgs args)
        {
            click.Play();
            _game.ChangeState(new CreateCharactersState(_game, _content));
        }

        private void ButtonEndGameClickedHandler(object sender, EventArgs args)
        {
            click.Play();
            _game.Exit();
        }
    }
}
