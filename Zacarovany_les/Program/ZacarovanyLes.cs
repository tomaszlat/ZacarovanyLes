using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System.Collections.Generic;
using Zacarovany_les.Classes;
using Zacarovany_les.Classes.Interface;
using Zacarovany_les.Classes.Pomocne;
using System;
using System.Diagnostics;
using Zacarovany_les.Classes.Mapy;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Zacarovany_les
{
    public class ZacarovanyLes : Game
    {
        //Monogame
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        //States
        private State _currentState;
        private State _nextState;
        //States-
        public static ZacarovanyLes game;
        public static ContentManager content; 
        public static State gameState;
        public static State mapState;
        public static State menuState;
        //static vars
        public static Postava utocnik;
        public static Postava obrance;
        public static MapManager maps;

        public const double DELAY_TIME = 0.5; 
        public static double delay = 0;
        public static bool delayed = false;
        public static double keyDelay = 0;
        public static bool keyDelayed = false;
        public static KeyboardState oldState;
        public static KeyboardState newState;
        //Music
        Song menuMusic;
        Song mapMusic;
        Song battleMusic;



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
            //gameState = new GameState(this, Content);
            //mapState = new MenuState(this, Content);
            menuState = new MenuState(this, Content);

        }
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _currentState = menuState;
            _currentState.LoadContent();
            _nextState = null;
            menuMusic = Content.Load<Song>("Music\\intromusic");
            mapMusic = Content.Load<Song>("Music\\mapmusic");
            battleMusic = Content.Load<Song>("Music\\battlemusic");
            MediaPlayer.Volume = 0.2f;
            MediaPlayer.IsRepeating = true;
            //button.Visible = false;
        }

        protected override void Update(GameTime gameTime)
        {
            MusicPlayer(gameTime);
            newState = Keyboard.GetState();
            if (delay > 0)
                delay -= gameTime.ElapsedGameTime.TotalSeconds;
            else delayed = false;
            if (keyDelay > 0)
                keyDelay -= gameTime.ElapsedGameTime.TotalSeconds;
            else keyDelayed = false;

            if (_nextState != null)
            {
                _currentState = _nextState;
                _currentState.LoadContent();
                _nextState = null;
                MediaPlayer.Stop();
            }
            _currentState.Update(gameTime);
            _currentState.PostUpdate(gameTime);
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
            _currentState.Draw(gameTime, _spriteBatch);
            base.Draw(gameTime);
        }
        public void ChangeState(State state)
        {
            _nextState = state;
        }
        public void ChangeCurrentState(State state)
        {

            _currentState = state;
            MediaPlayer.Stop();
        }
        private void MusicPlayer(GameTime update)
        {
            if (_currentState == menuState && MediaPlayer.State != MediaState.Playing)
            {
                MediaPlayer.Play(menuMusic);

            }
            if (_currentState == mapState && MediaPlayer.State != MediaState.Playing)
            {
                MediaPlayer.Play(mapMusic);

            }
            if (_currentState == gameState && MediaPlayer.State!=MediaState.Playing)
            {
                MediaPlayer.Play(battleMusic);
            }
        }
    }
}
