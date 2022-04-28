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
        //Monogame
        //Promenne ...
        private bool vyhra;
        string textVyhra;
        //Fonty
        private SpriteFont fontText;
        private SpriteFont fontNadpis;
        private Vector2 delkaU;
        //Textury
        private Texture2D panel;
        private Texture2D plocha;
        private Texture2D valecnik;
        private Texture2D valecnice;
        private Texture2D lucistnik;
        private Texture2D lucistnice;
        private Texture2D kouzelnik;
        private Texture2D kouzelnice;
        private Texture2D utocnikPortret;
        private Texture2D prazdnaTexturaBila;
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

        public MapState(ZacarovanyLes game, ContentManager content) : base(game, content)
        {
        }

        public override void LoadContent()
        {
            textVyhra = "Vyhrál si!";
            vyhra = false;
            //Fonty
            fontNadpis = _content.Load<SpriteFont>("Fonts\\Nadpis");
            fontText = _content.Load<SpriteFont>("Fonts\\Text");
            //Textury
            panel = _content.Load<Texture2D>("Sprites\\GUI\\menu");
            plocha = _content.Load<Texture2D>("Sprites\\GUI\\plocha_boj");
            valecnik = _content.Load<Texture2D>("Sprites\\Postavy\\valecnik");
            valecnice = _content.Load<Texture2D>("Sprites\\Postavy\\valecnice");
            lucistnik = _content.Load<Texture2D>("Sprites\\Postavy\\lucistnik");
            lucistnice = _content.Load<Texture2D>("Sprites\\Postavy\\lucistnice");
            kouzelnik = _content.Load<Texture2D>("Sprites\\Postavy\\kouzelnik");
            kouzelnice = _content.Load<Texture2D>("Sprites\\Postavy\\kouzelnice");
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
            prazdnaTexturaBila = new Texture2D(_game.GraphicsDevice, 1, 1);
            prazdnaTexturaBila.SetData(new Color[] { Color.White });
            
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
                            obj.Textura = dvere;
                            break;
                        case Hrac h:
                            obj.Textura = postavaDolu;
                            break;
                        case Kamen k:
                            obj.Textura = kamen;
                            break;
                        case LahvickaMany l:
                            obj.Textura = lahvickaMana;
                            break;
                        case LahvickaZdravi l:
                            obj.Textura = lahvickaZdravi;
                            break;
                        case SouperEasy s:
                            obj.Textura = enemyLehky;
                            break;
                        case SouperHard s:
                            obj.Textura = enemyTezky;
                            break;
                        case SouperMedium s:
                            obj.Textura = enemyStredni;
                            break;
                        case Strom s:
                            obj.Textura = strom;
                            break;
                        case Trava t:
                            obj.Textura = trava;
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
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X - 1, (int)aktualni.PoziceHrace.Y] = new Hrac(new Vector2(aktualni.PoziceHrace.X - 1, aktualni.PoziceHrace.Y), postavaLevo);
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
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X - 1, (int)aktualni.PoziceHrace.Y] = new Hrac(new Vector2(aktualni.PoziceHrace.X - 1, aktualni.PoziceHrace.Y), postavaLevo);
                                aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X - 1, aktualni.PoziceHrace.Y);
                                ZacarovanyLes.utocnik.Inventar.LahvickyMany++;
                                break;
                            case LahvickaZdravi l:
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X - 1, (int)aktualni.PoziceHrace.Y] = new Hrac(new Vector2(aktualni.PoziceHrace.X - 1, aktualni.PoziceHrace.Y), postavaLevo);
                                aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X - 1, aktualni.PoziceHrace.Y);
                                ZacarovanyLes.utocnik.Inventar.LahvickyZdravi++;
                                break;
                            case SouperEasy s:
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X - 1, (int)aktualni.PoziceHrace.Y] = new Hrac(new Vector2(aktualni.PoziceHrace.X - 1, aktualni.PoziceHrace.Y), postavaLevo);
                                aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X - 1, aktualni.PoziceHrace.Y);
                                ZacarovanyLes.obrance = Generator.DejLehkehoSoupere();
                                ZacarovanyLes.gameState = new GameState(_game, _content);
                                _game.ChangeState(ZacarovanyLes.gameState);
                                break;
                            case SouperMedium s:
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X - 1, (int)aktualni.PoziceHrace.Y] = new Hrac(new Vector2(aktualni.PoziceHrace.X - 1, aktualni.PoziceHrace.Y), postavaLevo);
                                aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X - 1, aktualni.PoziceHrace.Y);
                                ZacarovanyLes.obrance = Generator.DejStrednihoSoupere();
                                ZacarovanyLes.gameState = new GameState(_game, _content);
                                _game.ChangeState(ZacarovanyLes.gameState);
                                break;
                            case SouperHard s:
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X - 1, (int)aktualni.PoziceHrace.Y] = new Hrac(new Vector2(aktualni.PoziceHrace.X - 1, aktualni.PoziceHrace.Y), postavaLevo);
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
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X + 1, (int)aktualni.PoziceHrace.Y] = new Hrac(new Vector2(aktualni.PoziceHrace.X + 1, aktualni.PoziceHrace.Y), postavaPravo);
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
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X + 1, (int)aktualni.PoziceHrace.Y] = new Hrac(new Vector2(aktualni.PoziceHrace.X + 1, aktualni.PoziceHrace.Y), postavaPravo);
                                aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X + 1, aktualni.PoziceHrace.Y);
                                ZacarovanyLes.utocnik.Inventar.LahvickyMany++;
                                break;
                            case LahvickaZdravi l:
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X + 1, (int)aktualni.PoziceHrace.Y] = new Hrac(new Vector2(aktualni.PoziceHrace.X + 1, aktualni.PoziceHrace.Y), postavaPravo);
                                aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X + 1, aktualni.PoziceHrace.Y);
                                ZacarovanyLes.utocnik.Inventar.LahvickyZdravi++;
                                break;
                            case SouperEasy s:
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X + 1, (int)aktualni.PoziceHrace.Y] = new Hrac(new Vector2(aktualni.PoziceHrace.X + 1, aktualni.PoziceHrace.Y), postavaPravo);
                                aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X + 1, aktualni.PoziceHrace.Y);
                                ZacarovanyLes.obrance = Generator.DejLehkehoSoupere();
                                ZacarovanyLes.gameState = new GameState(_game, _content);
                                _game.ChangeState(ZacarovanyLes.gameState);
                                break;
                            case SouperMedium s:
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X + 1, (int)aktualni.PoziceHrace.Y] = new Hrac(new Vector2(aktualni.PoziceHrace.X + 1, aktualni.PoziceHrace.Y), postavaPravo);
                                aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X + 1, aktualni.PoziceHrace.Y);
                                ZacarovanyLes.obrance = Generator.DejStrednihoSoupere();
                                ZacarovanyLes.gameState = new GameState(_game, _content);
                                _game.ChangeState(ZacarovanyLes.gameState);
                                break;
                            case SouperHard s:
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X + 1, (int)aktualni.PoziceHrace.Y] = new Hrac(new Vector2(aktualni.PoziceHrace.X + 1, aktualni.PoziceHrace.Y), postavaPravo);
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
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y-1] = new Hrac(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y-1), postavaNahoru);
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
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y - 1] = new Hrac(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y - 1), postavaNahoru);
                                aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y - 1);
                                ZacarovanyLes.utocnik.Inventar.LahvickyMany++;
                                break;
                            case LahvickaZdravi l:
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y - 1] = new Hrac(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y - 1), postavaNahoru);
                                aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y - 1);
                                ZacarovanyLes.utocnik.Inventar.LahvickyZdravi++;
                                break;
                            case SouperEasy s:
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y - 1] = new Hrac(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y - 1), postavaNahoru);
                                aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y - 1);
                                ZacarovanyLes.obrance = Generator.DejLehkehoSoupere();
                                ZacarovanyLes.gameState = new GameState(_game, _content);
                                _game.ChangeState(ZacarovanyLes.gameState);
                                break;
                            case SouperMedium s:
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y - 1] = new Hrac(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y - 1), postavaNahoru);
                                aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y - 1);
                                ZacarovanyLes.obrance = Generator.DejStrednihoSoupere();
                                ZacarovanyLes.gameState = new GameState(_game, _content);
                                _game.ChangeState(ZacarovanyLes.gameState);
                                break;
                            case SouperHard s:
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y - 1] = new Hrac(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y - 1), postavaNahoru);
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
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y + 1] = new Hrac(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y + 1), postavaDolu);
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
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y + 1] = new Hrac(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y + 1), postavaDolu);
                                aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y + 1);
                                ZacarovanyLes.utocnik.Inventar.LahvickyMany++;
                                break;
                            case LahvickaZdravi l:
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y + 1] = new Hrac(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y + 1), postavaDolu);
                                aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y + 1);
                                ZacarovanyLes.utocnik.Inventar.LahvickyZdravi++;
                                break;
                            case SouperEasy s:
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y + 1] = new Hrac(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y + 1), postavaDolu);
                                aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y + 1);
                                ZacarovanyLes.obrance = Generator.DejLehkehoSoupere();
                                ZacarovanyLes.gameState = new GameState(_game, _content);
                                _game.ChangeState(ZacarovanyLes.gameState);
                                break;
                            case SouperMedium s:
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y + 1] = new Hrac(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y + 1), postavaDolu);
                                aktualni.PoziceHrace = new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y + 1);
                                ZacarovanyLes.obrance = Generator.DejStrednihoSoupere();
                                ZacarovanyLes.gameState = new GameState(_game, _content);
                                _game.ChangeState(ZacarovanyLes.gameState);
                                break;
                            case SouperHard s:
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y] = new Trava(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y), trava);
                                aktualni.Objekty[(int)aktualni.PoziceHrace.X, (int)aktualni.PoziceHrace.Y + 1] = new Hrac(new Vector2(aktualni.PoziceHrace.X, aktualni.PoziceHrace.Y + 1), postavaDolu);
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
        }
        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            utocnikPortret = DejPortret(ZacarovanyLes.utocnik.Pohlavi, ZacarovanyLes.utocnik.Trida);
            //sprites
            spriteBatch.Draw(panel, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(utocnikPortret, new Rectangle(5, 37, 190, 190), Color.White);
            //text
            //portret
            delkaU = fontNadpis.MeasureString(ZacarovanyLes.utocnik.Name);
            spriteBatch.DrawString(fontNadpis, ZacarovanyLes.utocnik.Name, new Vector2(100 - delkaU.X / 2, 20 - delkaU.Y / 2), Color.Black);
            //trida
            delkaU = fontNadpis.MeasureString(PomocneMetody.TridaToString(ZacarovanyLes.utocnik.Trida, ZacarovanyLes.utocnik.Pohlavi));
            spriteBatch.DrawString(fontNadpis, PomocneMetody.TridaToString(ZacarovanyLes.utocnik.Trida, ZacarovanyLes.utocnik.Pohlavi), new Vector2(100 - delkaU.X / 2, 245 - delkaU.Y / 2), Color.Black);
            //Pohlavi                    
            delkaU = fontNadpis.MeasureString(PomocneMetody.PohlaviToString(ZacarovanyLes.utocnik.Pohlavi));
            spriteBatch.DrawString(fontNadpis, PomocneMetody.PohlaviToString(ZacarovanyLes.utocnik.Pohlavi), new Vector2(100 - delkaU.X / 2, 280 - delkaU.Y / 2), Color.Black);
            //Level
            spriteBatch.DrawString(fontText, "Level: " + ZacarovanyLes.utocnik.Level, new Vector2(20, 300), Color.Black);
            //Zkusenosti
            spriteBatch.DrawString(fontText, "Zkušenosti: " + ZacarovanyLes.utocnik.Zkusenosti + "/" + ZacarovanyLes.utocnik.ZkusenostiNext, new Vector2(20, 320), Color.Black);
            //Zivoty
            spriteBatch.DrawString(fontText, "Životy: " + ZacarovanyLes.utocnik.Zivoty + "/" + ZacarovanyLes.utocnik.ZivotyMax, new Vector2(20, 350), Color.Black);
            //Mana
            spriteBatch.DrawString(fontText, "Mana: " + ZacarovanyLes.utocnik.Mana + "/" + ZacarovanyLes.utocnik.ManaMax, new Vector2(20, 370), Color.Black);
            //Sila
            spriteBatch.DrawString(fontText, "Síla: " + ZacarovanyLes.utocnik.Sila, new Vector2(20, 400), Color.Black);
            //Obratnost
            spriteBatch.DrawString(fontText, "Obratnost: " + ZacarovanyLes.utocnik.Obratnost, new Vector2(20, 420), Color.Black);
            //Inteligence
            spriteBatch.DrawString(fontText, "Inteligence: " + ZacarovanyLes.utocnik.Inteligence, new Vector2(20, 440), Color.Black);
            //Brneni
            spriteBatch.DrawString(fontText, "Brnění: " + ZacarovanyLes.utocnik.Brneni, new Vector2(20, 460), Color.Black);
            
            foreach(Objekt obj in ZacarovanyLes.maps.Aktualni.Objekty)
            {
                obj.Draw(gameTime, spriteBatch);
            }
            if (vyhra)
            {
                delkaU = fontNadpis.MeasureString(textVyhra);
                spriteBatch.DrawString(fontNadpis, textVyhra, new Vector2(400 - delkaU.X / 2, 300 - delkaU.Y / 2), Color.White);
            }
            spriteBatch.End();
        }

        protected Texture2D DejPortret(Pohlavi pohlavi, Trida trida)
        {
            switch (pohlavi)
            {
                case Pohlavi.Muz:
                    switch (trida)
                    {
                        case Trida.Bojovnik: return valecnik;
                        case Trida.Lucistnik: return lucistnik;
                        case Trida.Kouzelnik: return kouzelnik;

                    }
                    break;
                case Pohlavi.Zena:
                    switch (trida)
                    {
                        case Trida.Bojovnik: return valecnice;
                        case Trida.Lucistnik: return lucistnice;
                        case Trida.Kouzelnik: return kouzelnice;
                    }
                    break;
            }
            return null;
        }
    }
}
