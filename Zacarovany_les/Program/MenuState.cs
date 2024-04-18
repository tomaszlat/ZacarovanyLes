using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Zacarovany_les.Classes;
using Zacarovany_les.Classes.Interface;
using Zacarovany_les.Classes.Mapy;
using Zacarovany_les.Classes.Mapy.Objekty;

namespace Zacarovany_les
{
    public class MenuState : State
    {
        //spravce medii
        public SpravceMedii SpravceMedii;
        //promenne ...
        private bool isIntro = true;
        //tlacitka
        Button buttonNewGame;
        Button button1v1;
        Button buttonEndGame;
        public MenuState(ZacarovanyLes game, ContentManager content) : base(game, content)
        {

        }

        public override void LoadContent()
        {
            //spravce medii
            SpravceMedii = ZacarovanyLes.spravceMedii;

            //tlacitka
            buttonNewGame = new Button(SpravceMedii.FontText, new Vector2(275, 200), SpravceMedii.ButtonNovaHra, 250, 75, Color.White)
            {
                Animated = false
            };
            buttonNewGame.Click += ButtonNewGameClickedHandler;
            button1v1 = new Button(SpravceMedii.FontText, new Vector2(275, 300), SpravceMedii.Button1v1, 250, 75, Color.White)
            {
                Animated = false
            };
            button1v1.Click += Button1v1ClickedHandler;
            buttonEndGame = new Button(SpravceMedii.FontText, new Vector2(275, 400), SpravceMedii.ButtonKonec, 250, 75, Color.White)
            {
                Animated = false
            };
            buttonEndGame.Click += ButtonEndGameClickedHandler;
        }



        public override void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalSeconds > 5)
            {
                isIntro = false;
            }
            if (!isIntro)
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
                spriteBatch.Draw(SpravceMedii.Intro, new Rectangle(0, 0, 800, 600), Color.White);
            }
            else
            {
                spriteBatch.Draw(SpravceMedii.HlavniMenu, new Rectangle(0, 0, 800, 600), Color.White);
                buttonNewGame.Draw(gameTime, spriteBatch);
                button1v1.Draw(gameTime, spriteBatch);
                buttonEndGame.Draw(gameTime, spriteBatch);

            }
            spriteBatch.End();
        }
        private void ButtonNewGameClickedHandler(object sender, EventArgs args)
        {
            SpravceMedii.Click.Play();
            ZacarovanyLes.gameState = null;
            ZacarovanyLes.mapState = null;
            int cislomapy = 1;
            string mapa = "Maps\\mapa";
            List<Map> maps = new List<Map>();
            int vyskytHracu = 0;
            while (File.Exists(mapa + cislomapy + ".csv"))
            {
                string[] radky = new string[12];
                try
                {
                    string[] radkyZeSouboru = File.ReadAllLines(mapa + cislomapy + ".csv");
                    for (int i = 0; i < radky.Length; i++)
                    {
                        if (i <= radkyZeSouboru.Length - 1)
                            radky[i] = radkyZeSouboru[i];
                        else
                            radky[i] = "0";
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("RadkyMapy" + ex.Message);
                }

                Objekt[,] objekty = new Objekt[12, 12];
                Vector2 pozice = new Vector2();
                bool jeHrac = false;

                for (int i = 0; i < 12; i++)
                {
                    string[] sloupce = new string[12];

                    try
                    {
                        string[] sloupceZeSouboru = radky[i].Split(';');
                        for (int k = 0; k < sloupce.Length; k++)
                        {
                            if (k <= sloupceZeSouboru.Length - 1)
                                sloupce[k] = sloupceZeSouboru[k];
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("SloupceMapy: " + ex.Message);
                    }

                    for (int j = 0; j < 12; j++)
                    {
                        Objekt objekt;
                        Vector2 pos = new Vector2(j, i);
                        switch (sloupce[j])
                        {
                            case "0":
                                objekt = new Trava(pos, SpravceMedii.Trava);
                                break;
                            case "1":
                                objekt = new Kamen(pos, SpravceMedii.Kamen);
                                break;
                            case "2":
                                objekt = new Strom(pos, SpravceMedii.Strom);
                                break;
                            case "3":
                                if (!jeHrac)
                                {
                                    objekt = new Hrac(pos,SpravceMedii.Trava,pos, SpravceMedii.PostavaDolu);
                                    pozice = pos;
                                    jeHrac = true;
                                    vyskytHracu++;
                                    break;
                                }
                                objekt = new Trava(pos, SpravceMedii.Trava);
                                break;
                            case "4":
                                objekt = new DvereDalsi(pos, SpravceMedii.Dvere);
                                break;
                            case "5":
                                objekt = new DverePredchozi(pos, SpravceMedii.Dvere);
                                break;
                            case "6":
                                objekt = new SouperEasy(pos, SpravceMedii.EnemyLehky);
                                break;
                            case "7":
                                objekt = new SouperMedium(pos, SpravceMedii.EnemyStredni);
                                break;
                            case "8":
                                objekt = new SouperHard(pos, SpravceMedii.EnemyTezky);
                                break;
                            case "9":
                                objekt = new LahvickaZdravi(pos, SpravceMedii.LahvickaZdravi);
                                break;
                            case "10":
                                objekt = new LahvickaMany(pos, SpravceMedii.LahvickaMana);
                                break;
                            case "11":
                                objekt = new DverePosledni(pos, SpravceMedii.Dvere);
                                break;
                            default:
                                objekt = new Trava(pos, SpravceMedii.Trava);
                                break;
                        }

                        objekty[j, i] = objekt;
                    }
                }

                Map map = new Map(objekty, pozice);
                maps.Add(map);
                cislomapy++;
            }
            if (maps.Count > 0 && vyskytHracu == maps.Count)
            {
                ZacarovanyLes.maps = new MapManager(maps);
                _game.ChangeState(new CreateCharacterState(_game, _content));
            }

        }
        private void Button1v1ClickedHandler(object sender, EventArgs args)
        {
            SpravceMedii.Click.Play();
            _game.ChangeState(new CreateCharactersState(_game, _content));
        }

        private void ButtonEndGameClickedHandler(object sender, EventArgs args)
        {
            SpravceMedii.Click.Play();
            _game.Exit();
        }
    }
}
