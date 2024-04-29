using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Zacarovany_les.Classes.Mapy.Objekty;

namespace Zacarovany_les.Classes.Mapy
{
    public class MapManager
    {
        public List<Map> Maps { get; set; }
        public Map Aktualni { get; set; }

        public MapManager(List<Map> maps, Map aktualni)
        {
            Maps = maps;
            Aktualni = aktualni;
        }

        public MapManager(List<Map> maps)
        {
            Maps = maps;
            if (maps.Count > 0)
                Aktualni = maps[0];
        }

        public void DalsiMapa()
        {
            int index = Maps.IndexOf(Aktualni) + 1;
            if (index < Maps.Count && index >= 0)
            {
                Aktualni = Maps[index];
            }
        }

        public void PredchoziMapa()
        {
            int index = Maps.IndexOf(Aktualni) - 1;
            if (index < Maps.Count && index >= 0)
            {
                Aktualni = Maps[index];
            }
        }

        public void Write(BinaryWriter bw)
        {
            bw.Write(Maps.Count);
            for (int i = 0; i < Maps.Count; i++)
            {
                bw.Write(Maps[i].Objekty.GetLength(0));
                bw.Write(Maps[i].Objekty.GetLength(1));
                for (int j = 0; j < Maps[i].Objekty.GetLength(0); j++)
                {
                    for (int k = 0; k < Maps[i].Objekty.GetLength(1); k++)
                    {
                        bw.Write(Maps[i].Objekty[j, k].GetType().Name);
                        bw.Write((int)Maps[i].Objekty[j, k].Position.X);
                        bw.Write((int)Maps[i].Objekty[j, k].Position.Y);
                    }
                }
                bw.Write((int)Maps[i].PoziceHrace.X);
                bw.Write((int)Maps[i].PoziceHrace.Y);
            }
            bw.Write(Maps.IndexOf(Aktualni));
            
        }
        public static MapManager Read(BinaryReader br)
        {
            List<Map> maps = new List<Map>();
            int count = br.ReadInt32();
            int delkaJ;
            int delkaK;
            for (int i = 0; i < count; i++)
            {
                Map map;
                delkaJ = br.ReadInt32();
                delkaK = br.ReadInt32();
                Objekt[,] obj = new Objekt[delkaJ, delkaK];
                for (int j = 0; j < delkaJ; j++)
                {
                    for (int k = 0; k < delkaK; k++)
                    {
                        string typ = br.ReadString();
                        int posx = br.ReadInt32();
                        int posy = br.ReadInt32();
                        obj[j, k] = typ switch
                        {
                            "DvereDalsi" => new DvereDalsi(new Vector2(posx, posy), null),
                            "DverePosledni" => new DverePosledni(new Vector2(posx, posy), null),
                            "DverePredchozi" => new DverePredchozi(new Vector2(posx, posy), null),
                            "Hrac" => new Hrac(new Vector2(posx, posy),null,new Vector2(posx, posy), null),
                            "Kamen" => new Kamen(new Vector2(posx, posy), null),
                            "LahvickaMany" => new LahvickaMany(new Vector2(posx, posy), null),
                            "LahvickaZdravi" => new LahvickaZdravi(new Vector2(posx, posy), null),
                            "SouperEasy" => new SouperEasy(new Vector2(posx, posy), null),
                            "SouperHard" => new SouperHard(new Vector2(posx, posy), null),
                            "SouperMedium" => new SouperMedium(new Vector2(posx, posy), null),
                            "Strom" => new Strom(new Vector2(posx, posy), null),
                            _ => new Trava(new Vector2(posx, posy), null),
                        };
                    }
                }
                int hracX = br.ReadInt32();
                int hracY = br.ReadInt32();
                map = new Map(obj, new Vector2(hracX, hracY));
                maps.Add(map);
            }
            Map aktualni = maps[br.ReadInt32()];
            return new MapManager(maps, aktualni);
        }
    }
}
