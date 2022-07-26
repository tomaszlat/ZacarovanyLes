using System;
using System.Collections.Generic;
using System.Text;

namespace Zacarovany_les.Classes
{
    public static class Generator
    {
        public static Postava DejLehkehoSoupere()
        {
            Random rand = new Random();
            return DejSoupere(rand.Next(1, 3), Majitel.Pocitac_Lehky);
        }

        public static Postava DejStrednihoSoupere()
        {
            Random rand = new Random();
            return DejSoupere(rand.Next(3, 5), Majitel.Pocitac_Stredni);
        }

        public static Postava DejTezkehoSoupere(Postava hrac)
        {
            int level = hrac.Level < 5 ? 5 : hrac.Level;
            return DejSoupere(level, Majitel.Pocitac_Tezky);
        }
        public static Postava DejSoupere(int level, Majitel maj)
        {
            Random rand = new Random();
            int random = rand.Next(0, 3);
            Trida trida = (Trida)random;
            random = rand.Next(0, 2);
            Pohlavi pohl = (Pohlavi)random;
            random = rand.Next(0, 3);
            Minulost min = (Minulost)random;

            if (pohl == Pohlavi.Muz)
            {
                return new Postava(trida, pohl, min, new Inventar(1, 1), maj, level, DejJmeno(Pohlavi.Muz));
            }
            else
            {
                return new Postava(trida, pohl, min, new Inventar(1, 1), maj, level, DejJmeno(Pohlavi.Zena));
            }

        }
        public static string DejJmeno(Pohlavi pohlavi)
        {
            Random rand = new Random();
            string[] namesMale = { "Ashwar", "Dyncheo", "Raknath", "Ornest", "Rynath", "Areck", "Achis", "Ightmir", "Caedric", "Yole",
                "Karel", "Václav", "Vladimír", "Bořivoj", "Boleslav","Tomáš" };
            string[] namesFemale = { "Nadya", "Daithine", "Olena", "Eloria", "Kossia", "Raia", "Xirenia", "Galaka", "Madia", "Alada",
                "Anežka", "Vladimíra", "Alada", "Iowyn", "Lydia","Domka" };
            return pohlavi == Pohlavi.Muz ? namesMale[rand.Next(namesMale.Length)] : namesFemale[rand.Next(namesFemale.Length)];
        }
    }
}
