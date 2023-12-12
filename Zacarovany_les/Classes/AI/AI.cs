using System;
using System.Collections.Generic;
using System.Text;

namespace Zacarovany_les.Classes
{
    static class AI
    {
        public static Schopnost VyberSchopnostAI(Postava pocitac, Schopnost souperSch,Schopnost vybranaSouper, Postava souper,Postava hrajici,Souboj souboj)
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
                        case Trida.Valecnik:
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
                        case Trida.Valecnik:
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
    }
}
