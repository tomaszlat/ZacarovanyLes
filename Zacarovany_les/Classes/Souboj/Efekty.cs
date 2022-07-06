using System;
using System.Collections.Generic;
using System.Text;
using Zacarovany_les.Classes.Pomocne;

namespace Zacarovany_les.Classes
{
    internal class Efekty
    {
        
        public delegate void HracHoriEventHandler(object sender, PoskozeniArg arg);
        public Postava Postava { get; set; }
        public int Horeni { get; set; } = 0;
        public int Mraz { get; set; } = 0;
        public int Pokrik { get; set; } = 0;
        public int Soustredeni { get; set; } = 0;
        public int Rychlost { get; set; } = 0;
        public int Krvaceni { get; set; } = 0;
        public int Omraceni { get; set; } = 0;
        public int Jed { get; set; } = 0;

        public int PuvodniIntelekt { get; set; }
        public int PuvodniObratnost { get; set; }
        public int PuvodniSila { get; set; }

        public Efekty(Postava postava)
        {
            Postava = postava;
            PuvodniIntelekt = postava.Inteligence;
            PuvodniObratnost = postava.Obratnost;
            PuvodniSila = postava.Sila;
        }
        public void ResetEfekty()
        {
                if (Postava.Inteligence != PuvodniIntelekt)
                {
                    Postava.Inteligence = PuvodniIntelekt;
                }
                if (Postava.Obratnost != PuvodniObratnost)
                {
                    Postava.Obratnost = PuvodniObratnost;
                }
                if (Postava.Sila != PuvodniSila)
                {
                    Postava.Sila = PuvodniSila;
                }
            Horeni = 0;
            Mraz = 0;
            Pokrik = 0;
            Soustredeni = 0;
            Rychlost = 0;
            Krvaceni = 0;
            Omraceni = 0;
            Jed = 0;
        }
        public void AktivujMraz()
        {
            if (Mraz > 0)
            {
                if (Postava.Inteligence == PuvodniIntelekt)
                {
                    Postava.Inteligence =(int)Math.Round(Postava.Inteligence - Postava.Inteligence / 5.0);
                }
                if (Postava.Obratnost == PuvodniObratnost)
                {
                    Postava.Obratnost = (int)Math.Round(Postava.Obratnost - Postava.Obratnost / 5.0);
                }
                if (Postava.Sila == PuvodniSila)
                {
                    Postava.Sila = (int)Math.Round(Postava.Sila - Postava.Sila / 5.0);
                }
            }
        }

        public void ZhodnotEfekty()
        {
            if (Mraz == 1)
            {
                if (Postava.Inteligence != PuvodniIntelekt)
                {
                    Postava.Inteligence = PuvodniIntelekt;
                }
                if (Postava.Obratnost != PuvodniObratnost)
                {
                    Postava.Obratnost = PuvodniObratnost;
                }
                if (Postava.Sila != PuvodniSila)
                {
                    Postava.Sila = PuvodniSila;
                }
                Mraz--;
            }
            if (Pokrik > 0)
            {
                Pokrik--;
            }
            if (Soustredeni > 0)
            {
                Soustredeni--;
            }
            if (Rychlost > 0)
            {
                Rychlost--;
            }
            if (Krvaceni > 0)
            {
                Krvaceni--;
            }
            if (Omraceni > 0)
            {
                Omraceni--;
            }
            if (Horeni > 0)
            {
                Horeni--;
            }
            if (Jed > 0)
            {
                Jed--;
            }
            if (Mraz > 1)
            {
                Mraz--;
            }
            
        }
    }
}
