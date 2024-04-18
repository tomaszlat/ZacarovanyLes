using Microsoft.Xna.Framework;
using Zacarovany_les.Classes.Mapy.Objekty;

namespace Zacarovany_les.Classes.Mapy
{
    public class Map
    {
        public Objekt[,] Objekty { get; set; }
        public Vector2 PoziceHrace { get; set; }

        public Map(Objekt[,] objekty, Vector2 poziceHrace)
        {
            Objekty = objekty;
            PoziceHrace = poziceHrace;
        }
    }
}
