namespace Zacarovany_les.Classes
{
    static class AI
    {
        public static Schopnost VyberSchopnostAI(Postava pocitac, Schopnost druhySch,Schopnost vybranaDruhy, Postava druhy,Postava prvni,Souboj souboj)
        {
            Efekty efektyPocitac = pocitac == souboj.Obrance ? souboj.EfektyObrance : souboj.EfektyUtocnika;
            Inventar inventarPocitac = pocitac == souboj.Obrance ? souboj.Obrance.Inventar : souboj.Utocnik.Inventar;
            Efekty efektyDruhy = druhy == souboj.Obrance ? souboj.EfektyObrance : souboj.EfektyUtocnika;
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
                            if (druhySch != null && ((druhySch.Druh == Druh.Magicky_sip && druhySch.Faze == 0) || (druhySch.Druh == Druh.Strelba_Lukem && druhySch.Faze == 0) ||
                                ((druhySch.Druh == Druh.Utok_Mecem || druhySch.Druh == Druh.Uder_stitem || druhySch.Druh == Druh.Vrh_sekerou ||
                                (druhySch.Druh == Druh.Berserk && druhy.Zivoty / (double)druhy.ZivotyMax < 0.4)) && efektyDruhy.Pokrik > 0) || druhySch.Druh == Druh.Ohniva_Koule ||
                                druhySch.Druh == Druh.Ledove_Kopi || druhySch.Druh == Druh.Vysati_zivota || druhySch.Druh == Druh.Vysati_many || druhySch.Druh == Druh.Jedova_sipka))
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
                                if (s.Druh == Druh.Uder_stitem && pocitac == prvni)
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
                            if (druhySch != null && ((druhySch.Druh == Druh.Magicky_sip && druhySch.Faze == 0) || (druhySch.Druh == Druh.Strelba_Lukem && druhySch.Faze == 0) ||
                                ((druhySch.Druh == Druh.Utok_Mecem || druhySch.Druh == Druh.Uder_stitem || druhySch.Druh == Druh.Vrh_sekerou ||
                                (druhySch.Druh == Druh.Berserk && druhy.Zivoty / (double)druhy.ZivotyMax < 0.4)) && efektyDruhy.Pokrik > 0) || druhySch.Druh == Druh.Ohniva_Koule ||
                                druhySch.Druh == Druh.Ledove_Kopi || druhySch.Druh == Druh.Vysati_zivota || druhySch.Druh == Druh.Vysati_many || druhySch.Druh == Druh.Jedova_sipka))
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
                                if (s.Druh == Druh.Rychlost && prvni == pocitac)
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
                            if (druhySch != null && ((druhySch.Druh == Druh.Magicky_sip && druhySch.Faze == 0) || (druhySch.Druh == Druh.Strelba_Lukem && druhySch.Faze == 0) ||
                                ((druhySch.Druh == Druh.Utok_Mecem || druhySch.Druh == Druh.Uder_stitem || druhySch.Druh == Druh.Vrh_sekerou ||
                                (druhySch.Druh == Druh.Berserk && druhy.Zivoty / (double)druhy.ZivotyMax < 0.4)) && efektyDruhy.Pokrik > 0) || druhySch.Druh == Druh.Ohniva_Koule ||
                                druhySch.Druh == Druh.Ledove_Kopi || druhySch.Druh == Druh.Vysati_zivota || druhySch.Druh == Druh.Vysati_many || druhySch.Druh == Druh.Jedova_sipka))
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
                            foreach (Schopnost schop in druhy.Schopnosti)
                            {
                                if (schop.Druh == Druh.Uskok && schop.Cd > 1 || schop.Druh == Druh.Obrana_Stitem && schop.Cd > 1 ||
                                    schop.Druh == Druh.Magicky_Stit && (schop.Cd > 1 || schop.CenaMany > druhy.Mana))
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

                            foreach (Schopnost schop in druhy.Schopnosti)
                            {
                                if (schop.Druh == Druh.Uskok && schop.Cd > 0 || schop.Druh == Druh.Obrana_Stitem && schop.Cd > 0 ||
                                    schop.Druh == Druh.Magicky_Stit && (schop.Cd > 0 || schop.CenaMany > druhy.Mana))
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
                                if (s.Druh == Druh.Uder_stitem && pocitac == prvni && efektyPocitac.Pokrik > 0)
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
                                if (s.Druh == Druh.Utok_Mecem && druhy.Zivoty <= (s.Pouzij(pocitac, false) - druhy.Brneni))
                                {
                                    sch = s.Cd > 0 || s.CenaMany > pocitac.Mana ? sch : s;
                                }
                            }

                            foreach (Schopnost s in pocitac.Schopnosti)
                            {
                                if (s.Druh == Druh.Berserk && druhy.Zivoty <= (s.Pouzij(pocitac, false) - druhy.Brneni))
                                {
                                    sch = s.Cd > 0 || s.CenaMany > pocitac.Mana ? sch : s;
                                }
                            }

                            if (druhySch != null && ((druhySch.Druh == Druh.Magicky_sip && druhySch.Faze == 0) ||
                                (druhySch.Druh == Druh.Strelba_Lukem && druhySch.Faze == 0) || ((druhySch.Druh == Druh.Utok_Mecem || druhySch.Druh == Druh.Uder_stitem ||
                                druhySch.Druh == Druh.Vrh_sekerou || (druhySch.Druh == Druh.Berserk && druhy.Zivoty / (double)druhy.ZivotyMax < 0.4)) && efektyDruhy.Pokrik > 0) ||
                                druhySch.Druh == Druh.Ohniva_Koule || druhySch.Druh == Druh.Ledove_Kopi || druhySch.Druh == Druh.Vysati_zivota))
                            {
                                foreach (Schopnost s in pocitac.Schopnosti)
                                {
                                    if (s.Druh == Druh.Obrana_Stitem)
                                    {
                                        sch = s.Cd > 0 || s.CenaMany > pocitac.Mana ? sch : s;
                                    }
                                }
                            }

                            if (pocitac == prvni && druhySch != null && ((druhySch.Druh == Druh.Magicky_sip && druhySch.Faze == 0) ||
                                (druhySch.Druh == Druh.Strelba_Lukem && druhySch.Faze == 0) || ((druhySch.Druh == Druh.Utok_Mecem || druhySch.Druh == Druh.Uder_stitem ||
                                druhySch.Druh == Druh.Vrh_sekerou || (druhySch.Druh == Druh.Berserk && druhy.Zivoty / (double)druhy.ZivotyMax < 0.4)) && efektyDruhy.Pokrik > 0) ||
                                druhySch.Druh == Druh.Ohniva_Koule || druhySch.Druh == Druh.Ledove_Kopi || druhySch.Druh == Druh.Vysati_zivota))
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

                            foreach (Schopnost schop in druhy.Schopnosti)
                            {
                                if (schop.Druh == Druh.Uskok && schop.Cd > 1 || schop.Druh == Druh.Obrana_Stitem && schop.Cd > 1 ||
                                    schop.Druh == Druh.Magicky_Stit && (schop.Cd > 1 || schop.CenaMany > druhy.Mana))
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
                                if (s.Druh == Druh.Bodnuti_Dykou && druhy.Zivoty <= (s.Pouzij(pocitac, false) - druhy.Brneni))
                                {
                                    sch = s.Cd > 0 || s.CenaMany > pocitac.Mana ? sch : s;
                                }
                            }

                            if (druhySch != null && ((druhySch.Druh == Druh.Magicky_sip && druhySch.Faze == 0) ||
                                (druhySch.Druh == Druh.Strelba_Lukem && druhySch.Faze == 0) || ((druhySch.Druh == Druh.Utok_Mecem || druhySch.Druh == Druh.Uder_stitem ||
                                druhySch.Druh == Druh.Vrh_sekerou || (druhySch.Druh == Druh.Berserk && druhy.Zivoty / (double)druhy.ZivotyMax < 0.4)) && efektyDruhy.Pokrik > 0) ||
                                druhySch.Druh == Druh.Ohniva_Koule || druhySch.Druh == Druh.Ledove_Kopi || druhySch.Druh == Druh.Vysati_zivota))
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
                                if (s.Druh == Druh.Rychlost && prvni == pocitac)
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

                            if (efektyDruhy.Mraz == 0)
                            {
                                foreach (Schopnost s in pocitac.Schopnosti)
                                {
                                    if (s.Druh == Druh.Ledove_Kopi)
                                    {
                                        sch = s.Cd > 0 || (efektyPocitac.Soustredeni == 0 && s.CenaMany > pocitac.Mana) || (efektyPocitac.Soustredeni > 0 && (s.CenaMany / 2) > pocitac.Mana) ? sch : s;
                                    }
                                }
                            }
                            if (efektyDruhy.Horeni == 0)
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
                                if (efektyDruhy.Mraz == 0)
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
                                if (efektyDruhy.Horeni == 0)
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
                            if (druhy.Brneni > 1 || (vybranaDruhy != null && (vybranaDruhy.Druh == Druh.Magicky_sip || vybranaDruhy.Druh == Druh.Strelba_Lukem) && vybranaDruhy.Faze > 0) ||
                                (vybranaDruhy != null && (vybranaDruhy.Druh == Druh.Magicky_sip || vybranaDruhy.Druh == Druh.Strelba_Lukem) && vybranaDruhy.Faze == 0 && prvni == pocitac))
                            {
                                if (efektyDruhy.Mraz == 0)
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
                                if (s.Druh == Druh.Vysati_many && pocitac.Mana < (pocitac.ManaMax - (s.Pouzij(pocitac, false) - druhy.Brneni) * 2)
                                    && druhy.Mana > ((s.Pouzij(pocitac, false) - druhy.Brneni) * 2))
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
                                if (s.Druh == Druh.Uder_Holi && druhy.Zivoty <= (s.Pouzij(pocitac, false) - druhy.Brneni))
                                {
                                    sch = s.Cd > 0 || (efektyPocitac.Soustredeni == 0 && s.CenaMany > pocitac.Mana) || (efektyPocitac.Soustredeni > 0 && (s.CenaMany / 2) > pocitac.Mana) ? sch : s;
                                }
                            }
                            foreach (Schopnost s in pocitac.Schopnosti)
                            {
                                if (s.Druh == Druh.Ohniva_Koule && druhy.Zivoty <= (s.Pouzij(pocitac, false) - druhy.Brneni / 2.0))
                                {
                                    sch = s.Cd > 0 || (efektyPocitac.Soustredeni == 0 && s.CenaMany > pocitac.Mana) || (efektyPocitac.Soustredeni > 0 && (s.CenaMany / 2) > pocitac.Mana) ? sch : s;
                                }
                            }


                            if (druhySch != null && ((druhySch.Druh == Druh.Magicky_sip && druhySch.Faze == 0) || (druhySch.Druh == Druh.Strelba_Lukem && druhySch.Faze == 0) ||
                                ((druhySch.Druh == Druh.Utok_Mecem || druhySch.Druh == Druh.Uder_stitem || druhySch.Druh == Druh.Vrh_sekerou ||
                                (druhySch.Druh == Druh.Berserk && druhy.Zivoty / (double)druhy.ZivotyMax < 0.4)) && efektyDruhy.Pokrik > 0) || druhySch.Druh == Druh.Ohniva_Koule ||
                                druhySch.Druh == Druh.Ledove_Kopi || druhySch.Druh == Druh.Vysati_zivota || druhySch.Druh == Druh.Vysati_many))
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
