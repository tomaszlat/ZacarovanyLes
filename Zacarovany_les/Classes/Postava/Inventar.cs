using System;
using System.Collections.Generic;
using System.Text;

namespace Zacarovany_les.Classes
{
    public class Inventar
    {
        public int LahvickyZdravi { get; set; }
        public int LahvickyMany { get; set; }

        public Inventar(int lahvickyZdravi, int lahvickyMany)
        {
            LahvickyZdravi = lahvickyZdravi;
            LahvickyMany = lahvickyMany;
        }
    }
}
