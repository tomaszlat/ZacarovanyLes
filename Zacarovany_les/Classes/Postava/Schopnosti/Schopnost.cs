using System;
using System.Collections.Generic;
using System.Text;

namespace Zacarovany_les.Classes
{
    public class Schopnost
    {
        public Druh Druh { get; set; }
        public bool Magicka { get; set; }
        public int CdVychozi { get; set; }
        public int Cd { get; set; }
        public int FazeVychozi { get; set; }
        public int Faze { get; set; }
        public int CenaMany { get; set; }

        public int Pouzij(Postava postava,bool boj)
        {
            Random kostka = new Random();
            if(boj)
            Cd = CdVychozi;
            switch (Druh)
            {
                // Valecnik
                case Druh.Utok_Mecem:
                    return (int)Math.Round(postava.Sila + postava.Obratnost / 2.0 - postava.Level / 2.0 + kostka.Next(postava.Level + 1));
                case Druh.Regenerace:
                    return (int)Math.Round(postava.Inteligence + postava.Sila - postava.Level / 2.0 + kostka.Next(postava.Level + 1));
                case Druh.Bojovy_Pokrik:
                    return 0;
                case Druh.Obrana_Stitem:
                    return 0;
                case Druh.Uder_stitem:
                    return (int)Math.Round(postava.Sila / 2.0 - postava.Level / 2.0 + kostka.Next(postava.Level + 1));
                case Druh.Vrh_sekerou:
                    return (int)Math.Round(postava.Sila / 2.0 + postava.Obratnost / 2.0 - postava.Level / 2.0 + kostka.Next(postava.Level + 1));
                case Druh.Berserk:
                    return (int)Math.Round(Math.Max(1 - postava.Zivoty / (double)postava.ZivotyMax, 0.01) * postava.Sila + postava.Sila / 2.0 + postava.Obratnost / 3.0 - postava.Level / 2.0 + kostka.Next(postava.Level + 1));


                // Lucistnik
                case Druh.Bodnuti_Dykou:
                    return (int)Math.Round(postava.Sila / 2.0 + postava.Obratnost - postava.Level / 2.0 + kostka.Next(postava.Level + 1));
                case Druh.Strelba_Lukem:
                    return (int)Math.Round(2 * (postava.Sila + postava.Obratnost - postava.Level / 2.0 + kostka.Next(postava.Level + 1)));
                case Druh.Uskok:
                    return 0;
                case Druh.Magicky_sip:
                    return (int)Math.Round(2 * (postava.Sila + postava.Obratnost + postava.Inteligence / 2.0 - postava.Level / 2.0 + kostka.Next(postava.Level + 1)));
                case Druh.Rychlost:
                    return 0;
                case Druh.Lesni_bobule:
                    return (int)Math.Round(postava.Obratnost + postava.Inteligence / 2.0 + postava.Sila / 2.0 - postava.Level / 2.0 + kostka.Next(postava.Level + 1));
                case Druh.Jedova_sipka:
                    return (int)Math.Round(postava.Obratnost / 2.0 + postava.Inteligence / 2.0 - postava.Level / 2.0 + kostka.Next(postava.Level + 1));

                // Kouzelnik
                case Druh.Uder_Holi:
                    return (int)Math.Round(postava.Sila / 4.0 + postava.Obratnost / 4.0 + postava.Inteligence - postava.Level / 2.0 + kostka.Next(postava.Level + 1));
                case Druh.Ohniva_Koule:
                    return (int)Math.Round(postava.Inteligence + postava.Sila - postava.Level / 2.0 + kostka.Next(postava.Level + 1));
                case Druh.Ledove_Kopi:
                    return (int)Math.Round(postava.Inteligence + postava.Obratnost - postava.Level / 2.0 + kostka.Next(postava.Level + 1));
                case Druh.Magicky_Stit:
                    return 0;
                case Druh.Vysati_zivota:
                    return (int)Math.Round(postava.Inteligence + postava.Sila / 3.0 - postava.Level / 2.0 + kostka.Next(postava.Level + 1));
                case Druh.Vysati_many:
                    return (int)Math.Round((postava.Inteligence + postava.Obratnost / 3.0 - postava.Level / 2.0 + kostka.Next(postava.Level + 1)) / 2.0);
                case Druh.Magicke_soustredeni:
                    return 0;

                // Obecne
                case Druh.Utek:
                    Random rand = new Random();
                    return postava.Obratnost * 2.5 > rand.Next(1, 100) ? 1 : 0;

            }
            return -1;
        }

        public Schopnost(Druh druh, int cdVychozi, int fazeVychozi, int cenaMany, bool magicka)
        {
            Druh = druh;
            CdVychozi = cdVychozi;
            Cd = 0;
            FazeVychozi = fazeVychozi;
            Faze = fazeVychozi;
            CenaMany = cenaMany;
            Magicka = magicka;
        }

        public Schopnost(Druh druh, int cdVychozi, int cd, int fazeVychozi, int faze, int cenaMany, bool magicka)
        {
            Druh = druh;
            CdVychozi = cdVychozi;
            Cd = cd;
            FazeVychozi = fazeVychozi;
            Faze = faze;
            CenaMany = cenaMany;
            Magicka = magicka;
        }

        public void ZhodnotSchopnost()
        {
            if (Cd > 0)
            {
                Cd--;
            }
        }
    }
}
