using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using Zacarovany_les.Classes;
using Zacarovany_les.Classes.Interface;
using Zacarovany_les.Classes.Pomocne;

namespace Zacarovany_les
{
    public class GameState : State
    {
        //Promenne, konstanty ...
        private Souboj souboj;
        private Faze faze;
        private Postava hrajici;
        private Postava souper;
        private Schopnost vybranaHrajici = null;
        private Schopnost vybranaSouper = null;
        private string messageKolo = "";
        private string messagePrvni = "";
        private string messageDruhy = "";
        private bool zacatek = true;
        //Zvuky
        SoundEffect fireball;
        SoundEffect frostbolt;
        SoundEffect bowPull;
        SoundEffect bowMiss;
        SoundEffect hit;
        SoundEffect regen;
        SoundEffect battlecry;
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
        private Texture2D obrancePortret;
        private Texture2D prazdnaTexturaBila;
        private Texture2D prazdnaTexturaCerna;
        //Fonty
        private SpriteFont fontNadpis;
        private SpriteFont fontText;
        private Vector2 delkaU;
        private Vector2 delkaO;
        //Buttons
        private Button buttonUtokMecem;
        private Button buttonObranaStitem;
        private Button buttonRegenerace;
        private Button buttonBojovyPokrik;
        private Button buttonBodnutiDykou;
        private Button buttonStrelbaLukem;
        private Button buttonMagickySip;
        private Button buttonUskok;
        private Button buttonUderHoli;
        private Button buttonOhnivaKoule;
        private Button buttonLedoveKopi;
        private Button buttonMagickyStit;
        private Button buttonUtek;
        private Button buttonLahvickaZdravi;
        private Button buttonLahvickaMany;
        private List<Button> buttons;
        private List<Druh> schopnostiDruh;
        public GameState(ZacarovanyLes game, ContentManager content) : base(game, content)
        {
        }


        public override void LoadContent()
        {
            souboj = new Souboj(ZacarovanyLes.utocnik, ZacarovanyLes.obrance);
            faze = Faze.Kolo;
            //Hudba

            //Zvuky
            fireball = _content.Load<SoundEffect>("Sound\\fireball");
            frostbolt = _content.Load<SoundEffect>("Sound\\ledove_kopi");
            bowPull = _content.Load<SoundEffect>("Sound\\luk_natah");
            bowMiss = _content.Load<SoundEffect>("Sound\\netrefil_sip");
            hit = _content.Load<SoundEffect>("Sound\\zasah");
            regen = _content.Load<SoundEffect>("Sound\\regen");
            battlecry = _content.Load<SoundEffect>("Sound\\battlecry");
            //Textury
            panel = _content.Load<Texture2D>("Sprites\\GUI\\menu");
            plocha = _content.Load<Texture2D>("Sprites\\GUI\\plocha_boj");
            valecnik = _content.Load<Texture2D>("Sprites\\Postavy\\valecnik");
            valecnice = _content.Load<Texture2D>("Sprites\\Postavy\\valecnice");
            lucistnik = _content.Load<Texture2D>("Sprites\\Postavy\\lucistnik");
            lucistnice = _content.Load<Texture2D>("Sprites\\Postavy\\lucistnice");
            kouzelnik = _content.Load<Texture2D>("Sprites\\Postavy\\kouzelnik");
            kouzelnice = _content.Load<Texture2D>("Sprites\\Postavy\\kouzelnice");
            prazdnaTexturaBila = new Texture2D(_game.GraphicsDevice, 1, 1);
            prazdnaTexturaBila.SetData(new Color[] { Color.White });
            prazdnaTexturaCerna = new Texture2D(_game.GraphicsDevice, 1, 1);
            prazdnaTexturaCerna.SetData(new Color[] { Color.Black });
            //Fonty
            fontNadpis = _content.Load<SpriteFont>("Fonts\\Nadpis");
            fontText = _content.Load<SpriteFont>("Fonts\\Text");
            //Buttons
            buttonUtokMecem = new Button(fontText, new Vector2(0, 0), prazdnaTexturaBila, 150, 20, Color.White, Color.Black, "");
            buttonUtokMecem.Click += ButtonUtokMecemClickedHandler;
            buttonObranaStitem = new Button(fontText, new Vector2(0, 0), prazdnaTexturaBila, 150, 20, Color.White, Color.Black, "");
            buttonObranaStitem.Click += ButtonObranaStitemClickedHandler;
            buttonRegenerace = new Button(fontText, new Vector2(0, 0), prazdnaTexturaBila, 150, 20, Color.White, Color.Black, "");
            buttonRegenerace.Click += ButtonRegeneraceClickedHandler;
            buttonBojovyPokrik = new Button(fontText, new Vector2(0, 0), prazdnaTexturaBila, 150, 20, Color.White, Color.Black, "");
            buttonBojovyPokrik.Click += ButtonBojovyPokrikClickedHandler;
            buttonBodnutiDykou = new Button(fontText, new Vector2(0, 0), prazdnaTexturaBila, 150, 20, Color.White, Color.Black, "");
            buttonBodnutiDykou.Click += ButtonBodnutiDykouClickedHandler;
            buttonStrelbaLukem = new Button(fontText, new Vector2(0, 0), prazdnaTexturaBila, 150, 20, Color.White, Color.Black, "");
            buttonStrelbaLukem.Click += ButtonStrelbaLukemClickedHandler;
            buttonMagickySip = new Button(fontText, new Vector2(0, 0), prazdnaTexturaBila, 150, 20, Color.White, Color.Black, "");
            buttonMagickySip.Click += ButtonMagickySipClickedHandler;
            buttonUskok = new Button(fontText, new Vector2(0, 0), prazdnaTexturaBila, 150, 20, Color.White, Color.Black, "");
            buttonUskok.Click += ButtonUskokClickedHandler;
            buttonUderHoli = new Button(fontText, new Vector2(0, 0), prazdnaTexturaBila, 150, 20, Color.White, Color.Black, "");
            buttonUderHoli.Click += ButtonUderHoliClickedHandler;
            buttonOhnivaKoule = new Button(fontText, new Vector2(0, 0), prazdnaTexturaBila, 150, 20, Color.White, Color.Black, "");
            buttonOhnivaKoule.Click += ButtonOhnivaKouleClickedHandler;
            buttonLedoveKopi = new Button(fontText, new Vector2(0, 0), prazdnaTexturaBila, 150, 20, Color.White, Color.Black, "");
            buttonLedoveKopi.Click += ButtonLedoveKopiClickedHandler;
            buttonMagickyStit = new Button(fontText, new Vector2(0, 0), prazdnaTexturaBila, 150, 20, Color.White, Color.Black, "");
            buttonMagickyStit.Click += ButtonMagickyStitClickedHandler;
            buttonUtek = new Button(fontText, new Vector2(0, 0), prazdnaTexturaBila, 150, 20, Color.White, Color.Black, "");
            buttonUtek.Click += ButtonUtekClickedHandler;
            buttonLahvickaZdravi = new Button(fontText, new Vector2(0, 0), prazdnaTexturaBila, 150, 20, Color.White, Color.Black, "");
            buttonLahvickaZdravi.Click += ButtonLahvickaZdraviClickedHandler;
            buttonLahvickaMany = new Button(fontText, new Vector2(0, 0), prazdnaTexturaBila, 150, 20, Color.White, Color.Black, "");
            buttonLahvickaMany.Click += ButtonLahvickaManyClickedHandler;
            buttons = new List<Button>()
            {
            buttonUtokMecem,
            buttonObranaStitem,
            buttonRegenerace,
            buttonBojovyPokrik,
            buttonBodnutiDykou,
            buttonStrelbaLukem,
            buttonMagickySip,
            buttonUskok,
            buttonUderHoli,
            buttonOhnivaKoule,
            buttonLedoveKopi,
            buttonMagickyStit,
            buttonUtek,
            buttonLahvickaZdravi,
            buttonLahvickaMany
            };
            schopnostiDruh = Enum.GetValues(typeof(Druh)).Cast<Druh>().ToList();
            schopnostiDruh.Remove(Druh.Zadna);
        }



        public override void Update(GameTime gameTime)
        {
            if ((souboj.Utocnik.Zivoty <= 0 || souboj.Obrance.Zivoty <= 0) && faze != Faze.Konec && faze != Faze.VyhraObrance && faze != Faze.VyhraUtocnik)
            {
                faze = Faze.Konec;
                ZacarovanyLes.delay = 3;
                ZacarovanyLes.delayed = true;
            }
            foreach (Button butt in buttons)
            {
                butt.Update(gameTime);
            }
            switch (faze)
            {
                case Faze.Konec:
                    if (!ZacarovanyLes.delayed)
                    {

                        messagePrvni = "";
                        messageDruhy = "";
                        if (souboj.Utocnik.Zivoty <= 0)
                        {
                            messagePrvni = souboj.Utocnik.Name + " byl poražen";
                            messageDruhy = souboj.Obrance.Name + " zvítězil !";
                            faze = Faze.VyhraObrance;
                            ZacarovanyLes.delay = 3;
                            ZacarovanyLes.delayed = true;
                        }
                        else
                        {
                            messagePrvni = souboj.Obrance.Name + " byl poražen";
                            messageDruhy = souboj.Utocnik.Name + " zvítězil !";
                            faze = Faze.VyhraUtocnik;
                            ZacarovanyLes.delay = 3;
                            ZacarovanyLes.delayed = true;
                        }
                    }
                    break;
                case Faze.VyhraUtocnik:
                    if (!ZacarovanyLes.delayed)
                    {
                        if (ZacarovanyLes.mapState != null)
                        {
                            switch (souboj.Obrance.Majitel)
                            {
                                case Majitel.Pocitac_Lehky:
                                    souboj.Utocnik.PridejZkusenosti(25);
                                    break;
                                case Majitel.Pocitac_Stredni:
                                    souboj.Utocnik.PridejZkusenosti(50);
                                    break;
                                case Majitel.Pocitac_Tezky:
                                    souboj.Utocnik.PridejZkusenosti(75);
                                    break;
                            }
                            souboj.Reset();
                            _game.ChangeCurrentState(ZacarovanyLes.mapState);
                        }
                        else
                        {
                            _game.ChangeState(ZacarovanyLes.menuState);
                        }
                    }
                    break;
                case Faze.VyhraObrance:
                    if (!ZacarovanyLes.delayed)
                    {
                        if (ZacarovanyLes.mapState != null)
                        {
                            ZacarovanyLes.gameState = null;
                            ZacarovanyLes.mapState = null;
                            _game.ChangeState(ZacarovanyLes.menuState);
                        }
                        else
                        {
                            _game.ChangeState(ZacarovanyLes.menuState);
                        }
                    }
                    break;
                case Faze.EfektyPrvni:
                    if (!ZacarovanyLes.delayed)
                    {
                        messagePrvni = "";
                        messageDruhy = "";
                        if (hrajici == souboj.Obrance)
                        {
                            if (souboj.EfektyObrance.Horeni > 0)
                            {
                                int poskozeni = souboj.ZautocNaObrance((int)Math.Round(souper.Inteligence / 2.0), true);
                                messagePrvni = hrajici.Name + " hoří za " + poskozeni + " poškození";
                                fireball.Play();
                            }
                            else
                            {
                                faze = Faze.EfektyDruhy;
                                break;
                            }
                        }
                        else
                        {
                            if (souboj.EfektyUtocnika.Horeni > 0)
                            {
                                int poskozeni = souboj.ZautocNaUtocnika((int)Math.Round(souper.Inteligence / 2.0), true);
                                messagePrvni = hrajici.Name + " hoří za " + poskozeni + " poškození";
                                fireball.Play();
                            }
                            else
                            {
                                faze = Faze.EfektyDruhy;
                                break;
                            }
                        }
                        faze = Faze.EfektyDruhy;
                        ZacarovanyLes.delay = 3;
                        ZacarovanyLes.delayed = true;
                    }
                    break;
                case Faze.EfektyDruhy:
                    if (!ZacarovanyLes.delayed)
                    {
                        if (souper == souboj.Obrance)
                        {
                            if (souboj.EfektyObrance.Horeni > 0)
                            {
                                int poskozeni = souboj.ZautocNaObrance((int)Math.Round(hrajici.Inteligence / 2.0), true);
                                messageDruhy = souper.Name + " hoří za " + poskozeni + " poškození";
                                fireball.Play();
                            }
                            else
                            {
                                faze = Faze.Kolo;
                                souboj.ZhodnotEfekty();
                                souboj.ZhodnotSchopnosti();
                                break;
                            }
                        }
                        else
                        {
                            if (souboj.EfektyUtocnika.Horeni > 0)
                            {
                                int poskozeni = souboj.ZautocNaUtocnika((int)Math.Round(hrajici.Inteligence / 2.0), true);
                                messageDruhy = souper.Name + " hoří za " + poskozeni + " poškození";
                                fireball.Play();
                            }
                            else
                            {
                                faze = Faze.Kolo;
                                souboj.ZhodnotEfekty();
                                souboj.ZhodnotSchopnosti();
                                break;
                            }
                        }

                        souboj.ZhodnotEfekty();
                        souboj.ZhodnotSchopnosti();
                        faze = Faze.Kolo;
                        ZacarovanyLes.delay = 3;
                        ZacarovanyLes.delayed = true;
                    }
                    break;
                case Faze.Kolo:
                    if (!ZacarovanyLes.delayed)
                    {
                        if (zacatek)
                        {
                            hrajici = souboj.Obrance;
                            souper = souboj.Utocnik;
                            if (souboj.ZacinaUtocnik)
                            {
                                hrajici = souboj.Utocnik;
                                souper = souboj.Obrance;
                            }

                            zacatek = false;

                        }
                        else
                        {
                            if (hrajici == souboj.Utocnik)
                            {
                                hrajici = souboj.Obrance;
                                souper = souboj.Utocnik;
                            }
                            else
                            {
                                hrajici = souboj.Utocnik;
                                souper = souboj.Obrance;
                            }
                        }
                        souboj.PocetKol++;
                        messagePrvni = "";
                        messageDruhy = "";
                        messageKolo = souboj.PocetKol + ". kolo, začíná ho " + hrajici.Name;
                        faze = Faze.Zacatek;
                    }
                    break;
                case Faze.Zacatek:
                    if (!ZacarovanyLes.delayed)
                    {
                        ZacarovanyLes.delay = 3;
                        ZacarovanyLes.delayed = true;
                        faze = Faze.VyberPrvni;
                    }
                    break;
                case Faze.VyberPrvni:
                    messagePrvni = hrajici.Name + " vybírá schopnost";
                    if (!ZacarovanyLes.delayed)
                    {
                        if (hrajici.Majitel == Majitel.Hrac && vybranaHrajici != null)
                        {

                            messagePrvni = hrajici.Name + " použije schopnost " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh);
                            ZacarovanyLes.delay = 3;
                            ZacarovanyLes.delayed = true;
                            faze = Faze.VyberDruhy;
                        }

                        if (hrajici.Majitel != Majitel.Hrac)
                        {
                            if (vybranaHrajici == null)
                            {
                                vybranaHrajici = VyberSchopnostAI(hrajici, null, souper);
                            }
                            messagePrvni = hrajici.Name + " použije schopnost " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh);
                            ZacarovanyLes.delay = 3;
                            ZacarovanyLes.delayed = true;
                            faze = Faze.VyberDruhy;
                        }
                    }
                    break;
                case Faze.VyberDruhy:
                    messageDruhy = souper.Name + " vybírá schopnost";
                    if (!ZacarovanyLes.delayed)
                    {
                        if (souper.Majitel == Majitel.Hrac && vybranaSouper != null)
                        {
                            messageDruhy = souper.Name + " použije schopnost " + PomocneMetody.SchopnostToString(vybranaSouper.Druh);
                            ZacarovanyLes.delay = 3;
                            ZacarovanyLes.delayed = true;
                            faze = Faze.UtokPrvni;
                        }
                        if (souper.Majitel != Majitel.Hrac)
                        {
                            if (vybranaSouper == null)
                            {
                                vybranaSouper = VyberSchopnostAI(souper, vybranaHrajici, hrajici);
                            }
                            messageDruhy = souper.Name + " použije schopnost " + PomocneMetody.SchopnostToString(vybranaSouper.Druh);
                            ZacarovanyLes.delay = 3;
                            ZacarovanyLes.delayed = true;
                            faze = Faze.UtokPrvni;
                        }
                    }
                    break;
                case Faze.UtokPrvni:
                    if (!ZacarovanyLes.delayed)
                    {
                        int poskozeni = hrajici.PouzijSchopnost(vybranaHrajici.Druh);
                        if (((vybranaSouper.Druh != Druh.Magicky_sip && vybranaSouper.Faze != 0) || vybranaSouper.Druh != Druh.Ohniva_Koule || vybranaSouper.Druh != Druh.Ledove_Kopi)
                            && (vybranaHrajici.Druh == Druh.Magicky_Stit || vybranaHrajici.Druh == Druh.Obrana_Stitem || vybranaHrajici.Druh == Druh.Uskok))
                        {
                            messagePrvni = hrajici.Name + " se brání schopností " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh);
                        }
                        else if (((vybranaSouper.Druh == Druh.Magicky_sip && vybranaSouper.Faze == 0) || vybranaSouper.Druh == Druh.Ohniva_Koule || vybranaSouper.Druh == Druh.Ledove_Kopi)
                           && (vybranaHrajici.Druh == Druh.Magicky_Stit || vybranaHrajici.Druh == Druh.Obrana_Stitem || vybranaHrajici.Druh == Druh.Uskok))
                        {
                            messagePrvni = hrajici.Name + " se částečně brání schopností " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh);
                        }
                        else if ((vybranaSouper.Druh == Druh.Magicky_Stit || vybranaSouper.Druh == Druh.Obrana_Stitem || vybranaSouper.Druh == Druh.Uskok)
                            && ((vybranaHrajici.Druh == Druh.Magicky_sip && vybranaHrajici.Faze == 0) || vybranaHrajici.Druh == Druh.Ohniva_Koule || vybranaHrajici.Druh == Druh.Ledove_Kopi))
                        {
                            switch (vybranaHrajici.Druh)
                            {
                                case Druh.Ohniva_Koule:
                                    fireball.Play();
                                    break;
                                case Druh.Ledove_Kopi:
                                    frostbolt.Play();
                                    break;
                                default:
                                    hit.Play();
                                    break;
                            }
                            int skPosk;
                            if (hrajici == souboj.Utocnik)
                            {
                                skPosk = souboj.ZautocNaObrance(poskozeni / 2, true);
                            }
                            else
                            {
                                skPosk = souboj.ZautocNaUtocnika(poskozeni / 2, true);
                            }
                            messagePrvni = hrajici.Name + " útočí schopností " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh) + " za " + skPosk + " poškození";
                        }
                        else if (vybranaHrajici.Druh == Druh.Regenerace)
                        {
                            regen.Play();
                            if (hrajici == souboj.Utocnik)
                            {
                                if (souboj.EfektyUtocnika.Pokrik > 0)
                                {
                                    poskozeni = poskozeni * 2;
                                }
                            }
                            else
                            {
                                if (souboj.EfektyObrance.Pokrik > 0)
                                {
                                    poskozeni = poskozeni * 2;
                                }
                            }
                            hrajici.PridejNeboUberZdravi(poskozeni);
                            messagePrvni = hrajici.Name + " regeneruje zdraví za " + poskozeni + " bodů pomocí schopnosti " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh);
                        }
                        else if (vybranaHrajici.Druh == Druh.Lahvicka_Zdravi)
                        {
                            regen.Play();
                            hrajici.Inventar.LahvickyZdravi--;
                            messagePrvni = hrajici.Name + " regeneruje zdravi za " + poskozeni + " bodů pomocí " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh);
                        }

                        else if (vybranaHrajici.Druh == Druh.Lahvicka_Many)
                        {
                            regen.Play();
                            hrajici.Inventar.LahvickyMany--;
                            messagePrvni = hrajici.Name + " regeneruje manu za " + poskozeni + " bodů pomocí " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh);
                        }
                        else if (vybranaHrajici.Druh == Druh.Bojovy_Pokrik)
                        {
                            battlecry.Play();
                            if (hrajici == souboj.Utocnik)
                            {
                                souboj.EfektyUtocnika.Pokrik += 2;
                            }
                            else
                            {
                                souboj.EfektyObrance.Pokrik += 2;
                            }
                            messagePrvni = hrajici.Name + " zařval " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh);
                        }
                        else if (vybranaHrajici.Druh == Druh.Strelba_Lukem && vybranaHrajici.Faze == 1 || vybranaHrajici.Druh == Druh.Magicky_sip && vybranaHrajici.Faze == 1)
                        {
                            messagePrvni = hrajici.Name + " natahuje luk, aby použil schopnost " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh);
                            bowPull.Play();
                        }
                        else
                        {
                            if (hrajici == souboj.Utocnik)
                            {
                                if (souboj.EfektyUtocnika.Pokrik > 0)
                                {
                                    poskozeni = poskozeni * 2;
                                }
                            }
                            else
                            {
                                if (souboj.EfektyObrance.Pokrik > 0)
                                {
                                    poskozeni = poskozeni * 2;
                                }
                            }

                            if (vybranaSouper.Druh == Druh.Magicky_Stit || vybranaSouper.Druh == Druh.Obrana_Stitem || vybranaSouper.Druh == Druh.Uskok)
                            {
                                poskozeni = 0;
                                if (vybranaHrajici.Druh == Druh.Magicky_sip || vybranaHrajici.Druh == Druh.Strelba_Lukem)
                                    bowMiss.Play();
                            }
                            else if (vybranaHrajici.Druh == Druh.Ohniva_Koule)
                            {
                                fireball.Play();
                                if (hrajici == souboj.Utocnik)
                                {
                                    souboj.EfektyObrance.Horeni += 2;
                                }
                                else
                                {
                                    souboj.EfektyUtocnika.Horeni += 2;
                                }
                            }
                            else if (vybranaHrajici.Druh == Druh.Ledove_Kopi)
                            {
                                frostbolt.Play();
                                if (hrajici == souboj.Utocnik)
                                {
                                    souboj.EfektyObrance.Mraz += 2;
                                    souboj.EfektyObrance.AktivujMraz();
                                }
                                else
                                {
                                    souboj.EfektyUtocnika.Mraz += 2;
                                    souboj.EfektyUtocnika.AktivujMraz();
                                }
                            }
                            else
                            {
                               hit.Play();
                            }
                            int skPosk;
                            if (hrajici == souboj.Utocnik)
                            {
                                skPosk = souboj.ZautocNaObrance(poskozeni, false);
                            }
                            else
                            {
                                skPosk = souboj.ZautocNaUtocnika(poskozeni, false);
                            }
                            messagePrvni = hrajici.Name + " útočí schopností " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh) + " za " + skPosk + " poškození";
                        }
                        if (vybranaHrajici.Faze == 0)
                        {
                            hrajici.PridejNeboUberManu(-vybranaHrajici.CenaMany);
                        }
                        ZacarovanyLes.delay = 3;
                        ZacarovanyLes.delayed = true;
                        faze = Faze.UtokDruhy;
                    }
                    break;
                case Faze.UtokDruhy:
                    if (!ZacarovanyLes.delayed)
                    {
                        int poskozeni = souper.PouzijSchopnost(vybranaSouper.Druh);
                        if (((vybranaHrajici.Druh != Druh.Magicky_sip && vybranaHrajici.Faze != 0) || vybranaHrajici.Druh != Druh.Ohniva_Koule || vybranaHrajici.Druh != Druh.Ledove_Kopi)
                            && (vybranaSouper.Druh == Druh.Magicky_Stit || vybranaSouper.Druh == Druh.Obrana_Stitem || vybranaSouper.Druh == Druh.Uskok))
                        {
                            messageDruhy = souper.Name + " se brání schopností " + PomocneMetody.SchopnostToString(vybranaSouper.Druh);
                        }
                        else if (((vybranaHrajici.Druh == Druh.Magicky_sip && vybranaHrajici.Faze == 0) || vybranaHrajici.Druh == Druh.Ohniva_Koule || vybranaHrajici.Druh == Druh.Ledove_Kopi)
                           && (vybranaSouper.Druh == Druh.Magicky_Stit || vybranaSouper.Druh == Druh.Obrana_Stitem || vybranaSouper.Druh == Druh.Uskok))
                        {
                            messageDruhy = souper.Name + " se částečně brání schopností " + PomocneMetody.SchopnostToString(vybranaSouper.Druh);
                        }
                        else if ((vybranaHrajici.Druh == Druh.Magicky_Stit || vybranaHrajici.Druh == Druh.Obrana_Stitem || vybranaHrajici.Druh == Druh.Uskok)
                            && ((vybranaSouper.Druh == Druh.Magicky_sip && vybranaSouper.Faze == 0) || vybranaSouper.Druh == Druh.Ohniva_Koule || vybranaSouper.Druh == Druh.Ledove_Kopi))
                        {
                            switch (vybranaSouper.Druh)
                            {
                                case Druh.Ohniva_Koule:
                                    fireball.Play();
                                    break;
                                case Druh.Ledove_Kopi:
                                    frostbolt.Play();
                                    break;
                                default:
                                    hit.Play();
                                    break;
                            }
                            int skPosk;
                            if (souper == souboj.Utocnik)
                            {
                                skPosk = souboj.ZautocNaObrance(poskozeni / 2, true);
                            }
                            else
                            {
                                skPosk = souboj.ZautocNaUtocnika(poskozeni / 2, true);
                            }
                            messageDruhy = souper.Name + " útočí schopností " + PomocneMetody.SchopnostToString(vybranaSouper.Druh) + " za " + skPosk + " poškození";
                        }
                        else if (vybranaSouper.Druh == Druh.Regenerace)
                        {
                            regen.Play();
                            if (souper == souboj.Utocnik)
                            {
                                if (souboj.EfektyUtocnika.Pokrik > 0)
                                {
                                    poskozeni = poskozeni * 2;
                                }
                            }
                            else
                            {
                                if (souboj.EfektyObrance.Pokrik > 0)
                                {
                                    poskozeni = poskozeni * 2;
                                }
                            }
                            souper.PridejNeboUberZdravi(poskozeni);
                            messageDruhy = souper.Name + " regeneruje zdraví za " + poskozeni + " bodů pomocí schopnosti " + PomocneMetody.SchopnostToString(vybranaSouper.Druh);
                        }
                        else if (vybranaSouper.Druh == Druh.Lahvicka_Zdravi)
                        {
                            regen.Play();
                            souper.Inventar.LahvickyZdravi--;
                            messageDruhy = souper.Name + " regeneruje zdravi za " + poskozeni + " bodů pomocí " + PomocneMetody.SchopnostToString(vybranaSouper.Druh);
                        }

                        else if (vybranaSouper.Druh == Druh.Lahvicka_Many)
                        {
                            regen.Play();
                            souper.Inventar.LahvickyMany--;
                            messageDruhy = souper.Name + " regeneruje manu za " + poskozeni + " bodů pomocí " + PomocneMetody.SchopnostToString(vybranaSouper.Druh);
                        }
                        else if (vybranaSouper.Druh == Druh.Bojovy_Pokrik)
                        {
                            battlecry.Play();
                            if (souper == souboj.Utocnik)
                            {
                                souboj.EfektyUtocnika.Pokrik += 2;
                            }
                            else
                            {
                                souboj.EfektyObrance.Pokrik += 2;
                            }
                            messageDruhy = souper.Name + " zařval " + PomocneMetody.SchopnostToString(vybranaSouper.Druh);
                        }
                        else if (vybranaSouper.Druh == Druh.Strelba_Lukem && vybranaSouper.Faze == 1 || vybranaSouper.Druh == Druh.Magicky_sip && vybranaSouper.Faze == 1)
                        {
                            messageDruhy = souper.Name + " natahuje luk, aby použil schopnost " + PomocneMetody.SchopnostToString(vybranaSouper.Druh);
                            bowPull.Play();
                        }
                        else
                        {
                            if (souper == souboj.Utocnik)
                            {
                                if (souboj.EfektyUtocnika.Pokrik > 0)
                                {
                                    poskozeni = poskozeni * 2;
                                }
                            }
                            else
                            {
                                if (souboj.EfektyObrance.Pokrik > 0)
                                {
                                    poskozeni = poskozeni * 2;
                                }
                            }

                            if (vybranaHrajici.Druh == Druh.Magicky_Stit || vybranaHrajici.Druh == Druh.Obrana_Stitem || vybranaHrajici.Druh == Druh.Uskok)
                            {
                                poskozeni = 0;
                                if (vybranaSouper.Druh == Druh.Magicky_sip || vybranaSouper.Druh == Druh.Strelba_Lukem)
                                    bowMiss.Play();
                            }
                            else if (vybranaSouper.Druh == Druh.Ohniva_Koule)
                            {
                                fireball.Play();
                                if (souper == souboj.Utocnik)
                                {
                                    souboj.EfektyObrance.Horeni += 2;
                                }
                                else
                                {
                                    souboj.EfektyUtocnika.Horeni += 2;
                                }
                            }
                            else if (vybranaSouper.Druh == Druh.Ledove_Kopi)
                            {
                                frostbolt.Play();
                                if (souper == souboj.Utocnik)
                                {
                                    souboj.EfektyObrance.Mraz += 2;
                                    souboj.EfektyObrance.AktivujMraz();
                                }
                                else
                                {
                                    souboj.EfektyUtocnika.Mraz += 2;
                                    souboj.EfektyUtocnika.AktivujMraz();
                                }
                            }
                            else
                            {
                                hit.Play();
                            }
                            int skPosk;
                            if (souper == souboj.Utocnik)
                            {
                                skPosk = souboj.ZautocNaObrance(poskozeni, false);
                            }
                            else
                            {
                                skPosk = souboj.ZautocNaUtocnika(poskozeni, false);
                            }
                            messageDruhy = souper.Name + " útočí schopností " + PomocneMetody.SchopnostToString(vybranaSouper.Druh) + " za " + skPosk + " poškození";
                        }
                        if (vybranaSouper.Faze == 0)
                        {
                            souper.PridejNeboUberManu(-vybranaSouper.CenaMany);
                        }
                        faze = Faze.Zhodnoceni;
                    }
                    break;
                case Faze.Zhodnoceni:
                    Schopnost pom1 = null;
                    Schopnost pom2 = null;
                    if (vybranaHrajici.Faze > 0)
                    {
                        vybranaHrajici.Faze--;
                        pom1 = vybranaHrajici;
                    }
                    else
                    {
                        vybranaHrajici.Faze = vybranaHrajici.FazeVychozi;
                    }
                    if (vybranaSouper.Faze > 0)
                    {
                        vybranaSouper.Faze--;
                        pom2 = vybranaSouper;
                    }
                    else
                    {
                        vybranaSouper.Faze = vybranaSouper.FazeVychozi;
                    }
                    vybranaHrajici = pom2;
                    vybranaSouper = pom1;
                    ZacarovanyLes.delay = 3;
                    ZacarovanyLes.delayed = true;
                    faze = Faze.EfektyPrvni;
                    break;
            }

            if (ZacarovanyLes.newState.IsKeyDown(Keys.Enter) && (!ZacarovanyLes.keyDelayed || ZacarovanyLes.oldState.IsKeyUp(Keys.Enter)))
            {
                ZacarovanyLes.keyDelayed = true;
                ZacarovanyLes.keyDelay = ZacarovanyLes.DELAY_TIME;
            }

            if (ZacarovanyLes.newState.IsKeyDown(Keys.Escape) && (!ZacarovanyLes.keyDelayed || ZacarovanyLes.oldState.IsKeyUp(Keys.Escape)))
            {
                ZacarovanyLes.keyDelayed = true;
                ZacarovanyLes.keyDelay = ZacarovanyLes.DELAY_TIME;
                _game.ChangeState(ZacarovanyLes.menuState);
            }

        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            foreach (Button butt in buttons)
            {
                butt.Visible = false;
            }

            if ((hrajici != null && hrajici.Majitel == Majitel.Hrac && faze == Faze.VyberPrvni && vybranaHrajici == null) ||
                (souper != null && souper.Majitel == Majitel.Hrac && faze == Faze.VyberDruhy && vybranaSouper == null))
            {
                int y = 200;
                int i = 0;
                Postava naRade = faze == Faze.VyberPrvni ? hrajici : souper;
                foreach (Schopnost schop in naRade.Schopnosti)
                {
                    switch (schop.Druh)
                    {
                        case Druh.Utok_Mecem:
                            PomocneMetody.NastavButton(buttonUtokMecem, schop, naRade, 325, y + i * 30, 150, 20, fontText, prazdnaTexturaBila, Color.White, Color.Black);
                            buttonUtokMecem.Visible = true;
                            break;
                        case Druh.Obrana_Stitem:
                            PomocneMetody.NastavButton(buttonObranaStitem, schop, naRade, 325, y + i * 30, 150, 20, fontText, prazdnaTexturaBila, Color.White, Color.Black);
                            buttonObranaStitem.Visible = true;
                            break;
                        case Druh.Regenerace:
                            PomocneMetody.NastavButton(buttonRegenerace, schop, naRade, 325, y + i * 30, 150, 20, fontText, prazdnaTexturaBila, Color.White, Color.Black);
                            buttonRegenerace.Visible = true;
                            break;
                        case Druh.Bojovy_Pokrik:
                            PomocneMetody.NastavButton(buttonBojovyPokrik, schop, naRade, 325, y + i * 30, 150, 20, fontText, prazdnaTexturaBila, Color.White, Color.Black);
                            buttonBojovyPokrik.Visible = true;
                            break;
                        case Druh.Bodnuti_Dykou:
                            PomocneMetody.NastavButton(buttonBodnutiDykou, schop, naRade, 325, y + i * 30, 150, 20, fontText, prazdnaTexturaBila, Color.White, Color.Black);
                            buttonBodnutiDykou.Visible = true;
                            break;
                        case Druh.Strelba_Lukem:
                            PomocneMetody.NastavButton(buttonStrelbaLukem, schop, naRade, 325, y + i * 30, 150, 20, fontText, prazdnaTexturaBila, Color.White, Color.Black);
                            buttonStrelbaLukem.Visible = true;
                            break;
                        case Druh.Magicky_sip:
                            PomocneMetody.NastavButton(buttonMagickySip, schop, naRade, 325, y + i * 30, 150, 20, fontText, prazdnaTexturaBila, Color.White, Color.Black);
                            buttonMagickySip.Visible = true;
                            break;
                        case Druh.Uskok:
                            PomocneMetody.NastavButton(buttonUskok, schop, naRade, 325, y + i * 30, 150, 20, fontText, prazdnaTexturaBila, Color.White, Color.Black);
                            buttonUskok.Visible = true;
                            break;
                        case Druh.Uder_Holi:
                            PomocneMetody.NastavButton(buttonUderHoli, schop, naRade, 325, y + i * 30, 150, 20, fontText, prazdnaTexturaBila, Color.White, Color.Black);
                            buttonUderHoli.Visible = true;
                            break;
                        case Druh.Ohniva_Koule:
                            PomocneMetody.NastavButton(buttonOhnivaKoule, schop, naRade, 325, y + i * 30, 150, 20, fontText, prazdnaTexturaBila, Color.White, Color.Black);
                            buttonOhnivaKoule.Visible = true;
                            break;
                        case Druh.Ledove_Kopi:
                            PomocneMetody.NastavButton(buttonLedoveKopi, schop, naRade, 325, y + i * 30, 150, 20, fontText, prazdnaTexturaBila, Color.White, Color.Black);
                            buttonLedoveKopi.Visible = true;
                            break;
                        case Druh.Magicky_Stit:
                            PomocneMetody.NastavButton(buttonMagickyStit, schop, naRade, 325, y + i * 30, 150, 20, fontText, prazdnaTexturaBila, Color.White, Color.Black);
                            buttonMagickyStit.Visible = true;
                            break;
                        case Druh.Utek:
                            PomocneMetody.NastavButton(buttonUtek, schop, naRade, 325, y + i * 30, 150, 20, fontText, prazdnaTexturaBila, Color.White, Color.Black);
                            buttonUtek.Visible = true;
                            break;
                        case Druh.Lahvicka_Many:
                            PomocneMetody.NastavButton(buttonLahvickaMany, schop, naRade, 325, y + i * 30, 150, 20, fontText, prazdnaTexturaBila, Color.White, Color.Black);
                            buttonLahvickaMany.Visible = true;
                            break;
                        case Druh.Lahvicka_Zdravi:
                            PomocneMetody.NastavButton(buttonLahvickaZdravi, schop, naRade, 325, y + i * 30, 150, 20, fontText, prazdnaTexturaBila, Color.White, Color.Black);
                            buttonLahvickaZdravi.Visible = true;
                            break;
                    }
                    i++;
                }
            }

            utocnikPortret = DejPortret(souboj.Utocnik.Pohlavi, souboj.Utocnik.Trida);
            obrancePortret = DejPortret(souboj.Obrance.Pohlavi, souboj.Obrance.Trida);
            spriteBatch.Begin();
            //sprites
            spriteBatch.Draw(panel, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(plocha, new Vector2(200, 0), Color.White);
            spriteBatch.Draw(panel, new Vector2(600, 0), Color.White);
            spriteBatch.Draw(utocnikPortret, new Rectangle(5, 37, 190, 190), Color.White);
            spriteBatch.Draw(obrancePortret, new Rectangle(605, 37, 190, 190), Color.White);
            //text
            //portret
            delkaU = fontNadpis.MeasureString(souboj.Utocnik.Name);
            spriteBatch.DrawString(fontNadpis, souboj.Utocnik.Name, new Vector2(100 - delkaU.X / 2, 20 - delkaU.Y / 2), Color.Black);
            delkaO = fontNadpis.MeasureString(souboj.Obrance.Name);
            spriteBatch.DrawString(fontNadpis, souboj.Obrance.Name, new Vector2(700 - delkaO.X / 2, 20 - delkaO.Y / 2), Color.Black);
            //trida
            delkaU = fontNadpis.MeasureString(PomocneMetody.TridaToString(souboj.Utocnik.Trida, souboj.Utocnik.Pohlavi));
            spriteBatch.DrawString(fontNadpis, PomocneMetody.TridaToString(souboj.Utocnik.Trida, souboj.Utocnik.Pohlavi), new Vector2(100 - delkaU.X / 2, 245 - delkaU.Y / 2), Color.Black);
            delkaO = fontNadpis.MeasureString(PomocneMetody.TridaToString(souboj.Obrance.Trida, souboj.Obrance.Pohlavi));
            spriteBatch.DrawString(fontNadpis, PomocneMetody.TridaToString(souboj.Obrance.Trida, souboj.Obrance.Pohlavi), new Vector2(700 - delkaO.X / 2, 245 - delkaO.Y / 2), Color.Black);
            //Pohlavi                    
            delkaU = fontNadpis.MeasureString(PomocneMetody.PohlaviToString(souboj.Utocnik.Pohlavi));
            spriteBatch.DrawString(fontNadpis, PomocneMetody.PohlaviToString(souboj.Utocnik.Pohlavi), new Vector2(100 - delkaU.X / 2, 280 - delkaU.Y / 2), Color.Black);
            delkaO = fontNadpis.MeasureString(PomocneMetody.PohlaviToString(souboj.Obrance.Pohlavi));
            spriteBatch.DrawString(fontNadpis, PomocneMetody.PohlaviToString(souboj.Obrance.Pohlavi), new Vector2(700 - delkaO.X / 2, 280 - delkaO.Y / 2), Color.Black);
            //Level
            spriteBatch.DrawString(fontText, "Level: " + souboj.Utocnik.Level, new Vector2(20, 300), Color.Black);
            spriteBatch.DrawString(fontText, "Level: " + souboj.Obrance.Level, new Vector2(620, 300), Color.Black);
            //Zkusenosti
            spriteBatch.DrawString(fontText, "Zkušenosti: " + souboj.Utocnik.Zkusenosti + "/" + souboj.Utocnik.ZkusenostiNext, new Vector2(20, 320), Color.Black);
            if (souboj.Obrance.Majitel == Majitel.Hrac)
                spriteBatch.DrawString(fontText, "Zkušenosti: " + souboj.Obrance.Zkusenosti + "/" + souboj.Obrance.ZkusenostiNext, new Vector2(620, 320), Color.Black);
            //Zivoty
            spriteBatch.DrawString(fontText, "Životy: " + souboj.Utocnik.Zivoty + "/" + souboj.Utocnik.ZivotyMax, new Vector2(20, 350), Color.Black);
            spriteBatch.DrawString(fontText, "Životy: " + souboj.Obrance.Zivoty + "/" + souboj.Obrance.ZivotyMax, new Vector2(620, 350), Color.Black);
            //Mana
            spriteBatch.DrawString(fontText, "Mana: " + souboj.Utocnik.Mana + "/" + souboj.Utocnik.ManaMax, new Vector2(20, 370), Color.Black);
            spriteBatch.DrawString(fontText, "Mana: " + souboj.Obrance.Mana + "/" + souboj.Obrance.ManaMax, new Vector2(620, 370), Color.Black);
            //Sila
            if (souboj.EfektyUtocnika.Mraz > 0)
                spriteBatch.DrawString(fontText, "Síla: " + souboj.Utocnik.Sila, new Vector2(20, 400), Color.Red);
            else spriteBatch.DrawString(fontText, "Síla: " + souboj.Utocnik.Sila, new Vector2(20, 400), Color.Black);
            if (souboj.EfektyObrance.Mraz > 0)
                spriteBatch.DrawString(fontText, "Síla: " + souboj.Obrance.Sila, new Vector2(620, 400), Color.Red);
            else spriteBatch.DrawString(fontText, "Síla: " + souboj.Obrance.Sila, new Vector2(620, 400), Color.Black);
            //Obratnost
            if (souboj.EfektyUtocnika.Mraz > 0)
                spriteBatch.DrawString(fontText, "Obratnost: " + souboj.Utocnik.Obratnost, new Vector2(20, 420), Color.Red);
            else spriteBatch.DrawString(fontText, "Obratnost: " + souboj.Utocnik.Obratnost, new Vector2(20, 420), Color.Black);
            if (souboj.EfektyObrance.Mraz > 0)
                spriteBatch.DrawString(fontText, "Obratnost: " + souboj.Obrance.Obratnost, new Vector2(620, 420), Color.Red);
            else spriteBatch.DrawString(fontText, "Obratnost: " + souboj.Obrance.Obratnost, new Vector2(620, 420), Color.Black);
            //Inteligence
            if (souboj.EfektyUtocnika.Mraz > 0)
                spriteBatch.DrawString(fontText, "Inteligence: " + souboj.Utocnik.Inteligence, new Vector2(20, 440), Color.Red);
            else spriteBatch.DrawString(fontText, "Inteligence: " + souboj.Utocnik.Inteligence, new Vector2(20, 440), Color.Black);
            if (souboj.EfektyObrance.Mraz > 0)
                spriteBatch.DrawString(fontText, "Inteligence: " + souboj.Obrance.Inteligence, new Vector2(620, 440), Color.Red);
            else spriteBatch.DrawString(fontText, "Inteligence: " + souboj.Obrance.Inteligence, new Vector2(620, 440), Color.Black);
            //Brneni
            spriteBatch.DrawString(fontText, "Brnění: " + souboj.Utocnik.Brneni, new Vector2(20, 460), Color.Black);
            spriteBatch.DrawString(fontText, "Brnění: " + souboj.Obrance.Brneni, new Vector2(620, 460), Color.Black);
            //Efekty
            //Pokřik
            if (souboj.EfektyUtocnika.Pokrik > 0)
                spriteBatch.DrawString(fontText, "Pokřik [" + souboj.EfektyUtocnika.Pokrik + "]", new Vector2(20, 490), Color.Red);
            if (souboj.EfektyObrance.Pokrik > 0)
                spriteBatch.DrawString(fontText, "Pokřik [" + souboj.EfektyObrance.Pokrik + "]", new Vector2(620, 490), Color.Red);
            //Hoří
            if (souboj.EfektyUtocnika.Horeni > 0)
                spriteBatch.DrawString(fontText, "Hoří [" + souboj.EfektyUtocnika.Horeni + "]", new Vector2(20, 510), Color.Red);
            if (souboj.EfektyObrance.Horeni > 0)
                spriteBatch.DrawString(fontText, "Hoří [" + souboj.EfektyObrance.Horeni + "]", new Vector2(620, 510), Color.Red);
            //Mrzne
            if (souboj.EfektyUtocnika.Mraz > 0)
                spriteBatch.DrawString(fontText, "Mrzne [" + souboj.EfektyUtocnika.Mraz + "]", new Vector2(20, 530), Color.Red);
            if (souboj.EfektyObrance.Mraz > 0)
                spriteBatch.DrawString(fontText, "Mrzne [" + souboj.EfektyObrance.Mraz + "]", new Vector2(620, 530), Color.Red);
            //MessageBox
            spriteBatch.Draw(prazdnaTexturaCerna, new Rectangle(0, 550, 800, 50), Color.Black);
            delkaU = fontText.MeasureString(messagePrvni);
            spriteBatch.DrawString(fontText, messagePrvni, new Vector2(400 - delkaU.X / 2, 563 - delkaU.Y / 2), Color.White);
            delkaO = fontText.MeasureString(messageDruhy);
            spriteBatch.DrawString(fontText, messageDruhy, new Vector2(400 - delkaO.X / 2, 588 - delkaO.Y / 2), Color.White);
            delkaU = fontNadpis.MeasureString(messageKolo);
            spriteBatch.DrawString(fontNadpis, messageKolo, new Vector2(400 - delkaU.X / 2, 100 - delkaU.Y / 2), Color.White);
            //Buttons
            if (faze == Faze.VyberPrvni || faze == Faze.VyberDruhy)
            {
                foreach (Button butt in buttons)
                {
                    butt.Draw(gameTime, spriteBatch);
                }
            }
            //Test
            //spriteBatch.DrawString(fontText, souboj.EfektyUtocnika.Postava.Inteligence.ToString()+" "+souboj.EfektyUtocnika.PuvodniIntelekt.ToString(), new Vector2(400, 300), Color.White);
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

        protected Schopnost VyberSchopnostAI(Postava pocitac, Schopnost souperSch, Postava souper)
        {
            Schopnost sch;
            switch (pocitac.Majitel)
            {
                case Majitel.Pocitac_Lehky:
                start00:
                    sch = pocitac.Schopnosti[souboj.Kostka.Next(0, pocitac.Schopnosti.Count)];
                    if (sch.Cd > 0 || sch.CenaMany > pocitac.Mana)
                    {
                        goto start00;
                    }
                    return sch;
                case Majitel.Pocitac_Stredni:
                    switch (pocitac.Trida)
                    {
                        case Trida.Bojovnik:
                        start10:
                            sch = pocitac.Schopnosti[souboj.Kostka.Next(0, pocitac.Schopnosti.Count)];
                            if (sch.Cd > 0 || sch.CenaMany > pocitac.Mana || sch.Druh == Druh.Obrana_Stitem)
                            {
                                goto start10;
                            }
                            if (souperSch != null && (souperSch.Druh == Druh.Magicky_sip && souperSch.Faze == 0 || souperSch.Druh == Druh.Strelba_Lukem && souperSch.Faze == 0 || souperSch.Druh == Druh.Utok_Mecem && souboj.EfektyUtocnika.Pokrik > 0 || souperSch.Druh == Druh.Ohniva_Koule || souperSch.Druh == Druh.Ledove_Kopi))
                            {
                                foreach (Schopnost s in pocitac.Schopnosti)
                                {
                                    if (s.Druh == Druh.Obrana_Stitem)
                                    {
                                        sch = s.Cd > 0 ? sch : s;
                                    }
                                }
                            }

                            return sch;
                        case Trida.Lucistnik:
                        start11:
                            sch = pocitac.Schopnosti[souboj.Kostka.Next(0, pocitac.Schopnosti.Count)];
                            if (sch.Cd > 0 || sch.CenaMany > pocitac.Mana || sch.Druh == Druh.Uskok)
                            {
                                goto start11;
                            }
                            if (souperSch != null && (souperSch.Druh == Druh.Magicky_sip && souperSch.Faze == 0 || souperSch.Druh == Druh.Strelba_Lukem && souperSch.Faze == 0 || souperSch.Druh == Druh.Utok_Mecem && souboj.EfektyUtocnika.Pokrik > 0 || souperSch.Druh == Druh.Ohniva_Koule || souperSch.Druh == Druh.Ledove_Kopi))
                            {
                                foreach (Schopnost s in pocitac.Schopnosti)
                                {
                                    if (s.Druh == Druh.Uskok)
                                    {
                                        sch = s.Cd > 0 ? sch : s;
                                    }
                                }
                            }
                            return sch;
                        case Trida.Kouzelnik:
                        start12:
                            sch = pocitac.Schopnosti[souboj.Kostka.Next(0, pocitac.Schopnosti.Count)];
                            if (sch.Cd > 0 || sch.CenaMany > pocitac.Mana || sch.Druh == Druh.Obrana_Stitem)
                            {
                                goto start12;
                            }
                            if (souperSch != null && (souperSch.Druh == Druh.Magicky_sip && souperSch.Faze == 0 || souperSch.Druh == Druh.Strelba_Lukem && souperSch.Faze == 0 || souperSch.Druh == Druh.Utok_Mecem && souboj.EfektyUtocnika.Pokrik > 0 || souperSch.Druh == Druh.Ohniva_Koule || souperSch.Druh == Druh.Ledove_Kopi))
                            {
                                foreach (Schopnost s in pocitac.Schopnosti)
                                {
                                    if (s.Druh == Druh.Magicky_Stit)
                                    {
                                        sch = s.Cd > 0 || s.CenaMany > pocitac.Mana ? sch : s;
                                    }
                                }
                            }
                            return sch;
                    }
                    break;
                case Majitel.Pocitac_Tezky:
                    switch (pocitac.Trida)
                    {
                        case Trida.Bojovnik:
                            sch = null;
                            foreach (Schopnost s in pocitac.Schopnosti)
                            {
                                if (s.Druh == Druh.Utok_Mecem)
                                {
                                    sch = s;
                                }
                            }
                            foreach (Schopnost s in pocitac.Schopnosti)
                            {
                                if (s.Druh == Druh.Bojovy_Pokrik)
                                {
                                    sch = s.Cd > 0 || s.CenaMany > pocitac.Mana ? sch : s;
                                }
                            }
                            foreach (Schopnost s in pocitac.Schopnosti)
                            {
                                if (s.Druh == Druh.Utok_Mecem && souboj.EfektyObrance.Pokrik > 0)
                                {
                                    sch = s;
                                }
                            }

                            foreach (Schopnost s in pocitac.Schopnosti)
                            {
                                if (s.Druh == Druh.Regenerace && pocitac.Zivoty <= pocitac.ZivotyMax / 2)
                                {
                                    sch = s.Cd > 0 || s.CenaMany > pocitac.Mana ? sch : s;
                                }
                            }
                            if (souperSch != null && (souperSch.Druh == Druh.Magicky_sip && souperSch.Faze == 0 || souperSch.Druh == Druh.Strelba_Lukem && souperSch.Faze == 0 || souperSch.Druh == Druh.Utok_Mecem && souboj.EfektyUtocnika.Pokrik > 0 || souperSch.Druh == Druh.Ohniva_Koule || souperSch.Druh == Druh.Ledove_Kopi))
                            {
                                foreach (Schopnost s in pocitac.Schopnosti)
                                {
                                    if (s.Druh == Druh.Obrana_Stitem)
                                    {
                                        sch = s.Cd > 0 ? sch : s;
                                    }
                                }
                            }
                            return sch;
                        case Trida.Lucistnik:
                            sch = null;
                            foreach (Schopnost s in pocitac.Schopnosti)
                            {
                                if (s.Druh == Druh.Strelba_Lukem)
                                {
                                    sch = s;
                                }
                            }
                            foreach (Schopnost s in pocitac.Schopnosti)
                            {
                                if (s.Druh == Druh.Bodnuti_Dykou && souper.Zivoty <= s.Pouzij(pocitac.Sila, pocitac.Obratnost, pocitac.Inteligence))
                                {
                                    sch = s;
                                }
                            }
                            foreach (Schopnost schop in souper.Schopnosti)
                            {
                                if (schop.Druh == Druh.Uskok && schop.Cd > 1 || schop.Druh == Druh.Obrana_Stitem && schop.Cd > 1 || schop.Druh == Druh.Magicky_Stit && (schop.Cd > 1 || schop.CenaMany > souper.Mana))
                                {
                                    foreach (Schopnost schopnost in pocitac.Schopnosti)
                                    {
                                        if (schopnost.Druh == Druh.Magicky_sip)
                                        {
                                            sch = schopnost.CenaMany > pocitac.Mana ? sch : schopnost;
                                        }
                                    }
                                }
                            }
                            if (souperSch != null && (souperSch.Druh == Druh.Magicky_sip && souperSch.Faze == 0 || souperSch.Druh == Druh.Strelba_Lukem && souperSch.Faze == 0 || souperSch.Druh == Druh.Utok_Mecem && souboj.EfektyUtocnika.Pokrik > 0 || souperSch.Druh == Druh.Ohniva_Koule || souperSch.Druh == Druh.Ledove_Kopi))
                            {
                                foreach (Schopnost s in pocitac.Schopnosti)
                                {
                                    if (s.Druh == Druh.Uskok)
                                    {
                                        sch = s.Cd > 0 ? sch : s;
                                    }
                                }
                            }
                            return sch;
                        case Trida.Kouzelnik:
                            sch = null;
                            foreach (Schopnost s in pocitac.Schopnosti)
                            {
                                if (s.Druh == Druh.Uder_Holi)
                                {
                                    sch = s;
                                }
                            }

                            if (souboj.EfektyUtocnika.Mraz == 0)
                            {
                                foreach (Schopnost s in pocitac.Schopnosti)
                                {
                                    if (s.Druh == Druh.Ledove_Kopi)
                                    {
                                        sch = s.Cd > 0 || s.CenaMany > pocitac.Mana ? sch : s;
                                    }
                                }
                            }
                            if (souboj.EfektyUtocnika.Horeni == 0)
                            {
                                foreach (Schopnost s in pocitac.Schopnosti)
                                {
                                    if (s.Druh == Druh.Ohniva_Koule)
                                    {
                                        sch = s.Cd > 0 || s.CenaMany > pocitac.Mana ? sch : s;
                                    }
                                }
                            }
                            if (souperSch != null && (souperSch.Druh == Druh.Magicky_sip && souperSch.Faze == 0 || souperSch.Druh == Druh.Strelba_Lukem && souperSch.Faze == 0 || souperSch.Druh == Druh.Utok_Mecem && souboj.EfektyUtocnika.Pokrik > 0 || souperSch.Druh == Druh.Ohniva_Koule || souperSch.Druh == Druh.Ledove_Kopi))
                            {
                                foreach (Schopnost s in pocitac.Schopnosti)
                                {
                                    if (s.Druh == Druh.Magicky_Stit)
                                    {
                                        sch = s.Cd > 0 || s.CenaMany > pocitac.Mana ? sch : s;
                                    }
                                }
                            }
                            return sch;
                    }
                    break;
            }
            return null;
        }





        private void ButtonUtokMecemClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaHrajici = hrajici.DejSchopnost(Druh.Utok_Mecem);
                    break;
                case Faze.VyberDruhy:
                    vybranaSouper = souper.DejSchopnost(Druh.Utok_Mecem);
                    break;
            }
        }
        private void ButtonObranaStitemClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaHrajici = hrajici.DejSchopnost(Druh.Obrana_Stitem);
                    break;
                case Faze.VyberDruhy:
                    vybranaSouper = souper.DejSchopnost(Druh.Obrana_Stitem);
                    break;
            }
        }
        private void ButtonRegeneraceClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaHrajici = hrajici.DejSchopnost(Druh.Regenerace);
                    break;
                case Faze.VyberDruhy:
                    vybranaSouper = souper.DejSchopnost(Druh.Regenerace);
                    break;
            }
        }
        private void ButtonBojovyPokrikClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaHrajici = hrajici.DejSchopnost(Druh.Bojovy_Pokrik);
                    break;
                case Faze.VyberDruhy:
                    vybranaSouper = souper.DejSchopnost(Druh.Bojovy_Pokrik);
                    break;
            }
        }

        private void ButtonBodnutiDykouClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaHrajici = hrajici.DejSchopnost(Druh.Bodnuti_Dykou);
                    break;
                case Faze.VyberDruhy:
                    vybranaSouper = souper.DejSchopnost(Druh.Bodnuti_Dykou);
                    break;
            }
        }

        private void ButtonStrelbaLukemClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaHrajici = hrajici.DejSchopnost(Druh.Strelba_Lukem);
                    break;
                case Faze.VyberDruhy:
                    vybranaSouper = souper.DejSchopnost(Druh.Strelba_Lukem);
                    break;
            }
        }

        private void ButtonMagickySipClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaHrajici = hrajici.DejSchopnost(Druh.Magicky_sip);
                    break;
                case Faze.VyberDruhy:
                    vybranaSouper = souper.DejSchopnost(Druh.Magicky_sip);
                    break;
            }
        }

        private void ButtonUskokClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaHrajici = hrajici.DejSchopnost(Druh.Uskok);
                    break;
                case Faze.VyberDruhy:
                    vybranaSouper = souper.DejSchopnost(Druh.Uskok);
                    break;
            }
        }
        private void ButtonUderHoliClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaHrajici = hrajici.DejSchopnost(Druh.Uder_Holi);
                    break;
                case Faze.VyberDruhy:
                    vybranaSouper = souper.DejSchopnost(Druh.Uder_Holi);
                    break;
            }
        }
        private void ButtonOhnivaKouleClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaHrajici = hrajici.DejSchopnost(Druh.Ohniva_Koule);
                    break;
                case Faze.VyberDruhy:
                    vybranaSouper = souper.DejSchopnost(Druh.Ohniva_Koule);
                    break;
            }
        }
        private void ButtonLedoveKopiClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaHrajici = hrajici.DejSchopnost(Druh.Ledove_Kopi);
                    break;
                case Faze.VyberDruhy:
                    vybranaSouper = souper.DejSchopnost(Druh.Ledove_Kopi);
                    break;
            }
        }

        private void ButtonMagickyStitClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaHrajici = hrajici.DejSchopnost(Druh.Magicky_Stit);
                    break;
                case Faze.VyberDruhy:
                    vybranaSouper = souper.DejSchopnost(Druh.Magicky_Stit);
                    break;
            }
        }

        private void ButtonUtekClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaHrajici = hrajici.DejSchopnost(Druh.Utek);
                    break;
                case Faze.VyberDruhy:
                    vybranaSouper = souper.DejSchopnost(Druh.Utek);
                    break;
            }
        }

        private void ButtonLahvickaZdraviClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaHrajici = hrajici.DejSchopnost(Druh.Lahvicka_Zdravi);
                    break;
                case Faze.VyberDruhy:
                    vybranaSouper = souper.DejSchopnost(Druh.Lahvicka_Zdravi);
                    break;
            }
        }

        private void ButtonLahvickaManyClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaHrajici = hrajici.DejSchopnost(Druh.Lahvicka_Many);
                    break;
                case Faze.VyberDruhy:
                    vybranaSouper = souper.DejSchopnost(Druh.Lahvicka_Many);
                    break;
            }
        }
    }
}
