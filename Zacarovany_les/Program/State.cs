using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Zacarovany_les
{
    public abstract class State
    {
        protected ZacarovanyLes _game;
        protected ContentManager _content;
        public State(ZacarovanyLes game,ContentManager content)
        {
            _game = game;
            _content = content;
        }

        public abstract void LoadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void PostUpdate(GameTime gameTime);
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
