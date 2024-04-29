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
        public string AplikujEfekty(Postava cil, ref bool efekt, SpravceMedii spravceMedii)
        {
            string message = "";
            Postava utocnik = cil == Prvni ? Druhy : Prvni;
            Efekty efektyCil = cil == Obrance ? EfektyObrance : EfektyUtocnika;
            if (efektyCil.Horeni > 0)
            {
                int poskozeni = ZautocNaPostavu(ref cil, (int)Math.Round(utocnik.Inteligence / 3.0), true);
                message = cil.Name + " hoří za " + poskozeni + " poškození";
                spravceMedii.Fireball.Play();
                efekt = true;
            }
            if (efektyCil.Krvaceni > 0)
            {
                int poskozeni = ZautocNaPostavu(ref cil, (int)Math.Round(utocnik.Sila / 3.0), false);
                message = cil.Name + " krvácí za " + poskozeni + " poškození";
                efekt = true;
            }
            if (efektyCil.Jed > 0)
            {
                int poskozeni = ZautocNaPostavu(ref cil, (int)Math.Round(utocnik.Obratnost / 3.0), false);
                message = cil.Name + " je otrávený za " + poskozeni + " poškození";
                efekt = true;
            }
            return message;
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

        public string UtokSchopnosti(ref Postava naTahu, ref Postava cil, ref Schopnost vybranaNaTahu, ref Schopnost vybranaDruhy, ref SpravceMedii spravceMedii)
        {
            Efekty efektyNaTahu = naTahu == Utocnik ? EfektyUtocnika : EfektyObrance;
            string message;
            if (efektyNaTahu.Omraceni == 0)
            {
                Postava druhy = naTahu == Utocnik ? Obrance : Utocnik;
                Efekty efektyDruhy = naTahu == Utocnik ? EfektyObrance : EfektyUtocnika;
                vybranaDruhy ??= new Schopnost(Druh.Zadna, 0, 0, 0, false);

                bool rychlost = (efektyDruhy.Rychlost > 0) && (Kostka.Next(10) < (int)Math.Round(efektyDruhy.Rychlost * 6.0 / 4.0));
                int poskozeni = naTahu.PouzijSchopnost(vybranaNaTahu.Druh);

                int skPosk;
                int zdravi;
                int mana;


                switch (vybranaNaTahu.Druh)
                {
                    case Druh.Obrana_Stitem:
                    case Druh.Uskok:
                    case Druh.Magicky_Stit:
                        if (vybranaDruhy.Magicka && vybranaDruhy.Faze == 0)
                        {
                            message = naTahu.Name + " se částečně brání schopností " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh);
                        }
                        else
                        {
                            message = naTahu.Name + " se brání schopností " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh);
                        }
                        break;
                    case Druh.Utok_Mecem:
                    case Druh.Berserk:
                    case Druh.Bodnuti_Dykou:
                    case Druh.Uder_Holi:
                    case Druh.Uder_stitem:
                    case Druh.Vrh_sekerou:
                    case Druh.Jedova_sipka:
                        if (BraniSe(vybranaDruhy, rychlost))
                        {
                            spravceMedii.BowMiss.Play();
                            message = naTahu.Name + " se schopností " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh) + " netrefil";
                        }
                        else
                        {
                            spravceMedii.Hit.Play();
                            skPosk = ZautocNaPostavu(ref cil, efektyNaTahu.Pokrik > 0 ? poskozeni * 2 : poskozeni, vybranaNaTahu.Magicka);
                            message = naTahu.Name + " útočí schopností " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh) + " za " + skPosk + " poškození";
                            if (vybranaNaTahu.Druh == Druh.Berserk)
                                spravceMedii.Battlecry.Play();
                            if (vybranaNaTahu.Druh == Druh.Uder_stitem)
                                efektyDruhy.Omraceni += efektyNaTahu.Pokrik > 0 ? 2 : 1;
                            if (vybranaNaTahu.Druh == Druh.Vrh_sekerou)
                                efektyDruhy.Krvaceni += efektyNaTahu.Pokrik > 0 ? 3 : 2;
                            if (vybranaNaTahu.Druh == Druh.Jedova_sipka)
                                efektyDruhy.Jed += 3;
                        }
                        break;
                    case Druh.Regenerace:
                        spravceMedii.Regen.Play();
                        poskozeni = efektyNaTahu.Pokrik > 0 ? poskozeni * 2 : poskozeni;
                        if (efektyNaTahu.Horeni > 0 || efektyNaTahu.Jed > 0 || efektyNaTahu.Krvaceni > 0)
                        {
                            poskozeni = (int)Math.Round(poskozeni * 2.0 / 3.0);
                        }
                        naTahu.PridejNeboUberZdravi(poskozeni);
                        message = naTahu.Name + " regeneruje zdraví za " + poskozeni + " bodů schopností " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh);

                        break;
                    case Druh.Bojovy_Pokrik:
                        spravceMedii.Battlecry.Play();
                        efektyNaTahu.Pokrik += 2;
                        message = naTahu.Name + " zařval " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh);
                        break;

                    case Druh.Strelba_Lukem:
                        if (vybranaNaTahu.Faze > 0)
                        {
                            message = naTahu.Name + " natahuje luk, aby použil schopnost " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh);
                            spravceMedii.BowPull.Play();
                        }
                        else
                        {
                            if (BraniSe(vybranaDruhy, rychlost))
                            {
                                message = naTahu.Name + " se schopností " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh) + " netrefil";
                                spravceMedii.BowMiss.Play();
                            }
                            else
                            {
                                skPosk = ZautocNaPostavu(ref cil, poskozeni, vybranaNaTahu.Magicka);
                                message = naTahu.Name + " útočí schopností " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh) + " za " + skPosk + " poškození";
                                spravceMedii.Hit.Play();
                            }
                        }
                        break;
                    case Druh.Magicky_sip:
                        if (vybranaNaTahu.Faze > 0)
                        {
                            message = naTahu.Name + " natahuje luk, aby použil schopnost " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh);
                            spravceMedii.BowPull.Play();
                        }
                        else
                        {
                            if (BraniSe(vybranaDruhy, rychlost))
                            {
                                skPosk = ZautocNaPostavu(ref cil, poskozeni / 2, vybranaNaTahu.Magicka);
                                message = naTahu.Name + " částečně útočí schopností " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh) + " za " + skPosk + " poškození";
                                spravceMedii.Hit.Play();
                            }
                            else
                            {
                                skPosk = ZautocNaPostavu(ref cil, poskozeni, vybranaNaTahu.Magicka);
                                message = naTahu.Name + " útočí schopností " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh) + " za " + skPosk + " poškození";
                                spravceMedii.Hit.Play();
                            }
                        }
                        break;

                    case Druh.Rychlost:
                        spravceMedii.Battlecry.Play();
                        efektyNaTahu.Rychlost += 4;
                        message = naTahu.Name + " použil schopnost " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh);
                        break;
                    case Druh.Lesni_bobule:
                        spravceMedii.Regen.Play();
                        if (efektyNaTahu.Horeni > 0 || efektyNaTahu.Jed > 0 || efektyNaTahu.Krvaceni > 0)
                        {
                            poskozeni = (int)Math.Round(poskozeni * 2.0 / 3.0);
                        }
                        naTahu.PridejNeboUberZdravi(poskozeni);
                        message = naTahu.Name + " regeneruje zdravi za " + poskozeni + " bodů schopností " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh);
                        break;
                    case Druh.Ohniva_Koule:
                        spravceMedii.Fireball.Play();
                        if (BraniSe(vybranaDruhy, rychlost))
                        {
                            skPosk = ZautocNaPostavu(ref cil, poskozeni / 2, vybranaNaTahu.Magicka);
                            message = naTahu.Name + " částečně útočí schopností " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh) + " za " + skPosk + " poškození";
                        }
                        else
                        {
                            efektyDruhy.Horeni += 2;
                            skPosk = ZautocNaPostavu(ref cil, poskozeni, vybranaNaTahu.Magicka);
                            message = naTahu.Name + " útočí schopností " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh) + " za " + skPosk + " poškození";
                        }
                        break;
                    case Druh.Ledove_Kopi:
                        spravceMedii.Frostbolt.Play();
                        if (BraniSe(vybranaDruhy, rychlost))
                        {
                            skPosk = ZautocNaPostavu(ref cil, poskozeni / 2, vybranaNaTahu.Magicka);
                            message = naTahu.Name + " částečně útočí schopností " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh) + " za " + skPosk + " poškození";
                        }
                        else
                        {
                            efektyDruhy.Mraz += 2;
                            skPosk = ZautocNaPostavu(ref cil, poskozeni, vybranaNaTahu.Magicka);
                            message = naTahu.Name + " útočí schopností " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh) + " za " + skPosk + " poškození";
                        }
                        break;
                    case Druh.Vysati_zivota:
                        spravceMedii.Fireball.Play();
                        if (BraniSe(vybranaDruhy, rychlost))
                        {
                            skPosk = ZautocNaPostavu(ref cil, poskozeni / 2, vybranaNaTahu.Magicka);
                            zdravi = skPosk;
                            if (efektyNaTahu.Horeni > 0 || efektyNaTahu.Jed > 0 || efektyNaTahu.Krvaceni > 0)
                            {
                                zdravi = (int)Math.Round(skPosk * 2.0 / 3.0);
                            }
                            message = naTahu.Name + " částečně ubral schopností " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh) + " " + skPosk + " zdraví a vysál " + zdravi + " zdraví";
                        }
                        else
                        {
                            skPosk = ZautocNaPostavu(ref cil, poskozeni, vybranaNaTahu.Magicka);
                            zdravi = skPosk;
                            if (efektyNaTahu.Horeni > 0 || efektyNaTahu.Jed > 0 || efektyNaTahu.Krvaceni > 0)
                            {
                                zdravi = (int)Math.Round(skPosk * 2.0 / 3.0);
                            }
                            message = naTahu.Name + " ubral schopností " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh) + " " + skPosk + " zdraví a vysál " + zdravi + " zdraví";

                        }
                        naTahu.PridejNeboUberZdravi(zdravi);
                        break;
                    case Druh.Vysati_many:
                        spravceMedii.Frostbolt.Play();


                        if (BraniSe(vybranaDruhy, rychlost))
                        {
                            skPosk = ZautocNaPostavu(ref cil, poskozeni / 2, vybranaNaTahu.Magicka);
                            mana = druhy.Mana - Math.Max(druhy.Mana - skPosk, 0);
                            if (efektyNaTahu.Horeni > 0 || efektyNaTahu.Jed > 0 || efektyNaTahu.Krvaceni > 0)
                            {
                                mana = (int)Math.Round(mana * 2.0 / 3.0);
                            }
                            message = naTahu.Name + " částečně ubral schopností " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh) + " " + skPosk + " zdraví a vysál " + mana + " many ";
                        }
                        else
                        {
                            skPosk = ZautocNaPostavu(ref cil, poskozeni, vybranaNaTahu.Magicka);
                            mana = druhy.Mana - Math.Max(druhy.Mana - skPosk, 0);
                            if (efektyNaTahu.Horeni > 0 || efektyNaTahu.Jed > 0 || efektyNaTahu.Krvaceni > 0)
                            {
                                mana = (int)Math.Round(mana * 2.0 / 3.0);
                            }
                            message = naTahu.Name + " ubral schopností " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh) + " " + skPosk + " zdraví a vysál " + mana + " many ";
                        }
                        druhy.PridejNeboUberManu(-mana);
                        naTahu.PridejNeboUberManu(mana);
                        break;
                    case Druh.Magicke_soustredeni:
                        spravceMedii.Battlecry.Play();
                        efektyNaTahu.Soustredeni += 4;
                        message = naTahu.Name + " se soustředí schopností " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh);
                        break;
                    case Druh.Utek:
                        message = naTahu.Name + " utíká"; // možnost přidat funkčnost, nyní není součástí hry
                        break;
                    case Druh.Lahvicka_Zdravi:
                        spravceMedii.Regen.Play();
                        naTahu.Inventar.LahvickyZdravi--;
                        if (efektyNaTahu.Horeni > 0 || efektyNaTahu.Jed > 0 || efektyNaTahu.Krvaceni > 0)
                        {
                            poskozeni = (int)Math.Round(poskozeni * 2.0 / 3.0);

                        }
                        message = naTahu.Name + " regeneruje zdravi za " + poskozeni + " bodů pomocí " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh);
                        naTahu.PridejNeboUberZdravi(poskozeni);
                        break;
                    case Druh.Lahvicka_Many:
                        spravceMedii.Regen.Play();
                        naTahu.Inventar.LahvickyMany--;
                        if (efektyNaTahu.Horeni > 0 || efektyNaTahu.Jed > 0 || efektyNaTahu.Krvaceni > 0)
                        {
                            poskozeni = (int)Math.Round(poskozeni * 2.0 / 3.0);
                        }
                        naTahu.PridejNeboUberManu(poskozeni);
                        message = naTahu.Name + " regeneruje manu za " + poskozeni + " bodů pomocí " + PomocneMetody.SchopnostToString(vybranaNaTahu.Druh);
                        break;
                    case Druh.Zadna:
                        message = naTahu.Name + " nepoužil žádnou schopnost";
                        break;
                    default:
                        message = naTahu.Name + " neví co dělá";
                        break;
                }

                if (vybranaNaTahu.Faze == 0)
                {
                    if (efektyNaTahu.Soustredeni > 0)
                        naTahu.PridejNeboUberManu(-vybranaNaTahu.CenaMany / 2);
                    else
                        naTahu.PridejNeboUberManu(-vybranaNaTahu.CenaMany);
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
        public bool BraniSe(Schopnost vybranaSchopnost, bool rychlost)
        {
            return vybranaSchopnost.Druh == Druh.Obrana_Stitem || vybranaSchopnost.Druh == Druh.Uskok || vybranaSchopnost.Druh == Druh.Magicky_Stit || rychlost;
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
        public int ZautocNaPostavu(ref Postava postava, int poskozeni, bool magicke)
        {
            return postava.ObdrzPoskozeni(poskozeni, magicke);
        }
    }
}
