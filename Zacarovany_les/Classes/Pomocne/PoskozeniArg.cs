using System;
using System.Collections.Generic;
using System.Text;

namespace Zacarovany_les.Classes.Pomocne
{
    public class PoskozeniArg : EventArgs
    {
        public int Poskozeni { get; }

        public PoskozeniArg(int poskozeni)
        {
            Poskozeni = poskozeni;
        }
    }
}
