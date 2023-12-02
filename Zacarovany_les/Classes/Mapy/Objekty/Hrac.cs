using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zacarovany_les.Classes.Mapy.Objekty
{
    public class Hrac : Pruchodny
    {
        public Vector2 ZemePosition { get; set; }
        public Texture2D ZemeTextura { get; set; }
        public Hrac(Vector2 zemePositon,Texture2D zemeTextura,Vector2 position, Texture2D textura) : base(position, textura)
        {
            ZemePosition = zemePositon;
            ZemeTextura = zemeTextura;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Color barva = Color.White;
            spriteBatch.Draw(ZemeTextura, new Vector2(200 + ZemePosition.X * 50, ZemePosition.Y * 50), barva);
            spriteBatch.Draw(Textura, new Vector2(200 + Position.X * 50, Position.Y * 50), barva*1f);
        }
    }
}
