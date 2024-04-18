using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Zacarovany_les.Classes;
using Zacarovany_les.Classes.Pomocne;
using System;
using Zacarovany_les.Classes.Mapy;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Zacarovany_les
{
    public class ZacarovanyLes : Game
    {
        // Monogame
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public static ContentManager content;
        public static ZacarovanyLes game;
        // States - fáze hry
        public State CurrentState;
        public State NextState;
        // Jednotlivé druhy - herní, pohyb po mapě a menu
        public static State gameState;
        public static State mapState;
        public static State menuState;
        // Statické proměnné - využívá je vícero tříd
        public static Postava utocnik;
        public static Postava obrance;
        public static MapManager maps;
        // Zpoždění hry a kláves
        public const double DELAY_TIME = 0.5;
        public static double delay = 0;
        public static bool delayed = false;
        public static double keyDelay = 0;
        public static bool keyDelayed = false;
        // Stavy kláves po a při stisknutí
        public static KeyboardState oldState;
        public static KeyboardState newState;

        // Správce Médii - spravuje obrázky, zvuk atd.
        public static SpravceMedii spravceMedii;



        public ZacarovanyLes()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            base.Window.Title = "Začarovaný les";
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromMilliseconds(20);
            game = this;
            content = Content;
            menuState = new MenuState(this, Content);

        }
        protected override void Initialize()
        {
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spravceMedii = new SpravceMedii(this, Content);
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            CurrentState = menuState;
            CurrentState.LoadContent();
            NextState = null;
            //button.Visible = false;
        }

        protected override void Update(GameTime gameTime)
        {
            spravceMedii.MusicPlayer();
            newState = Keyboard.GetState();
            if (delay > 0)
            {
                delay -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            else delayed = false;

            if (keyDelay > 0)
            {
                keyDelay -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            else keyDelayed = false;

            if (NextState != null)
            {
                CurrentState = NextState;
                CurrentState.LoadContent();
                NextState = null;
                MediaPlayer.Stop();
            }
            CurrentState.Update(gameTime);
            CurrentState.PostUpdate(gameTime);
            oldState = newState;

            if (newState.IsKeyDown(Keys.F9) && (!keyDelayed || oldState.IsKeyUp(Keys.F9)))
            {
                keyDelayed = true;
                keyDelay = DELAY_TIME;
                UkladaniNacitani.Nacti();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkSeaGreen);
            CurrentState.Draw(gameTime, _spriteBatch);
            base.Draw(gameTime);
        }
        public void ChangeState(State state)
        {
            NextState = state;
        }
        public void ChangeCurrentState(State state)
        {

            CurrentState = state;
            MediaPlayer.Stop();
        }
    }
}
