using System;
using System.Collections.Generic;
using System.IO;

namespace Zacarovany_les.Classes
{
    public class Postava
    {
        private const int MaxLevel = 20;
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
        private int _sila;
        public int Sila { get { return Efekty.Mraz > 0 ? (int)(_sila * 0.8) : _sila; } set { _sila = value; } }
        private int _obratnost;
        public int Obratnost { get { return Efekty.Mraz > 0 ? (int)(_obratnost * 0.8) : _obratnost; } set { _obratnost = value; } }
        private int _inteligence;
        public int Inteligence { get { return Efekty.Mraz > 0 ? (int)(_inteligence * 0.8) : _inteligence; } set { _inteligence = value; } }
        public int Brneni { get; set; }
        public List<Schopnost> Schopnosti { get; set; }
        public Efekty Efekty { get; set; }

        public void Write(BinaryWriter bw)
        {
            bw.Write((int)Trida);
            bw.Write((int)Pohlavi);
            bw.Write((int)Minulost);
            bw.Write(Inventar.LahvickyZdravi);
            bw.Write(Inventar.LahvickyMany);
            bw.Write(Level);
            bw.Write(Zkusenosti);
            bw.Write(Name);
        }
        public static Postava Read(BinaryReader br)
        {
            Trida trida = (Trida)br.ReadInt32();
            Pohlavi pohlavi = (Pohlavi)br.ReadInt32();
            Minulost minulost = (Minulost)br.ReadInt32();
            int lahvickyZdravi = br.ReadInt32();
            int lahvickyMany = br.ReadInt32();
            int level = br.ReadInt32();
            int zkusenosti = br.ReadInt32();
            string jmeno = br.ReadString();
            Postava postava = new Postava(trida, pohlavi, minulost, new Inventar(lahvickyZdravi, lahvickyMany), Majitel.Hrac, level, jmeno)
            {
                Zkusenosti = zkusenosti
            };
            return postava;
        }
        public int ObdrzPoskozeni(int poskozeni, bool magicke)
        {
            int realne;
            if (magicke)
            {
                realne = (int)Math.Round(Math.Max(0, poskozeni - Brneni / 2.0));
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
            Efekty = new Efekty();
        }

        public int PouzijSchopnost(Druh druh)
        {
            switch (druh)
            {
                case Druh.Lahvicka_Zdravi:
                    return 100;
                case Druh.Lahvicka_Many:
                    return 100;
                default:
                    Schopnost sch = VratSchopnost(druh);
                    return sch != null ? sch.Pouzij(this, true) : 0;

            }
        }

        public void ZhodnotSchopnosti()
        {
            foreach (Schopnost sch in Schopnosti)
            {
                sch.ZhodnotSchopnost();
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
                if (Level < MaxLevel)
                {
                    Zkusenosti -= ZkusenostiNext;
                    ZkusenostiNext += 25;
                    Level++;
                    ZivotyMax += 10;
                    ManaMax += 1;
                    Zivoty = ZivotyMax;
                    Mana = ManaMax;
                    switch (Trida)
                    {
                        case Trida.Valecnik:
                            Sila += 2;
                            Obratnost++;
                            Inteligence++;
                            break;
                        case Trida.Lucistnik:
                            Sila++;
                            Obratnost += 2;
                            Inteligence++;
                            break;
                        case Trida.Kouzelnik:
                            Sila++;
                            Obratnost++;
                            Inteligence += 2;
                            break;
                        default:
                            break;
                    }

                }
                else
                {
                    Zkusenosti = ZkusenostiNext;
                }
            }
        }

        public Postava(Trida trida, Pohlavi pohlavi, Minulost minulost, Inventar inventar, Majitel majitel, int level, string name)
        {
            Efekty = new Efekty();
            Schopnosti = new List<Schopnost>();
            Trida = trida;
            Pohlavi = pohlavi;
            Minulost = minulost;
            Inventar = inventar;
            Majitel = majitel;
            Level = level <= MaxLevel ? (level > 0 ? level : 1) : MaxLevel;
            Name = name;
            
            Zkusenosti = 0;
            ZkusenostiNext = 25 + Level * 25;
            ZivotyMax = 70 + Level * 10;
            ManaMax = 30 + Level;
            Brneni = 1;
            Sila = 5;
            Obratnost = 5;
            Inteligence = 5;

            switch (trida)
            {
                case Trida.Valecnik:
                    Sila += 3 + (Level - 1) * 2;
                    Obratnost += -1 + (Level - 1);
                    Inteligence += -2 + (Level - 1);
                    Brneni += 2;
                    Schopnosti.Add(new Schopnost(Druh.Utok_Mecem, 0, 0, 0, false));
                    Schopnosti.Add(new Schopnost(Druh.Obrana_Stitem, 3, 0, 0, false));
                    Schopnosti.Add(new Schopnost(Druh.Bojovy_Pokrik, 2, 0, 0, false));
                    Schopnosti.Add(new Schopnost(Druh.Regenerace, 2, 0, 20, false));
                    Schopnosti.Add(new Schopnost(Druh.Uder_stitem, 5, 0, 0, false));
                    Schopnosti.Add(new Schopnost(Druh.Vrh_sekerou, 3, 0, 0, false));
                    Schopnosti.Add(new Schopnost(Druh.Berserk, 2, 0, 0, false));
                    break;
                case Trida.Lucistnik:
                    Sila += -1 + (Level - 1);
                    Obratnost += 3 + (Level - 1) * 2;
                    Inteligence += -1 + (Level - 1);
                    Brneni += 1;
                    Schopnosti.Add(new Schopnost(Druh.Bodnuti_Dykou, 0, 0, 0, false));
                    Schopnosti.Add(new Schopnost(Druh.Uskok, 3, 0, 0, false));
                    Schopnosti.Add(new Schopnost(Druh.Strelba_Lukem, 0, 1, 0, false));
                    Schopnosti.Add(new Schopnost(Druh.Magicky_sip, 0, 1, 20, true));
                    Schopnosti.Add(new Schopnost(Druh.Rychlost, 6, 0, 0, false));
                    Schopnosti.Add(new Schopnost(Druh.Lesni_bobule, 4, 0, 0, false));
                    Schopnosti.Add(new Schopnost(Druh.Jedova_sipka, 3, 0, 0, false));
                    break;
                case Trida.Kouzelnik:
                    Sila += -2 + (Level - 1);
                    Obratnost += -1 + (Level - 1);
                    Inteligence += 3 + (Level - 1) * 2;
                    ManaMax += 60;
                    Schopnosti.Add(new Schopnost(Druh.Uder_Holi, 0, 0, 0, false));
                    Schopnosti.Add(new Schopnost(Druh.Magicky_Stit, 3, 0, 10, false));
                    Schopnosti.Add(new Schopnost(Druh.Ohniva_Koule, 0, 0, 20, true));
                    Schopnosti.Add(new Schopnost(Druh.Ledove_Kopi, 0, 0, 20, true));
                    Schopnosti.Add(new Schopnost(Druh.Vysati_zivota, 3, 0, 10, true));
                    Schopnosti.Add(new Schopnost(Druh.Vysati_many, 4, 0, 10, true));
                    Schopnosti.Add(new Schopnost(Druh.Magicke_soustredeni, 5, 0, 0, false));
                    break;
            }
            Schopnosti.Add(new Schopnost(Druh.Lahvicka_Many, 0, 0, 0, false));
            Schopnosti.Add(new Schopnost(Druh.Lahvicka_Zdravi, 0, 0, 0, false));

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
                    Brneni += 2;
                    break;
                case Minulost.Lovec:
                    Obratnost += 1;
                    ZivotyMax += 20;
                    break;
                case Minulost.Carodej:
                    Inteligence += 1;
                    ManaMax += 20;
                    break;
            }
            Zivoty = ZivotyMax;
            Mana = ManaMax;

        }
    }
}
