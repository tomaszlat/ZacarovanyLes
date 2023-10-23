﻿using Microsoft.Xna.Framework;
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
            buttonNewGame = new Button(SpravceMedii.FontText, new Vector2(275, 200),SpravceMedii.ButtonNovaHra,250,75,Color.White);
            buttonNewGame.Animated = false;
            buttonNewGame.Click += ButtonNewGameClickedHandler;
            button1v1 = new Button(SpravceMedii.FontText, new Vector2(275, 300), SpravceMedii.Button1v1, 250, 75, Color.White);
            button1v1.Animated = false;
            button1v1.Click += Button1v1ClickedHandler;
            buttonEndGame = new Button(SpravceMedii.FontText, new Vector2(275, 400), SpravceMedii.ButtonKonec, 250, 75, Color.White);
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
                spriteBatch.Draw(SpravceMedii.Intro, new Rectangle(0, 0, 800, 600), Color.White);
            }
            else
            {
                spriteBatch.Draw(SpravceMedii.HlavniMenu, new Rectangle(0, 0, 800, 600), Color.White);
                buttonNewGame.Draw(gameTime,spriteBatch);
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
                                objekt = new Trava(pos, SpravceMedii.Trava);
                                break;
                            case "1":
                                objekt = new Kamen(pos, SpravceMedii.Kamen);
                                break;
                            case "2":
                                objekt = new Strom(pos, SpravceMedii.Strom);
                                break;
                            case "3":
                                if (neniHrac)
                                {
                                    objekt = new Hrac(pos, SpravceMedii.PostavaDolu);
                                    pozice = pos;
                                    neniHrac = false;
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
