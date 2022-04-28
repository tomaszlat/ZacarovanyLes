using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Zacarovany_les.Classes
{
    public class Postava
    {
        public Trida Trida { get; set; }
        public Pohlavi Pohlavi { get; set; }
        public Minulost Minulost { get; set; }
        public Inventar Inventar { get; set; }
        public Majitel Majitel { get; set; }
        public int Level { get; set; }
        public int Zkusenosti { get; set; }
        public int ZkusenostiNext { get; set; }
        public string Name { get; set; }
        public int Zivoty { get; set; }
        public int ZivotyMax { get; set; }
        public int Mana { get; set; }
        public int ManaMax { get; set; }
        public int Sila { get; set; }
        public int Obratnost { get; set; }
        public int Inteligence { get; set; }
        public int Brneni { get; set; }
        public List<Schopnost> Schopnosti { get; set; }

        public void Write(BinaryWriter bw)
        {
            bw.Write((int)Trida);
            bw.Write((int)Pohlavi);
            bw.Write((int)Minulost);
            bw.Write(Inventar.LahvickyZdravi);
            bw.Write(Inventar.LahvickyMany);
            bw.Write(Level);
            bw.Write(Name);
        }
         public static Postava Read(BinaryReader br)
        {
            Trida tr =(Trida) br.ReadInt32();
            Pohlavi po = (Pohlavi)br.ReadInt32();
            Minulost mi = (Minulost)br.ReadInt32();
            int lz = br.ReadInt32();
            int lm = br.ReadInt32();
            int lvl = br.ReadInt32();
            string name = br.ReadString();
            return new Postava(tr, po, mi, new Inventar(lz,lm),Majitel.Hrac, lvl, name);
        }
        public int ObdrzPoskozeni(int poskozeni, bool magicke)
        {
            int realne;
            if (magicke)
            {
                realne = Math.Max(0, poskozeni - Brneni / 2);
            }
            else
            {
                realne = Math.Max(0, poskozeni - Brneni);
            }

            Zivoty -= realne;
            return realne;
        }
        private Schopnost VratSchopnost(Druh druh)
        {
            foreach (Schopnost schopnost in Schopnosti)
            {
                if (schopnost.Druh == druh)
                {
                    return schopnost;
                }
            }
            return null;
        }
        public void Reset()
        {
            foreach (Schopnost sch in Schopnosti)
            {
                sch.Cd = 0;
                sch.Faze = sch.FazeVychozi;
            }
            Mana = ManaMax;
            Zivoty = ZivotyMax;
        }

        public int PouzijSchopnost(Druh druh)
        {
            switch (druh)
            {
                case Druh.Lahvicka_Zdravi:
                    PridejNeboUberZdravi(100);
                    return 100;
                case Druh.Lahvicka_Many:
                    PridejNeboUberManu(100);
                    return 100;
                default:
                    Schopnost sch = VratSchopnost(druh);
                    return sch != null ? sch.Pouzij(Sila, Obratnost, Inteligence) : 0;

            }
        }

        public void ZhodnotSchopnosti()
        {
            foreach (Schopnost sch in Schopnosti)
            {
                if (sch.Cd > 0)
                {
                    sch.Cd--;
                }
            }
        }

        public void PridejNeboUberZdravi(int zivoty)
        {
            Zivoty += zivoty;
            if (Zivoty > ZivotyMax)
            {
                Zivoty = ZivotyMax;
            }
            else if (Zivoty < 0)
            {
                Zivoty = 0;
            }
        }

        public void PridejNeboUberManu(int mana)
        {
            Mana += mana;
            if (Mana > ManaMax)
            {
                Mana = ManaMax;
            }
            else if (Mana < 0)
            {
                Mana = 0;
            }
        }

        public Schopnost DejSchopnost(Druh druh)
        {
            foreach (Schopnost sch in Schopnosti)
            {
                if (sch.Druh.Equals(druh))
                {
                    return sch;
                }
            }
            return null;
        }
        public void PridejZkusenosti(int zkusenosti)
        {
            Zkusenosti += zkusenosti;
            if (Zkusenosti >= ZkusenostiNext)
            {
                Zkusenosti = Zkusenosti - ZkusenostiNext;
                ZkusenostiNext += 25;
                Level++;
                ZivotyMax += 5;
                ManaMax += 5;
                Zivoty = ZivotyMax;
                Mana = ManaMax;
                switch (Trida)
                {
                    case Trida.Bojovnik:
                        Sila += 2;
                        Obratnost++;
                        Inteligence++;
                        break;
                    case Trida.Lucistnik:
                        Sila ++;
                        Obratnost+=2;
                        Inteligence++;
                        break;
                    case Trida.Kouzelnik:
                        Sila++;
                        Obratnost ++;
                        Inteligence+=2;
                        break;
                }
            }
        }

        public Postava(Trida trida, Pohlavi pohlavi, Minulost minulost, Inventar inventar, Majitel majitel, int level, string name)
        {
            Schopnosti = new List<Schopnost>();
            Trida = trida;
            Pohlavi = pohlavi;
            Minulost = minulost;
            Inventar = inventar;
            Majitel = majitel;
            Level = level;
            Name = name;
            Zkusenosti = 0;
            ZkusenostiNext = 50 + (level - 1) * 25;
            ZivotyMax = 100 + (level - 1) * 5;
            ManaMax = 20 + (level - 1) * 5;
            switch (trida)
            {
                case Trida.Bojovnik:
                    Sila += 3;
                    Obratnost -= 1;
                    Inteligence -= 2;
                    Brneni += 2;
                    Sila = 5 + (level - 1) * 2;
                    Obratnost = 5 + (level - 1) * 1;
                    Inteligence = 5 + (level - 1) * 1;
                    Schopnosti.Add(new Schopnost(Druh.Utok_Mecem, 0, 0, 0));
                    Schopnosti.Add(new Schopnost(Druh.Obrana_Stitem, 3, 0, 0));
                    Schopnosti.Add(new Schopnost(Druh.Bojovy_Pokrik, 2, 0, 0));
                    Schopnosti.Add(new Schopnost(Druh.Regenerace, 2, 0, 20));
                    break;
                case Trida.Lucistnik:
                    Obratnost += 3;
                    Sila -= 1;
                    Inteligence -= 1;
                    Brneni += 1;
                    Sila = 5 + (level - 1) * 1;
                    Obratnost = 5 + (level - 1) * 2;
                    Inteligence = 5 + (level - 1) * 1;
                    Schopnosti.Add(new Schopnost(Druh.Bodnuti_Dykou, 0, 0, 0));
                    Schopnosti.Add(new Schopnost(Druh.Uskok, 3, 0, 0));
                    Schopnosti.Add(new Schopnost(Druh.Strelba_Lukem, 0, 1, 0));
                    Schopnosti.Add(new Schopnost(Druh.Magicky_sip, 0, 1, 20));
                    break;
                case Trida.Kouzelnik:
                    Inteligence += 3;
                    ManaMax += 80;
                    Sila -= 2;
                    Obratnost -= 1;
                    Brneni = 0;
                    Sila = 5 + (level - 1) * 1;
                    Obratnost = 5 + (level - 1) * 1;
                    Inteligence = 5 + (level - 1) * 2;
                    Schopnosti.Add(new Schopnost(Druh.Uder_Holi, 0, 0, 0));
                    Schopnosti.Add(new Schopnost(Druh.Magicky_Stit, 3, 0, 10));
                    Schopnosti.Add(new Schopnost(Druh.Ohniva_Koule, 0, 0, 20));
                    Schopnosti.Add(new Schopnost(Druh.Ledove_Kopi, 0, 0, 20));
                    break;
            }
            if (majitel == Majitel.Hrac)
            {
                //Schopnosti.Add(new Schopnost(Druh.Utek, 2, 0, 0));
                Schopnosti.Add(new Schopnost(Druh.Lahvicka_Many, 0, 0, 0));
                Schopnosti.Add(new Schopnost(Druh.Lahvicka_Zdravi, 0, 0, 0));
            }
            switch (Pohlavi)
            {
                case Pohlavi.Zena:
                    Obratnost += 1;
                    break;
                case Pohlavi.Muz:
                    Sila += 1;
                    break;

            }
            switch (Minulost)
            {
                case Minulost.Rytir:
                    Sila += 1;
                    Brneni += 1;
                    break;
                case Minulost.Lovec:
                    Obratnost += 1;
                    ZivotyMax += 10;
                    break;
                case Minulost.Carodej:
                    Inteligence += 1;
                    ManaMax += 10;
                    break;
            }
            Zivoty = ZivotyMax;
            Mana = ManaMax;
        }
    }
}
