using System;
using Zacarovany_les.Classes.Pomocne;

namespace Zacarovany_les.Classes
{
    internal class Souboj
    {
        // První hráč je takový hráč, který začíná tah, druhým hráčem pak rozumíme toho kdo reaguje na schopnost vybranou prvním hráčem.
        // Panel s portrétem i vlastnostmi hráče, který je právě na tahu, je mírně zvýrazněn a soupeřův znevýrazněn
        // Útočník je hráč, který zaútočil na nepřítele na mapě a je umístěný v levé části obrazovky (v módu 1v1 může být i AI)
        // Obránce je AI (nebo může být i hráč v módu 1v1) a je umístěný v pravé části obrazovky
        // Souboj trvá dokud jedné z postav neklesne zdraví na 0, nebo méně.
        // Začínající hráč je náhodně vybrán
        public Postava Prvni { get; set; }
        public Postava Druhy { get; set; }
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
            if (Prvni == Obrance)
            {
                if (EfektyObrance.Horeni > 0)
                {
                    int poskozeni = ZautocNaObrance((int)Math.Round(Druhy.Inteligence / 3.0), true);
                    messagePrvni = Prvni.Name + " hoří za " + poskozeni + " poškození";
                    spravceMedii.Fireball.Play();
                    efekt = true;
                }
                if (EfektyObrance.Krvaceni > 0)
                {
                    int poskozeni = ZautocNaObrance((int)Math.Round(Druhy.Sila / 3.0), true);
                    messagePrvni = Prvni.Name + " krvácí za " + poskozeni + " poškození";
                    efekt = true;
                }
                if (EfektyObrance.Jed > 0)
                {
                    int poskozeni = ZautocNaObrance((int)Math.Round(Druhy.Obratnost / 3.0), true);
                    messagePrvni = Prvni.Name + " je otrávený za " + poskozeni + " poškození";
                    efekt = true;
                }
            }
            else
            {
                if (EfektyUtocnika.Horeni > 0)
                {
                    int poskozeni = ZautocNaUtocnika((int)Math.Round(Druhy.Inteligence / 3.0), true);
                    messagePrvni = Prvni.Name + " hoří za " + poskozeni + " poškození";
                    spravceMedii.Fireball.Play();
                    efekt = true;
                }
                if (EfektyUtocnika.Krvaceni > 0)
                {
                    int poskozeni = ZautocNaUtocnika((int)Math.Round(Druhy.Sila / 3.0), false);
                    messagePrvni = Prvni.Name + " krvácí za " + poskozeni + " poškození";
                    efekt = true;
                }
                if (EfektyUtocnika.Jed > 0)
                {
                    int poskozeni = ZautocNaUtocnika((int)Math.Round(Druhy.Obratnost / 3.0), false);
                    messagePrvni = Prvni.Name + " je otrávený za " + poskozeni + " poškození";
                    efekt = true;
                }
            }
            return messagePrvni;
        }
        public string EfektyDruhy(ref bool efekt, SpravceMedii spravceMedii)
        {
            string messageDruhy = "";
            if (Druhy == Obrance)
            {
                if (EfektyObrance.Horeni > 0)
                {
                    int poskozeni = ZautocNaObrance((int)Math.Round(Prvni.Inteligence / 3.0), true);
                    messageDruhy = Druhy.Name + " hoří za " + poskozeni + " poškození";
                    spravceMedii.Fireball.Play();
                    efekt = true;
                }
                if (EfektyObrance.Krvaceni > 0)
                {
                    int poskozeni = ZautocNaObrance((int)Math.Round(Prvni.Sila / 3.0), false);
                    messageDruhy = Druhy.Name + " krvácí za " + poskozeni + " poškození";
                    efekt = true;
                }
                if (EfektyObrance.Jed > 0)
                {
                    int poskozeni = ZautocNaObrance((int)Math.Round(Prvni.Obratnost / 3.0), false);
                    messageDruhy = Druhy.Name + " je otrávený za " + poskozeni + " poškození";
                    efekt = true;
                }
            }
            else
            {
                if (EfektyUtocnika.Horeni > 0)
                {
                    int poskozeni = ZautocNaUtocnika((int)Math.Round(Prvni.Inteligence / 3.0), true);
                    messageDruhy = Druhy.Name + " hoří za " + poskozeni + " poškození";
                    spravceMedii.Fireball.Play();
                    efekt = true;
                }
                if (EfektyUtocnika.Krvaceni > 0)
                {
                    int poskozeni = ZautocNaUtocnika((int)Math.Round(Prvni.Sila / 3.0), false);
                    messageDruhy = Druhy.Name + " krvácí za " + poskozeni + " poškození";
                    efekt = true;
                }
                if (EfektyUtocnika.Jed > 0)
                {
                    int poskozeni = ZautocNaUtocnika((int)Math.Round(Prvni.Obratnost / 3.0), false);
                    messageDruhy = Druhy.Name + " je otrávený za " + poskozeni + " poškození";
                    efekt = true;
                }
            }
            return messageDruhy;
        }

        public string VyberPrvniSchopnosti(ref Schopnost vybranaDruhy, ref Schopnost vybranaPrvni)
        {
            string messagePrvni = "";
            if (Prvni.Majitel == Majitel.Hrac && vybranaPrvni != null)
            {
                messagePrvni = Prvni.Name + " použije schopnost " + PomocneMetody.SchopnostToString(vybranaPrvni.Druh);
            }

            if (Prvni.Majitel != Majitel.Hrac)
            {
                vybranaPrvni ??= AI.VyberSchopnostAI(Prvni, vybranaDruhy, vybranaDruhy, Druhy, Prvni, this);
                messagePrvni = Prvni.Name + " použije schopnost " + PomocneMetody.SchopnostToString(vybranaPrvni.Druh);
            }
            return messagePrvni;
        }
        public string VyberDruheSchopnosti(ref Schopnost vybranaDruhy, ref Schopnost vybranaPrvni)
        {
            string messageDruhy = "";
            if (Druhy.Majitel == Majitel.Hrac && vybranaDruhy != null)
            {
                messageDruhy = Druhy.Name + " použije schopnost " + PomocneMetody.SchopnostToString(vybranaDruhy.Druh);
            }
            if (Druhy.Majitel != Majitel.Hrac)
            {
                vybranaDruhy ??= AI.VyberSchopnostAI(Druhy, vybranaPrvni, vybranaDruhy, Prvni, Prvni, this);
                messageDruhy = Druhy.Name + " použije schopnost " + PomocneMetody.SchopnostToString(vybranaDruhy.Druh);
            }
            return messageDruhy;
        }
       
        public string UtokSchopnosti(Postava naTahu, ref Schopnost vybranaNaTahu, ref Schopnost vybranaDruhy,  SpravceMedii spravceMedii)
        {
            Efekty efektyNaTahu = naTahu == Utocnik ? EfektyUtocnika : EfektyObrance;
            string message;
            if (efektyNaTahu.Omraceni == 0)
            {
                Postava druhy = naTahu == Utocnik ? Obrance : Utocnik;
                Efekty efektyDruhy = naTahu == Utocnik ? EfektyObrance: EfektyUtocnika;
                vybranaDruhy ??= new Schopnost(Druh.Zadna, 0, 0, 0, false);

                bool rychlost = ((efektyDruhy.Rychlost > 0) && (Kostka.Next(10) < (int)Math.Round(efektyDruhy.Rychlost * 6.0 / 4.0)));
                int poskozeni = naTahu.PouzijSchopnost(vybranaNaTahu.Druh);
                int mana = 0;

                if (((vybranaDruhy.Druh != Druh.Magicky_sip && vybranaDruhy.Faze != 0) || vybranaDruhy.Druh != Druh.Ohniva_Koule || vybranaDruhy.Druh != Druh.Ledove_Kopi || vybranaDruhy.Druh != Druh.Vysati_zivota || vybranaDruhy.Druh != Druh.Vysati_many)
                    && (vybranaNaTahu.Druh == Druh.Magicky_Stit || vybranaNaTahu.Druh == Druh.Obrana_Stitem || vybranaNaTahu.Druh == Druh.Uskok))
                {
                    message = naTahu.Name + " se brání schopností " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh);
                }
                else if (((vybranaDruhy.Druh == Druh.Magicky_sip && vybranaDruhy.Faze == 0) || vybranaDruhy.Druh == Druh.Ohniva_Koule || vybranaDruhy.Druh == Druh.Ledove_Kopi || vybranaDruhy.Druh == Druh.Vysati_zivota || vybranaDruhy.Druh == Druh.Vysati_many)
                   && (vybranaNaTahu.Druh == Druh.Magicky_Stit || vybranaNaTahu.Druh == Druh.Obrana_Stitem || vybranaNaTahu.Druh == Druh.Uskok))
                {
                    message = naTahu.Name + " se částečně brání schopností " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh);
                }
                else if (((vybranaDruhy.Druh == Druh.Magicky_Stit || vybranaDruhy.Druh == Druh.Obrana_Stitem || vybranaDruhy.Druh == Druh.Uskok) || rychlost)
                    && ((vybranaNaTahu.Druh == Druh.Magicky_sip && vybranaNaTahu.Faze == 0) || vybranaNaTahu.Druh == Druh.Ohniva_Koule || vybranaNaTahu.Druh == Druh.Ledove_Kopi || vybranaNaTahu.Druh == Druh.Vysati_zivota || vybranaNaTahu.Druh == Druh.Vysati_many))
                {
                    switch (vybranaNaTahu.Druh)
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
                    if (naTahu == Utocnik)
                    {
                        skPosk = ZautocNaObrance(poskozeni / 2, true);
                    }
                    else
                    {
                        skPosk = ZautocNaUtocnika(poskozeni / 2, true);
                    }
                    if (vybranaNaTahu.Druh == Druh.Vysati_zivota)
                    {
                        int zdravi = skPosk;
                        if (efektyNaTahu.Horeni > 0 || efektyNaTahu.Jed > 0 || efektyNaTahu.Krvaceni > 0)
                        {
                            zdravi = (int)Math.Round(skPosk * 2.0 / 3.0);
                        }
                        message = naTahu.Name + " částečně ubral schopností " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh) + " " + skPosk + " zdraví a vysál " + zdravi + " zdraví";

                        naTahu.PridejNeboUberZdravi(zdravi);
                    }
                    else if (vybranaNaTahu.Druh == Druh.Vysati_many)
                    {
                        mana = druhy.Mana - Math.Max(druhy.Mana - skPosk, 0);
                        if (efektyNaTahu.Horeni > 0 || efektyNaTahu.Jed > 0 || efektyNaTahu.Krvaceni > 0)
                        {
                            mana = (int)Math.Round(mana * 2.0 / 3.0);
                        }
                        message = naTahu.Name + " částečně ubral schopností " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh) + " " + skPosk + " zdraví a vysál " + mana + " many ";
                    }
                    else
                        message = naTahu.Name + " částečně útočí schopností " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh) + " za " + skPosk + " poškození";
                }
                else if (vybranaNaTahu.Druh == Druh.Regenerace)
                {
                    spravceMedii.Regen.Play();
                    if (efektyNaTahu.Pokrik > 0)
                    {
                        poskozeni *= 2;
                    }
                    if (efektyNaTahu.Horeni > 0 || efektyNaTahu.Jed > 0 || efektyNaTahu.Krvaceni > 0)
                    {
                        poskozeni = (int)Math.Round(poskozeni * 2.0 / 3.0);
                    }
                    naTahu.PridejNeboUberZdravi(poskozeni);
                    message = naTahu.Name + " regeneruje zdraví za " + poskozeni + " bodů schopností " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh);
                }
                else if (vybranaNaTahu.Druh == Druh.Lahvicka_Zdravi)
                {
                    spravceMedii.Regen.Play();
                    naTahu.Inventar.LahvickyZdravi--;
                    if (efektyNaTahu.Horeni > 0 || efektyNaTahu.Jed > 0 || efektyNaTahu.Krvaceni > 0)
                    {
                        poskozeni = (int)Math.Round(poskozeni * 2.0 / 3.0);
                    }
                    naTahu.PridejNeboUberZdravi(poskozeni);
                    message = naTahu.Name + " regeneruje zdravi za " + poskozeni + " bodů pomocí " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh);
                }
                else if (vybranaNaTahu.Druh == Druh.Lesni_bobule)
                {
                    spravceMedii.Regen.Play();
                    if (efektyNaTahu.Horeni > 0 || efektyNaTahu.Jed > 0 || efektyNaTahu.Krvaceni > 0)
                    {
                        poskozeni = (int)Math.Round(poskozeni * 2.0 / 3.0);
                    }
                    naTahu.PridejNeboUberZdravi(poskozeni);
                    message = naTahu.Name + " regeneruje zdravi za " + poskozeni + " bodů schopností " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh);
                }
                else if (vybranaNaTahu.Druh == Druh.Lahvicka_Many)
                {
                    spravceMedii.Regen.Play();
                    if (efektyNaTahu.Horeni > 0 || efektyNaTahu.Jed > 0 || efektyNaTahu.Krvaceni > 0)
                    {
                        poskozeni = (int)Math.Round(poskozeni * 2.0 / 3.0);
                    }
                    naTahu.PridejNeboUberManu(poskozeni);
                    naTahu.Inventar.LahvickyMany--;
                    message = naTahu.Name + " regeneruje manu za " + poskozeni + " bodů pomocí " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh);
                }
                else if (vybranaNaTahu.Druh == Druh.Bojovy_Pokrik)
                {
                    spravceMedii.Battlecry.Play();
                    efektyNaTahu.Pokrik += 2;
                    message = naTahu.Name + " zařval " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh);
                }

                else if (vybranaNaTahu.Druh == Druh.Magicke_soustredeni)
                {
                    spravceMedii.Battlecry.Play();
                    efektyNaTahu.Soustredeni += 4;
                    message = naTahu.Name + " se soustředí schopností " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh);
                }
                else if (vybranaNaTahu.Druh == Druh.Rychlost)
                {
                    spravceMedii.Battlecry.Play();
                    efektyNaTahu.Rychlost += 4;
                    message = naTahu.Name + " použil schopnost " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh);
                }
                else if (vybranaNaTahu.Druh == Druh.Strelba_Lukem && vybranaNaTahu.Faze == 1 || vybranaNaTahu.Druh == Druh.Magicky_sip && vybranaNaTahu.Faze == 1)
                {
                    message = naTahu.Name + " natahuje luk, aby použil schopnost " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh);
                    spravceMedii.BowPull.Play();
                }
                else if ((vybranaDruhy.Druh == Druh.Magicky_Stit || vybranaDruhy.Druh == Druh.Obrana_Stitem || vybranaDruhy.Druh == Druh.Uskok) &&
                    (vybranaNaTahu.Druh == Druh.Strelba_Lukem))
                {
                    message = naTahu.Name + " se schopností " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh) + " netrefil";
                    spravceMedii.BowMiss.Play();
                }
                else if (rychlost || vybranaDruhy.Druh == Druh.Magicky_Stit || vybranaDruhy.Druh == Druh.Obrana_Stitem || vybranaDruhy.Druh == Druh.Uskok)
                {
                    message = naTahu.Name + " se schopností " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh) + " netrefil";
                }
                else
                {

                    if (vybranaNaTahu.Druh == Druh.Ohniva_Koule)
                    {
                        spravceMedii.Fireball.Play();
                        efektyDruhy.Horeni += 2;
                    }
                    else if (vybranaNaTahu.Druh == Druh.Ledove_Kopi)
                    {
                        spravceMedii.Frostbolt.Play();
                        efektyDruhy.Mraz += 2;
                    }
                    else if (vybranaNaTahu.Druh == Druh.Vrh_sekerou)
                    {
                        spravceMedii.Hit.Play();
                        efektyDruhy.Krvaceni += 2;
                        if (efektyNaTahu.Pokrik > 0)
                            efektyDruhy.Krvaceni += 1;
                    }
                    else if (vybranaNaTahu.Druh == Druh.Uder_stitem)
                    {
                        spravceMedii.Hit.Play();
                        efektyDruhy.Omraceni += 1;
                        if (efektyNaTahu.Pokrik > 0)
                            efektyDruhy.Omraceni += 1;
                    }
                    else if (vybranaNaTahu.Druh == Druh.Jedova_sipka)
                    {
                        spravceMedii.Hit.Play();
                        efektyDruhy.Jed += 3;
                    }
                    else if (vybranaNaTahu.Druh == Druh.Berserk)
                    {
                        spravceMedii.Battlecry.Play();
                        spravceMedii.Hit.Play();
                    }
                    else
                    {
                        spravceMedii.Hit.Play();
                    }

                    if (efektyNaTahu.Pokrik > 0)
                    {
                        poskozeni *= 2;
                    }
                    int skPosk;
                    if (naTahu == Utocnik)
                    {
                        skPosk = ZautocNaObrance(poskozeni, vybranaNaTahu.Magicka);
                    }
                    else
                    {
                        skPosk = ZautocNaUtocnika(poskozeni, vybranaNaTahu.Magicka);
                    }
                    if (vybranaNaTahu.Druh == Druh.Vysati_zivota)
                    {
                        int zdravi = skPosk;
                        if (efektyNaTahu.Horeni > 0 || efektyNaTahu.Jed > 0 || efektyNaTahu.Krvaceni > 0)
                        {
                            zdravi = (int)Math.Round(skPosk * 2.0 / 3.0);
                        }
                        message = naTahu.Name + " ubral schopností " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh) + " " + skPosk + " zdraví a vysál " + zdravi + " zdraví";


                        naTahu.PridejNeboUberZdravi(zdravi);
                    }
                    else if (vybranaNaTahu.Druh == Druh.Vysati_many)
                    {
                        mana = druhy.Mana - Math.Max(druhy.Mana - skPosk, 0);
                        if (efektyNaTahu.Horeni > 0 || efektyNaTahu.Jed > 0 || efektyNaTahu.Krvaceni > 0)
                        {
                            mana = (int)Math.Round(mana * 2.0 / 3.0);
                        }
                        message = naTahu.Name + " ubral schopností " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh) + " " + skPosk + " zdraví a vysál " + mana + " many ";
                    }
                    else
                        message = naTahu.Name + " útočí schopností " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh) + " za " + skPosk + " poškození";
                }

                if (vybranaNaTahu.Faze == 0)
                {
                    if (naTahu == Utocnik)
                    {
                        if (EfektyUtocnika.Soustredeni > 0)
                            naTahu.PridejNeboUberManu(-vybranaNaTahu.CenaMany / 2);
                        else
                            naTahu.PridejNeboUberManu(-vybranaNaTahu.CenaMany);
                    }
                    else
                    {
                        if (EfektyObrance.Soustredeni > 0)
                            naTahu.PridejNeboUberManu(-vybranaNaTahu.CenaMany / 2);
                        else
                            naTahu.PridejNeboUberManu(-vybranaNaTahu.CenaMany);
                    }
                    if (vybranaNaTahu.Druh == Druh.Vysati_many)
                    {
                        druhy.PridejNeboUberManu(-mana);

                        naTahu.PridejNeboUberManu(mana);

                    }

                }
            }
            else
            {
                if (vybranaNaTahu != null)
                    vybranaNaTahu.Faze = vybranaNaTahu.FazeVychozi;
                vybranaNaTahu = new Schopnost(Druh.Zadna, 0, 0, 0, false);
                message = naTahu.Name + " je omráčený";
            }
            return message;
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
        public void ZhodnotSchopnosti(ref Schopnost vybranaPrvni, ref Schopnost vybranaDruhy)
        {
            Utocnik.ZhodnotSchopnosti();
            Obrance.ZhodnotSchopnosti();

            Schopnost pom1 = null;
            Schopnost pom2 = null;
            if (vybranaPrvni.Faze > 0)
            {

                if (Prvni.Efekty.Omraceni == 0)
                {
                    pom1 = vybranaPrvni;
                    vybranaPrvni.Faze--;
                }
                else
                {
                    vybranaPrvni.Faze = vybranaPrvni.FazeVychozi;
                }

            }
            else
            {
                vybranaPrvni.Faze = vybranaPrvni.FazeVychozi;
            }
            if (vybranaDruhy.Faze > 0)
            {

                if (Druhy.Efekty.Omraceni == 0)
                {
                    pom2 = vybranaDruhy;
                    vybranaDruhy.Faze--;
                }
                else
                {
                    vybranaDruhy.Faze = vybranaDruhy.FazeVychozi;
                }

            }
            else
            {
                vybranaDruhy.Faze = vybranaDruhy.FazeVychozi;
            }
            vybranaPrvni = pom2;
            vybranaDruhy = pom1;
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
