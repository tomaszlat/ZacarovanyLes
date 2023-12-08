using System;
using System.Collections.Generic;
using System.Text;
using Zacarovany_les.Classes.Pomocne;

namespace Zacarovany_les.Classes
{
    internal class Souboj
    {
        // Hrající hráč je takový hráč, který je momentálně na tahu, soupeřem pak rozumíme toho kdo na tahu momentálně není.
        // Hrajícího hráče ve hře poznáme tak, že je jeho panel s vlastnostmi a portrét mírně zvýrazněn a soupeřův znevýrazněn
        // Útočník je hráč, který zaútočil na nepřítele na mapě, je umístěný v levé části obrazovky, v módu 1v1 může být i AI
        // Obránce může být AI, nebo hráč v módu 1v1, je umístěný v pravé části obrazovky
        // Souboj trvá dokud jedné z postav neklesne zdraví na 0, nebo méně.
        // Začínající hráč je však zcela náhodný
        public Postava Hrajici { get; set; }
        public Postava Souper { get; set; }
        public Random Kostka { get; }
        public Postava Utocnik { get; }
        public Postava Obrance { get; }
        public Efekty EfektyUtocnika { get; }
        public Efekty EfektyObrance { get; }

        public int PocetKol { get; set; } = 0;
        public bool ZacinaUtocnik { get; } = true;

        public Souboj(Postava utocnik, Postava obrance)
        {
            Kostka = new Random();
            Utocnik = utocnik;
            Obrance = obrance;
            if (Kostka.Next(0, 2) == 1)
            {
                ZacinaUtocnik = false;
            }
            EfektyUtocnika = utocnik.Efekty;
            EfektyObrance = obrance.Efekty;
        }
        public string EfektyPrvni(ref bool efekt, SpravceMedii spravceMedii)
        {
            string messagePrvni = "";
            if (Hrajici == Obrance)
            {
                if (EfektyObrance.Horeni > 0)
                {
                    int poskozeni = ZautocNaObrance((int)Math.Round(Souper.Inteligence / 3.0), true);
                    messagePrvni = Hrajici.Name + " hoří za " + poskozeni + " poškození";
                    spravceMedii.Fireball.Play();
                    efekt = true;
                }
                if (EfektyObrance.Krvaceni > 0)
                {
                    int poskozeni = ZautocNaObrance((int)Math.Round(Souper.Sila / 3.0), true);
                    messagePrvni = Hrajici.Name + " krvácí za " + poskozeni + " poškození";
                    efekt = true;
                }
                if (EfektyObrance.Jed > 0)
                {
                    int poskozeni = ZautocNaObrance((int)Math.Round(Souper.Obratnost / 3.0), true);
                    messagePrvni = Hrajici.Name + " je otrávený za " + poskozeni + " poškození";
                    efekt = true;
                }
            }
            else
            {
                if (EfektyUtocnika.Horeni > 0)
                {
                    int poskozeni = ZautocNaUtocnika((int)Math.Round(Souper.Inteligence / 3.0), true);
                    messagePrvni = Hrajici.Name + " hoří za " + poskozeni + " poškození";
                    spravceMedii.Fireball.Play();
                    efekt = true;
                }
                if (EfektyUtocnika.Krvaceni > 0)
                {
                    int poskozeni = ZautocNaUtocnika((int)Math.Round(Souper.Sila / 3.0), false);
                    messagePrvni = Hrajici.Name + " krvácí za " + poskozeni + " poškození";
                    efekt = true;
                }
                if (EfektyUtocnika.Jed > 0)
                {
                    int poskozeni = ZautocNaUtocnika((int)Math.Round(Souper.Obratnost / 3.0), false);
                    messagePrvni = Hrajici.Name + " je otrávený za " + poskozeni + " poškození";
                    efekt = true;
                }
            }
            return messagePrvni;
        }
        public string EfektyDruhy(ref bool efekt, SpravceMedii spravceMedii)
        {
            string messageDruhy = "";
            if (Souper == Obrance)
            {
                if (EfektyObrance.Horeni > 0)
                {
                    int poskozeni = ZautocNaObrance((int)Math.Round(Hrajici.Inteligence / 3.0), true);
                    messageDruhy = Souper.Name + " hoří za " + poskozeni + " poškození";
                    spravceMedii.Fireball.Play();
                    efekt = true;
                }
                if (EfektyObrance.Krvaceni > 0)
                {
                    int poskozeni = ZautocNaObrance((int)Math.Round(Hrajici.Sila / 3.0), false);
                    messageDruhy = Souper.Name + " krvácí za " + poskozeni + " poškození";
                    efekt = true;
                }
                if (EfektyObrance.Jed > 0)
                {
                    int poskozeni = ZautocNaObrance((int)Math.Round(Hrajici.Obratnost / 3.0), false);
                    messageDruhy = Souper.Name + " je otrávený za " + poskozeni + " poškození";
                    efekt = true;
                }
            }
            else
            {
                if (EfektyUtocnika.Horeni > 0)
                {
                    int poskozeni = ZautocNaUtocnika((int)Math.Round(Hrajici.Inteligence / 3.0), true);
                    messageDruhy = Souper.Name + " hoří za " + poskozeni + " poškození";
                    spravceMedii.Fireball.Play();
                    efekt = true;
                }
                if (EfektyUtocnika.Krvaceni > 0)
                {
                    int poskozeni = ZautocNaUtocnika((int)Math.Round(Hrajici.Sila / 3.0), false);
                    messageDruhy = Souper.Name + " krvácí za " + poskozeni + " poškození";
                    efekt = true;
                }
                if (EfektyUtocnika.Jed > 0)
                {
                    int poskozeni = ZautocNaUtocnika((int)Math.Round(Hrajici.Obratnost / 3.0), false);
                    messageDruhy = Souper.Name + " je otrávený za " + poskozeni + " poškození";
                    efekt = true;
                }
            }
            return messageDruhy;
        }

        public string VyberPrvniSchopnosti(ref Schopnost vybranaSouper, ref Schopnost vybranaHrajici)
        {
            string messagePrvni = "";
            if (Hrajici.Majitel == Majitel.Hrac && vybranaHrajici != null)
            {
                messagePrvni = Hrajici.Name + " použije schopnost " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh);
            }

            if (Hrajici.Majitel != Majitel.Hrac)
            {
                if (vybranaHrajici == null)
                {
                    vybranaHrajici = AI.VyberSchopnostAI(Hrajici, vybranaSouper, vybranaSouper, Souper, Hrajici, this);
                }
                messagePrvni = Hrajici.Name + " použije schopnost " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh);
            }
            return messagePrvni;
        }
        public string VyberDruheSchopnosti(ref Schopnost vybranaSouper, ref Schopnost vybranaHrajici)
        {
            string messageDruhy = "";
            if (Souper.Majitel == Majitel.Hrac && vybranaSouper != null)
            {
                messageDruhy = Souper.Name + " použije schopnost " + PomocneMetody.SchopnostToString(vybranaSouper.Druh);
            }
            if (Souper.Majitel != Majitel.Hrac)
            {
                if (vybranaSouper == null)
                {
                    vybranaSouper = AI.VyberSchopnostAI(Souper, vybranaHrajici, vybranaSouper, Hrajici, Hrajici, this);
                }
                messageDruhy = Souper.Name + " použije schopnost " + PomocneMetody.SchopnostToString(vybranaSouper.Druh);
            }
            return messageDruhy;
        }
        public string UtokPrvniSchopnosti(Schopnost vybranaSouper, ref Schopnost vybranaHrajici, SpravceMedii spravceMedii)
        {
            Efekty efektyHrajici = Hrajici == Utocnik ? EfektyUtocnika : EfektyObrance;
            string messagePrvni;
            if (efektyHrajici.Omraceni == 0)
            {
                Efekty efektySouper = Souper == Utocnik ? EfektyUtocnika : EfektyObrance;

                bool rychlost = ((efektySouper.Rychlost > 0) && (Kostka.Next(10) < (int)Math.Round(efektySouper.Rychlost * 6.0 / 4.0)));
                int poskozeni = Hrajici.PouzijSchopnost(vybranaHrajici.Druh);
                int mana = 0;

                if (((vybranaSouper.Druh != Druh.Magicky_sip && vybranaSouper.Faze != 0) || vybranaSouper.Druh != Druh.Ohniva_Koule || vybranaSouper.Druh != Druh.Ledove_Kopi || vybranaSouper.Druh != Druh.Vysati_zivota || vybranaSouper.Druh != Druh.Vysati_many)
                    && (vybranaHrajici.Druh == Druh.Magicky_Stit || vybranaHrajici.Druh == Druh.Obrana_Stitem || vybranaHrajici.Druh == Druh.Uskok))
                {
                    messagePrvni = Hrajici.Name + " se brání schopností " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh);
                }
                else if (((vybranaSouper.Druh == Druh.Magicky_sip && vybranaSouper.Faze == 0) || vybranaSouper.Druh == Druh.Ohniva_Koule || vybranaSouper.Druh == Druh.Ledove_Kopi || vybranaSouper.Druh == Druh.Vysati_zivota || vybranaSouper.Druh == Druh.Vysati_many)
                   && (vybranaHrajici.Druh == Druh.Magicky_Stit || vybranaHrajici.Druh == Druh.Obrana_Stitem || vybranaHrajici.Druh == Druh.Uskok))
                {
                    messagePrvni = Hrajici.Name + " se částečně brání schopností " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh);
                }
                else if (((vybranaSouper.Druh == Druh.Magicky_Stit || vybranaSouper.Druh == Druh.Obrana_Stitem || vybranaSouper.Druh == Druh.Uskok) || rychlost)
                    && ((vybranaHrajici.Druh == Druh.Magicky_sip && vybranaHrajici.Faze == 0) || vybranaHrajici.Druh == Druh.Ohniva_Koule || vybranaHrajici.Druh == Druh.Ledove_Kopi || vybranaHrajici.Druh == Druh.Vysati_zivota || vybranaHrajici.Druh == Druh.Vysati_many))
                {
                    switch (vybranaHrajici.Druh)
                    {
                        case Druh.Ohniva_Koule:
                            spravceMedii.Fireball.Play();
                            break;
                        case Druh.Ledove_Kopi:
                            spravceMedii.Frostbolt.Play();
                            break;
                        case Druh.Vysati_zivota:
                            spravceMedii.Fireball.Play();
                            break;
                        case Druh.Vysati_many:
                            spravceMedii.Frostbolt.Play();
                            break;
                        default:
                            spravceMedii.Hit.Play();
                            break;
                    }
                    int skPosk;
                    if (Hrajici == Utocnik)
                    {
                        skPosk = ZautocNaObrance(poskozeni / 2, true);
                    }
                    else
                    {
                        skPosk = ZautocNaUtocnika(poskozeni / 2, true);
                    }
                    if (vybranaHrajici.Druh == Druh.Vysati_zivota)
                    {
                        messagePrvni = Hrajici.Name + " částečně vysál pomocí schopnosti " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh) + " " + skPosk + " zdraví ";
                        if (efektyHrajici.Horeni > 0 || efektyHrajici.Jed > 0 || efektyHrajici.Krvaceni > 0)
                        {
                            skPosk = (int)Math.Round(skPosk * 2.0 / 3.0);
                        }
                        Hrajici.PridejNeboUberZdravi(skPosk);
                    }
                    else if (vybranaHrajici.Druh == Druh.Vysati_many)
                    {
                        mana = Souper.Mana - Math.Max(Souper.Mana - skPosk * 2, 0);
                        messagePrvni = Hrajici.Name + " částečně ubral pomocí schopnosti " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh) + " " + skPosk + " zdraví a vysál " + mana + " many ";
                    }
                    else
                        messagePrvni = Hrajici.Name + " částečně útočí schopností " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh) + " za " + skPosk + " poškození";
                }
                else if (vybranaHrajici.Druh == Druh.Regenerace)
                {
                    spravceMedii.Regen.Play();
                    if (efektyHrajici.Pokrik > 0)
                    {
                        poskozeni *= 2;
                    }
                    if (efektyHrajici.Horeni > 0 || efektyHrajici.Jed > 0 || efektyHrajici.Krvaceni > 0)
                    {
                        poskozeni = (int)Math.Round(poskozeni * 2.0 / 3.0);
                    }
                    Hrajici.PridejNeboUberZdravi(poskozeni);
                    messagePrvni = Hrajici.Name + " regeneruje zdraví za " + poskozeni + " bodů pomocí schopnosti " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh);
                }
                else if (vybranaHrajici.Druh == Druh.Lahvicka_Zdravi)
                {
                    spravceMedii.Regen.Play();
                    Hrajici.Inventar.LahvickyZdravi--;
                    if (efektyHrajici.Horeni > 0 || efektyHrajici.Jed > 0 || efektyHrajici.Krvaceni > 0)
                    {
                        poskozeni = (int)Math.Round(poskozeni * 2.0 / 3.0);
                    }
                    Hrajici.PridejNeboUberZdravi(poskozeni);
                    messagePrvni = Hrajici.Name + " regeneruje zdravi za " + poskozeni + " bodů pomocí " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh);
                }
                else if (vybranaHrajici.Druh == Druh.Lesni_bobule)
                {
                    spravceMedii.Regen.Play();
                    if (efektyHrajici.Horeni > 0 || efektyHrajici.Jed > 0 || efektyHrajici.Krvaceni > 0)
                    {
                        poskozeni = (int)Math.Round(poskozeni * 2.0 / 3.0);
                    }
                    Hrajici.PridejNeboUberZdravi(poskozeni);
                    messagePrvni = Hrajici.Name + " regeneruje zdravi za " + poskozeni + " bodů pomocí schopnosti " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh);
                }
                else if (vybranaHrajici.Druh == Druh.Lahvicka_Many)
                {
                    spravceMedii.Regen.Play();
                    if (efektyHrajici.Horeni > 0 || efektyHrajici.Jed > 0 || efektyHrajici.Krvaceni > 0)
                    {
                        poskozeni = (int)Math.Round(poskozeni * 2.0 / 3.0);
                    }
                    Hrajici.PridejNeboUberManu(poskozeni);
                    Hrajici.Inventar.LahvickyMany--;
                    messagePrvni = Hrajici.Name + " regeneruje manu za " + poskozeni + " bodů pomocí " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh);
                }
                else if (vybranaHrajici.Druh == Druh.Bojovy_Pokrik)
                {
                    spravceMedii.Battlecry.Play();
                    efektyHrajici.Pokrik += 2;
                    messagePrvni = Hrajici.Name + " zařval " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh);
                }

                else if (vybranaHrajici.Druh == Druh.Magicke_soustredeni)
                {
                    spravceMedii.Battlecry.Play();
                    efektyHrajici.Soustredeni += 4;
                    messagePrvni = Hrajici.Name + " se soustředí schopnosti " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh);
                }
                else if (vybranaHrajici.Druh == Druh.Rychlost)
                {
                    spravceMedii.Battlecry.Play();
                    efektyHrajici.Rychlost += 4;
                    messagePrvni = Hrajici.Name + " použil schopnost " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh);
                }
                else if (vybranaHrajici.Druh == Druh.Strelba_Lukem && vybranaHrajici.Faze == 1 || vybranaHrajici.Druh == Druh.Magicky_sip && vybranaHrajici.Faze == 1)
                {
                    messagePrvni = Hrajici.Name + " natahuje luk, aby použil schopnost " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh);
                    spravceMedii.BowPull.Play();
                }
                else if ((vybranaSouper.Druh == Druh.Magicky_Stit || vybranaSouper.Druh == Druh.Obrana_Stitem || vybranaSouper.Druh == Druh.Uskok) &&
                    (vybranaHrajici.Druh == Druh.Strelba_Lukem))
                {
                    messagePrvni = Hrajici.Name + " se pomocí schopnosti " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh) + " netrefil";
                    spravceMedii.BowMiss.Play();
                }
                else if (rychlost || vybranaSouper.Druh == Druh.Magicky_Stit || vybranaSouper.Druh == Druh.Obrana_Stitem || vybranaSouper.Druh == Druh.Uskok)
                {
                    messagePrvni = Hrajici.Name + " se pomocí schopnosti " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh) + " netrefil";
                }
                else
                {

                    if (vybranaHrajici.Druh == Druh.Ohniva_Koule)
                    {
                        spravceMedii.Fireball.Play();
                        efektySouper.Horeni += 2;
                    }
                    else if (vybranaHrajici.Druh == Druh.Ledove_Kopi)
                    {
                        spravceMedii.Frostbolt.Play();
                        efektySouper.Mraz += 2;
                    }
                    else if (vybranaHrajici.Druh == Druh.Vrh_sekerou)
                    {
                        spravceMedii.Hit.Play();
                        efektySouper.Krvaceni += 2;
                        if (efektyHrajici.Pokrik > 0)
                            efektySouper.Krvaceni += 1;
                    }
                    else if (vybranaHrajici.Druh == Druh.Uder_stitem)
                    {
                        spravceMedii.Hit.Play();
                        efektySouper.Omraceni += 1;
                        if (efektyHrajici.Pokrik > 0)
                            efektySouper.Omraceni += 1;
                    }
                    else if (vybranaHrajici.Druh == Druh.Jedova_sipka)
                    {
                        spravceMedii.Hit.Play();
                        efektySouper.Jed += 3;
                    }
                    else if (vybranaHrajici.Druh == Druh.Berserk)
                    {
                        spravceMedii.Battlecry.Play();
                        spravceMedii.Hit.Play();
                    }
                    else
                    {
                        spravceMedii.Hit.Play();
                    }



                    if (efektyHrajici.Pokrik > 0)
                    {
                        poskozeni *= 2;
                    }
                    int skPosk;
                    if (Hrajici == Utocnik)
                    {
                        skPosk = ZautocNaObrance(poskozeni, vybranaHrajici.Magicka);
                    }
                    else
                    {
                        skPosk = ZautocNaUtocnika(poskozeni, vybranaHrajici.Magicka);
                    }
                    if (vybranaHrajici.Druh == Druh.Vysati_zivota)
                    {
                        messagePrvni = Hrajici.Name + " vysál pomocí schopnosti " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh) + " " + skPosk + " zdraví ";
                        if (efektyHrajici.Horeni > 0 || efektyHrajici.Jed > 0 || efektyHrajici.Krvaceni > 0)
                        {
                            skPosk = (int)Math.Round(skPosk * 2.0 / 3.0);
                        }
                        Hrajici.PridejNeboUberZdravi(skPosk);
                    }
                    else if (vybranaHrajici.Druh == Druh.Vysati_many)
                    {
                        mana = Souper.Mana - Math.Max(Souper.Mana - skPosk * 2, 0);
                        messagePrvni = Hrajici.Name + " ubral pomocí schopnosti " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh) + " " + skPosk + " zdraví a vysál " + mana + " many ";
                    }
                    else
                        messagePrvni = Hrajici.Name + " útočí schopností " + PomocneMetody.SchopnostToString(vybranaHrajici.Druh) + " za " + skPosk + " poškození";
                }

                if (vybranaHrajici.Faze == 0)
                {
                    if (Hrajici == Utocnik)
                    {
                        if (EfektyUtocnika.Soustredeni > 0)
                            Hrajici.PridejNeboUberManu(-vybranaHrajici.CenaMany / 2);
                        else
                            Hrajici.PridejNeboUberManu(-vybranaHrajici.CenaMany);
                    }
                    else
                    {
                        if (EfektyObrance.Soustredeni > 0)
                            Hrajici.PridejNeboUberManu(-vybranaHrajici.CenaMany / 2);
                        else
                            Hrajici.PridejNeboUberManu(-vybranaHrajici.CenaMany);
                    }
                    if (vybranaHrajici.Druh == Druh.Vysati_many)
                    {
                        Souper.PridejNeboUberManu(-mana);
                        if (efektyHrajici.Horeni > 0 || efektyHrajici.Jed > 0 || efektyHrajici.Krvaceni > 0)
                        {
                            mana = (int)Math.Round(mana * 2.0 / 3.0);
                        }
                        Hrajici.PridejNeboUberManu(mana);

                    }

                }
            }
            else
            {
                if (vybranaHrajici != null)
                    vybranaHrajici.Faze = vybranaHrajici.FazeVychozi;
                vybranaHrajici = new Schopnost(Druh.Zadna, 0, 0, 0, false);
                messagePrvni = Hrajici.Name + " je omráčený ";
            }
            return messagePrvni;
        }
        public string UtokDruheSchopnosti(ref Schopnost vybranaSouper, Schopnost vybranaHrajici, SpravceMedii spravceMedii)
        {
            Efekty efektySouper = Souper == Utocnik ? EfektyUtocnika : EfektyObrance;
            string messageDruhy;
            if (efektySouper.Omraceni == 0)
            {
                Efekty efektyHrajici = Hrajici == Utocnik ? EfektyUtocnika : EfektyObrance;
                bool rychlost = ((efektyHrajici.Rychlost > 0) && (Kostka.Next(10) < (int)Math.Round(efektyHrajici.Rychlost * 6.0 / 4.0)));
                int poskozeni = Souper.PouzijSchopnost(vybranaSouper.Druh);
                int mana = 0;

                if (((vybranaHrajici.Druh != Druh.Magicky_sip && vybranaHrajici.Faze != 0) || vybranaHrajici.Druh != Druh.Ohniva_Koule || vybranaHrajici.Druh != Druh.Ledove_Kopi || vybranaHrajici.Druh != Druh.Vysati_zivota || vybranaHrajici.Druh != Druh.Vysati_many)
                    && (vybranaSouper.Druh == Druh.Magicky_Stit || vybranaSouper.Druh == Druh.Obrana_Stitem || vybranaSouper.Druh == Druh.Uskok))
                {
                    messageDruhy = Souper.Name + " se brání schopností " + PomocneMetody.SchopnostToString(vybranaSouper.Druh);
                }
                else if (((vybranaHrajici.Druh == Druh.Magicky_sip && vybranaHrajici.Faze == 0) || vybranaHrajici.Druh == Druh.Ohniva_Koule || vybranaHrajici.Druh == Druh.Ledove_Kopi || vybranaHrajici.Druh == Druh.Vysati_zivota || vybranaHrajici.Druh == Druh.Vysati_many)
                   && (vybranaSouper.Druh == Druh.Magicky_Stit || vybranaSouper.Druh == Druh.Obrana_Stitem || vybranaSouper.Druh == Druh.Uskok))
                {
                    messageDruhy = Souper.Name + " se částečně brání schopností " + PomocneMetody.SchopnostToString(vybranaSouper.Druh);
                }
                else if (((vybranaHrajici.Druh == Druh.Magicky_Stit || vybranaHrajici.Druh == Druh.Obrana_Stitem || vybranaHrajici.Druh == Druh.Uskok) || rychlost)
                    && ((vybranaSouper.Druh == Druh.Magicky_sip && vybranaSouper.Faze == 0) || vybranaSouper.Druh == Druh.Ohniva_Koule || vybranaSouper.Druh == Druh.Ledove_Kopi || vybranaSouper.Druh == Druh.Vysati_zivota || vybranaSouper.Druh == Druh.Vysati_many))
                {
                    switch (vybranaSouper.Druh)
                    {
                        case Druh.Ohniva_Koule:
                            spravceMedii.Fireball.Play();
                            break;
                        case Druh.Ledove_Kopi:
                            spravceMedii.Frostbolt.Play();
                            break;
                        case Druh.Vysati_zivota:
                            spravceMedii.Fireball.Play();
                            break;
                        case Druh.Vysati_many:
                            spravceMedii.Frostbolt.Play();
                            break;
                        default:
                            spravceMedii.Hit.Play();
                            break;
                    }

                    int skPosk;

                    if (Souper == Utocnik)
                    {
                        skPosk = ZautocNaObrance(poskozeni / 2, true);
                    }
                    else
                    {
                        skPosk = ZautocNaUtocnika(poskozeni / 2, true);
                    }
                    if (vybranaSouper.Druh == Druh.Vysati_zivota)
                    {
                        messageDruhy = Souper.Name + " částečně vysál pomocí schopnosti " + PomocneMetody.SchopnostToString(vybranaSouper.Druh) + " " + skPosk + " zdraví ";
                        if (efektySouper.Horeni > 0 || efektySouper.Jed > 0 || efektySouper.Krvaceni > 0)
                        {
                            skPosk = (int)Math.Round(skPosk * 2.0 / 3.0);
                        }
                        Souper.PridejNeboUberZdravi(skPosk);
                    }
                    else if (vybranaSouper.Druh == Druh.Vysati_many)
                    {
                        mana = Hrajici.Mana - Math.Max(Hrajici.Mana - skPosk * 2, 0);
                        messageDruhy = Souper.Name + " částečně ubral pomocí schopnosti " + PomocneMetody.SchopnostToString(vybranaSouper.Druh) + " " + skPosk + " zdraví a vysál " + mana + " many ";
                    }
                    else
                        messageDruhy = Souper.Name + " částečně útočí schopností " + PomocneMetody.SchopnostToString(vybranaSouper.Druh) + " za " + skPosk + " poškození";
                }
                else if (vybranaSouper.Druh == Druh.Regenerace)
                {
                    spravceMedii.Regen.Play();
                    if (efektySouper.Pokrik > 0)
                    {
                        poskozeni *= 2;
                    }
                    if (efektySouper.Horeni > 0 || efektySouper.Jed > 0 || efektySouper.Krvaceni > 0)
                    {
                        poskozeni = (int)Math.Round(poskozeni * 2.0 / 3.0);
                    }
                    Souper.PridejNeboUberZdravi(poskozeni);
                    messageDruhy = Souper.Name + " regeneruje zdraví za " + poskozeni + " bodů pomocí schopnosti " + PomocneMetody.SchopnostToString(vybranaSouper.Druh);
                }
                else if (vybranaSouper.Druh == Druh.Lahvicka_Zdravi)
                {
                    spravceMedii.Regen.Play();
                    Souper.Inventar.LahvickyZdravi--;
                    if (efektySouper.Horeni > 0 || efektySouper.Jed > 0 || efektySouper.Krvaceni > 0)
                    {
                        poskozeni = (int)Math.Round(poskozeni * 2.0 / 3.0);
                    }
                    Souper.PridejNeboUberZdravi(poskozeni);
                    messageDruhy = Souper.Name + " regeneruje zdravi za " + poskozeni + " bodů pomocí " + PomocneMetody.SchopnostToString(vybranaSouper.Druh);
                }
                else if (vybranaSouper.Druh == Druh.Lesni_bobule)
                {
                    spravceMedii.Regen.Play();
                    if (efektySouper.Horeni > 0 || efektySouper.Jed > 0 || efektySouper.Krvaceni > 0)
                    {
                        poskozeni = (int)Math.Round(poskozeni * 2.0 / 3.0);
                    }
                    Souper.PridejNeboUberZdravi(poskozeni);
                    messageDruhy = Souper.Name + " regeneruje zdravi za " + poskozeni + " bodů pomocí schopnosti " + PomocneMetody.SchopnostToString(vybranaSouper.Druh);
                }
                else if (vybranaSouper.Druh == Druh.Lahvicka_Many)
                {
                    spravceMedii.Regen.Play();
                    Souper.Inventar.LahvickyMany--;
                    if (efektySouper.Horeni > 0 || efektySouper.Jed > 0 || efektySouper.Krvaceni > 0)
                    {
                        poskozeni = (int)Math.Round(poskozeni * 2.0 / 3.0);
                    }
                    Souper.PridejNeboUberManu(poskozeni);
                    messageDruhy = Souper.Name + " regeneruje manu za " + poskozeni + " bodů pomocí " + PomocneMetody.SchopnostToString(vybranaSouper.Druh);
                }
                else if (vybranaSouper.Druh == Druh.Magicke_soustredeni)
                {
                    spravceMedii.Battlecry.Play();
                    efektySouper.Soustredeni += 4;

                    messageDruhy = Souper.Name + " se soustředí schopnosti " + PomocneMetody.SchopnostToString(vybranaSouper.Druh);
                }
                else if (vybranaSouper.Druh == Druh.Rychlost)
                {
                    spravceMedii.Battlecry.Play();
                    efektySouper.Rychlost += 4;
                    messageDruhy = Souper.Name + " použil schopnost " + PomocneMetody.SchopnostToString(vybranaSouper.Druh);
                }
                else if (vybranaSouper.Druh == Druh.Bojovy_Pokrik)
                {
                    spravceMedii.Battlecry.Play();
                    efektySouper.Pokrik += 2;
                    messageDruhy = Souper.Name + " zařval " + PomocneMetody.SchopnostToString(vybranaSouper.Druh);
                }
                else if (vybranaSouper.Druh == Druh.Strelba_Lukem && vybranaSouper.Faze == 1 || vybranaSouper.Druh == Druh.Magicky_sip && vybranaSouper.Faze == 1)
                {
                    messageDruhy = Souper.Name + " natahuje luk, aby použil schopnost " + PomocneMetody.SchopnostToString(vybranaSouper.Druh);
                    spravceMedii.BowPull.Play();
                }
                else if ((vybranaHrajici.Druh == Druh.Magicky_Stit || vybranaHrajici.Druh == Druh.Obrana_Stitem || vybranaHrajici.Druh == Druh.Uskok) &&
                    (vybranaSouper.Druh == Druh.Strelba_Lukem))
                {
                    messageDruhy = Souper.Name + " se pomocí schopnosti " + PomocneMetody.SchopnostToString(vybranaSouper.Druh) + " netrefil";
                    spravceMedii.BowMiss.Play();
                }
                else if (rychlost || vybranaHrajici.Druh == Druh.Magicky_Stit || vybranaHrajici.Druh == Druh.Obrana_Stitem || vybranaHrajici.Druh == Druh.Uskok)
                {
                    messageDruhy = Souper.Name + " se pomocí schopnosti " + PomocneMetody.SchopnostToString(vybranaSouper.Druh) + " netrefil";
                }
                else
                {
                    if (vybranaSouper.Druh == Druh.Ohniva_Koule)
                    {
                        spravceMedii.Fireball.Play();
                        efektyHrajici.Horeni += 2;
                    }
                    else if (vybranaSouper.Druh == Druh.Ledove_Kopi)
                    {
                        spravceMedii.Frostbolt.Play();
                        efektyHrajici.Mraz += 2;
                    }
                    else if (vybranaSouper.Druh == Druh.Vrh_sekerou)
                    {
                        spravceMedii.Hit.Play();
                        efektyHrajici.Krvaceni += 2;
                        if (efektySouper.Pokrik > 0)
                            efektyHrajici.Krvaceni += 1;
                    }
                    else if (vybranaSouper.Druh == Druh.Uder_stitem)
                    {
                        spravceMedii.Hit.Play();
                        efektyHrajici.Omraceni += 1;
                        if (efektySouper.Pokrik > 0)
                            efektyHrajici.Omraceni += 1;
                    }
                    else if (vybranaSouper.Druh == Druh.Jedova_sipka)
                    {
                        spravceMedii.Hit.Play();
                        efektyHrajici.Jed += 3;
                    }
                    else if (vybranaSouper.Druh == Druh.Berserk)
                    {
                        spravceMedii.Battlecry.Play();
                        spravceMedii.Hit.Play();
                    }
                    else
                    {
                        spravceMedii.Hit.Play();
                    }


                    if (efektySouper.Pokrik > 0)
                    {
                        poskozeni *= 2;
                    }
                    int skPosk;
                    if (Souper == Utocnik)
                    {
                        skPosk = ZautocNaObrance(poskozeni, vybranaSouper.Magicka);
                    }
                    else
                    {
                        skPosk = ZautocNaUtocnika(poskozeni, vybranaSouper.Magicka);
                    }
                    if (vybranaSouper.Druh == Druh.Vysati_zivota)
                    {
                        messageDruhy = Souper.Name + " vysál pomocí schopnosti " + PomocneMetody.SchopnostToString(vybranaSouper.Druh) + " " + skPosk + " zdraví ";
                        if (efektySouper.Horeni > 0 || efektySouper.Jed > 0 || efektySouper.Krvaceni > 0)
                        {
                            skPosk = (int)Math.Round(skPosk * 2.0 / 3.0);
                        }
                        Souper.PridejNeboUberZdravi(skPosk);
                    }
                    else if (vybranaSouper.Druh == Druh.Vysati_many)
                    {
                        mana = Hrajici.Mana - Math.Max(Hrajici.Mana - skPosk * 2, 0);
                        messageDruhy = Souper.Name + "  ubral pomocí schopnosti " + PomocneMetody.SchopnostToString(vybranaSouper.Druh) + " " + skPosk + " zdraví a vysál " + mana + " many ";
                    }
                    else
                        messageDruhy = Souper.Name + " útočí schopností " + PomocneMetody.SchopnostToString(vybranaSouper.Druh) + " za " + skPosk + " poškození";
                }

                if (vybranaSouper.Faze == 0)
                {
                    if (Souper == Utocnik)
                    {
                        if (EfektyUtocnika.Soustredeni > 0)
                            Souper.PridejNeboUberManu(-vybranaSouper.CenaMany / 2);
                        else
                            Souper.PridejNeboUberManu(-vybranaSouper.CenaMany);
                    }
                    else
                    {
                        if (EfektyObrance.Soustredeni > 0)
                            Souper.PridejNeboUberManu(-vybranaSouper.CenaMany / 2);
                        else
                            Souper.PridejNeboUberManu(-vybranaSouper.CenaMany);
                    }
                    if (vybranaSouper.Druh == Druh.Vysati_many)
                    {
                        Hrajici.PridejNeboUberManu(-mana);
                        if (efektySouper.Horeni > 0 || efektySouper.Jed > 0 || efektySouper.Krvaceni > 0)
                        {
                            mana = (int)Math.Round(mana * 2.0 / 3.0);
                        }
                        Souper.PridejNeboUberManu(mana);
                    }
                }
            }
            else
            {
                if (vybranaSouper != null)
                    vybranaSouper.Faze = vybranaSouper.FazeVychozi;
                vybranaSouper = new Schopnost(Druh.Zadna, 0, 0, 0, false);
                messageDruhy = Souper.Name + " je omráčený";
            }
            return messageDruhy;
        }
        public void ZhodnotEfekty()
        {
            EfektyUtocnika.ZhodnotEfekty();
            EfektyObrance.ZhodnotEfekty();
        }
        public void ResetEfekty()
        {
            EfektyUtocnika.ResetEfekty();
            EfektyObrance.ResetEfekty();
        }
        public void Reset()
        {
            ResetEfekty();
            Utocnik.Reset();
            Obrance.Reset();
        }
        public void ZhodnotSchopnosti()
        {
            Utocnik.ZhodnotSchopnosti();
            Obrance.ZhodnotSchopnosti();
        }
        public int ZautocNaObrance(int poskozeni, bool magicke)
        {
            return Obrance.ObdrzPoskozeni(poskozeni, magicke);
        }

        public int ZautocNaUtocnika(int poskozeni, bool magicke)
        {
            return Utocnik.ObdrzPoskozeni(poskozeni, magicke);
        }
    }
}
