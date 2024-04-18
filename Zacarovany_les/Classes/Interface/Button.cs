using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Zacarovany_les.Classes.Interface
{
    public class Button : Component
    {
        private MouseState _currentState;
        
        private MouseState _previousState;
        public bool _isMouseOver;

        public SpriteFont Font;

        public event EventHandler Click;
        public event EventHandler RightClick;
        //public bool Clicked { get; private set; }
        public Color FontColor { get; set; } = Color.Black;
        public Vector2 Position { get; set; }
        public Texture2D Texture { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Color Color1 { get; set; } = Color.White;
        public Color Color2 { get; set; } = Color.Red;
        public bool Clicable { get; set; } = true;
        public bool Visible { get; set; } = true;
        public bool Animated { get; set; } = true;
        public string Text { get; set; }
        public Druh Druh { get; set; } = Druh.Zadna;
        public Rectangle Rectangle { get { return new Rectangle((int)Position.X, (int)Position.Y, Width, Height); } }

        public Button(SpriteFont font, Vector2 position, Texture2D texture, int width, int height)
        {
            Font = font;
            Position = position;
            Texture = texture;
            Width = width;
            Height = height;
        }
        public Button(SpriteFont font, Vector2 position, Texture2D texture, int width, int height, Color color1)
        {
            Font = font;
            Position = position;
            Texture = texture;
            Width = width;
            Height = height;
            Color1 = color1;
        }
        public Button(SpriteFont font, Vector2 position, Texture2D texture, int width, int height, Color color1, Color color2)
        {
            Font = font;
            Position = position;
            Texture = texture;
            Width = width;
            Height = height;
            Color1 = color1;
            Color2 = color2;
        }
        public Button(SpriteFont font,Vector2 position, Texture2D texture, int width, int height, Color color1,Color fontColor,string text)
        {
            Font = font;
            Position = position;
            Texture = texture;
            Width = width;
            Height = height;
            Color1 = color1;
            FontColor = fontColor;
            Text = text;
        }
        public Button(SpriteFont font, Vector2 position, Texture2D texture, int width, int height, Color color1, Color color2, Color fontColor, string text)
        {
            Font = font;
            Position = position;
            Texture = texture;
            Width = width;
            Height = height;
            Color1 = color1;
            FontColor = fontColor;
            Text = text;
            Color2 = color2;
        }
        public Button(SpriteFont font, Vector2 position, Texture2D texture, int width, int height, Color color1, Color color2, Color fontColor, string text,Druh druh)
        {
            Font = font;
            Position = position;
            Texture = texture;
            Width = width;
            Height = height;
            Color1 = color1;
            FontColor = fontColor;
            Text = text;
            Druh = druh;
            Color2 = color2;
        }

        public override void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            if (Visible)
            {
                Color barvaFontu = FontColor;
                Color barva = Color1;
                if (_isMouseOver&&Animated)
                    barva = Color2;
                if (!Clicable)
                {
                    barva = Color.Gray;
                    barvaFontu = Color.DarkGray;
                }
                _spriteBatch.Draw(Texture, Rectangle, barva);

                if (!string.IsNullOrEmpty(Text))
                {
                    var x = (Position.X + (Width / 2)) - (Font.MeasureString(Text).X / 2);
                    var y = (Position.Y + (Height / 2)) - (Font.MeasureString(Text).Y / 2);
                    _spriteBatch.DrawString(Font, Text, new Vector2(x, y), barvaFontu);

                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (Visible)
            {
                _previousState = _currentState;
                _currentState = Mouse.GetState();
                var mouseRect = new Rectangle(_currentState.X, _currentState.Y, 1, 1);
                _isMouseOver = false;

                if (mouseRect.Intersects(Rectangle))
                {
                    _isMouseOver = true;
                    if (Clicable && _currentState.LeftButton == ButtonState.Released && _previousState.LeftButton == ButtonState.Pressed )
                    {
                        Click?.Invoke(this, new EventArgs());
                    }
                    if (Clicable && _currentState.RightButton == ButtonState.Released && _previousState.RightButton == ButtonState.Pressed)
                    {
                        RightClick?.Invoke(this, new EventArgs());
                    }
                }
            }
        }
    }
}
