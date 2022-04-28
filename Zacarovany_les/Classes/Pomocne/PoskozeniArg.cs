using System;
using System.Collections.Generic;
using System.Text;

namespace Zacarovany_les.Classes.Pomocne
{
    internal class PoskozeniArg : EventArgs
    {
        public int Poskozeni { get; }

        public PoskozeniArg(int poskozeni)
        {
            Poskozeni = poskozeni;
        }
    }
}
