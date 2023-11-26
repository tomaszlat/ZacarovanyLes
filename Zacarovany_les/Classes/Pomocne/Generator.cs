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
            Random random = new Random();
            int level = hrac.Level < 5 ? 5 : hrac.Level;
            Postava postava = DejSoupere(level, Majitel.Pocitac_Tezky);
            postava.Inventar.LahvickyZdravi = random.Next(random.Next(2));
            postava.Inventar.LahvickyMany = random.Next(random.Next(2));
            return postava;
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

            return new Postava(trida, pohl, min, new Inventar(0, 0), maj, level, DejJmeno(pohl));

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
