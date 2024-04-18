﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zacarovany_les.Classes.Mapy.Objekty
{
    public abstract class Nepruchodny : Objekt
    {
        protected Nepruchodny(Vector2 position, Texture2D textura) : base(position, textura)
        {
        }
    }
}
