using System;
using System.Collections.Generic;
using System.Text;

namespace Zacarovany_les.Classes
{
    public class Schopnost
    {
        public Druh Druh { get; set; }
        public int CdVychozi { get; set; }
        public int Cd { get; set; }
        public int FazeVychozi { get; set; }
        public int Faze { get; set; }
        public int CenaMany { get; set; }
        
        public int Pouzij(int sila,int obratnost,int inteligence)
        {
            switch (Druh)
            {
                    //Valecnik
                case Druh.Utok_Mecem:
                    return (int)Math.Round(15.0 + sila + obratnost / 2.0);
                case Druh.Regenerace:
                    return (int)Math.Round(20.0 + inteligence);
                case Druh.Bojovy_Pokrik:
                    Cd = CdVychozi;
                    return 0;
                case Druh.Obrana_Stitem:
                    Cd = CdVychozi;
                    return 0;
                    //Lucistnik
                case Druh.Bodnuti_Dykou:
                    return (int)Math.Round(12.0 + sila/2.0 + obratnost);
                case Druh.Strelba_Lukem:
                    return (int)Math.Round(25.0 + sila/2.0 + obratnost);
                case Druh.Uskok:
                    Cd = CdVychozi;
                    return 0;
                case Druh.Magicky_sip:
                    return (int)Math.Round(25.0 + sila / 2.0 + obratnost + inteligence);

                    //Kouzelnik
                case Druh.Uder_Holi:
                    return (int)Math.Round(10.0 + sila*2.0/3.0 + obratnost*2.0/3.0 + inteligence/3.0);
                case Druh.Ohniva_Koule:
                    return (int)Math.Round(20.0 + inteligence);
                case Druh.Ledove_Kopi:
                    return (int)Math.Round(20.0 + inteligence);
                case Druh.Magicky_Stit:
                    Cd = CdVychozi;
                    return 0;

                    //Obecne
                case Druh.Utek:
                    Random rand = new Random();
                    return obratnost*2.5 > rand.Next(1, 100) ? 1 : 0;

            }
            return -1;
        }

        public Schopnost(Druh druh, int cdVychozi, int fazeVychozi, int cenaMany)
        {
            Druh = druh;
            CdVychozi = cdVychozi;
            Cd = 0;
            FazeVychozi = fazeVychozi;
            Faze = fazeVychozi;
            CenaMany = cenaMany;
        }

        public Schopnost(Druh druh, int cdVychozi, int cd, int fazeVychozi, int faze, int cenaMany)
        {
            Druh = druh;
            CdVychozi = cdVychozi;
            Cd = cd;
            FazeVychozi = fazeVychozi;
            Faze = faze;
            CenaMany = cenaMany;
        }
    }
}
