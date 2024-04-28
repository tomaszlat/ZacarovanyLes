using Microsoft.Xna.Framework;
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
        //spravce medii
        public SpravceMedii SpravceMedii;
        //promenne, konstanty ...
        public bool Duel = false;
        private Souboj souboj;
        private Faze faze;
        private Postava prvni;
        private Postava druhy;
        private Schopnost vybranaPrvni = null;
        private Schopnost vybranaDruhy = null;
        private string messageKolo = "";
        private string messagePrvni = "";
        private string messageDruhy = "";
        private bool zacatek = true;
        private bool oznaceniUtocnik = true;
        //fonty
        private Vector2 velikostTextuUtocnik;
        private Vector2 velikostTextuObrance;
        //Textury
        private Texture2D obrancePortret;
        private Texture2D utocnikPortret;
        //Tlacitka
        private Button buttonUtokMecem;
        private Button buttonObranaStitem;
        private Button buttonRegenerace;
        private Button buttonBojovyPokrik;
        private Button buttonUderStitem;
        private Button buttonVrhSekerou;
        private Button buttonBerserk;
        private Button buttonBodnutiDykou;
        private Button buttonStrelbaLukem;
        private Button buttonMagickySip;
        private Button buttonUskok;
        private Button buttonRychlost;
        private Button buttonLesniBobule;
        private Button buttonJedovaSipka;
        private Button buttonUderHoli;
        private Button buttonOhnivaKoule;
        private Button buttonLedoveKopi;
        private Button buttonMagickyStit;
        private Button buttonVysatiZivota;
        private Button buttonVysatiMany;
        private Button buttonMagickeSoustredeni;
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
            //spravce medii
            SpravceMedii = ZacarovanyLes.spravceMedii;
            //novy souboj
            souboj = new Souboj(ZacarovanyLes.utocnik, ZacarovanyLes.obrance);
            faze = Faze.Kolo;
            //Buttons
            buttonUtokMecem = new Button(SpravceMedii.FontText, new Vector2(0, 0), SpravceMedii.PrazdnaTexturaBila, 160, 20, Color.White, Color.Black, "");
            buttonUtokMecem.Click += ButtonUtokMecemClickedHandler;
            buttonObranaStitem = new Button(SpravceMedii.FontText, new Vector2(0, 0), SpravceMedii.PrazdnaTexturaBila, 160, 20, Color.White, Color.Black, "");
            buttonObranaStitem.Click += ButtonObranaStitemClickedHandler;
            buttonRegenerace = new Button(SpravceMedii.FontText, new Vector2(0, 0), SpravceMedii.PrazdnaTexturaBila, 160, 20, Color.White, Color.Black, "");
            buttonRegenerace.Click += ButtonRegeneraceClickedHandler;
            buttonBojovyPokrik = new Button(SpravceMedii.FontText, new Vector2(0, 0), SpravceMedii.PrazdnaTexturaBila, 160, 20, Color.White, Color.Black, "");
            buttonBojovyPokrik.Click += ButtonBojovyPokrikClickedHandler;
            buttonUderStitem = new Button(SpravceMedii.FontText, new Vector2(0, 0), SpravceMedii.PrazdnaTexturaBila, 160, 20, Color.White, Color.Black, "");
            buttonUderStitem.Click += ButtonUderStitemClickedHandler;
            buttonVrhSekerou = new Button(SpravceMedii.FontText, new Vector2(0, 0), SpravceMedii.PrazdnaTexturaBila, 160, 20, Color.White, Color.Black, "");
            buttonVrhSekerou.Click += ButtonVrhSekerouClickedHandler;
            buttonBerserk = new Button(SpravceMedii.FontText, new Vector2(0, 0), SpravceMedii.PrazdnaTexturaBila, 160, 20, Color.White, Color.Black, "");
            buttonBerserk.Click += ButtonBerserkClickedHandler;
            buttonBodnutiDykou = new Button(SpravceMedii.FontText, new Vector2(0, 0), SpravceMedii.PrazdnaTexturaBila, 160, 20, Color.White, Color.Black, "");
            buttonBodnutiDykou.Click += ButtonBodnutiDykouClickedHandler;
            buttonStrelbaLukem = new Button(SpravceMedii.FontText, new Vector2(0, 0), SpravceMedii.PrazdnaTexturaBila, 160, 20, Color.White, Color.Black, "");
            buttonStrelbaLukem.Click += ButtonStrelbaLukemClickedHandler;
            buttonMagickySip = new Button(SpravceMedii.FontText, new Vector2(0, 0), SpravceMedii.PrazdnaTexturaBila, 160, 20, Color.White, Color.Black, "");
            buttonMagickySip.Click += ButtonMagickySipClickedHandler;
            buttonUskok = new Button(SpravceMedii.FontText, new Vector2(0, 0), SpravceMedii.PrazdnaTexturaBila, 160, 20, Color.White, Color.Black, "");
            buttonUskok.Click += ButtonUskokClickedHandler;
            buttonRychlost = new Button(SpravceMedii.FontText, new Vector2(0, 0), SpravceMedii.PrazdnaTexturaBila, 160, 20, Color.White, Color.Black, "");
            buttonRychlost.Click += ButtonRychlostClickedHandler;
            buttonLesniBobule = new Button(SpravceMedii.FontText, new Vector2(0, 0), SpravceMedii.PrazdnaTexturaBila, 160, 20, Color.White, Color.Black, "");
            buttonLesniBobule.Click += ButtonLesniBobuleClickedHandler;
            buttonJedovaSipka = new Button(SpravceMedii.FontText, new Vector2(0, 0), SpravceMedii.PrazdnaTexturaBila, 160, 20, Color.White, Color.Black, "");
            buttonJedovaSipka.Click += ButtonJedovaSipkaClickedHandler;
            buttonUderHoli = new Button(SpravceMedii.FontText, new Vector2(0, 0), SpravceMedii.PrazdnaTexturaBila, 160, 20, Color.White, Color.Black, "");
            buttonUderHoli.Click += ButtonUderHoliClickedHandler;
            buttonOhnivaKoule = new Button(SpravceMedii.FontText, new Vector2(0, 0), SpravceMedii.PrazdnaTexturaBila, 160, 20, Color.White, Color.Black, "");
            buttonOhnivaKoule.Click += ButtonOhnivaKouleClickedHandler;
            buttonLedoveKopi = new Button(SpravceMedii.FontText, new Vector2(0, 0), SpravceMedii.PrazdnaTexturaBila, 160, 20, Color.White, Color.Black, "");
            buttonLedoveKopi.Click += ButtonLedoveKopiClickedHandler;
            buttonVysatiZivota = new Button(SpravceMedii.FontText, new Vector2(0, 0), SpravceMedii.PrazdnaTexturaBila, 160, 20, Color.White, Color.Black, "");
            buttonVysatiZivota.Click += ButtonVysatiZivotaClickedHandler;
            buttonVysatiMany = new Button(SpravceMedii.FontText, new Vector2(0, 0), SpravceMedii.PrazdnaTexturaBila, 160, 20, Color.White, Color.Black, "");
            buttonVysatiMany.Click += ButtonVysatiManyClickedHandler;
            buttonMagickeSoustredeni = new Button(SpravceMedii.FontText, new Vector2(0, 0), SpravceMedii.PrazdnaTexturaBila, 160, 20, Color.White, Color.Black, "");
            buttonMagickeSoustredeni.Click += ButtonMagickeSoustredeniClickedHandler;
            buttonMagickyStit = new Button(SpravceMedii.FontText, new Vector2(0, 0), SpravceMedii.PrazdnaTexturaBila, 160, 20, Color.White, Color.Black, "");
            buttonMagickyStit.Click += ButtonMagickyStitClickedHandler;
            buttonUtek = new Button(SpravceMedii.FontText, new Vector2(0, 0), SpravceMedii.PrazdnaTexturaBila, 160, 20, Color.White, Color.Black, "");
            buttonUtek.Click += ButtonUtekClickedHandler;
            buttonLahvickaZdravi = new Button(SpravceMedii.FontText, new Vector2(0, 0), SpravceMedii.PrazdnaTexturaBila, 160, 20, Color.White, Color.Black, "");
            buttonLahvickaZdravi.Click += ButtonLahvickaZdraviClickedHandler;
            buttonLahvickaMany = new Button(SpravceMedii.FontText, new Vector2(0, 0), SpravceMedii.PrazdnaTexturaBila, 160, 20, Color.White, Color.Black, "");
            buttonLahvickaMany.Click += ButtonLahvickaManyClickedHandler;

            buttons = new List<Button>()
            {
            buttonUtokMecem,
            buttonObranaStitem,
            buttonRegenerace,
            buttonBojovyPokrik,
            buttonUderStitem,
            buttonVrhSekerou,
            buttonBerserk,
            buttonBodnutiDykou,
            buttonStrelbaLukem,
            buttonMagickySip,
            buttonUskok,
            buttonRychlost,
            buttonLesniBobule,
            buttonJedovaSipka,
            buttonUderHoli,
            buttonOhnivaKoule,
            buttonLedoveKopi,
            buttonMagickyStit,
            buttonVysatiZivota,
            buttonVysatiMany,
            buttonMagickeSoustredeni,
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
                ZacarovanyLes.NastavitZpozdeniHry(3);
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
                            oznaceniUtocnik = false;
                            messageKolo = souboj.PocetKol + ". kolo, " + souboj.Obrance.Name + " zvítěžil";
                            messagePrvni = souboj.Utocnik.Name + " byl poražen";
                            messageDruhy = souboj.Obrance.Name + " zvítězil !";
                            faze = Faze.VyhraObrance;
                            ZacarovanyLes.NastavitZpozdeniHry(3);
                        }
                        else
                        {
                            oznaceniUtocnik = true;
                            messageKolo = souboj.PocetKol + ". kolo, " + souboj.Utocnik.Name + " zvítěžil";
                            messagePrvni = souboj.Obrance.Name + " byl poražen";
                            messageDruhy = souboj.Utocnik.Name + " zvítězil !";
                            faze = Faze.VyhraUtocnik;
                            ZacarovanyLes.NastavitZpozdeniHry(3);
                        }
                    }
                    break;
                case Faze.VyhraUtocnik:
                    if (!ZacarovanyLes.delayed)
                    {
                        if (ZacarovanyLes.mapState != null)
                        {
                            souboj.Reset();
                            switch (souboj.Obrance.Majitel)
                            {
                                case Majitel.Pocitac_Lehky:
                                    ZacarovanyLes.utocnik.PridejZkusenosti(25);
                                    break;
                                case Majitel.Pocitac_Stredni:
                                    ZacarovanyLes.utocnik.PridejZkusenosti(50);
                                    break;
                                case Majitel.Pocitac_Tezky:
                                    ZacarovanyLes.utocnik.PridejZkusenosti(100);
                                    break;
                            }
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
                        oznaceniUtocnik = prvni == souboj.Utocnik;
                        messageKolo = souboj.PocetKol + ". kolo, efekty postavy " + prvni.Name;
                        //Efekty efektyPrvni = prvni == souboj.Utocnik ? souboj.EfektyUtocnika : souboj.EfektyObrance;
                        bool efekt = false;
                        messagePrvni = souboj.EfektyPrvni(ref efekt, SpravceMedii);
                        messageDruhy = "";

                        faze = Faze.EfektyDruhy;
                        if (efekt)
                        {
                            ZacarovanyLes.NastavitZpozdeniHry(3);
                        }
                    }
                    break;
                case Faze.EfektyDruhy:
                    if (!ZacarovanyLes.delayed)
                    {
                        oznaceniUtocnik = druhy == souboj.Utocnik;
                        messageKolo = souboj.PocetKol + ". kolo, efekty postavy " + druhy.Name;
                        //Efekty efektyDruhy = druhy == souboj.Utocnik ? souboj.EfektyUtocnika : souboj.EfektyObrance;
                        bool efekt = false;
                        messagePrvni = souboj.EfektyDruhy(ref efekt, SpravceMedii);

                        souboj.ZhodnotEfekty();
                        faze = Faze.Kolo;
                        if (efekt)
                        {
                            ZacarovanyLes.NastavitZpozdeniHry(3);
                        }
                    }
                    break;
                case Faze.Kolo:
                    if (!ZacarovanyLes.delayed)
                    {
                        if (zacatek)
                        {
                            prvni = souboj.Obrance;
                            druhy = souboj.Utocnik;
                            if (souboj.ZacinaUtocnik)
                            {
                                prvni = souboj.Utocnik;
                                druhy = souboj.Obrance;
                            }
                            zacatek = false;
                        }
                        else
                        {
                            if (prvni == souboj.Utocnik)
                            {
                                prvni = souboj.Obrance;
                                druhy = souboj.Utocnik;
                            }
                            else
                            {
                                prvni = souboj.Utocnik;
                                druhy = souboj.Obrance;
                            }
                        }
                        souboj.Prvni = prvni;
                        souboj.Druhy = druhy;
                        souboj.PocetKol++;
                        messagePrvni = "";
                        messageDruhy = "";
                        messageKolo = souboj.PocetKol + ". kolo, začíná ho " + prvni.Name;
                        oznaceniUtocnik = prvni == souboj.Utocnik;
                        faze = Faze.Zacatek;
                    }
                    break;
                case Faze.Zacatek:
                    if (!ZacarovanyLes.delayed)
                    {
                        ZacarovanyLes.NastavitZpozdeniHry(3);
                        faze = Faze.VyberPrvni;
                    }
                    break;
                case Faze.VyberPrvni:
                    if (prvni.Efekty.Omraceni == 0)
                    {
                        messagePrvni = prvni.Name + " vybírá schopnost";
                        if (!ZacarovanyLes.delayed)
                        {
                            string message = souboj.VyberPrvniSchopnosti(ref vybranaDruhy, ref vybranaPrvni);
                            if (message != "")
                            {
                                messagePrvni = message;
                                ZacarovanyLes.NastavitZpozdeniHry(3);
                                faze = Faze.VyberDruhy;
                            }
                        }
                    }
                    else
                    {
                        messagePrvni = prvni.Name + " je omráčený";
                        ZacarovanyLes.NastavitZpozdeniHry(3);
                        faze = Faze.VyberDruhy;
                    }
                    break;
                case Faze.VyberDruhy:
                    if (druhy.Efekty.Omraceni == 0)
                    {
                        messageDruhy = druhy.Name + " vybírá schopnost";
                        messageKolo = souboj.PocetKol + ". kolo, nyní hraje " + druhy.Name;
                        oznaceniUtocnik = druhy == souboj.Utocnik;
                        if (!ZacarovanyLes.delayed)
                        {
                            string message = souboj.VyberDruheSchopnosti(ref vybranaDruhy, ref vybranaPrvni);
                            if (message != "")
                            {
                                messageDruhy = message;
                                ZacarovanyLes.NastavitZpozdeniHry(3);
                                faze = Faze.UtokPrvni;
                            }
                        }
                    }
                    else
                    {
                        messageDruhy = druhy.Name + " je omráčený";
                        ZacarovanyLes.NastavitZpozdeniHry(3);
                        faze = Faze.UtokPrvni;
                    }
                    break;
                case Faze.UtokPrvni:
                    oznaceniUtocnik = prvni == souboj.Utocnik;
                    messageKolo = souboj.PocetKol + ". kolo, " + prvni.Name + " používá schopnost";
                    if (!ZacarovanyLes.delayed)
                    {
                        messagePrvni = souboj.UtokSchopnosti(ref prvni,ref druhy,ref vybranaPrvni, ref vybranaDruhy,ref SpravceMedii);

                        ZacarovanyLes.NastavitZpozdeniHry(3);
                        faze = Faze.UtokDruhy;
                    }
                    break;
                case Faze.UtokDruhy:
                    oznaceniUtocnik = druhy == souboj.Utocnik;
                    messageKolo = souboj.PocetKol + ". kolo, " + druhy.Name + " používá schopnost";
                    if (!ZacarovanyLes.delayed)
                    {
                        messageDruhy = souboj.UtokSchopnosti(ref druhy,ref prvni,ref vybranaDruhy, ref vybranaPrvni, ref SpravceMedii);
                        faze = Faze.Zhodnoceni;
                    }
                    break;
                case Faze.Zhodnoceni:
                    souboj.ZhodnotSchopnosti(ref vybranaPrvni,ref vybranaDruhy);
                    ZacarovanyLes.NastavitZpozdeniHry(3);
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
            if (ZacarovanyLes.newState.IsKeyDown(Keys.OemPlus) && (!ZacarovanyLes.keyDelayed || ZacarovanyLes.oldState.IsKeyUp(Keys.OemPlus)))
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

            foreach (Button butt in buttons)
            {
                butt.Visible = false;
            }

            if ((prvni != null && prvni.Majitel == Majitel.Hrac && faze == Faze.VyberPrvni && vybranaPrvni == null) ||
                (druhy != null && druhy.Majitel == Majitel.Hrac && faze == Faze.VyberDruhy && vybranaDruhy == null))
            {
                int y = 200;
                int i = 0;

                Postava naRade = faze == Faze.VyberPrvni ? prvni : druhy;
                Efekty efekty = naRade == souboj.Obrance ? souboj.EfektyObrance : souboj.EfektyUtocnika;
                foreach (Schopnost schop in naRade.Schopnosti)
                {
                    Button button = schop.Druh switch
                    {
                        Druh.Utok_Mecem => buttonUtokMecem,
                        Druh.Obrana_Stitem => buttonObranaStitem,
                        Druh.Regenerace => buttonRegenerace,
                        Druh.Bojovy_Pokrik => buttonBojovyPokrik,
                        Druh.Uder_stitem => buttonUderStitem,
                        Druh.Vrh_sekerou => buttonVrhSekerou,
                        Druh.Berserk => buttonBerserk,
                        Druh.Bodnuti_Dykou => buttonBodnutiDykou,
                        Druh.Strelba_Lukem => buttonStrelbaLukem,
                        Druh.Magicky_sip => buttonMagickySip,
                        Druh.Uskok => buttonUskok,
                        Druh.Rychlost => buttonRychlost,
                        Druh.Lesni_bobule => buttonLesniBobule,
                        Druh.Jedova_sipka => buttonJedovaSipka,
                        Druh.Uder_Holi => buttonUderHoli,
                        Druh.Ohniva_Koule => buttonOhnivaKoule,
                        Druh.Ledove_Kopi => buttonLedoveKopi,
                        Druh.Magicky_Stit => buttonMagickyStit,
                        Druh.Vysati_zivota => buttonVysatiZivota,
                        Druh.Vysati_many => buttonVysatiMany,
                        Druh.Magicke_soustredeni => buttonMagickeSoustredeni,
                        Druh.Utek => buttonUtek,
                        Druh.Lahvicka_Many => buttonLahvickaMany,
                        Druh.Lahvicka_Zdravi => buttonLahvickaZdravi,
                        _ => new Button(SpravceMedii.FontText, new Vector2(), SpravceMedii.PrazdnaTexturaBila, 160, 20),
                    };
                    button.Visible = true;
                    PomocneMetody.NastavButtonSchopnosti(efekty, button, schop, naRade, 325, y + i * 30, 160, 20, SpravceMedii.FontText, SpravceMedii.PrazdnaTexturaBila, Color.White, Color.Black);
                    i++;
                }
            }

            utocnikPortret = SpravceMedii.DejPortret(souboj.Utocnik.Pohlavi, souboj.Utocnik.Trida);
            obrancePortret = SpravceMedii.DejPortret(souboj.Obrance.Pohlavi, souboj.Obrance.Trida);
            spriteBatch.Begin();
            //sprites
            spriteBatch.Draw(SpravceMedii.Plocha, new Vector2(200, 0), Color.White);
            if (oznaceniUtocnik)
            {
                spriteBatch.Draw(SpravceMedii.Panel, new Vector2(0, 0), Color.White);
                spriteBatch.Draw(SpravceMedii.Panel, new Vector2(600, 0), Color.LightGray);
                spriteBatch.Draw(utocnikPortret, new Rectangle(5, 37, 190, 190), Color.White);
                spriteBatch.Draw(obrancePortret, new Rectangle(605, 37, 190, 190), Color.LightGray);
            }
            else
            {
                spriteBatch.Draw(SpravceMedii.Panel, new Vector2(0, 0), Color.LightGray);
                spriteBatch.Draw(SpravceMedii.Panel, new Vector2(600, 0), Color.White);
                spriteBatch.Draw(utocnikPortret, new Rectangle(5, 37, 190, 190), Color.LightGray);
                spriteBatch.Draw(obrancePortret, new Rectangle(605, 37, 190, 190), Color.White);
            }
            //text
            //portret
            velikostTextuUtocnik = SpravceMedii.FontNadpis.MeasureString(souboj.Utocnik.Name);
            spriteBatch.DrawString(SpravceMedii.FontNadpis, souboj.Utocnik.Name, new Vector2(100 - velikostTextuUtocnik.X / 2, 20 - velikostTextuUtocnik.Y / 2), Color.Black);
            velikostTextuObrance = SpravceMedii.FontNadpis.MeasureString(souboj.Obrance.Name);
            spriteBatch.DrawString(SpravceMedii.FontNadpis, souboj.Obrance.Name, new Vector2(700 - velikostTextuObrance.X / 2, 20 - velikostTextuObrance.Y / 2), Color.Black);
            //trida
            velikostTextuUtocnik = SpravceMedii.FontNadpis.MeasureString(PomocneMetody.TridaToString(souboj.Utocnik.Trida, souboj.Utocnik.Pohlavi));
            spriteBatch.DrawString(SpravceMedii.FontNadpis, PomocneMetody.TridaToString(souboj.Utocnik.Trida, souboj.Utocnik.Pohlavi), new Vector2(100 - velikostTextuUtocnik.X / 2, 245 - velikostTextuUtocnik.Y / 2), Color.Black);
            velikostTextuObrance = SpravceMedii.FontNadpis.MeasureString(PomocneMetody.TridaToString(souboj.Obrance.Trida, souboj.Obrance.Pohlavi));
            spriteBatch.DrawString(SpravceMedii.FontNadpis, PomocneMetody.TridaToString(souboj.Obrance.Trida, souboj.Obrance.Pohlavi), new Vector2(700 - velikostTextuObrance.X / 2, 245 - velikostTextuObrance.Y / 2), Color.Black);
            //Pohlavi                    
            velikostTextuUtocnik = SpravceMedii.FontNadpis.MeasureString(PomocneMetody.PohlaviToString(souboj.Utocnik.Pohlavi));
            spriteBatch.DrawString(SpravceMedii.FontNadpis, PomocneMetody.PohlaviToString(souboj.Utocnik.Pohlavi), new Vector2(100 - velikostTextuUtocnik.X / 2, 280 - velikostTextuUtocnik.Y / 2), Color.Black);
            velikostTextuObrance = SpravceMedii.FontNadpis.MeasureString(PomocneMetody.PohlaviToString(souboj.Obrance.Pohlavi));
            spriteBatch.DrawString(SpravceMedii.FontNadpis, PomocneMetody.PohlaviToString(souboj.Obrance.Pohlavi), new Vector2(700 - velikostTextuObrance.X / 2, 280 - velikostTextuObrance.Y / 2), Color.Black);
            //Level
            spriteBatch.DrawString(SpravceMedii.FontText, "Level: " + souboj.Utocnik.Level, new Vector2(20, 300), Color.Black);
            spriteBatch.DrawString(SpravceMedii.FontText, "Level: " + souboj.Obrance.Level, new Vector2(620, 300), Color.Black);
            //Zkusenosti
            if(!Duel)
            spriteBatch.DrawString(SpravceMedii.FontText, "Zkušenosti: " + souboj.Utocnik.Zkusenosti + "/" + souboj.Utocnik.ZkusenostiNext, new Vector2(20, 320), Color.Black);
            //spriteBatch.DrawString(SpravceMedii.FontText, "Zkušenosti: " + souboj.Obrance.Zkusenosti + "/" + souboj.Obrance.ZkusenostiNext, new Vector2(620, 320), Color.Black);

            //Zivoty
            spriteBatch.DrawString(SpravceMedii.FontText, "Životy: " + souboj.Utocnik.Zivoty + "/" + souboj.Utocnik.ZivotyMax, new Vector2(20, 340), Color.Black);
            spriteBatch.DrawString(SpravceMedii.FontText, "Životy: " + souboj.Obrance.Zivoty + "/" + souboj.Obrance.ZivotyMax, new Vector2(620, 340), Color.Black);
            //Mana
            spriteBatch.DrawString(SpravceMedii.FontText, "Mana: " + souboj.Utocnik.Mana + "/" + souboj.Utocnik.ManaMax, new Vector2(20, 360), Color.Black);
            spriteBatch.DrawString(SpravceMedii.FontText, "Mana: " + souboj.Obrance.Mana + "/" + souboj.Obrance.ManaMax, new Vector2(620, 360), Color.Black);
            //Sila
            if (souboj.EfektyUtocnika.Mraz > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Síla: " + souboj.Utocnik.Sila, new Vector2(20, 390), Color.Red);
            else spriteBatch.DrawString(SpravceMedii.FontText, "Síla: " + souboj.Utocnik.Sila, new Vector2(20, 390), Color.Black);
            if (souboj.EfektyObrance.Mraz > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Síla: " + souboj.Obrance.Sila, new Vector2(620, 390), Color.Red);
            else spriteBatch.DrawString(SpravceMedii.FontText, "Síla: " + souboj.Obrance.Sila, new Vector2(620, 390), Color.Black);
            //Obratnost
            if (souboj.EfektyUtocnika.Mraz > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Obratnost: " + souboj.Utocnik.Obratnost, new Vector2(20, 410), Color.Red);
            else spriteBatch.DrawString(SpravceMedii.FontText, "Obratnost: " + souboj.Utocnik.Obratnost, new Vector2(20, 410), Color.Black);
            if (souboj.EfektyObrance.Mraz > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Obratnost: " + souboj.Obrance.Obratnost, new Vector2(620, 410), Color.Red);
            else spriteBatch.DrawString(SpravceMedii.FontText, "Obratnost: " + souboj.Obrance.Obratnost, new Vector2(620, 410), Color.Black);
            //Inteligence
            if (souboj.EfektyUtocnika.Mraz > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Inteligence: " + souboj.Utocnik.Inteligence, new Vector2(20, 430), Color.Red);
            else spriteBatch.DrawString(SpravceMedii.FontText, "Inteligence: " + souboj.Utocnik.Inteligence, new Vector2(20, 430), Color.Black);
            if (souboj.EfektyObrance.Mraz > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Inteligence: " + souboj.Obrance.Inteligence, new Vector2(620, 430), Color.Red);
            else spriteBatch.DrawString(SpravceMedii.FontText, "Inteligence: " + souboj.Obrance.Inteligence, new Vector2(620, 430), Color.Black);
            //Brneni
            spriteBatch.DrawString(SpravceMedii.FontText, "Brnění: " + souboj.Utocnik.Brneni, new Vector2(20, 450), Color.Black);
            spriteBatch.DrawString(SpravceMedii.FontText, "Brnění: " + souboj.Obrance.Brneni, new Vector2(620, 450), Color.Black);
            //Efekty
            //Pokřik, Rychlost, Magické soustředění
            if (souboj.EfektyUtocnika.Pokrik > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Pokřik [" + souboj.EfektyUtocnika.Pokrik + "]", new Vector2(20, 480), Color.Red);
            if (souboj.EfektyObrance.Pokrik > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Pokřik [" + souboj.EfektyObrance.Pokrik + "]", new Vector2(620, 480), Color.Red);
            if (souboj.EfektyUtocnika.Rychlost > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Rychlost [" + souboj.EfektyUtocnika.Rychlost + "]", new Vector2(20, 480), Color.Red);
            if (souboj.EfektyObrance.Rychlost > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Rychlost [" + souboj.EfektyObrance.Rychlost + "]", new Vector2(620, 480), Color.Red);
            if (souboj.EfektyUtocnika.Soustredeni > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Soustředění [" + souboj.EfektyUtocnika.Soustredeni + "]", new Vector2(20, 480), Color.Red);
            if (souboj.EfektyObrance.Soustredeni > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Soustředění [" + souboj.EfektyObrance.Soustredeni + "]", new Vector2(620, 480), Color.Red);
            //Hoří, krvácí, jed
            if (souboj.EfektyUtocnika.Horeni > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Hoří [" + souboj.EfektyUtocnika.Horeni + "]", new Vector2(20, 500), Color.Red);
            if (souboj.EfektyObrance.Horeni > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Hoří [" + souboj.EfektyObrance.Horeni + "]", new Vector2(620, 500), Color.Red);
            if (souboj.EfektyUtocnika.Krvaceni > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Krvácí [" + souboj.EfektyUtocnika.Krvaceni + "]", new Vector2(20, 500), Color.Red);
            if (souboj.EfektyObrance.Krvaceni > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Krvácí [" + souboj.EfektyObrance.Krvaceni + "]", new Vector2(620, 500), Color.Red);
            if (souboj.EfektyUtocnika.Jed > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Jed [" + souboj.EfektyUtocnika.Jed + "]", new Vector2(20, 500), Color.Red);
            if (souboj.EfektyObrance.Jed > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Jed [" + souboj.EfektyObrance.Jed + "]", new Vector2(620, 500), Color.Red);
            //Mrzne, omráčený
            if (souboj.EfektyUtocnika.Mraz > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Mrzne [" + souboj.EfektyUtocnika.Mraz + "]", new Vector2(20, 520), Color.Red);
            if (souboj.EfektyObrance.Mraz > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Mrzne [" + souboj.EfektyObrance.Mraz + "]", new Vector2(620, 520), Color.Red);
            if (souboj.EfektyUtocnika.Omraceni > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Omráčený [" + souboj.EfektyUtocnika.Omraceni + "]", new Vector2(20, 520), Color.Red);
            if (souboj.EfektyObrance.Omraceni > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Omráčený [" + souboj.EfektyObrance.Omraceni + "]", new Vector2(620, 520), Color.Red);
            //MessageBox
            spriteBatch.Draw(SpravceMedii.PrazdnaTexturaCerna, new Rectangle(0, 540, 800, 60), Color.Black);
            velikostTextuUtocnik = SpravceMedii.FontNadpis.MeasureString(messagePrvni);
            spriteBatch.DrawString(SpravceMedii.FontNadpis, messagePrvni, new Vector2(400 - velikostTextuUtocnik.X / 2, 555 - velikostTextuUtocnik.Y / 2), Color.White);
            velikostTextuObrance = SpravceMedii.FontNadpis.MeasureString(messageDruhy);
            spriteBatch.DrawString(SpravceMedii.FontNadpis, messageDruhy, new Vector2(400 - velikostTextuObrance.X / 2, 585 - velikostTextuObrance.Y / 2), Color.White);
            velikostTextuUtocnik = SpravceMedii.FontNadpis.MeasureString(messageKolo);
            spriteBatch.DrawString(SpravceMedii.FontNadpis, messageKolo, new Vector2(400 - velikostTextuUtocnik.X / 2, 100 - velikostTextuUtocnik.Y / 2), Color.White);
            //Buttons
            if (faze == Faze.VyberPrvni || faze == Faze.VyberDruhy)
            {
                foreach (Button butt in buttons)
                {
                    butt.Draw(gameTime, spriteBatch);
                }
            }
            spriteBatch.End();
        }

        private void ButtonUtokMecemClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaPrvni = prvni.DejSchopnost(Druh.Utok_Mecem);
                    break;
                case Faze.VyberDruhy:
                    vybranaDruhy = druhy.DejSchopnost(Druh.Utok_Mecem);
                    break;
            }
        }
        private void ButtonObranaStitemClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaPrvni = prvni.DejSchopnost(Druh.Obrana_Stitem);
                    break;
                case Faze.VyberDruhy:
                    vybranaDruhy = druhy.DejSchopnost(Druh.Obrana_Stitem);
                    break;
            }
        }
        private void ButtonRegeneraceClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaPrvni = prvni.DejSchopnost(Druh.Regenerace);
                    break;
                case Faze.VyberDruhy:
                    vybranaDruhy = druhy.DejSchopnost(Druh.Regenerace);
                    break;
            }
        }
        private void ButtonBojovyPokrikClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaPrvni = prvni.DejSchopnost(Druh.Bojovy_Pokrik);
                    break;
                case Faze.VyberDruhy:
                    vybranaDruhy = druhy.DejSchopnost(Druh.Bojovy_Pokrik);
                    break;
            }
        }

        private void ButtonUderStitemClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaPrvni = prvni.DejSchopnost(Druh.Uder_stitem);
                    break;
                case Faze.VyberDruhy:
                    vybranaDruhy = druhy.DejSchopnost(Druh.Uder_stitem);
                    break;
            }
        }

        private void ButtonVrhSekerouClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaPrvni = prvni.DejSchopnost(Druh.Vrh_sekerou);
                    break;
                case Faze.VyberDruhy:
                    vybranaDruhy = druhy.DejSchopnost(Druh.Vrh_sekerou);
                    break;
            }
        }

        private void ButtonBerserkClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaPrvni = prvni.DejSchopnost(Druh.Berserk);
                    break;
                case Faze.VyberDruhy:
                    vybranaDruhy = druhy.DejSchopnost(Druh.Berserk);
                    break;
            }
        }

        private void ButtonBodnutiDykouClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaPrvni = prvni.DejSchopnost(Druh.Bodnuti_Dykou);
                    break;
                case Faze.VyberDruhy:
                    vybranaDruhy = druhy.DejSchopnost(Druh.Bodnuti_Dykou);
                    break;
            }
        }

        private void ButtonStrelbaLukemClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaPrvni = prvni.DejSchopnost(Druh.Strelba_Lukem);
                    break;
                case Faze.VyberDruhy:
                    vybranaDruhy = druhy.DejSchopnost(Druh.Strelba_Lukem);
                    break;
            }
        }

        private void ButtonMagickySipClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaPrvni = prvni.DejSchopnost(Druh.Magicky_sip);
                    break;
                case Faze.VyberDruhy:
                    vybranaDruhy = druhy.DejSchopnost(Druh.Magicky_sip);
                    break;
            }
        }

        private void ButtonUskokClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaPrvni = prvni.DejSchopnost(Druh.Uskok);
                    break;
                case Faze.VyberDruhy:
                    vybranaDruhy = druhy.DejSchopnost(Druh.Uskok);
                    break;
            }
        }
        private void ButtonRychlostClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaPrvni = prvni.DejSchopnost(Druh.Rychlost);
                    break;
                case Faze.VyberDruhy:
                    vybranaDruhy = druhy.DejSchopnost(Druh.Rychlost);
                    break;
            }
        }
        private void ButtonLesniBobuleClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaPrvni = prvni.DejSchopnost(Druh.Lesni_bobule);
                    break;
                case Faze.VyberDruhy:
                    vybranaDruhy = druhy.DejSchopnost(Druh.Lesni_bobule);
                    break;
            }
        }

        private void ButtonJedovaSipkaClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaPrvni = prvni.DejSchopnost(Druh.Jedova_sipka);
                    break;
                case Faze.VyberDruhy:
                    vybranaDruhy = druhy.DejSchopnost(Druh.Jedova_sipka);
                    break;
            }
        }
        private void ButtonUderHoliClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaPrvni = prvni.DejSchopnost(Druh.Uder_Holi);
                    break;
                case Faze.VyberDruhy:
                    vybranaDruhy = druhy.DejSchopnost(Druh.Uder_Holi);
                    break;
            }
        }
        private void ButtonOhnivaKouleClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaPrvni = prvni.DejSchopnost(Druh.Ohniva_Koule);
                    break;
                case Faze.VyberDruhy:
                    vybranaDruhy = druhy.DejSchopnost(Druh.Ohniva_Koule);
                    break;
            }
        }
        private void ButtonLedoveKopiClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaPrvni = prvni.DejSchopnost(Druh.Ledove_Kopi);
                    break;
                case Faze.VyberDruhy:
                    vybranaDruhy = druhy.DejSchopnost(Druh.Ledove_Kopi);
                    break;
            }
        }
        private void ButtonVysatiZivotaClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaPrvni = prvni.DejSchopnost(Druh.Vysati_zivota);
                    break;
                case Faze.VyberDruhy:
                    vybranaDruhy = druhy.DejSchopnost(Druh.Vysati_zivota);
                    break;
            }
        }

        private void ButtonMagickeSoustredeniClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaPrvni = prvni.DejSchopnost(Druh.Magicke_soustredeni);
                    break;
                case Faze.VyberDruhy:
                    vybranaDruhy = druhy.DejSchopnost(Druh.Magicke_soustredeni);
                    break;
            }
        }

        private void ButtonVysatiManyClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaPrvni = prvni.DejSchopnost(Druh.Vysati_many);
                    break;
                case Faze.VyberDruhy:
                    vybranaDruhy = druhy.DejSchopnost(Druh.Vysati_many);
                    break;
            }
        }

        private void ButtonMagickyStitClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaPrvni = prvni.DejSchopnost(Druh.Magicky_Stit);
                    break;
                case Faze.VyberDruhy:
                    vybranaDruhy = druhy.DejSchopnost(Druh.Magicky_Stit);
                    break;
            }
        }

        private void ButtonUtekClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaPrvni = prvni.DejSchopnost(Druh.Utek);
                    break;
                case Faze.VyberDruhy:
                    vybranaDruhy = druhy.DejSchopnost(Druh.Utek);
                    break;
            }
        }

        private void ButtonLahvickaZdraviClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaPrvni = prvni.DejSchopnost(Druh.Lahvicka_Zdravi);
                    break;
                case Faze.VyberDruhy:
                    vybranaDruhy = druhy.DejSchopnost(Druh.Lahvicka_Zdravi);
                    break;
            }
        }

        private void ButtonLahvickaManyClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaPrvni = prvni.DejSchopnost(Druh.Lahvicka_Many);
                    break;
                case Faze.VyberDruhy:
                    vybranaDruhy = druhy.DejSchopnost(Druh.Lahvicka_Many);
                    break;
            }
        }
    }
}
