using Zacarovany_les.Classes.Pomocne;

namespace Zacarovany_les.Classes
{
    public class Efekty
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

        public Efekty()
        {
        }
        public void ResetEfekty()
        {
                //}
            Horeni = 0;
            Mraz = 0;
            Pokrik = 0;
            Soustredeni = 0;
            Rychlost = 0;
            Krvaceni = 0;
            Omraceni = 0;
            Jed = 0;
        }

        public void ZhodnotEfekty()
        {
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
            if (Mraz > 0)
            {
                Mraz--;
            }
            
        }
    }
}
