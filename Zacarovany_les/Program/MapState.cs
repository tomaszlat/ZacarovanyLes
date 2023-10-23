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
        //promenne ...
        //spravce medii
        public SpravceMedii SpravceMedii;
        //pomocne
        private bool vyhra;
        private string textVyhra;
        //fonty
        private Vector2 delkaU;
        //textury
        private Texture2D utocnikPortret;

        public MapState(ZacarovanyLes game, ContentManager content) : base(game, content)
        {
        }

        public override void LoadContent()
        {
            textVyhra = "Vyhrál si!";
            vyhra = false;
            SpravceMedii = ZacarovanyLes.spravceMedii;
            
        }

        public void UpdateTextures() {
            foreach (Map map in ZacarovanyLes.maps.Maps)
            {
                foreach (Objekt obj in map.Objekty)
                {
                    switch (obj)
                    {
                        case DverePosledni po:
                        case DvereDalsi da:
                        case DverePredchozi pr:
                            obj.Textura = SpravceMedii.Dvere;
                            break;
                        case Hrac h:
                            obj.Textura = SpravceMedii.PostavaDolu;
                            break;
                        case Kamen k:
                            obj.Textura = SpravceMedii.Kamen;
                            break;
                        case LahvickaMany l:
                            obj.Textura = SpravceMedii.LahvickaMana;
                            break;
                        case LahvickaZdravi l:
                            obj.Textura = SpravceMedii.LahvickaZdravi;
                            break;
                        case SouperEasy s:
                            obj.Textura = SpravceMedii.EnemyLehky;
                            break;
                        case SouperHard s:
                            obj.Textura = SpravceMedii.EnemyTezky;
                            break;
                        case SouperMedium s:
                            obj.Textura = SpravceMedii.EnemyStredni;
                            break;
                        case Strom s:
                            obj.Textura = SpravceMedii.Strom;
                            break;
                        case Trava t:
                            obj.Textura = SpravceMedii.Trava;
                            break;
                    }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (!ZacarovanyLes.delayed&&vyhra)
            {
                _game.ChangeCurrentState(ZacarovanyLes.menuState);
                ZacarovanyLes.gameState = null;
                ZacarovanyLes.mapState = null;
            }
            Map aktualni=ZacarovanyLes.maps.Aktualni;
            if (ZacarovanyLes.newState.IsKeyDown(Keys.Left)&&(!ZacarovanyLes.keyDelayed || ZacarovanyLes.oldState.IsKeyUp(Keys.Left))&&!vyhra)
            {
                ZacarovanyLes.keyDelayed = true;
                ZacarovanyLes.keyDelay = ZacarovanyLes.DELAY_TIME;
                if (aktualni.PoziceHrace.X-1 >= 0)
                {
                    if (aktualni.Objekty[(int)aktualni.PoziceHrace.X - 1, (int)aktualni.PoziceHrace.Y].GetType().BaseType == typeof(Pruchodny))
                    {
                        switch (aktualni.Objekty[(int)aktualni.PoziceHrace.X - 1, (int)aktualni.PoziceHrace.Y])
                        {
                            case Trava t:
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), SpravceMedii.Trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X - 1, (int)aktualni.PoziceHrace.Y] = new Hrac(new Vector2(aktualni.PoziceHrace.X - 1, aktualni.PoziceHrace.Y), SpravceMedii.PostavaLevo);
                                aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X - 1, aktualni.PoziceHrace.Y);
                                break;
                            case DvereDalsi d:
                                ZacarovanyLes.maps.dalsiMapa();
                                break;
                            case DverePredchozi d:
                                ZacarovanyLes.maps.predchoziMapa();
                                break;
                            case DverePosledni d:
                                vyhra = true;
                                ZacarovanyLes.delay = 3;
                                ZacarovanyLes.delayed = true;
                                break;
                            case LahvickaMany l:
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), SpravceMedii.Trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X - 1, (int)aktualni.PoziceHrace.Y] = new Hrac(new Vector2(aktualni.PoziceHrace.X - 1, aktualni.PoziceHrace.Y), SpravceMedii.PostavaLevo);
                                aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X - 1, aktualni.PoziceHrace.Y);
                                ZacarovanyLes.utocnik.Inventar.LahvickyMany++;
                                break;
                            case LahvickaZdravi l:
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), SpravceMedii.Trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X - 1, (int)aktualni.PoziceHrace.Y] = new Hrac(new Vector2(aktualni.PoziceHrace.X - 1, aktualni.PoziceHrace.Y), SpravceMedii.PostavaLevo);
                                aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X - 1, aktualni.PoziceHrace.Y);
                                ZacarovanyLes.utocnik.Inventar.LahvickyZdravi++;
                                break;
                            case SouperEasy s:
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), SpravceMedii.Trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X - 1, (int)aktualni.PoziceHrace.Y] = new Hrac(new Vector2(aktualni.PoziceHrace.X - 1, aktualni.PoziceHrace.Y), SpravceMedii.PostavaLevo);
                                aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X - 1, aktualni.PoziceHrace.Y);
                                ZacarovanyLes.obrance = Generator.DejLehkehoSoupere();
                                ZacarovanyLes.gameState = new GameState(_game, _content);
                                _game.ChangeState(ZacarovanyLes.gameState);
                                break;
                            case SouperMedium s:
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), SpravceMedii.Trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X - 1, (int)aktualni.PoziceHrace.Y] = new Hrac(new Vector2(aktualni.PoziceHrace.X - 1, aktualni.PoziceHrace.Y), SpravceMedii.PostavaLevo);
                                aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X - 1, aktualni.PoziceHrace.Y);
                                ZacarovanyLes.obrance = Generator.DejStrednihoSoupere();
                                ZacarovanyLes.gameState = new GameState(_game, _content);
                                _game.ChangeState(ZacarovanyLes.gameState);
                                break;
                            case SouperHard s:
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), SpravceMedii.Trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X - 1, (int)aktualni.PoziceHrace.Y] = new Hrac(new Vector2(aktualni.PoziceHrace.X - 1, aktualni.PoziceHrace.Y), SpravceMedii.PostavaLevo);
                                aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X - 1, aktualni.PoziceHrace.Y);
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
                            case Trava t:
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), SpravceMedii.Trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X + 1, (int)aktualni.PoziceHrace.Y] = new Hrac(new Vector2(aktualni.PoziceHrace.X + 1, aktualni.PoziceHrace.Y), SpravceMedii.PostavaPravo);
                                aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X + 1, aktualni.PoziceHrace.Y);
                                break;
                            case DvereDalsi d:
                                ZacarovanyLes.maps.dalsiMapa();
                                break;
                            case DverePredchozi d:
                                ZacarovanyLes.maps.predchoziMapa();
                                break;
                            case DverePosledni d:
                                vyhra = true;
                                ZacarovanyLes.delay = 3;
                                ZacarovanyLes.delayed = true;
                                break;
                            case LahvickaMany l:
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), SpravceMedii.Trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X + 1, (int)aktualni.PoziceHrace.Y] = new Hrac(new Vector2(aktualni.PoziceHrace.X + 1, aktualni.PoziceHrace.Y), SpravceMedii.PostavaPravo);
                                aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X + 1, aktualni.PoziceHrace.Y);
                                ZacarovanyLes.utocnik.Inventar.LahvickyMany++;
                                break;
                            case LahvickaZdravi l:
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), SpravceMedii.Trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X + 1, (int)aktualni.PoziceHrace.Y] = new Hrac(new Vector2(aktualni.PoziceHrace.X + 1, aktualni.PoziceHrace.Y), SpravceMedii.PostavaPravo);
                                aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X + 1, aktualni.PoziceHrace.Y);
                                ZacarovanyLes.utocnik.Inventar.LahvickyZdravi++;
                                break;
                            case SouperEasy s:
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), SpravceMedii.Trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X + 1, (int)aktualni.PoziceHrace.Y] = new Hrac(new Vector2(aktualni.PoziceHrace.X + 1, aktualni.PoziceHrace.Y), SpravceMedii.PostavaPravo);
                                aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X + 1, aktualni.PoziceHrace.Y);
                                ZacarovanyLes.obrance = Generator.DejLehkehoSoupere();
                                ZacarovanyLes.gameState = new GameState(_game, _content);
                                _game.ChangeState(ZacarovanyLes.gameState);
                                break;
                            case SouperMedium s:
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), SpravceMedii.Trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X + 1, (int)aktualni.PoziceHrace.Y] = new Hrac(new Vector2(aktualni.PoziceHrace.X + 1, aktualni.PoziceHrace.Y), SpravceMedii.PostavaPravo);
                                aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X + 1, aktualni.PoziceHrace.Y);
                                ZacarovanyLes.obrance = Generator.DejStrednihoSoupere();
                                ZacarovanyLes.gameState = new GameState(_game, _content);
                                _game.ChangeState(ZacarovanyLes.gameState);
                                break;
                            case SouperHard s:
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), SpravceMedii.Trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X + 1, (int)aktualni.PoziceHrace.Y] = new Hrac(new Vector2(aktualni.PoziceHrace.X + 1, aktualni.PoziceHrace.Y), SpravceMedii.PostavaPravo);
                                aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X + 1, aktualni.PoziceHrace.Y);
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
                    if (aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y-1].GetType().BaseType == typeof(Pruchodny))
                    {
                        switch (aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y-1])
                        {
                            case Trava t:
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), SpravceMedii.Trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y-1] = new Hrac(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y-1), SpravceMedii.PostavaNahoru);
                                aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y-1);
                                break;
                            case DvereDalsi d:
                                ZacarovanyLes.maps.dalsiMapa();
                                break;
                            case DverePredchozi d:
                                ZacarovanyLes.maps.predchoziMapa();
                                break;
                            case DverePosledni d:
                                vyhra = true;
                                ZacarovanyLes.delay = 3;
                                ZacarovanyLes.delayed = true;
                                break;
                            case LahvickaMany l:
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), SpravceMedii.Trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y - 1] = new Hrac(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y - 1), SpravceMedii.PostavaNahoru);
                                aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y - 1);
                                ZacarovanyLes.utocnik.Inventar.LahvickyMany++;
                                break;
                            case LahvickaZdravi l:
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), SpravceMedii.Trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y - 1] = new Hrac(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y - 1), SpravceMedii.PostavaNahoru);
                                aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y - 1);
                                ZacarovanyLes.utocnik.Inventar.LahvickyZdravi++;
                                break;
                            case SouperEasy s:
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), SpravceMedii.Trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y - 1] = new Hrac(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y - 1), SpravceMedii.PostavaNahoru);
                                aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y - 1);
                                ZacarovanyLes.obrance = Generator.DejLehkehoSoupere();
                                ZacarovanyLes.gameState = new GameState(_game, _content);
                                _game.ChangeState(ZacarovanyLes.gameState);
                                break;
                            case SouperMedium s:
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), SpravceMedii.Trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y - 1] = new Hrac(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y - 1), SpravceMedii.PostavaNahoru);
                                aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y - 1);
                                ZacarovanyLes.obrance = Generator.DejStrednihoSoupere();
                                ZacarovanyLes.gameState = new GameState(_game, _content);
                                _game.ChangeState(ZacarovanyLes.gameState);
                                break;
                            case SouperHard s:
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), SpravceMedii.Trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y - 1] = new Hrac(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y - 1), SpravceMedii.PostavaNahoru);
                                aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y - 1);
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
                            case Trava t:
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), SpravceMedii.Trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y + 1] = new Hrac(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y + 1), SpravceMedii.PostavaDolu);
                                aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y + 1);
                                break;
                            case DvereDalsi d:
                                ZacarovanyLes.maps.dalsiMapa();
                                break;
                            case DverePredchozi d:
                                ZacarovanyLes.maps.predchoziMapa();
                                break;
                            case DverePosledni d:
                                vyhra = true;
                                ZacarovanyLes.delay = 3;
                                ZacarovanyLes.delayed = true;
                                break;
                            case LahvickaMany l:
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), SpravceMedii.Trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y + 1] = new Hrac(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y + 1), SpravceMedii.PostavaDolu);
                                aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y + 1);
                                ZacarovanyLes.utocnik.Inventar.LahvickyMany++;
                                break;
                            case LahvickaZdravi l:
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), SpravceMedii.Trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y + 1] = new Hrac(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y + 1), SpravceMedii.PostavaDolu);
                                aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y + 1);
                                ZacarovanyLes.utocnik.Inventar.LahvickyZdravi++;
                                break;
                            case SouperEasy s:
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), SpravceMedii.Trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y + 1] = new Hrac(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y + 1), SpravceMedii.PostavaDolu);
                                aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y + 1);
                                ZacarovanyLes.obrance = Generator.DejLehkehoSoupere();
                                ZacarovanyLes.gameState = new GameState(_game, _content);
                                _game.ChangeState(ZacarovanyLes.gameState);
                                break;
                            case SouperMedium s:
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), SpravceMedii.Trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y + 1] = new Hrac(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y + 1), SpravceMedii.PostavaDolu);
                                aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y + 1);
                                ZacarovanyLes.obrance = Generator.DejStrednihoSoupere();
                                ZacarovanyLes.gameState = new GameState(_game, _content);
                                _game.ChangeState(ZacarovanyLes.gameState);
                                break;
                            case SouperHard s:
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), SpravceMedii.Trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y + 1] = new Hrac(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y + 1), SpravceMedii.PostavaDolu);
                                aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y + 1);
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
            delkaU = SpravceMedii.FontNadpis.MeasureString(ZacarovanyLes.utocnik.Name);
            spriteBatch.DrawString(SpravceMedii.FontNadpis, ZacarovanyLes.utocnik.Name, new Vector2(100 - delkaU.X / 2, 20 - delkaU.Y / 2), Color.Black);
            //trida
            delkaU = SpravceMedii.FontNadpis.MeasureString(PomocneMetody.TridaToString(ZacarovanyLes.utocnik.Trida, ZacarovanyLes.utocnik.Pohlavi));
            spriteBatch.DrawString(SpravceMedii.FontNadpis, PomocneMetody.TridaToString(ZacarovanyLes.utocnik.Trida, ZacarovanyLes.utocnik.Pohlavi), new Vector2(100 - delkaU.X / 2, 245 - delkaU.Y / 2), Color.Black);
            //Pohlavi                    
            delkaU = SpravceMedii.FontNadpis.MeasureString(PomocneMetody.PohlaviToString(ZacarovanyLes.utocnik.Pohlavi));
            spriteBatch.DrawString(SpravceMedii.FontNadpis, PomocneMetody.PohlaviToString(ZacarovanyLes.utocnik.Pohlavi), new Vector2(100 - delkaU.X / 2, 280 - delkaU.Y / 2), Color.Black);
            //Level
            spriteBatch.DrawString(SpravceMedii.FontText, "Level: " + ZacarovanyLes.utocnik.Level, new Vector2(20, 300), Color.Black);
            //Zkusenosti
            spriteBatch.DrawString(SpravceMedii.FontText, "Zkušenosti: " + ZacarovanyLes.utocnik.Zkusenosti + "/" + ZacarovanyLes.utocnik.ZkusenostiNext, new Vector2(20, 320), Color.Black);
            //Zivoty
            spriteBatch.DrawString(SpravceMedii.FontText, "Životy: " + ZacarovanyLes.utocnik.Zivoty + "/" + ZacarovanyLes.utocnik.ZivotyMax, new Vector2(20, 350), Color.Black);
            //Mana
            spriteBatch.DrawString(SpravceMedii.FontText, "Mana: " + ZacarovanyLes.utocnik.Mana + "/" + ZacarovanyLes.utocnik.ManaMax, new Vector2(20, 370), Color.Black);
            //Sila
            spriteBatch.DrawString(SpravceMedii.FontText, "Síla: " + ZacarovanyLes.utocnik.Sila, new Vector2(20, 400), Color.Black);
            //Obratnost
            spriteBatch.DrawString(SpravceMedii.FontText, "Obratnost: " + ZacarovanyLes.utocnik.Obratnost, new Vector2(20, 420), Color.Black);
            //Inteligence
            spriteBatch.DrawString(SpravceMedii.FontText, "Inteligence: " + ZacarovanyLes.utocnik.Inteligence, new Vector2(20, 440), Color.Black);
            //Brneni
            spriteBatch.DrawString(SpravceMedii.FontText, "Brnění: " + ZacarovanyLes.utocnik.Brneni, new Vector2(20, 460), Color.Black);
            
            foreach(Objekt obj in ZacarovanyLes.maps.Aktualni.Objekty)
            {
                obj.Draw(gameTime, spriteBatch);
            }
            if (vyhra)
            {
                delkaU = SpravceMedii.FontNadpis.MeasureString(textVyhra);
                spriteBatch.DrawString(SpravceMedii.FontNadpis, textVyhra, new Vector2(400 - delkaU.X / 2, 300 - delkaU.Y / 2), Color.White);
            }
            spriteBatch.End();
        }
    }
}
