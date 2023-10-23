using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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
        private bool oznaceniUtocnik = true;
        //fonty
        private Vector2 delkaU;
        private Vector2 delkaO;
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
                            oznaceniUtocnik = false;
                            messageKolo = souboj.PocetKol + ". kolo, " + souboj.Obrance.Name + " zvítěžil";
                            messagePrvni = souboj.Utocnik.Name + " byl poražen";
                            messageDruhy = souboj.Obrance.Name + " zvítězil !";
                            faze = Faze.VyhraObrance;
                            ZacarovanyLes.delay = 3;
                            ZacarovanyLes.delayed = true;
                        }
                        else
                        {
                            oznaceniUtocnik = true;
                            messageKolo = souboj.PocetKol + ". kolo, " + souboj.Utocnik.Name + " zvítěžil";
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
                                    ZacarovanyLes.utocnik.PridejZkusenosti(75);
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
                        oznaceniUtocnik = hrajici == souboj.Utocnik ? true : false;
                        messageKolo = souboj.PocetKol + ". kolo, efekty postavy " + hrajici.Name;
                        Efekty efektyHrajici = hrajici == souboj.Utocnik ? souboj.EfektyUtocnika : souboj.EfektyObrance;
                        bool efekt = false;
                        messagePrvni = "";
                        messageDruhy = "";
                        if (hrajici == souboj.Obrance)
                        {
                            if (souboj.EfektyObrance.Horeni > 0)
                            {
                                int poskozeni = souboj.ZautocNaObrance((int)Math.Round(souper.Inteligence / 3.0), true);
                                messagePrvni = hrajici.Name + " hoří za " + poskozeni + " poškození";
                                SpravceMedii.Fireball.Play();
                                efekt = true;
                            }
                            if (souboj.EfektyObrance.Krvaceni > 0)
                            {
                                int poskozeni = souboj.ZautocNaObrance((int)Math.Round(souper.Sila / 3.0), false);
                                messagePrvni = hrajici.Name + " krvácí za " + poskozeni + " poškození";
                                efekt = true;
                            }
                            if (souboj.EfektyObrance.Jed > 0)
                            {
                                int poskozeni = souboj.ZautocNaObrance((int)Math.Round(souper.Obratnost / 3.0), false);
                                messagePrvni = hrajici.Name + " je otrávený za " + poskozeni + " poškození";
                                efekt = true;
                            }
                        }
                        else
                        {
                            if (souboj.EfektyUtocnika.Horeni > 0)
                            {
                                int poskozeni = souboj.ZautocNaUtocnika((int)Math.Round(souper.Inteligence / 3.0), true);
                                messagePrvni = hrajici.Name + " hoří za " + poskozeni + " poškození";
                                SpravceMedii.Fireball.Play();
                                efekt = true;
                            }
                            if (souboj.EfektyUtocnika.Krvaceni > 0)
                            {
                                int poskozeni = souboj.ZautocNaUtocnika((int)Math.Round(souper.Sila / 3.0), false);
                                messagePrvni = hrajici.Name + " krvácí za " + poskozeni + " poškození";
                                efekt = true;
                            }
                            if (souboj.EfektyUtocnika.Jed > 0)
                            {
                                int poskozeni = souboj.ZautocNaUtocnika((int)Math.Round(souper.Obratnost / 3.0), false);
                                messagePrvni = hrajici.Name + " je otrávený za " + poskozeni + " poškození";
                                efekt = true;
                            }
                        }
                        faze = Faze.EfektyDruhy;
                        if (efekt)
                        {
                            ZacarovanyLes.delay = 3;
                            ZacarovanyLes.delayed = true;
                        }
                    }
                    break;
                case Faze.EfektyDruhy:
                    if (!ZacarovanyLes.delayed)
                    {
                        oznaceniUtocnik = souper == souboj.Utocnik ? true : false;
                        messageKolo = souboj.PocetKol + ". kolo, efekty postavy " + souper.Name;
                        Efekty efektySouper = souper == souboj.Utocnik ? souboj.EfektyUtocnika : souboj.EfektyObrance;
                        bool efekt = false;
                        if (souper == souboj.Obrance)
                        {
                            if (souboj.EfektyObrance.Horeni > 0)
                            {
                                int poskozeni = souboj.ZautocNaObrance((int)Math.Round(hrajici.Inteligence / 3.0), true);
                                messageDruhy = souper.Name + " hoří za " + poskozeni + " poškození";
                                SpravceMedii.Fireball.Play();
                                efekt = true;
                            }
                            if (souboj.EfektyObrance.Krvaceni > 0)
                            {
                                int poskozeni = souboj.ZautocNaObrance((int)Math.Round(hrajici.Sila / 3.0), false);
                                messageDruhy = souper.Name + " krvácí za " + poskozeni + " poškození";
                                efekt = true;
                            }
                            if (souboj.EfektyObrance.Jed > 0)
                            {
                                int poskozeni = souboj.ZautocNaObrance((int)Math.Round(hrajici.Obratnost / 3.0), false);
                                messageDruhy = souper.Name + " je otrávený za " + poskozeni + " poškození";
                                efekt = true;
                            }
                        }
                        else
                        {
                            if (souboj.EfektyUtocnika.Horeni > 0)
                            {
                                int poskozeni = souboj.ZautocNaUtocnika((int)Math.Round(hrajici.Inteligence / 3.0), true);
                                messageDruhy = souper.Name + " hoří za " + poskozeni + " poškození";
                                SpravceMedii.Fireball.Play();
                                efekt = true;
                            }
                            if (souboj.EfektyUtocnika.Krvaceni > 0)
                            {
                                int poskozeni = souboj.ZautocNaUtocnika((int)Math.Round(hrajici.Sila / 3.0), false);
                                messageDruhy = souper.Name + " krvácí za " + poskozeni + " poškození";
                                efekt = true;
                            }
                            if (souboj.EfektyUtocnika.Jed > 0)
                            {
                                int poskozeni = souboj.ZautocNaUtocnika((int)Math.Round(hrajici.Obratnost / 3.0), false);
                                messageDruhy = souper.Name + " je otrávený za " + poskozeni + " poškození";
                                efekt = true;
                            }
                        }

                        souboj.ZhodnotEfekty();
                        souboj.ZhodnotSchopnosti();
                        faze = Faze.Kolo;
                        if (efekt)
                        {
                            ZacarovanyLes.delay = 3;
                            ZacarovanyLes.delayed = true;
                        }
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
                        oznaceniUtocnik = hrajici == souboj.Utocnik ? true : false;
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
                                vybranaHrajici = VyberSchopnostAI(hrajici, vybranaSouper, souper);
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
                    messageKolo = souboj.PocetKol + ". kolo, nyní hraje " + souper.Name;
                    oznaceniUtocnik = souper == souboj.Utocnik ? true : false;
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
                    oznaceniUtocnik = hrajici == souboj.Utocnik ? true : false;
                    messageKolo = souboj.PocetKol + ". kolo, " + hrajici.Name + " používá schopnost";
                    if (!ZacarovanyLes.delayed)
                    {
                        Efekty efektyHrajici = hrajici == souboj.Utocnik ? souboj.EfektyUtocnika : souboj.EfektyObrance;
                        if (efektyHrajici.Omraceni == 0)
                        {
                            Efekty efektySouper = souper == souboj.Utocnik ? souboj.EfektyUtocnika : souboj.EfektyObrance;

                            bool rychlost = ((efektySouper.Rychlost > 0) && (souboj.Kostka.Next(10) < (int)Math.Round(efektySouper.Rychlost * 6.0 / 4.0))) ? true : false;
                            int poskozeni = hrajici.PouzijSchopnost(vybranaHrajici.Druh);
                            int mana = 0;

                            if (((vybranaSouper.Druh != Druh.Magicky_sip && vybranaSouper.Faze != 0) || vybranaSouper.Druh != Druh.Ohniva_Koule || vybranaSouper.Druh != Druh.Ledove_Kopi || vybranaSouper.Druh != Druh.Vysati_zivota || vybranaSouper.Druh != Druh.Vysati_many)
                                && (vybranaHrajici.Druh == Druh.Magicky_Stit || vybranaHrajici.Druh == Druh.Obrana_Stitem || vybranaHrajici.Druh == Druh.Uskok))
                            {
                                messagePrvni = hrajici.Name + " se brání schopností " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh);
                            }
                            else if (((vybranaSouper.Druh == Druh.Magicky_sip && vybranaSouper.Faze == 0) || vybranaSouper.Druh == Druh.Ohniva_Koule || vybranaSouper.Druh == Druh.Ledove_Kopi || vybranaSouper.Druh == Druh.Vysati_zivota || vybranaSouper.Druh == Druh.Vysati_many)
                               && (vybranaHrajici.Druh == Druh.Magicky_Stit || vybranaHrajici.Druh == Druh.Obrana_Stitem || vybranaHrajici.Druh == Druh.Uskok))
                            {
                                messagePrvni = hrajici.Name + " se částečně brání schopností " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh);
                            }
                            else if (((vybranaSouper.Druh == Druh.Magicky_Stit || vybranaSouper.Druh == Druh.Obrana_Stitem || vybranaSouper.Druh == Druh.Uskok) || rychlost)
                                && ((vybranaHrajici.Druh == Druh.Magicky_sip && vybranaHrajici.Faze == 0) || vybranaHrajici.Druh == Druh.Ohniva_Koule || vybranaHrajici.Druh == Druh.Ledove_Kopi || vybranaHrajici.Druh == Druh.Vysati_zivota || vybranaHrajici.Druh == Druh.Vysati_many))
                            {
                                switch (vybranaHrajici.Druh)
                                {
                                    case Druh.Ohniva_Koule:
                                        SpravceMedii.Fireball.Play();
                                        break;
                                    case Druh.Ledove_Kopi:
                                        SpravceMedii.Frostbolt.Play();
                                        break;
                                    case Druh.Vysati_zivota:
                                        SpravceMedii.Fireball.Play();
                                        break;
                                    case Druh.Vysati_many:
                                        SpravceMedii.Frostbolt.Play();
                                        break;
                                    default:
                                        SpravceMedii.Hit.Play();
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
                                if (vybranaHrajici.Druh == Druh.Vysati_zivota)
                                {
                                    messagePrvni = hrajici.Name + " částečně vysál pomocí schopnosti " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh) + " " + skPosk + " zdraví ";
                                    if (efektyHrajici.Horeni > 0 || efektyHrajici.Jed > 0 || efektyHrajici.Krvaceni > 0)
                                    {
                                        skPosk = (int)Math.Round(skPosk * 2.0 / 3.0);
                                    }
                                    hrajici.PridejNeboUberZdravi(skPosk);
                                }
                                else if (vybranaHrajici.Druh == Druh.Vysati_many)
                                {
                                    mana = souper.Mana - Math.Max(souper.Mana - skPosk * 2, 0);
                                    messagePrvni = hrajici.Name + " částečně ubral pomocí schopnosti " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh) + " " + skPosk + " zdraví a vysál " + mana + " many ";
                                }
                                else
                                    messagePrvni = hrajici.Name + " částečně útočí schopností " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh) + " za " + skPosk + " poškození";
                            }
                            else if (vybranaHrajici.Druh == Druh.Regenerace)
                            {
                                SpravceMedii.Regen.Play();
                                if (efektyHrajici.Pokrik > 0)
                                {
                                    poskozeni = poskozeni * 2;
                                }
                                if (efektyHrajici.Horeni > 0 || efektyHrajici.Jed > 0 || efektyHrajici.Krvaceni > 0)
                                {
                                    poskozeni = (int)Math.Round(poskozeni * 2.0 / 3.0);
                                }
                                hrajici.PridejNeboUberZdravi(poskozeni);
                                messagePrvni = hrajici.Name + " SpravceMedii.Regeneruje zdraví za " + poskozeni + " bodů pomocí schopnosti " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh);
                            }
                            else if (vybranaHrajici.Druh == Druh.Lahvicka_Zdravi)
                            {
                                SpravceMedii.Regen.Play();
                                hrajici.Inventar.LahvickyZdravi--;
                                if (efektyHrajici.Horeni > 0 || efektyHrajici.Jed > 0 || efektyHrajici.Krvaceni > 0)
                                {
                                    poskozeni = (int)Math.Round(poskozeni * 2.0 / 3.0);
                                }
                                hrajici.PridejNeboUberZdravi(poskozeni);
                                messagePrvni = hrajici.Name + " SpravceMedii.Regeneruje zdravi za " + poskozeni + " bodů pomocí " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh);
                            }
                            else if (vybranaHrajici.Druh == Druh.Lesni_bobule)
                            {
                                SpravceMedii.Regen.Play();
                                if (efektyHrajici.Horeni > 0 || efektyHrajici.Jed > 0 || efektyHrajici.Krvaceni > 0)
                                {
                                    poskozeni = (int)Math.Round(poskozeni * 2.0 / 3.0);
                                }
                                hrajici.PridejNeboUberZdravi(poskozeni);
                                messagePrvni = hrajici.Name + " SpravceMedii.Regeneruje zdravi za " + poskozeni + " bodů pomocí schopnosti " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh);
                            }
                            else if (vybranaHrajici.Druh == Druh.Lahvicka_Many)
                            {
                                SpravceMedii.Regen.Play();
                                if (efektyHrajici.Horeni > 0 || efektyHrajici.Jed > 0 || efektyHrajici.Krvaceni > 0)
                                {
                                    poskozeni = (int)Math.Round(poskozeni * 2.0 / 3.0);
                                }
                                hrajici.PridejNeboUberManu(poskozeni);
                                hrajici.Inventar.LahvickyMany--;
                                messagePrvni = hrajici.Name + " SpravceMedii.Regeneruje manu za " + poskozeni + " bodů pomocí " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh);
                            }
                            else if (vybranaHrajici.Druh == Druh.Bojovy_Pokrik)
                            {
                                SpravceMedii.Battlecry.Play();
                                efektyHrajici.Pokrik += 2;
                                messagePrvni = hrajici.Name + " zařval " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh);
                            }

                            else if (vybranaHrajici.Druh == Druh.Magicke_soustredeni)
                            {
                                SpravceMedii.Battlecry.Play();
                                efektyHrajici.Soustredeni += 4;
                                messagePrvni = hrajici.Name + " se soustředí schopnosti " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh);
                            }
                            else if (vybranaHrajici.Druh == Druh.Rychlost)
                            {
                                SpravceMedii.Battlecry.Play();
                                efektyHrajici.Rychlost += 4;
                                messagePrvni = hrajici.Name + " použil schopnost " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh);
                            }
                            else if (vybranaHrajici.Druh == Druh.Strelba_Lukem && vybranaHrajici.Faze == 1 || vybranaHrajici.Druh == Druh.Magicky_sip && vybranaHrajici.Faze == 1)
                            {
                                messagePrvni = hrajici.Name + " natahuje luk, aby použil schopnost " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh);
                                SpravceMedii.BowPull.Play();
                            }
                            else if ((vybranaSouper.Druh == Druh.Magicky_Stit || vybranaSouper.Druh == Druh.Obrana_Stitem || vybranaSouper.Druh == Druh.Uskok) &&
                                (vybranaHrajici.Druh == Druh.Strelba_Lukem))
                            {
                                messagePrvni = hrajici.Name + " se pomocí schopnosti " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh) + " netrefil";
                                SpravceMedii.BowMiss.Play();
                            }
                            else if (rychlost || vybranaSouper.Druh == Druh.Magicky_Stit || vybranaSouper.Druh == Druh.Obrana_Stitem || vybranaSouper.Druh == Druh.Uskok)
                            {
                                messagePrvni = hrajici.Name + " se pomocí schopnosti " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh) + " netrefil";
                            }
                            else
                            {

                                if (vybranaHrajici.Druh == Druh.Ohniva_Koule)
                                {
                                    SpravceMedii.Fireball.Play();
                                    efektySouper.Horeni += 2;
                                }
                                else if (vybranaHrajici.Druh == Druh.Ledove_Kopi)
                                {
                                    SpravceMedii.Frostbolt.Play();
                                    efektySouper.Mraz += 2;
                                    efektySouper.AktivujMraz();
                                }
                                else if (vybranaHrajici.Druh == Druh.Vrh_sekerou)
                                {
                                    SpravceMedii.Hit.Play();
                                    efektySouper.Krvaceni += 2;
                                    if (efektyHrajici.Pokrik > 0)
                                        efektySouper.Krvaceni += 1;
                                }
                                else if (vybranaHrajici.Druh == Druh.Uder_stitem)
                                {
                                    SpravceMedii.Hit.Play();
                                    efektySouper.Omraceni += 1;
                                    if (efektyHrajici.Pokrik > 0)
                                        efektySouper.Omraceni += 1;
                                }
                                else if (vybranaHrajici.Druh == Druh.Jedova_sipka)
                                {
                                    SpravceMedii.Hit.Play();
                                    efektySouper.Jed += 3;
                                }
                                else if (vybranaHrajici.Druh == Druh.Berserk)
                                {
                                    SpravceMedii.Battlecry.Play();
                                    SpravceMedii.Hit.Play();
                                }
                                else
                                {
                                    SpravceMedii.Hit.Play();
                                }



                                if (efektyHrajici.Pokrik > 0)
                                {
                                    poskozeni = poskozeni * 2;
                                }
                                int skPosk;
                                if (hrajici == souboj.Utocnik)
                                {
                                    skPosk = souboj.ZautocNaObrance(poskozeni, vybranaHrajici.Magicka);
                                }
                                else
                                {
                                    skPosk = souboj.ZautocNaUtocnika(poskozeni, vybranaHrajici.Magicka);
                                }
                                if (vybranaHrajici.Druh == Druh.Vysati_zivota)
                                {
                                    messagePrvni = hrajici.Name + " vysál pomocí schopnosti " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh) + " " + skPosk + " zdraví ";
                                    if (efektyHrajici.Horeni > 0 || efektyHrajici.Jed > 0 || efektyHrajici.Krvaceni > 0)
                                    {
                                        skPosk = (int)Math.Round(skPosk * 2.0 / 3.0);
                                    }
                                    hrajici.PridejNeboUberZdravi(skPosk);
                                }
                                else if (vybranaHrajici.Druh == Druh.Vysati_many)
                                {
                                    mana = souper.Mana - Math.Max(souper.Mana - skPosk * 2, 0);
                                    messagePrvni = hrajici.Name + " ubral pomocí schopnosti " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh) + " " + skPosk + " zdraví a vysál " + mana + " many ";
                                }
                                else
                                    messagePrvni = hrajici.Name + " útočí schopností " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh) + " za " + skPosk + " poškození";
                            }

                            if (vybranaHrajici.Faze == 0)
                            {
                                if (hrajici == souboj.Utocnik)
                                {
                                    if (souboj.EfektyUtocnika.Soustredeni > 0)
                                        hrajici.PridejNeboUberManu(-vybranaHrajici.CenaMany / 2);
                                    else
                                        hrajici.PridejNeboUberManu(-vybranaHrajici.CenaMany);
                                }
                                else
                                {
                                    if (souboj.EfektyObrance.Soustredeni > 0)
                                        hrajici.PridejNeboUberManu(-vybranaHrajici.CenaMany / 2);
                                    else
                                        hrajici.PridejNeboUberManu(-vybranaHrajici.CenaMany);
                                }
                                if (vybranaHrajici.Druh == Druh.Vysati_many)
                                {
                                    souper.PridejNeboUberManu(-mana);
                                    if (efektyHrajici.Horeni > 0 || efektyHrajici.Jed > 0 || efektyHrajici.Krvaceni > 0)
                                    {
                                        mana = (int)Math.Round(mana * 2.0 / 3.0);
                                    }
                                    hrajici.PridejNeboUberManu(mana);

                                }

                            }
                        }
                        else
                        {
                            vybranaHrajici.Faze = vybranaHrajici.FazeVychozi;
                            vybranaHrajici = new Schopnost(Druh.Zadna, 0, 0, 0, false);
                            messagePrvni = hrajici.Name + " je omráčený ";
                        }
                        ZacarovanyLes.delay = 3;
                        ZacarovanyLes.delayed = true;
                        faze = Faze.UtokDruhy;
                    }
                    break;
                case Faze.UtokDruhy:
                    oznaceniUtocnik = souper == souboj.Utocnik ? true : false;
                    messageKolo = souboj.PocetKol + ". kolo, " + souper.Name + " používá schopnost";
                    if (!ZacarovanyLes.delayed)
                    {
                        Efekty efektySouper = souper == souboj.Utocnik ? souboj.EfektyUtocnika : souboj.EfektyObrance;
                        if (efektySouper.Omraceni == 0)
                        {
                            Efekty efektyHrajici = hrajici == souboj.Utocnik ? souboj.EfektyUtocnika : souboj.EfektyObrance;
                            bool rychlost = ((efektyHrajici.Rychlost > 0) && (souboj.Kostka.Next(10) < (int)Math.Round(efektyHrajici.Rychlost * 6.0 / 4.0))) ? true : false;
                            int poskozeni = souper.PouzijSchopnost(vybranaSouper.Druh);
                            int mana = 0;

                            if (((vybranaHrajici.Druh != Druh.Magicky_sip && vybranaHrajici.Faze != 0) || vybranaHrajici.Druh != Druh.Ohniva_Koule || vybranaHrajici.Druh != Druh.Ledove_Kopi || vybranaHrajici.Druh != Druh.Vysati_zivota || vybranaHrajici.Druh != Druh.Vysati_many)
                                && (vybranaSouper.Druh == Druh.Magicky_Stit || vybranaSouper.Druh == Druh.Obrana_Stitem || vybranaSouper.Druh == Druh.Uskok))
                            {
                                messageDruhy = souper.Name + " se brání schopností " + PomocneMetody.SchopnostToString(vybranaSouper.Druh);
                            }
                            else if (((vybranaHrajici.Druh == Druh.Magicky_sip && vybranaHrajici.Faze == 0) || vybranaHrajici.Druh == Druh.Ohniva_Koule || vybranaHrajici.Druh == Druh.Ledove_Kopi || vybranaHrajici.Druh == Druh.Vysati_zivota || vybranaHrajici.Druh == Druh.Vysati_many)
                               && (vybranaSouper.Druh == Druh.Magicky_Stit || vybranaSouper.Druh == Druh.Obrana_Stitem || vybranaSouper.Druh == Druh.Uskok))
                            {
                                messageDruhy = souper.Name + " se částečně brání schopností " + PomocneMetody.SchopnostToString(vybranaSouper.Druh);
                            }
                            else if (((vybranaHrajici.Druh == Druh.Magicky_Stit || vybranaHrajici.Druh == Druh.Obrana_Stitem || vybranaHrajici.Druh == Druh.Uskok) || rychlost)
                                && ((vybranaSouper.Druh == Druh.Magicky_sip && vybranaSouper.Faze == 0) || vybranaSouper.Druh == Druh.Ohniva_Koule || vybranaSouper.Druh == Druh.Ledove_Kopi || vybranaSouper.Druh == Druh.Vysati_zivota || vybranaSouper.Druh == Druh.Vysati_many))
                            {
                                switch (vybranaSouper.Druh)
                                {
                                    case Druh.Ohniva_Koule:
                                        SpravceMedii.Fireball.Play();
                                        break;
                                    case Druh.Ledove_Kopi:
                                        SpravceMedii.Frostbolt.Play();
                                        break;
                                    case Druh.Vysati_zivota:
                                        SpravceMedii.Fireball.Play();
                                        break;
                                    case Druh.Vysati_many:
                                        SpravceMedii.Frostbolt.Play();
                                        break;
                                    default:
                                        SpravceMedii.Hit.Play();
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
                                if (vybranaSouper.Druh == Druh.Vysati_zivota)
                                {
                                    messageDruhy = souper.Name + " částečně vysál pomocí schopnosti " + PomocneMetody.SchopnostToString(vybranaSouper.Druh) + " " + skPosk + " zdraví ";
                                    if (efektySouper.Horeni > 0 || efektySouper.Jed > 0 || efektySouper.Krvaceni > 0)
                                    {
                                        skPosk = (int)Math.Round(skPosk * 2.0 / 3.0);
                                    }
                                    souper.PridejNeboUberZdravi(skPosk);
                                }
                                else if (vybranaSouper.Druh == Druh.Vysati_many)
                                {
                                    mana = hrajici.Mana - Math.Max(hrajici.Mana - skPosk * 2, 0);
                                    messageDruhy = souper.Name + " částečně ubral pomocí schopnosti " + PomocneMetody.SchopnostToString(vybranaSouper.Druh) + " " + skPosk + " zdraví a vysál " + mana + " many ";
                                }
                                else
                                    messageDruhy = souper.Name + " částečně útočí schopností " + PomocneMetody.SchopnostToString(vybranaSouper.Druh) + " za " + skPosk + " poškození";
                            }
                            else if (vybranaSouper.Druh == Druh.Regenerace)
                            {
                                SpravceMedii.Regen.Play();
                                if (efektySouper.Pokrik > 0)
                                {
                                    poskozeni = poskozeni * 2;
                                }
                                if (efektySouper.Horeni > 0 || efektySouper.Jed > 0 || efektySouper.Krvaceni > 0)
                                {
                                    poskozeni = (int)Math.Round(poskozeni * 2.0 / 3.0);
                                }
                                souper.PridejNeboUberZdravi(poskozeni);
                                messageDruhy = souper.Name + " SpravceMedii.Regeneruje zdraví za " + poskozeni + " bodů pomocí schopnosti " + PomocneMetody.SchopnostToString(vybranaSouper.Druh);
                            }
                            else if (vybranaSouper.Druh == Druh.Lahvicka_Zdravi)
                            {
                                SpravceMedii.Regen.Play();
                                souper.Inventar.LahvickyZdravi--;
                                if (efektySouper.Horeni > 0 || efektySouper.Jed > 0 || efektySouper.Krvaceni > 0)
                                {
                                    poskozeni = (int)Math.Round(poskozeni * 2.0 / 3.0);
                                }
                                souper.PridejNeboUberZdravi(poskozeni);
                                messageDruhy = souper.Name + " SpravceMedii.Regeneruje zdravi za " + poskozeni + " bodů pomocí " + PomocneMetody.SchopnostToString(vybranaSouper.Druh);
                            }
                            else if (vybranaSouper.Druh == Druh.Lesni_bobule)
                            {
                                SpravceMedii.Regen.Play();
                                if (efektySouper.Horeni > 0 || efektySouper.Jed > 0 || efektySouper.Krvaceni > 0)
                                {
                                    poskozeni = (int)Math.Round(poskozeni * 2.0 / 3.0);
                                }
                                souper.PridejNeboUberZdravi(poskozeni);
                                messageDruhy = souper.Name + " SpravceMedii.Regeneruje zdravi za " + poskozeni + " bodů pomocí schopnosti " + PomocneMetody.SchopnostToString(vybranaSouper.Druh);
                            }
                            else if (vybranaSouper.Druh == Druh.Lahvicka_Many)
                            {
                                SpravceMedii.Regen.Play();
                                souper.Inventar.LahvickyMany--;
                                if (efektySouper.Horeni > 0 || efektySouper.Jed > 0 || efektySouper.Krvaceni > 0)
                                {
                                    poskozeni = (int)Math.Round(poskozeni * 2.0 / 3.0);
                                }
                                souper.PridejNeboUberManu(poskozeni);
                                messageDruhy = souper.Name + " SpravceMedii.Regeneruje manu za " + poskozeni + " bodů pomocí " + PomocneMetody.SchopnostToString(vybranaSouper.Druh);
                            }
                            else if (vybranaSouper.Druh == Druh.Magicke_soustredeni)
                            {
                                SpravceMedii.Battlecry.Play();
                                efektySouper.Soustredeni += 4;

                                messageDruhy = souper.Name + " se soustředí schopnosti " + PomocneMetody.SchopnostToString(vybranaSouper.Druh);
                            }
                            else if (vybranaSouper.Druh == Druh.Rychlost)
                            {
                                SpravceMedii.Battlecry.Play();
                                efektySouper.Rychlost += 4;
                                messageDruhy = souper.Name + " použil schopnost " + PomocneMetody.SchopnostToString(vybranaSouper.Druh);
                            }
                            else if (vybranaSouper.Druh == Druh.Bojovy_Pokrik)
                            {
                                SpravceMedii.Battlecry.Play();
                                efektySouper.Pokrik += 2;
                                messageDruhy = souper.Name + " zařval " + PomocneMetody.SchopnostToString(vybranaSouper.Druh);
                            }
                            else if (vybranaSouper.Druh == Druh.Strelba_Lukem && vybranaSouper.Faze == 1 || vybranaSouper.Druh == Druh.Magicky_sip && vybranaSouper.Faze == 1)
                            {
                                messageDruhy = souper.Name + " natahuje luk, aby použil schopnost " + PomocneMetody.SchopnostToString(vybranaSouper.Druh);
                                SpravceMedii.BowPull.Play();
                            }
                            else if ((vybranaHrajici.Druh == Druh.Magicky_Stit || vybranaHrajici.Druh == Druh.Obrana_Stitem || vybranaHrajici.Druh == Druh.Uskok) &&
                                (vybranaSouper.Druh == Druh.Strelba_Lukem))
                            {
                                messageDruhy = souper.Name + " se pomocí schopnosti " + PomocneMetody.SchopnostToString(vybranaSouper.Druh) + " netrefil";
                                SpravceMedii.BowMiss.Play();
                            }
                            else if (rychlost || vybranaHrajici.Druh == Druh.Magicky_Stit || vybranaHrajici.Druh == Druh.Obrana_Stitem || vybranaHrajici.Druh == Druh.Uskok)
                            {
                                messageDruhy = souper.Name + " se pomocí schopnosti " + PomocneMetody.SchopnostToString(vybranaSouper.Druh) + " netrefil";
                            }
                            else
                            {
                                if (vybranaSouper.Druh == Druh.Ohniva_Koule)
                                {
                                    SpravceMedii.Fireball.Play();
                                    efektyHrajici.Horeni += 2;
                                }
                                else if (vybranaSouper.Druh == Druh.Ledove_Kopi)
                                {
                                    SpravceMedii.Frostbolt.Play();
                                    efektyHrajici.Mraz += 2;
                                    efektyHrajici.AktivujMraz();
                                }
                                else if (vybranaSouper.Druh == Druh.Vrh_sekerou)
                                {
                                    SpravceMedii.Hit.Play();
                                    efektyHrajici.Krvaceni += 2;
                                    if (efektySouper.Pokrik > 0)
                                        efektyHrajici.Krvaceni += 1;
                                }
                                else if (vybranaSouper.Druh == Druh.Uder_stitem)
                                {
                                    SpravceMedii.Hit.Play();
                                    efektyHrajici.Omraceni += 1;
                                    if (efektySouper.Pokrik > 0)
                                        efektyHrajici.Omraceni += 1;
                                }
                                else if (vybranaSouper.Druh == Druh.Jedova_sipka)
                                {
                                    SpravceMedii.Hit.Play();
                                    efektyHrajici.Jed += 3;
                                }
                                else if (vybranaSouper.Druh == Druh.Berserk)
                                {
                                    SpravceMedii.Battlecry.Play();
                                    SpravceMedii.Hit.Play();
                                }
                                else
                                {
                                    SpravceMedii.Hit.Play();
                                }


                                if (efektySouper.Pokrik > 0)
                                {
                                    poskozeni = poskozeni * 2;
                                }
                                int skPosk;
                                if (souper == souboj.Utocnik)
                                {
                                    skPosk = souboj.ZautocNaObrance(poskozeni, vybranaSouper.Magicka);
                                }
                                else
                                {
                                    skPosk = souboj.ZautocNaUtocnika(poskozeni, vybranaSouper.Magicka);
                                }
                                if (vybranaSouper.Druh == Druh.Vysati_zivota)
                                {
                                    messageDruhy = souper.Name + " vysál pomocí schopnosti " + PomocneMetody.SchopnostToString(vybranaSouper.Druh) + " " + skPosk + " zdraví ";
                                    if (efektySouper.Horeni > 0 || efektySouper.Jed > 0 || efektySouper.Krvaceni > 0)
                                    {
                                        skPosk = (int)Math.Round(skPosk * 2.0 / 3.0);
                                    }
                                    souper.PridejNeboUberZdravi(skPosk);
                                }
                                else if (vybranaSouper.Druh == Druh.Vysati_many)
                                {
                                    mana = hrajici.Mana - Math.Max(hrajici.Mana - skPosk * 2, 0);
                                    messageDruhy = souper.Name + "  ubral pomocí schopnosti " + PomocneMetody.SchopnostToString(vybranaSouper.Druh) + " " + skPosk + " zdraví a vysál " + mana + " many ";
                                }
                                else
                                    messageDruhy = souper.Name + " útočí schopností " + PomocneMetody.SchopnostToString(vybranaSouper.Druh) + " za " + skPosk + " poškození";
                            }

                            if (vybranaSouper.Faze == 0)
                            {
                                if (souper == souboj.Utocnik)
                                {
                                    if (souboj.EfektyUtocnika.Soustredeni > 0)
                                        souper.PridejNeboUberManu(-vybranaSouper.CenaMany / 2);
                                    else
                                        souper.PridejNeboUberManu(-vybranaSouper.CenaMany);
                                }
                                else
                                {
                                    if (souboj.EfektyObrance.Soustredeni > 0)
                                        souper.PridejNeboUberManu(-vybranaSouper.CenaMany / 2);
                                    else
                                        souper.PridejNeboUberManu(-vybranaSouper.CenaMany);
                                }
                                if (vybranaSouper.Druh == Druh.Vysati_many)
                                {
                                    hrajici.PridejNeboUberManu(-mana);
                                    if (efektySouper.Horeni > 0 || efektySouper.Jed > 0 || efektySouper.Krvaceni > 0)
                                    {
                                        mana = (int)Math.Round(mana * 2.0 / 3.0);
                                    }
                                    souper.PridejNeboUberManu(mana);
                                }
                            }
                        }
                        else
                        {
                            vybranaSouper.Faze = vybranaSouper.FazeVychozi;
                            vybranaSouper = new Schopnost(Druh.Zadna, 0, 0, 0, false);
                            messageDruhy = souper.Name + " je omráčený";
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

            if ((hrajici != null && hrajici.Majitel == Majitel.Hrac && faze == Faze.VyberPrvni && vybranaHrajici == null) ||
                (souper != null && souper.Majitel == Majitel.Hrac && faze == Faze.VyberDruhy && vybranaSouper == null))
            {
                int y = 200;
                int i = 0;

                Postava naRade = faze == Faze.VyberPrvni ? hrajici : souper;
                Efekty efekty = naRade == souboj.Obrance ? souboj.EfektyObrance : souboj.EfektyUtocnika;
                foreach (Schopnost schop in naRade.Schopnosti)
                {
                    switch (schop.Druh)
                    {
                        case Druh.Utok_Mecem:
                            PomocneMetody.NastavButton(efekty, buttonUtokMecem, schop, naRade, 325, y + i * 30, 150, 20, SpravceMedii.FontText, SpravceMedii.PrazdnaTexturaBila, Color.White, Color.Black);
                            buttonUtokMecem.Visible = true;
                            break;
                        case Druh.Obrana_Stitem:
                            PomocneMetody.NastavButton(efekty, buttonObranaStitem, schop, naRade, 325, y + i * 30, 150, 20, SpravceMedii.FontText, SpravceMedii.PrazdnaTexturaBila, Color.White, Color.Black);
                            buttonObranaStitem.Visible = true;
                            break;
                        case Druh.Regenerace:
                            PomocneMetody.NastavButton(efekty, buttonRegenerace, schop, naRade, 325, y + i * 30, 150, 20, SpravceMedii.FontText, SpravceMedii.PrazdnaTexturaBila, Color.White, Color.Black);
                            buttonRegenerace.Visible = true;
                            break;
                        case Druh.Bojovy_Pokrik:
                            PomocneMetody.NastavButton(efekty, buttonBojovyPokrik, schop, naRade, 325, y + i * 30, 150, 20, SpravceMedii.FontText, SpravceMedii.PrazdnaTexturaBila, Color.White, Color.Black);
                            buttonBojovyPokrik.Visible = true;
                            break;
                        case Druh.Uder_stitem:
                            PomocneMetody.NastavButton(efekty, buttonUderStitem, schop, naRade, 325, y + i * 30, 150, 20, SpravceMedii.FontText, SpravceMedii.PrazdnaTexturaBila, Color.White, Color.Black);
                            buttonUderStitem.Visible = true;
                            break;
                        case Druh.Vrh_sekerou:
                            PomocneMetody.NastavButton(efekty, buttonVrhSekerou, schop, naRade, 325, y + i * 30, 150, 20, SpravceMedii.FontText, SpravceMedii.PrazdnaTexturaBila, Color.White, Color.Black);
                            buttonVrhSekerou.Visible = true;
                            break;
                        case Druh.Berserk:
                            PomocneMetody.NastavButton(efekty, buttonBerserk, schop, naRade, 325, y + i * 30, 150, 20, SpravceMedii.FontText, SpravceMedii.PrazdnaTexturaBila, Color.White, Color.Black);
                            buttonBerserk.Visible = true;
                            break;
                        case Druh.Bodnuti_Dykou:
                            PomocneMetody.NastavButton(efekty, buttonBodnutiDykou, schop, naRade, 325, y + i * 30, 150, 20, SpravceMedii.FontText, SpravceMedii.PrazdnaTexturaBila, Color.White, Color.Black);
                            buttonBodnutiDykou.Visible = true;
                            break;
                        case Druh.Strelba_Lukem:
                            PomocneMetody.NastavButton(efekty, buttonStrelbaLukem, schop, naRade, 325, y + i * 30, 150, 20, SpravceMedii.FontText, SpravceMedii.PrazdnaTexturaBila, Color.White, Color.Black);
                            buttonStrelbaLukem.Visible = true;
                            break;
                        case Druh.Magicky_sip:
                            PomocneMetody.NastavButton(efekty, buttonMagickySip, schop, naRade, 325, y + i * 30, 150, 20, SpravceMedii.FontText, SpravceMedii.PrazdnaTexturaBila, Color.White, Color.Black);
                            buttonMagickySip.Visible = true;
                            break;
                        case Druh.Uskok:
                            PomocneMetody.NastavButton(efekty, buttonUskok, schop, naRade, 325, y + i * 30, 150, 20, SpravceMedii.FontText, SpravceMedii.PrazdnaTexturaBila, Color.White, Color.Black);
                            buttonUskok.Visible = true;
                            break;
                        case Druh.Rychlost:
                            PomocneMetody.NastavButton(efekty, buttonRychlost, schop, naRade, 325, y + i * 30, 150, 20, SpravceMedii.FontText, SpravceMedii.PrazdnaTexturaBila, Color.White, Color.Black);
                            buttonRychlost.Visible = true;
                            break;
                        case Druh.Lesni_bobule:
                            PomocneMetody.NastavButton(efekty, buttonLesniBobule, schop, naRade, 325, y + i * 30, 150, 20, SpravceMedii.FontText, SpravceMedii.PrazdnaTexturaBila, Color.White, Color.Black);
                            buttonLesniBobule.Visible = true;
                            break;
                        case Druh.Jedova_sipka:
                            PomocneMetody.NastavButton(efekty, buttonJedovaSipka, schop, naRade, 325, y + i * 30, 150, 20, SpravceMedii.FontText, SpravceMedii.PrazdnaTexturaBila, Color.White, Color.Black);
                            buttonJedovaSipka.Visible = true;
                            break;
                        case Druh.Uder_Holi:
                            PomocneMetody.NastavButton(efekty, buttonUderHoli, schop, naRade, 325, y + i * 30, 150, 20, SpravceMedii.FontText, SpravceMedii.PrazdnaTexturaBila, Color.White, Color.Black);
                            buttonUderHoli.Visible = true;
                            break;
                        case Druh.Ohniva_Koule:
                            PomocneMetody.NastavButton(efekty, buttonOhnivaKoule, schop, naRade, 325, y + i * 30, 150, 20, SpravceMedii.FontText, SpravceMedii.PrazdnaTexturaBila, Color.White, Color.Black);
                            buttonOhnivaKoule.Visible = true;
                            break;
                        case Druh.Ledove_Kopi:
                            PomocneMetody.NastavButton(efekty, buttonLedoveKopi, schop, naRade, 325, y + i * 30, 150, 20, SpravceMedii.FontText, SpravceMedii.PrazdnaTexturaBila, Color.White, Color.Black);
                            buttonLedoveKopi.Visible = true;
                            break;
                        case Druh.Magicky_Stit:
                            PomocneMetody.NastavButton(efekty, buttonMagickyStit, schop, naRade, 325, y + i * 30, 150, 20, SpravceMedii.FontText, SpravceMedii.PrazdnaTexturaBila, Color.White, Color.Black);
                            buttonMagickyStit.Visible = true;
                            break;
                        case Druh.Vysati_zivota:
                            PomocneMetody.NastavButton(efekty, buttonVysatiZivota, schop, naRade, 325, y + i * 30, 150, 20, SpravceMedii.FontText, SpravceMedii.PrazdnaTexturaBila, Color.White, Color.Black);
                            buttonVysatiZivota.Visible = true;
                            break;
                        case Druh.Vysati_many:
                            PomocneMetody.NastavButton(efekty, buttonVysatiMany, schop, naRade, 325, y + i * 30, 150, 20, SpravceMedii.FontText, SpravceMedii.PrazdnaTexturaBila, Color.White, Color.Black);
                            buttonVysatiMany.Visible = true;
                            break;
                        case Druh.Magicke_soustredeni:
                            PomocneMetody.NastavButton(efekty, buttonMagickeSoustredeni, schop, naRade, 325, y + i * 30, 150, 20, SpravceMedii.FontText, SpravceMedii.PrazdnaTexturaBila, Color.White, Color.Black);
                            buttonMagickeSoustredeni.Visible = true;
                            break;
                        case Druh.Utek:
                            PomocneMetody.NastavButton(efekty, buttonUtek, schop, naRade, 325, y + i * 30, 150, 20, SpravceMedii.FontText, SpravceMedii.PrazdnaTexturaBila, Color.White, Color.Black);
                            buttonUtek.Visible = true;
                            break;
                        case Druh.Lahvicka_Many:
                            PomocneMetody.NastavButton(efekty, buttonLahvickaMany, schop, naRade, 325, y + i * 30, 150, 20, SpravceMedii.FontText, SpravceMedii.PrazdnaTexturaBila, Color.White, Color.Black);
                            buttonLahvickaMany.Visible = true;
                            break;
                        case Druh.Lahvicka_Zdravi:
                            PomocneMetody.NastavButton(efekty, buttonLahvickaZdravi, schop, naRade, 325, y + i * 30, 150, 20, SpravceMedii.FontText, SpravceMedii.PrazdnaTexturaBila, Color.White, Color.Black);
                            buttonLahvickaZdravi.Visible = true;
                            break;
                    }
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
            delkaU = SpravceMedii.FontNadpis.MeasureString(souboj.Utocnik.Name);
            spriteBatch.DrawString(SpravceMedii.FontNadpis, souboj.Utocnik.Name, new Vector2(100 - delkaU.X / 2, 20 - delkaU.Y / 2), Color.Black);
            delkaO = SpravceMedii.FontNadpis.MeasureString(souboj.Obrance.Name);
            spriteBatch.DrawString(SpravceMedii.FontNadpis, souboj.Obrance.Name, new Vector2(700 - delkaO.X / 2, 20 - delkaO.Y / 2), Color.Black);
            //trida
            delkaU = SpravceMedii.FontNadpis.MeasureString(PomocneMetody.TridaToString(souboj.Utocnik.Trida, souboj.Utocnik.Pohlavi));
            spriteBatch.DrawString(SpravceMedii.FontNadpis, PomocneMetody.TridaToString(souboj.Utocnik.Trida, souboj.Utocnik.Pohlavi), new Vector2(100 - delkaU.X / 2, 245 - delkaU.Y / 2), Color.Black);
            delkaO = SpravceMedii.FontNadpis.MeasureString(PomocneMetody.TridaToString(souboj.Obrance.Trida, souboj.Obrance.Pohlavi));
            spriteBatch.DrawString(SpravceMedii.FontNadpis, PomocneMetody.TridaToString(souboj.Obrance.Trida, souboj.Obrance.Pohlavi), new Vector2(700 - delkaO.X / 2, 245 - delkaO.Y / 2), Color.Black);
            //Pohlavi                    
            delkaU = SpravceMedii.FontNadpis.MeasureString(PomocneMetody.PohlaviToString(souboj.Utocnik.Pohlavi));
            spriteBatch.DrawString(SpravceMedii.FontNadpis, PomocneMetody.PohlaviToString(souboj.Utocnik.Pohlavi), new Vector2(100 - delkaU.X / 2, 280 - delkaU.Y / 2), Color.Black);
            delkaO = SpravceMedii.FontNadpis.MeasureString(PomocneMetody.PohlaviToString(souboj.Obrance.Pohlavi));
            spriteBatch.DrawString(SpravceMedii.FontNadpis, PomocneMetody.PohlaviToString(souboj.Obrance.Pohlavi), new Vector2(700 - delkaO.X / 2, 280 - delkaO.Y / 2), Color.Black);
            //Level
            spriteBatch.DrawString(SpravceMedii.FontText, "Level: " + souboj.Utocnik.Level, new Vector2(20, 300), Color.Black);
            spriteBatch.DrawString(SpravceMedii.FontText, "Level: " + souboj.Obrance.Level, new Vector2(620, 300), Color.Black);
            //Zkusenosti
            spriteBatch.DrawString(SpravceMedii.FontText, "Zkušenosti: " + souboj.Utocnik.Zkusenosti + "/" + souboj.Utocnik.ZkusenostiNext, new Vector2(20, 320), Color.Black);
            if (souboj.Obrance.Majitel == Majitel.Hrac)
                spriteBatch.DrawString(SpravceMedii.FontText, "Zkušenosti: " + souboj.Obrance.Zkusenosti + "/" + souboj.Obrance.ZkusenostiNext, new Vector2(620, 320), Color.Black);
            //Zivoty
            spriteBatch.DrawString(SpravceMedii.FontText, "Životy: " + souboj.Utocnik.Zivoty + "/" + souboj.Utocnik.ZivotyMax, new Vector2(20, 350), Color.Black);
            spriteBatch.DrawString(SpravceMedii.FontText, "Životy: " + souboj.Obrance.Zivoty + "/" + souboj.Obrance.ZivotyMax, new Vector2(620, 350), Color.Black);
            //Mana
            spriteBatch.DrawString(SpravceMedii.FontText, "Mana: " + souboj.Utocnik.Mana + "/" + souboj.Utocnik.ManaMax, new Vector2(20, 370), Color.Black);
            spriteBatch.DrawString(SpravceMedii.FontText, "Mana: " + souboj.Obrance.Mana + "/" + souboj.Obrance.ManaMax, new Vector2(620, 370), Color.Black);
            //Sila
            if (souboj.EfektyUtocnika.Mraz > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Síla: " + souboj.Utocnik.Sila, new Vector2(20, 400), Color.Red);
            else spriteBatch.DrawString(SpravceMedii.FontText, "Síla: " + souboj.Utocnik.Sila, new Vector2(20, 400), Color.Black);
            if (souboj.EfektyObrance.Mraz > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Síla: " + souboj.Obrance.Sila, new Vector2(620, 400), Color.Red);
            else spriteBatch.DrawString(SpravceMedii.FontText, "Síla: " + souboj.Obrance.Sila, new Vector2(620, 400), Color.Black);
            //Obratnost
            if (souboj.EfektyUtocnika.Mraz > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Obratnost: " + souboj.Utocnik.Obratnost, new Vector2(20, 420), Color.Red);
            else spriteBatch.DrawString(SpravceMedii.FontText, "Obratnost: " + souboj.Utocnik.Obratnost, new Vector2(20, 420), Color.Black);
            if (souboj.EfektyObrance.Mraz > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Obratnost: " + souboj.Obrance.Obratnost, new Vector2(620, 420), Color.Red);
            else spriteBatch.DrawString(SpravceMedii.FontText, "Obratnost: " + souboj.Obrance.Obratnost, new Vector2(620, 420), Color.Black);
            //Inteligence
            if (souboj.EfektyUtocnika.Mraz > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Inteligence: " + souboj.Utocnik.Inteligence, new Vector2(20, 440), Color.Red);
            else spriteBatch.DrawString(SpravceMedii.FontText, "Inteligence: " + souboj.Utocnik.Inteligence, new Vector2(20, 440), Color.Black);
            if (souboj.EfektyObrance.Mraz > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Inteligence: " + souboj.Obrance.Inteligence, new Vector2(620, 440), Color.Red);
            else spriteBatch.DrawString(SpravceMedii.FontText, "Inteligence: " + souboj.Obrance.Inteligence, new Vector2(620, 440), Color.Black);
            //Brneni
            spriteBatch.DrawString(SpravceMedii.FontText, "Brnění: " + souboj.Utocnik.Brneni, new Vector2(20, 460), Color.Black);
            spriteBatch.DrawString(SpravceMedii.FontText, "Brnění: " + souboj.Obrance.Brneni, new Vector2(620, 460), Color.Black);
            //Efekty
            //Pokřik, Rychlost, Magické soustředění
            if (souboj.EfektyUtocnika.Pokrik > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Pokřik [" + souboj.EfektyUtocnika.Pokrik + "]", new Vector2(20, 490), Color.Red);
            if (souboj.EfektyObrance.Pokrik > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Pokřik [" + souboj.EfektyObrance.Pokrik + "]", new Vector2(620, 490), Color.Red);
            if (souboj.EfektyUtocnika.Rychlost > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Rychlost [" + souboj.EfektyUtocnika.Rychlost + "]", new Vector2(20, 490), Color.Red);
            if (souboj.EfektyObrance.Rychlost > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Rychlost [" + souboj.EfektyObrance.Rychlost + "]", new Vector2(620, 490), Color.Red);
            if (souboj.EfektyUtocnika.Soustredeni > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Soustředění [" + souboj.EfektyUtocnika.Soustredeni + "]", new Vector2(20, 490), Color.Red);
            if (souboj.EfektyObrance.Soustredeni > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Soustředění [" + souboj.EfektyObrance.Soustredeni + "]", new Vector2(620, 490), Color.Red);
            //Hoří, krvácí, jed
            if (souboj.EfektyUtocnika.Horeni > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Hoří [" + souboj.EfektyUtocnika.Horeni + "]", new Vector2(20, 510), Color.Red);
            if (souboj.EfektyObrance.Horeni > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Hoří [" + souboj.EfektyObrance.Horeni + "]", new Vector2(620, 510), Color.Red);
            if (souboj.EfektyUtocnika.Krvaceni > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Krvácí [" + souboj.EfektyUtocnika.Krvaceni + "]", new Vector2(20, 510), Color.Red);
            if (souboj.EfektyObrance.Krvaceni > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Krvácí [" + souboj.EfektyObrance.Krvaceni + "]", new Vector2(620, 510), Color.Red);
            if (souboj.EfektyUtocnika.Jed > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Jed [" + souboj.EfektyUtocnika.Jed + "]", new Vector2(20, 510), Color.Red);
            if (souboj.EfektyObrance.Jed > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Jed [" + souboj.EfektyObrance.Jed + "]", new Vector2(620, 510), Color.Red);
            //Mrzne, omráčený
            if (souboj.EfektyUtocnika.Mraz > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Mrzne [" + souboj.EfektyUtocnika.Mraz + "]", new Vector2(20, 530), Color.Red);
            if (souboj.EfektyObrance.Mraz > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Mrzne [" + souboj.EfektyObrance.Mraz + "]", new Vector2(620, 530), Color.Red);
            if (souboj.EfektyUtocnika.Omraceni > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Omráčený [" + souboj.EfektyUtocnika.Omraceni + "]", new Vector2(20, 530), Color.Red);
            if (souboj.EfektyObrance.Omraceni > 0)
                spriteBatch.DrawString(SpravceMedii.FontText, "Omráčený [" + souboj.EfektyObrance.Omraceni + "]", new Vector2(620, 530), Color.Red);
            //MessageBox
            spriteBatch.Draw(SpravceMedii.PrazdnaTexturaCerna, new Rectangle(0, 550, 800, 50), Color.Black);
            delkaU = SpravceMedii.FontText.MeasureString(messagePrvni);
            spriteBatch.DrawString(SpravceMedii.FontText, messagePrvni, new Vector2(400 - delkaU.X / 2, 563 - delkaU.Y / 2), Color.White);
            delkaO = SpravceMedii.FontText.MeasureString(messageDruhy);
            spriteBatch.DrawString(SpravceMedii.FontText, messageDruhy, new Vector2(400 - delkaO.X / 2, 588 - delkaO.Y / 2), Color.White);
            delkaU = SpravceMedii.FontNadpis.MeasureString(messageKolo);
            spriteBatch.DrawString(SpravceMedii.FontNadpis, messageKolo, new Vector2(400 - delkaU.X / 2, 100 - delkaU.Y / 2), Color.White);
            //Buttons
            if (faze == Faze.VyberPrvni || faze == Faze.VyberDruhy)
            {
                foreach (Button butt in buttons)
                {
                    butt.Draw(gameTime, spriteBatch);
                }
            }
            //Test
            //spriteBatch.DrawString(SpravceMedii.FontText, souboj.EfektyUtocnika.Postava.Inteligence.ToString()+" "+souboj.EfektyUtocnika.PuvodniIntelekt.ToString(), new Vector2(400, 300), Color.White);
            spriteBatch.End();
        }

        protected Schopnost VyberSchopnostAI(Postava pocitac, Schopnost souperSch, Postava souper)
        {
            Efekty efektyPocitac = pocitac == souboj.Obrance ? souboj.EfektyObrance : souboj.EfektyUtocnika;
            Inventar inventarPocitac = pocitac == souboj.Obrance ? souboj.Obrance.Inventar : souboj.Utocnik.Inventar;
            Efekty efektySouper = souper == souboj.Obrance ? souboj.EfektyObrance : souboj.EfektyUtocnika;
            Schopnost sch;
            switch (pocitac.Majitel)
            {
                case Majitel.Pocitac_Lehky:
                start00:
                    sch = pocitac.Schopnosti[souboj.Kostka.Next(0, pocitac.Schopnosti.Count)];
                    if (sch.Cd > 0 || (efektyPocitac.Soustredeni == 0 && sch.CenaMany > pocitac.Mana) || (efektyPocitac.Soustredeni > 0 && (sch.CenaMany / 2) > pocitac.Mana))
                    {
                        goto start00;
                    }
                    if ((sch.Druh == Druh.Lahvicka_Many && inventarPocitac.LahvickyMany == 0) || (sch.Druh == Druh.Lahvicka_Zdravi && inventarPocitac.LahvickyZdravi == 0))
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
                            if (sch.Cd > 0 || (efektyPocitac.Soustredeni == 0 && sch.CenaMany > pocitac.Mana) || (efektyPocitac.Soustredeni > 0 && (sch.CenaMany / 2) > pocitac.Mana) || sch.Druh == Druh.Obrana_Stitem)
                            {
                                goto start10;
                            }
                            if ((sch.Druh == Druh.Lahvicka_Many && inventarPocitac.LahvickyMany == 0) || (sch.Druh == Druh.Lahvicka_Zdravi && inventarPocitac.LahvickyZdravi == 0))
                            {
                                goto start10;
                            }
                            if (souperSch != null && ((souperSch.Druh == Druh.Magicky_sip && souperSch.Faze == 0) || (souperSch.Druh == Druh.Strelba_Lukem && souperSch.Faze == 0) ||
                                ((souperSch.Druh == Druh.Utok_Mecem || souperSch.Druh == Druh.Uder_stitem || souperSch.Druh == Druh.Vrh_sekerou ||
                                (souperSch.Druh == Druh.Berserk && souper.Zivoty / (double)souper.ZivotyMax < 0.4)) && efektySouper.Pokrik > 0) || souperSch.Druh == Druh.Ohniva_Koule ||
                                souperSch.Druh == Druh.Ledove_Kopi || souperSch.Druh == Druh.Vysati_zivota || souperSch.Druh == Druh.Vysati_many || souperSch.Druh == Druh.Jedova_sipka))
                            {
                                foreach (Schopnost s in pocitac.Schopnosti)
                                {
                                    if (s.Druh == Druh.Obrana_Stitem)
                                    {
                                        sch = s.Cd > 0 || s.CenaMany > pocitac.Mana ? sch : s;
                                    }
                                }
                            }
                            foreach (Schopnost s in pocitac.Schopnosti)
                            {
                                if (s.Druh == Druh.Uder_stitem && pocitac == hrajici)
                                {
                                    sch = s.Cd > 0 || s.CenaMany > pocitac.Mana ? sch : s;
                                }
                            }


                            return sch;
                        case Trida.Lucistnik:
                        start11:
                            sch = pocitac.Schopnosti[souboj.Kostka.Next(0, pocitac.Schopnosti.Count)];

                            if (sch.Cd > 0 || (efektyPocitac.Soustredeni == 0 && sch.CenaMany > pocitac.Mana) || (efektyPocitac.Soustredeni > 0 && (sch.CenaMany / 2) > pocitac.Mana) || sch.Druh == Druh.Uskok)
                            {
                                goto start11;
                            }
                            if ((sch.Druh == Druh.Lahvicka_Many && inventarPocitac.LahvickyMany == 0) || (sch.Druh == Druh.Lahvicka_Zdravi && inventarPocitac.LahvickyZdravi == 0))
                            {
                                goto start11;
                            }
                            if (souperSch != null && ((souperSch.Druh == Druh.Magicky_sip && souperSch.Faze == 0) || (souperSch.Druh == Druh.Strelba_Lukem && souperSch.Faze == 0) ||
                                ((souperSch.Druh == Druh.Utok_Mecem || souperSch.Druh == Druh.Uder_stitem || souperSch.Druh == Druh.Vrh_sekerou ||
                                (souperSch.Druh == Druh.Berserk && souper.Zivoty / (double)souper.ZivotyMax < 0.4)) && efektySouper.Pokrik > 0) || souperSch.Druh == Druh.Ohniva_Koule ||
                                souperSch.Druh == Druh.Ledove_Kopi || souperSch.Druh == Druh.Vysati_zivota || souperSch.Druh == Druh.Vysati_many || souperSch.Druh == Druh.Jedova_sipka))
                            {
                                foreach (Schopnost s in pocitac.Schopnosti)
                                {
                                    if (s.Druh == Druh.Uskok)
                                    {
                                        sch = s.Cd > 0 || s.CenaMany > pocitac.Mana ? sch : s;
                                    }
                                }
                            }
                            foreach (Schopnost s in pocitac.Schopnosti)
                            {
                                if (s.Druh == Druh.Rychlost && hrajici == pocitac)
                                {
                                    sch = s.Cd > 0 || s.CenaMany > pocitac.Mana ? sch : s;
                                }
                            }
                            return sch;
                        case Trida.Kouzelnik:
                        start12:
                            sch = pocitac.Schopnosti[souboj.Kostka.Next(0, pocitac.Schopnosti.Count)];
                            if (sch.Cd > 0 || (efektyPocitac.Soustredeni == 0 && sch.CenaMany > pocitac.Mana) || (efektyPocitac.Soustredeni > 0 && (sch.CenaMany / 2) > pocitac.Mana) || sch.Druh == Druh.Magicky_Stit)
                            {
                                goto start12;
                            }
                            if ((sch.Druh == Druh.Lahvicka_Many && inventarPocitac.LahvickyMany == 0) || (sch.Druh == Druh.Lahvicka_Zdravi && inventarPocitac.LahvickyZdravi == 0))
                            {
                                goto start12;
                            }
                            foreach (Schopnost s in pocitac.Schopnosti)
                            {
                                if (s.Druh == Druh.Magicke_soustredeni)
                                {
                                    sch = s.Cd > 0 || s.CenaMany > pocitac.Mana ? sch : s;
                                }
                            }
                            if (souperSch != null && ((souperSch.Druh == Druh.Magicky_sip && souperSch.Faze == 0) || (souperSch.Druh == Druh.Strelba_Lukem && souperSch.Faze == 0) ||
                                ((souperSch.Druh == Druh.Utok_Mecem || souperSch.Druh == Druh.Uder_stitem || souperSch.Druh == Druh.Vrh_sekerou ||
                                (souperSch.Druh == Druh.Berserk && souper.Zivoty / (double)souper.ZivotyMax < 0.4)) && efektySouper.Pokrik > 0) || souperSch.Druh == Druh.Ohniva_Koule ||
                                souperSch.Druh == Druh.Ledove_Kopi || souperSch.Druh == Druh.Vysati_zivota || souperSch.Druh == Druh.Vysati_many || souperSch.Druh == Druh.Jedova_sipka))
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
                                    sch = s.Cd > 0 || s.CenaMany > pocitac.Mana ? sch : s;
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
                                if (s.Druh == Druh.Vrh_sekerou)
                                {
                                    sch = s.Cd > 0 || s.CenaMany > pocitac.Mana ? sch : s;
                                }
                            }


                            foreach (Schopnost s in pocitac.Schopnosti)
                            {
                                if (s.Druh == Druh.Utok_Mecem && efektyPocitac.Pokrik > 0)
                                {
                                    sch = s.Cd > 0 || s.CenaMany > pocitac.Mana ? sch : s;
                                }
                            }
                            foreach (Schopnost schop in souper.Schopnosti)
                            {
                                if (schop.Druh == Druh.Uskok && schop.Cd > 1 || schop.Druh == Druh.Obrana_Stitem && schop.Cd > 1 ||
                                    schop.Druh == Druh.Magicky_Stit && (schop.Cd > 1 || schop.CenaMany > souper.Mana))
                                {
                                    foreach (Schopnost schopnost in pocitac.Schopnosti)
                                    {
                                        if (schopnost.Druh == Druh.Bojovy_Pokrik)
                                        {
                                            sch = schopnost.Cd > 0 || schopnost.CenaMany > pocitac.Mana ? sch : schopnost;
                                        }
                                    }
                                }
                            }

                            foreach (Schopnost s in pocitac.Schopnosti)
                            {
                                if (s.Druh == Druh.Berserk && ((pocitac.Zivoty / (double)pocitac.ZivotyMax) < 0.4))
                                {
                                    sch = s.Cd > 0 || s.CenaMany > pocitac.Mana ? sch : s;
                                }
                            }

                            foreach (Schopnost s in pocitac.Schopnosti)
                            {
                                if (s.Druh == Druh.Regenerace && pocitac.Zivoty <= (double)pocitac.ZivotyMax / 2 && efektyPocitac.Pokrik > 0)
                                {
                                    sch = s.Cd > 0 || s.CenaMany > pocitac.Mana ? sch : s;
                                }
                            }

                            foreach (Schopnost schop in souper.Schopnosti)
                            {
                                if (schop.Druh == Druh.Uskok && schop.Cd > 0 || schop.Druh == Druh.Obrana_Stitem && schop.Cd > 0 ||
                                    schop.Druh == Druh.Magicky_Stit && (schop.Cd > 0 || schop.CenaMany > souper.Mana))
                                {
                                    foreach (Schopnost schopnost in pocitac.Schopnosti)
                                    {
                                        if (schopnost.Druh == Druh.Berserk && ((pocitac.Zivoty / (double)pocitac.ZivotyMax) < 0.4) && efektyPocitac.Pokrik > 0)
                                        {
                                            sch = schopnost.Cd > 0 || schopnost.CenaMany > pocitac.Mana ? sch : schopnost;
                                        }
                                    }
                                }
                            }

                            foreach (Schopnost s in pocitac.Schopnosti)
                            {
                                if (s.Druh == Druh.Uder_stitem && pocitac == hrajici && efektyPocitac.Pokrik > 0)
                                {
                                    sch = s.Cd > 0 || s.CenaMany > pocitac.Mana ? sch : s;
                                }
                            }

                            foreach (Schopnost s in pocitac.Schopnosti)
                            {
                                if (s.Druh == Druh.Lahvicka_Many && inventarPocitac.LahvickyMany > 0 && (pocitac.ManaMax - pocitac.Mana >= 100 || pocitac.Mana < 20))
                                {
                                    sch = s.Cd > 0 || s.CenaMany > pocitac.Mana ? sch : s;
                                }
                            }
                            foreach (Schopnost s in pocitac.Schopnosti)
                            {
                                if (s.Druh == Druh.Lahvicka_Zdravi && inventarPocitac.LahvickyZdravi > 0 && (pocitac.ZivotyMax - pocitac.Zivoty >= 100 || pocitac.Zivoty < 25))
                                {
                                    sch = s.Cd > 0 || s.CenaMany > pocitac.Mana ? sch : s;
                                }
                            }

                            foreach (Schopnost s in pocitac.Schopnosti)
                            {
                                if (s.Druh == Druh.Utok_Mecem && souper.Zivoty <= (s.Pouzij(pocitac, false) - souper.Brneni))
                                {
                                    sch = s.Cd > 0 || s.CenaMany > pocitac.Mana ? sch : s;
                                }
                            }

                            foreach (Schopnost s in pocitac.Schopnosti)
                            {
                                if (s.Druh == Druh.Berserk && souper.Zivoty <= (s.Pouzij(pocitac, false) - souper.Brneni))
                                {
                                    sch = s.Cd > 0 || s.CenaMany > pocitac.Mana ? sch : s;
                                }
                            }

                            if (souperSch != null && ((souperSch.Druh == Druh.Magicky_sip && souperSch.Faze == 0) ||
                                (souperSch.Druh == Druh.Strelba_Lukem && souperSch.Faze == 0) || ((souperSch.Druh == Druh.Utok_Mecem || souperSch.Druh == Druh.Uder_stitem ||
                                souperSch.Druh == Druh.Vrh_sekerou || (souperSch.Druh == Druh.Berserk && souper.Zivoty / (double)souper.ZivotyMax < 0.4)) && efektySouper.Pokrik > 0) ||
                                souperSch.Druh == Druh.Ohniva_Koule || souperSch.Druh == Druh.Ledove_Kopi || souperSch.Druh == Druh.Vysati_zivota))
                            {
                                foreach (Schopnost s in pocitac.Schopnosti)
                                {
                                    if (s.Druh == Druh.Obrana_Stitem)
                                    {
                                        sch = s.Cd > 0 || s.CenaMany > pocitac.Mana ? sch : s;
                                    }
                                }
                            }

                            if (pocitac == hrajici && souperSch != null && ((souperSch.Druh == Druh.Magicky_sip && souperSch.Faze == 0) ||
                                (souperSch.Druh == Druh.Strelba_Lukem && souperSch.Faze == 0) || ((souperSch.Druh == Druh.Utok_Mecem || souperSch.Druh == Druh.Uder_stitem ||
                                souperSch.Druh == Druh.Vrh_sekerou || (souperSch.Druh == Druh.Berserk && souper.Zivoty / (double)souper.ZivotyMax < 0.4)) && efektySouper.Pokrik > 0) ||
                                souperSch.Druh == Druh.Ohniva_Koule || souperSch.Druh == Druh.Ledove_Kopi || souperSch.Druh == Druh.Vysati_zivota))
                            {
                                foreach (Schopnost s in pocitac.Schopnosti)
                                {
                                    if (s.Druh == Druh.Uder_stitem)
                                    {
                                        sch = s.Cd > 0 || s.CenaMany > pocitac.Mana ? sch : s;
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
                                    sch = s.Cd > 0 || s.CenaMany > pocitac.Mana ? sch : s;
                                }
                            }

                            foreach (Schopnost s in pocitac.Schopnosti)
                            {
                                if (s.Druh == Druh.Magicky_sip)
                                {
                                    sch = s.Cd > 0 || s.CenaMany > pocitac.Mana ? sch : s;
                                }
                            }


                            foreach (Schopnost s in pocitac.Schopnosti)
                            {
                                if (s.Druh == Druh.Lesni_bobule && pocitac.Zivoty < (pocitac.ZivotyMax - s.Pouzij(pocitac, false)))
                                {
                                    sch = s.Cd > 0 || s.CenaMany > pocitac.Mana ? sch : s;
                                }
                            }

                            foreach (Schopnost s in pocitac.Schopnosti)
                            {
                                if (s.Druh == Druh.Jedova_sipka)
                                {
                                    sch = s.Cd > 0 || s.CenaMany > pocitac.Mana ? sch : s;
                                }
                            }

                            foreach (Schopnost s in pocitac.Schopnosti)
                            {
                                if (s.Druh == Druh.Lahvicka_Many && inventarPocitac.LahvickyMany > 0 && (pocitac.ManaMax - pocitac.Mana >= 100 || pocitac.Mana < 20))
                                {
                                    sch = s.Cd > 0 || s.CenaMany > pocitac.Mana ? sch : s;
                                }
                            }

                            foreach (Schopnost schop in souper.Schopnosti)
                            {
                                if (schop.Druh == Druh.Uskok && schop.Cd > 1 || schop.Druh == Druh.Obrana_Stitem && schop.Cd > 1 ||
                                    schop.Druh == Druh.Magicky_Stit && (schop.Cd > 1 || schop.CenaMany > souper.Mana))
                                {
                                    foreach (Schopnost schopnost in pocitac.Schopnosti)
                                    {
                                        if (schopnost.Druh == Druh.Strelba_Lukem)
                                        {
                                            sch = schopnost.CenaMany > pocitac.Mana ? sch : schopnost;
                                        }
                                        if (schopnost.Druh == Druh.Magicky_sip)
                                        {
                                            sch = schopnost.CenaMany > pocitac.Mana ? sch : schopnost;
                                        }
                                    }
                                }
                            }
                            foreach (Schopnost s in pocitac.Schopnosti)
                            {
                                if (s.Druh == Druh.Lahvicka_Zdravi && inventarPocitac.LahvickyZdravi > 0 && (pocitac.ZivotyMax - pocitac.Zivoty >= 100 || pocitac.Zivoty < 25))
                                {
                                    sch = s.Cd > 0 || s.CenaMany > pocitac.Mana ? sch : s;
                                }
                            }

                            foreach (Schopnost s in pocitac.Schopnosti)
                            {
                                if (s.Druh == Druh.Bodnuti_Dykou && souper.Zivoty <= (s.Pouzij(pocitac, false) - souper.Brneni))
                                {
                                    sch = s.Cd > 0 || s.CenaMany > pocitac.Mana ? sch : s;
                                }
                            }

                            if (souperSch != null && ((souperSch.Druh == Druh.Magicky_sip && souperSch.Faze == 0) ||
                                (souperSch.Druh == Druh.Strelba_Lukem && souperSch.Faze == 0) || ((souperSch.Druh == Druh.Utok_Mecem || souperSch.Druh == Druh.Uder_stitem ||
                                souperSch.Druh == Druh.Vrh_sekerou || (souperSch.Druh == Druh.Berserk && souper.Zivoty / (double)souper.ZivotyMax < 0.4)) && efektySouper.Pokrik > 0) ||
                                souperSch.Druh == Druh.Ohniva_Koule || souperSch.Druh == Druh.Ledove_Kopi || souperSch.Druh == Druh.Vysati_zivota))
                            {
                                foreach (Schopnost s in pocitac.Schopnosti)
                                {
                                    if (s.Druh == Druh.Uskok && efektyPocitac.Rychlost < 3)
                                    {
                                        sch = s.Cd > 0 || s.CenaMany > pocitac.Mana ? sch : s;
                                    }
                                }
                            }
                            foreach (Schopnost s in pocitac.Schopnosti)
                            {
                                if (s.Druh == Druh.Rychlost && hrajici == pocitac)
                                {
                                    sch = s.Cd > 0 || s.CenaMany > pocitac.Mana ? sch : s;
                                }
                            }
                            return sch;
                        case Trida.Kouzelnik:
                            sch = null;
                            foreach (Schopnost s in pocitac.Schopnosti)
                            {
                                if (s.Druh == Druh.Uder_Holi)
                                {
                                    sch = s.Cd > 0 || (efektyPocitac.Soustredeni == 0 && s.CenaMany > pocitac.Mana) || (efektyPocitac.Soustredeni > 0 && (s.CenaMany / 2) > pocitac.Mana) ? sch : s;
                                }
                            }

                            if (efektySouper.Mraz == 0)
                            {
                                foreach (Schopnost s in pocitac.Schopnosti)
                                {
                                    if (s.Druh == Druh.Ledove_Kopi)
                                    {
                                        sch = s.Cd > 0 || (efektyPocitac.Soustredeni == 0 && s.CenaMany > pocitac.Mana) || (efektyPocitac.Soustredeni > 0 && (s.CenaMany / 2) > pocitac.Mana) ? sch : s;
                                    }
                                }
                            }
                            if (efektySouper.Horeni == 0)
                            {
                                foreach (Schopnost s in pocitac.Schopnosti)
                                {
                                    if (s.Druh == Druh.Ohniva_Koule)
                                    {
                                        sch = s.Cd > 0 || (efektyPocitac.Soustredeni == 0 && s.CenaMany > pocitac.Mana) || (efektyPocitac.Soustredeni > 0 && (s.CenaMany / 2) > pocitac.Mana) ? sch : s;
                                    }
                                }
                            }
                            if (pocitac.Minulost == Minulost.Lovec)
                            {
                                if (efektySouper.Mraz == 0)
                                {
                                    foreach (Schopnost s in pocitac.Schopnosti)
                                    {
                                        if (s.Druh == Druh.Ledove_Kopi)
                                        {
                                            sch = s.Cd > 0 || (efektyPocitac.Soustredeni == 0 && s.CenaMany > pocitac.Mana) || (efektyPocitac.Soustredeni > 0 && (s.CenaMany / 2) > pocitac.Mana) ? sch : s;
                                        }
                                    }
                                }
                            }
                            else if (pocitac.Minulost == Minulost.Rytir)
                            {
                                if (efektySouper.Horeni == 0)
                                {
                                    foreach (Schopnost s in pocitac.Schopnosti)
                                    {
                                        if (s.Druh == Druh.Ohniva_Koule)
                                        {
                                            sch = s.Cd > 0 || (efektyPocitac.Soustredeni == 0 && s.CenaMany > pocitac.Mana) || (efektyPocitac.Soustredeni > 0 && (s.CenaMany / 2) > pocitac.Mana) ? sch : s;
                                        }
                                    }
                                }
                            }
                            if (souper.Brneni > 1 || (vybranaSouper != null && (vybranaSouper.Druh == Druh.Magicky_sip || vybranaSouper.Druh == Druh.Strelba_Lukem) && vybranaSouper.Faze > 0) ||
                                (vybranaSouper != null && (vybranaSouper.Druh == Druh.Magicky_sip || vybranaSouper.Druh == Druh.Strelba_Lukem) && vybranaSouper.Faze == 0 && hrajici == pocitac))
                            {
                                if (efektySouper.Mraz == 0)
                                {
                                    foreach (Schopnost s in pocitac.Schopnosti)
                                    {
                                        if (s.Druh == Druh.Ledove_Kopi)
                                        {
                                            sch = s.Cd > 0 || (efektyPocitac.Soustredeni == 0 && s.CenaMany > pocitac.Mana) || (efektyPocitac.Soustredeni > 0 && (s.CenaMany / 2) > pocitac.Mana) ? sch : s;
                                        }
                                    }
                                }
                            }

                            foreach (Schopnost s in pocitac.Schopnosti)
                            {
                                if (s.Druh == Druh.Uder_Holi && efektyPocitac.Soustredeni == 0 && pocitac.Mana < (double)pocitac.ManaMax / 2)
                                {
                                    sch = s.Cd > 0 || (efektyPocitac.Soustredeni == 0 && s.CenaMany > pocitac.Mana) || (efektyPocitac.Soustredeni > 0 && (s.CenaMany / 2) > pocitac.Mana) ? sch : s;
                                }
                            }

                            foreach (Schopnost s in pocitac.Schopnosti)
                            {
                                if (s.Druh == Druh.Vysati_zivota && pocitac.Zivoty < (pocitac.ZivotyMax - s.Pouzij(pocitac, false)))
                                {
                                    sch = s.Cd > 0 || (efektyPocitac.Soustredeni == 0 && s.CenaMany > pocitac.Mana) || (efektyPocitac.Soustredeni > 0 && (s.CenaMany / 2) > pocitac.Mana) ? sch : s;
                                }
                            }

                            foreach (Schopnost s in pocitac.Schopnosti)
                            {
                                if (s.Druh == Druh.Magicky_Stit)
                                {
                                    if (s.Cd > 0 || (efektyPocitac.Soustredeni == 0 && s.CenaMany > pocitac.Mana) || (efektyPocitac.Soustredeni > 0 && (s.CenaMany / 2) > pocitac.Mana))
                                    {
                                        foreach (Schopnost sc in pocitac.Schopnosti)
                                        {
                                            if (sc.Druh == Druh.Vysati_zivota)
                                            {
                                                if (sc.Cd > 0 || (efektyPocitac.Soustredeni == 0 && sc.CenaMany > pocitac.Mana) || (efektyPocitac.Soustredeni > 0 && (sc.CenaMany / 2) > pocitac.Mana))
                                                {
                                                    foreach (Schopnost scho in pocitac.Schopnosti)
                                                    {
                                                        if (scho.Druh == Druh.Ledove_Kopi)
                                                        {
                                                            sch = scho.Cd > 0 || (efektyPocitac.Soustredeni == 0 && scho.CenaMany > pocitac.Mana) || (efektyPocitac.Soustredeni > 0 && (scho.CenaMany / 2) > pocitac.Mana) ? sch : scho;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            foreach (Schopnost s in pocitac.Schopnosti)
                            {
                                if (s.Druh == Druh.Vysati_many && pocitac.Mana < (pocitac.ManaMax - (s.Pouzij(pocitac, false) - souper.Brneni) * 2)
                                    && souper.Mana > ((s.Pouzij(pocitac, false) - souper.Brneni) * 2))
                                {
                                    sch = s.Cd > 0 || (efektyPocitac.Soustredeni == 0 && s.CenaMany > pocitac.Mana) || (efektyPocitac.Soustredeni > 0 && (s.CenaMany / 2) > pocitac.Mana) ? sch : s;
                                }
                            }



                            foreach (Schopnost s in pocitac.Schopnosti)
                            {
                                if (s.Druh == Druh.Magicke_soustredeni)
                                {
                                    sch = s.Cd > 0 || (efektyPocitac.Soustredeni == 0 && s.CenaMany > pocitac.Mana) || (efektyPocitac.Soustredeni > 0 && (s.CenaMany / 2) > pocitac.Mana) ? sch : s;
                                }
                            }

                            foreach (Schopnost s in pocitac.Schopnosti)
                            {
                                if (s.Druh == Druh.Lahvicka_Many && inventarPocitac.LahvickyMany > 0 && (pocitac.ManaMax - pocitac.Mana >= 100 || pocitac.Mana < 10))
                                {
                                    sch = s.Cd > 0 || (efektyPocitac.Soustredeni == 0 && s.CenaMany > pocitac.Mana) || (efektyPocitac.Soustredeni > 0 && (s.CenaMany / 2) > pocitac.Mana) ? sch : s;
                                }
                            }
                            foreach (Schopnost s in pocitac.Schopnosti)
                            {
                                if (s.Druh == Druh.Lahvicka_Zdravi && inventarPocitac.LahvickyZdravi > 0 && (pocitac.ZivotyMax - pocitac.Zivoty >= 100 || pocitac.Zivoty < 25))
                                {
                                    sch = s.Cd > 0 || (efektyPocitac.Soustredeni == 0 && s.CenaMany > pocitac.Mana) || (efektyPocitac.Soustredeni > 0 && (s.CenaMany / 2) > pocitac.Mana) ? sch : s;
                                }
                            }

                            foreach (Schopnost s in pocitac.Schopnosti)
                            {
                                if (s.Druh == Druh.Uder_Holi && souper.Zivoty <= (s.Pouzij(pocitac, false) - souper.Brneni))
                                {
                                    sch = s.Cd > 0 || (efektyPocitac.Soustredeni == 0 && s.CenaMany > pocitac.Mana) || (efektyPocitac.Soustredeni > 0 && (s.CenaMany / 2) > pocitac.Mana) ? sch : s;
                                }
                            }
                            foreach (Schopnost s in pocitac.Schopnosti)
                            {
                                if (s.Druh == Druh.Ohniva_Koule && souper.Zivoty <= (s.Pouzij(pocitac, false) - souper.Brneni / 2.0))
                                {
                                    sch = s.Cd > 0 || (efektyPocitac.Soustredeni == 0 && s.CenaMany > pocitac.Mana) || (efektyPocitac.Soustredeni > 0 && (s.CenaMany / 2) > pocitac.Mana) ? sch : s;
                                }
                            }


                            if (souperSch != null && ((souperSch.Druh == Druh.Magicky_sip && souperSch.Faze == 0) || (souperSch.Druh == Druh.Strelba_Lukem && souperSch.Faze == 0) ||
                                ((souperSch.Druh == Druh.Utok_Mecem || souperSch.Druh == Druh.Uder_stitem || souperSch.Druh == Druh.Vrh_sekerou ||
                                (souperSch.Druh == Druh.Berserk && souper.Zivoty / (double)souper.ZivotyMax < 0.4)) && efektySouper.Pokrik > 0) || souperSch.Druh == Druh.Ohniva_Koule ||
                                souperSch.Druh == Druh.Ledove_Kopi || souperSch.Druh == Druh.Vysati_zivota || souperSch.Druh == Druh.Vysati_many))
                            {
                                foreach (Schopnost s in pocitac.Schopnosti)
                                {
                                    if (s.Druh == Druh.Magicky_Stit)
                                    {
                                        sch = s.Cd > 0 || (efektyPocitac.Soustredeni == 0 && s.CenaMany > pocitac.Mana) || (efektyPocitac.Soustredeni > 0 && (s.CenaMany / 2) > pocitac.Mana) ? sch : s;
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

        private void ButtonUderStitemClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaHrajici = hrajici.DejSchopnost(Druh.Uder_stitem);
                    break;
                case Faze.VyberDruhy:
                    vybranaSouper = souper.DejSchopnost(Druh.Uder_stitem);
                    break;
            }
        }

        private void ButtonVrhSekerouClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaHrajici = hrajici.DejSchopnost(Druh.Vrh_sekerou);
                    break;
                case Faze.VyberDruhy:
                    vybranaSouper = souper.DejSchopnost(Druh.Vrh_sekerou);
                    break;
            }
        }

        private void ButtonBerserkClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaHrajici = hrajici.DejSchopnost(Druh.Berserk);
                    break;
                case Faze.VyberDruhy:
                    vybranaSouper = souper.DejSchopnost(Druh.Berserk);
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
        private void ButtonRychlostClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaHrajici = hrajici.DejSchopnost(Druh.Rychlost);
                    break;
                case Faze.VyberDruhy:
                    vybranaSouper = souper.DejSchopnost(Druh.Rychlost);
                    break;
            }
        }
        private void ButtonLesniBobuleClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaHrajici = hrajici.DejSchopnost(Druh.Lesni_bobule);
                    break;
                case Faze.VyberDruhy:
                    vybranaSouper = souper.DejSchopnost(Druh.Lesni_bobule);
                    break;
            }
        }

        private void ButtonJedovaSipkaClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaHrajici = hrajici.DejSchopnost(Druh.Jedova_sipka);
                    break;
                case Faze.VyberDruhy:
                    vybranaSouper = souper.DejSchopnost(Druh.Jedova_sipka);
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
        private void ButtonVysatiZivotaClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaHrajici = hrajici.DejSchopnost(Druh.Vysati_zivota);
                    break;
                case Faze.VyberDruhy:
                    vybranaSouper = souper.DejSchopnost(Druh.Vysati_zivota);
                    break;
            }
        }

        private void ButtonMagickeSoustredeniClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaHrajici = hrajici.DejSchopnost(Druh.Magicke_soustredeni);
                    break;
                case Faze.VyberDruhy:
                    vybranaSouper = souper.DejSchopnost(Druh.Magicke_soustredeni);
                    break;
            }
        }

        private void ButtonVysatiManyClickedHandler(object sender, EventArgs args)
        {
            switch (faze)
            {
                case Faze.VyberPrvni:
                    vybranaHrajici = hrajici.DejSchopnost(Druh.Vysati_many);
                    break;
                case Faze.VyberDruhy:
                    vybranaSouper = souper.DejSchopnost(Druh.Vysati_many);
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
