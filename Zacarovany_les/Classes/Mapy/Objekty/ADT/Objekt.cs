using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zacarovany_les.Classes.Mapy.Objekty
{
    public abstract class Objekt
    {
        public Vector2 Position { get; set; }
        public Texture2D Textura { get; set; }

        protected Objekt(Vector2 position, Texture2D textura)
        {
            Position = position;
            Textura = textura;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Color barva= Color.White;
            if (this.GetType() == typeof(DverePosledni))
            {
                barva = Color.Goldenrod;
            }
            spriteBatch.Draw(Textura, new Vector2(200 + (int)Position.X * 50, (int)Position.Y * 50), barva);
        }

    }
}
