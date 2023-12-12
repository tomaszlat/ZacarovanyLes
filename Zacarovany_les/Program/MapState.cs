using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Zacarovany_les.Classes;
using Zacarovany_les.Classes.Interface;
using Zacarovany_les.Classes.Mapy;
using Zacarovany_les.Classes.Mapy.Objekty;
using Zacarovany_les.Classes.Pomocne;

namespace Zacarovany_les
{
    public class MapState : State
    {
        // promenne ...
        // spravce medii
        public SpravceMedii SpravceMedii;
        // pomocne
        // vyhra hry
        private bool vyhra; // určí konec souboje
        private string textVyhra; // Co se vypíše při dohrání hry
        // animace hráče při pohybu na mapě
        private double casAnimaceAktualni = 0; // aktuální čas, který zbývá do dokončení animace
        private bool aktivniAnimace = false; // udává zda se animace právě přehrává, nebo ne
        private const double DOBA_ANIMACE = 0.5; // doba po kterou je každá animace pohybu přehrávána
        private Vector2 novaPozice = new Vector2(); // souřadnice pozice na kterou se hráč přesunuje
        private Hrac animovanaPostava;  // třída Hráče jako postavy na mapě, která je animována
        // fonty
        private Vector2 velikostTextuUtocnik; // velikost textu, je využívána především pro zarovnání textu na střed
        // textury
        private Texture2D utocnikPortret;   // portrét útočníka na levé straně mapy

        public MapState(ZacarovanyLes game, ContentManager content) : base(game, content)
        {
        }

        public override void LoadContent()
        {
            textVyhra = "Vyhrál si!";
            vyhra = false;
            SpravceMedii = ZacarovanyLes.spravceMedii;

        }

        public void UpdateTextures()
        {
            foreach (Map map in ZacarovanyLes.maps.Maps)
            {
                foreach (Objekt obj in map.Objekty)
                {
                    switch (obj)
                    {
                        case DverePosledni _:
                        case DvereDalsi _:
                        case DverePredchozi _:
                            obj.Textura = SpravceMedii.Dvere;
                            break;
                        case Hrac _:
                            obj.Textura = SpravceMedii.PostavaDolu;
                            break;
                        case Kamen _:
                            obj.Textura = SpravceMedii.Kamen;
                            break;
                        case LahvickaMany _:
                            obj.Textura = SpravceMedii.LahvickaMana;
                            break;
                        case LahvickaZdravi _:
                            obj.Textura = SpravceMedii.LahvickaZdravi;
                            break;
                        case SouperEasy _:
                            obj.Textura = SpravceMedii.EnemyLehky;
                            break;
                        case SouperHard _:
                            obj.Textura = SpravceMedii.EnemyTezky;
                            break;
                        case SouperMedium _:
                            obj.Textura = SpravceMedii.EnemyStredni;
                            break;
                        case Strom _:
                            obj.Textura = SpravceMedii.Strom;
                            break;
                        case Trava _:
                            obj.Textura = SpravceMedii.Trava;
                            break;
                    }
                }
            }
        }
        public void JdiVeSmeru(Map aktualni, int X, int Y, Texture2D postava)
        {
            aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), SpravceMedii.Trava);
            aktualni.Objekty[(int)aktualni.PoziceHrace.X + X, (int)aktualni.PoziceHrace.Y + Y] = new Hrac(new Vector2(aktualni.PoziceHrace.X + X, aktualni.PoziceHrace.Y + Y), SpravceMedii.Trava, new Vector2(aktualni.PoziceHrace.X + X, aktualni.PoziceHrace.Y + Y), postava);
            aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X + X, aktualni.PoziceHrace.Y + Y);
        }
        public void NastavAnimaci(int X, int Y, ref Map aktualni)
        {
            aktivniAnimace = true;
            casAnimaceAktualni = 0;
            novaPozice = new Vector2(X, Y);
            Texture2D novaTextura = null;
            if (X == 1)
            {
                novaTextura = SpravceMedii.PostavaPravo;
            }
            if (X == -1)
            {
                novaTextura = SpravceMedii.PostavaLevo;
            }
            if (Y == -1)
            {
                novaTextura = SpravceMedii.PostavaNahoru;
            }
            if (Y == 1)
            {
                novaTextura = SpravceMedii.PostavaDolu;
            }
            animovanaPostava = (Hrac)aktualni.Objekty[((int)aktualni.PoziceHrace.X), (int)aktualni.PoziceHrace.Y];
            animovanaPostava.Textura = novaTextura;
        }


        public void AnimujPresun(GameTime gameTime, ref Map aktualni, Vector2 novaPozice)
        {
            if (casAnimaceAktualni < DOBA_ANIMACE)
            {
                casAnimaceAktualni += gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                aktivniAnimace = false;
                casAnimaceAktualni = 0;
            }
            double aktualniX = aktualni.PoziceHrace.X + novaPozice.X * casAnimaceAktualni / 0.5;
            double aktualniY = aktualni.PoziceHrace.Y + novaPozice.Y * casAnimaceAktualni / 0.5;
            Vector2 novaPoziceHrace = new Vector2((float)aktualniX, (float)aktualniY);
            animovanaPostava.Position = novaPoziceHrace;
        }

        public override void Update(GameTime gameTime)
        {
            if (!ZacarovanyLes.delayed && vyhra)
            {
                _game.ChangeCurrentState(ZacarovanyLes.menuState);
                ZacarovanyLes.gameState = null;
                ZacarovanyLes.mapState = null;
            }


            Map aktualni = ZacarovanyLes.maps.Aktualni;
            if (!aktivniAnimace)
            {
                if (ZacarovanyLes.newState.IsKeyDown(Keys.Left) && (!ZacarovanyLes.keyDelayed || ZacarovanyLes.oldState.IsKeyUp(Keys.Left)) && !vyhra)
                {
                    ZacarovanyLes.keyDelayed = true;
                    ZacarovanyLes.keyDelay = ZacarovanyLes.DELAY_TIME;
                    if (aktualni.PoziceHrace.X - 1 >= 0)
                    {
                        if (aktualni.Objekty[(int)aktualni.PoziceHrace.X - 1, (int)aktualni.PoziceHrace.Y].GetType().BaseType == typeof(Pruchodny))
                        {
                            switch (aktualni.Objekty[(int)aktualni.PoziceHrace.X - 1, (int)aktualni.PoziceHrace.Y])
                            {

                                case DvereDalsi _:
                                    ZacarovanyLes.maps.DalsiMapa();
                                    break;
                                case DverePredchozi _:
                                    ZacarovanyLes.maps.PredchoziMapa();
                                    break;
                                case DverePosledni _:
                                    vyhra = true;
                                    ZacarovanyLes.delay = 3;
                                    ZacarovanyLes.delayed = true;
                                    break;
                                case Trava _:
                                    NastavAnimaci(-1, 0, ref aktualni);
                                    break;
                                case LahvickaMany _:
                                    NastavAnimaci(-1, 0, ref aktualni);
                                    ZacarovanyLes.utocnik.Inventar.LahvickyMany++;
                                    break;
                                case LahvickaZdravi _:
                                    NastavAnimaci(-1, 0, ref aktualni);
                                    ZacarovanyLes.utocnik.Inventar.LahvickyZdravi++;
                                    break;
                                case SouperEasy _:
                                    NastavAnimaci(-1, 0, ref aktualni);
                                    ZacarovanyLes.obrance = Generator.DejLehkehoSoupere();
                                    ZacarovanyLes.gameState = new GameState(_game, _content);
                                    _game.ChangeState(ZacarovanyLes.gameState);
                                    break;
                                case SouperMedium _:
                                    NastavAnimaci(-1, 0, ref aktualni);
                                    ZacarovanyLes.obrance = Generator.DejStrednihoSoupere();
                                    ZacarovanyLes.gameState = new GameState(_game, _content);
                                    _game.ChangeState(ZacarovanyLes.gameState);
                                    break;
                                case SouperHard _:
                                    NastavAnimaci(-1, 0, ref aktualni);
                                    ZacarovanyLes.obrance = Generator.DejTezkehoSoupere(ZacarovanyLes.utocnik);
                                    ZacarovanyLes.gameState = new GameState(_game, _content);
                                    _game.ChangeState(ZacarovanyLes.gameState);
                                    break;
                                default:
                                    break;
                            }
                        }

                    }
                }
                if (ZacarovanyLes.newState.IsKeyDown(Keys.Right) && (!ZacarovanyLes.keyDelayed || ZacarovanyLes.oldState.IsKeyUp(Keys.Right)) && !vyhra)
                {
                    ZacarovanyLes.keyDelayed = true;
                    ZacarovanyLes.keyDelay = ZacarovanyLes.DELAY_TIME;
                    if (aktualni.PoziceHrace.X + 1 < 12)
                    {
                        if (aktualni.Objekty[(int)aktualni.PoziceHrace.X + 1, (int)aktualni.PoziceHrace.Y].GetType().BaseType == typeof(Pruchodny))
                        {
                            switch (aktualni.Objekty[(int)aktualni.PoziceHrace.X + 1, (int)aktualni.PoziceHrace.Y])
                            {
                                case DvereDalsi _:
                                    ZacarovanyLes.maps.DalsiMapa();
                                    break;
                                case DverePredchozi _:
                                    ZacarovanyLes.maps.PredchoziMapa();
                                    break;
                                case DverePosledni _:
                                    vyhra = true;
                                    ZacarovanyLes.delay = 3;
                                    ZacarovanyLes.delayed = true;
                                    break;
                                case Trava _:
                                    NastavAnimaci(1, 0, ref aktualni);
                                    break;
                                case LahvickaMany _:
                                    NastavAnimaci(1, 0, ref aktualni);
                                    ZacarovanyLes.utocnik.Inventar.LahvickyMany++;
                                    break;
                                case LahvickaZdravi _:
                                    NastavAnimaci(1, 0, ref aktualni);
                                    ZacarovanyLes.utocnik.Inventar.LahvickyZdravi++;
                                    break;
                                case SouperEasy _:
                                    NastavAnimaci(1, 0, ref aktualni);
                                    ZacarovanyLes.obrance = Generator.DejLehkehoSoupere();
                                    ZacarovanyLes.gameState = new GameState(_game, _content);
                                    _game.ChangeState(ZacarovanyLes.gameState);
                                    break;
                                case SouperMedium _:
                                    NastavAnimaci(1, 0, ref aktualni);
                                    ZacarovanyLes.obrance = Generator.DejStrednihoSoupere();
                                    ZacarovanyLes.gameState = new GameState(_game, _content);
                                    _game.ChangeState(ZacarovanyLes.gameState);
                                    break;
                                case SouperHard _:
                                    NastavAnimaci(1, 0, ref aktualni);
                                    ZacarovanyLes.obrance = Generator.DejTezkehoSoupere(ZacarovanyLes.utocnik);
                                    ZacarovanyLes.gameState = new GameState(_game, _content);
                                    _game.ChangeState(ZacarovanyLes.gameState);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                if (ZacarovanyLes.newState.IsKeyDown(Keys.Up) && (!ZacarovanyLes.keyDelayed || ZacarovanyLes.oldState.IsKeyUp(Keys.Up)) && !vyhra)
                {
                    ZacarovanyLes.keyDelayed = true;
                    ZacarovanyLes.keyDelay = ZacarovanyLes.DELAY_TIME;
                    if (aktualni.PoziceHrace.Y - 1 >= 0)
                    {
                        if (aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y - 1].GetType().BaseType == typeof(Pruchodny))
                        {
                            switch (aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y - 1])
                            {

                                case DvereDalsi _:
                                    ZacarovanyLes.maps.DalsiMapa();
                                    break;
                                case DverePredchozi _:
                                    ZacarovanyLes.maps.PredchoziMapa();
                                    break;
                                case DverePosledni _:
                                    vyhra = true;
                                    ZacarovanyLes.delay = 3;
                                    ZacarovanyLes.delayed = true;
                                    break;
                                case Trava _:
                                    NastavAnimaci(0, -1, ref aktualni);
                                    break;
                                case LahvickaMany _:
                                    NastavAnimaci(0, -1, ref aktualni);
                                    ZacarovanyLes.utocnik.Inventar.LahvickyMany++;
                                    break;
                                case LahvickaZdravi _:
                                    NastavAnimaci(0, -1, ref aktualni);
                                    ZacarovanyLes.utocnik.Inventar.LahvickyZdravi++;
                                    break;
                                case SouperEasy _:
                                    NastavAnimaci(0, -1, ref aktualni);
                                    ZacarovanyLes.obrance = Generator.DejLehkehoSoupere();
                                    ZacarovanyLes.gameState = new GameState(_game, _content);
                                    _game.ChangeState(ZacarovanyLes.gameState);
                                    break;
                                case SouperMedium _:
                                    NastavAnimaci(0, -1, ref aktualni);
                                    ZacarovanyLes.obrance = Generator.DejStrednihoSoupere();
                                    ZacarovanyLes.gameState = new GameState(_game, _content);
                                    _game.ChangeState(ZacarovanyLes.gameState);
                                    break;
                                case SouperHard _:
                                    NastavAnimaci(0, -1, ref aktualni);
                                    ZacarovanyLes.obrance = Generator.DejTezkehoSoupere(ZacarovanyLes.utocnik);
                                    ZacarovanyLes.gameState = new GameState(_game, _content);
                                    _game.ChangeState(ZacarovanyLes.gameState);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                }
                if (ZacarovanyLes.newState.IsKeyDown(Keys.Down) && (!ZacarovanyLes.keyDelayed || ZacarovanyLes.oldState.IsKeyUp(Keys.Down)) && !vyhra)
                {
                    ZacarovanyLes.keyDelayed = true;
                    ZacarovanyLes.keyDelay = ZacarovanyLes.DELAY_TIME;
                    if (aktualni.PoziceHrace.Y + 1 < 12)
                    {
                        if (aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y + 1].GetType().BaseType == typeof(Pruchodny))
                        {
                            switch (aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y + 1])
                            {
                                case DvereDalsi _:
                                    ZacarovanyLes.maps.DalsiMapa();
                                    break;
                                case DverePredchozi _:
                                    ZacarovanyLes.maps.PredchoziMapa();
                                    break;
                                case DverePosledni _:
                                    vyhra = true;
                                    ZacarovanyLes.delay = 3;
                                    ZacarovanyLes.delayed = true;
                                    break;
                                case Trava _:
                                    NastavAnimaci(0, 1, ref aktualni);
                                    break;
                                case LahvickaMany _:
                                    NastavAnimaci(0, 1, ref aktualni);
                                    ZacarovanyLes.utocnik.Inventar.LahvickyMany++;
                                    break;
                                case LahvickaZdravi _:
                                    NastavAnimaci(0, 1, ref aktualni);
                                    ZacarovanyLes.utocnik.Inventar.LahvickyZdravi++;
                                    break;
                                case SouperEasy _:
                                    NastavAnimaci(0, 1, ref aktualni);
                                    ZacarovanyLes.obrance = Generator.DejLehkehoSoupere();
                                    ZacarovanyLes.gameState = new GameState(_game, _content);
                                    _game.ChangeState(ZacarovanyLes.gameState);
                                    break;
                                case SouperMedium _:
                                    NastavAnimaci(0, 1, ref aktualni);
                                    ZacarovanyLes.obrance = Generator.DejStrednihoSoupere();
                                    ZacarovanyLes.gameState = new GameState(_game, _content);
                                    _game.ChangeState(ZacarovanyLes.gameState);
                                    break;
                                case SouperHard _:
                                    NastavAnimaci(0, 1, ref aktualni);
                                    ZacarovanyLes.obrance = Generator.DejTezkehoSoupere(ZacarovanyLes.utocnik);
                                    ZacarovanyLes.gameState = new GameState(_game, _content);
                                    _game.ChangeState(ZacarovanyLes.gameState);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            else
            {
                AnimujPresun(gameTime, ref aktualni, novaPozice);
                if (!aktivniAnimace)
                {
                    if ((int)novaPozice.X == 1)
                        JdiVeSmeru(aktualni, (int)novaPozice.X, (int)novaPozice.Y, SpravceMedii.PostavaPravo);
                    if ((int)novaPozice.X == -1)
                        JdiVeSmeru(aktualni, (int)novaPozice.X, (int)novaPozice.Y, SpravceMedii.PostavaLevo);
                    if ((int)novaPozice.Y == -1)
                        JdiVeSmeru(aktualni, (int)novaPozice.X, (int)novaPozice.Y, SpravceMedii.PostavaNahoru);
                    if ((int)novaPozice.Y == 1)
                        JdiVeSmeru(aktualni, (int)novaPozice.X, (int)novaPozice.Y, SpravceMedii.PostavaDolu);
                }
            }


            if (ZacarovanyLes.newState.IsKeyDown(Keys.Escape) && (!ZacarovanyLes.keyDelayed || ZacarovanyLes.oldState.IsKeyUp(Keys.Escape)) && !vyhra)
            {
                ZacarovanyLes.keyDelayed = true;
                ZacarovanyLes.keyDelay = ZacarovanyLes.DELAY_TIME;
                _game.ChangeState(ZacarovanyLes.menuState);
            }
            if (ZacarovanyLes.newState.IsKeyDown(Keys.F5) && (!ZacarovanyLes.keyDelayed || ZacarovanyLes.oldState.IsKeyUp(Keys.F5)) && !vyhra)
            {
                ZacarovanyLes.keyDelayed = true;
                ZacarovanyLes.keyDelay = ZacarovanyLes.DELAY_TIME;
                UkladaniNacitani.Uloz();
            }
            if (ZacarovanyLes.newState.IsKeyDown(Keys.OemPlus) && (!ZacarovanyLes.keyDelayed || ZacarovanyLes.oldState.IsKeyUp(Keys.OemPlus)) && !vyhra)
            {
                ZacarovanyLes.keyDelayed = true;
                ZacarovanyLes.keyDelay = ZacarovanyLes.DELAY_TIME;
                ZacarovanyLes.utocnik.PridejZkusenosti(50);
            }
        }
        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            utocnikPortret = SpravceMedii.DejPortret(ZacarovanyLes.utocnik.Pohlavi, ZacarovanyLes.utocnik.Trida);
            //sprites
            spriteBatch.Draw(SpravceMedii.Panel, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(utocnikPortret, new Rectangle(5, 37, 190, 190), Color.White);
            //text
            //portret
            velikostTextuUtocnik = SpravceMedii.FontNadpis.MeasureString(ZacarovanyLes.utocnik.Name);
            spriteBatch.DrawString(SpravceMedii.FontNadpis, ZacarovanyLes.utocnik.Name, new Vector2(100 - velikostTextuUtocnik.X / 2, 20 - velikostTextuUtocnik.Y / 2), Color.Black);
            //trida
            velikostTextuUtocnik = SpravceMedii.FontNadpis.MeasureString(PomocneMetody.TridaToString(ZacarovanyLes.utocnik.Trida, ZacarovanyLes.utocnik.Pohlavi));
            spriteBatch.DrawString(SpravceMedii.FontNadpis, PomocneMetody.TridaToString(ZacarovanyLes.utocnik.Trida, ZacarovanyLes.utocnik.Pohlavi), new Vector2(100 - velikostTextuUtocnik.X / 2, 245 - velikostTextuUtocnik.Y / 2), Color.Black);
            //Pohlavi                    
            velikostTextuUtocnik = SpravceMedii.FontNadpis.MeasureString(PomocneMetody.PohlaviToString(ZacarovanyLes.utocnik.Pohlavi));
            spriteBatch.DrawString(SpravceMedii.FontNadpis, PomocneMetody.PohlaviToString(ZacarovanyLes.utocnik.Pohlavi), new Vector2(100 - velikostTextuUtocnik.X / 2, 280 - velikostTextuUtocnik.Y / 2), Color.Black);
            //Level
            spriteBatch.DrawString(SpravceMedii.FontText, "Level: " + ZacarovanyLes.utocnik.Level, new Vector2(20, 300), Color.Black);
            //Zkusenosti
            spriteBatch.DrawString(SpravceMedii.FontText, "Zkušenosti: " + ZacarovanyLes.utocnik.Zkusenosti + "/" + ZacarovanyLes.utocnik.ZkusenostiNext, new Vector2(20, 320), Color.Black);
            //Zivoty
            spriteBatch.DrawString(SpravceMedii.FontText, "Životy: " + ZacarovanyLes.utocnik.Zivoty + "/" + ZacarovanyLes.utocnik.ZivotyMax, new Vector2(20, 340), Color.Black);
            //Mana
            spriteBatch.DrawString(SpravceMedii.FontText, "Mana: " + ZacarovanyLes.utocnik.Mana + "/" + ZacarovanyLes.utocnik.ManaMax, new Vector2(20, 360), Color.Black);
            //Sila
            spriteBatch.DrawString(SpravceMedii.FontText, "Síla: " + ZacarovanyLes.utocnik.Sila, new Vector2(20, 390), Color.Black);
            //Obratnost
            spriteBatch.DrawString(SpravceMedii.FontText, "Obratnost: " + ZacarovanyLes.utocnik.Obratnost, new Vector2(20, 410), Color.Black);
            //Inteligence
            spriteBatch.DrawString(SpravceMedii.FontText, "Inteligence: " + ZacarovanyLes.utocnik.Inteligence, new Vector2(20, 430), Color.Black);
            //Brneni
            spriteBatch.DrawString(SpravceMedii.FontText, "Brnění: " + ZacarovanyLes.utocnik.Brneni, new Vector2(20, 450), Color.Black);
            Hrac hracNahore = null;
            foreach (Objekt obj in ZacarovanyLes.maps.Aktualni.Objekty)
            {
                if (obj is Hrac hrac)
                {
                    hracNahore = hrac;
                }
                obj.Draw(gameTime, spriteBatch);
            }
            hracNahore?.Draw(gameTime, spriteBatch);
            if (vyhra)
            {
                velikostTextuUtocnik = SpravceMedii.FontNadpis.MeasureString(textVyhra);
                spriteBatch.DrawString(SpravceMedii.FontNadpis, textVyhra, new Vector2(400 - velikostTextuUtocnik.X / 2, 300 - velikostTextuUtocnik.Y / 2), Color.White);
            }
            spriteBatch.End();
        }
    }
}
