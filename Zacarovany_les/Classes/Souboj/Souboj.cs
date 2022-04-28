using System;
using System.Collections.Generic;
using System.Text;

namespace Zacarovany_les.Classes
{
    internal class Souboj
    {
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
            EfektyUtocnika = new Efekty(utocnik);
            EfektyObrance = new Efekty(obrance);
        }
        public void ZhodnotEfekty() {
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
        public void ZhodnotSchopnosti()
        {
            Utocnik.ZhodnotSchopnosti();
            Obrance.ZhodnotSchopnosti();
        }
        public int ZautocNaObrance(int poskozeni,bool magicke)
        {
            return Obrance.ObdrzPoskozeni(poskozeni,magicke);
        }

        public int ZautocNaUtocnika(int poskozeni,bool magicke)
        {
            return Utocnik.ObdrzPoskozeni(poskozeni,magicke);
        }
    }
}
