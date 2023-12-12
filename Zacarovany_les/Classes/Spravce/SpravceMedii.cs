using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Zacarovany_les.Classes
{
    public class SpravceMedii
    {
        protected ContentManager _content;
        protected ZacarovanyLes _game;

        //fonty
        public SpriteFont FontText;
        public SpriteFont FontNadpis;

        //textury
        public Texture2D Panel;
        public Texture2D Plocha;
        public Texture2D Valecnik;
        public Texture2D Valecnice;
        public Texture2D Lucistnik;
        public Texture2D Lucistnice;
        public Texture2D Kouzelnik;
        public Texture2D Kouzelnice;
        public Texture2D Dvere;
        public Texture2D EnemyLehky;
        public Texture2D EnemyStredni;
        public Texture2D EnemyTezky;
        public Texture2D Kamen;
        public Texture2D LahvickaMana;
        public Texture2D LahvickaZdravi;
        public Texture2D PostavaDolu;
        public Texture2D PostavaLevo;
        public Texture2D PostavaNahoru;
        public Texture2D PostavaPravo;
        public Texture2D Strom;
        public Texture2D Trava;
        public Texture2D PrazdnaTexturaBila;
        public Texture2D PrazdnaTexturaCerna;
        public Texture2D Intro;
        public Texture2D HlavniMenu;
        public Texture2D ButtonNovaHra;
        public Texture2D Button1v1;
        public Texture2D Informace;
        public Texture2D ButtonKonec;

        //zvuky
        public SoundEffect Fireball;
        public SoundEffect Frostbolt;
        public SoundEffect BowPull;
        public SoundEffect BowMiss;
        public SoundEffect Hit;
        public SoundEffect Regen;
        public SoundEffect Battlecry;
        public SoundEffect Click;

        //hudba
        public Song MenuMusic;
        public Song MapMusic;
        public Song BattleMusic;
        public Song CreateMusic;

        public SpravceMedii(ZacarovanyLes game,ContentManager content)
        {
            _game = game;
            _content = content;

            //načtení fontů
            FontNadpis = _content.Load<SpriteFont>("Fonts\\Nadpis");
            FontText = _content.Load<SpriteFont>("Fonts\\Text");

            //načtení textur
            Panel = _content.Load<Texture2D>("Sprites\\GUI\\menu");
            Plocha = _content.Load<Texture2D>("Sprites\\GUI\\plocha_boj");
            Valecnik = _content.Load<Texture2D>("Sprites\\Postavy\\valecnik");
            Valecnice = _content.Load<Texture2D>("Sprites\\Postavy\\valecnice");
            Lucistnik = _content.Load<Texture2D>("Sprites\\Postavy\\lucistnik");
            Lucistnice = _content.Load<Texture2D>("Sprites\\Postavy\\lucistnice");
            Kouzelnik = _content.Load<Texture2D>("Sprites\\Postavy\\kouzelnik");
            Kouzelnice = _content.Load<Texture2D>("Sprites\\Postavy\\kouzelnice");
            Dvere = _content.Load<Texture2D>("Sprites\\PrvkyMapy\\dvere");
            EnemyLehky = _content.Load<Texture2D>("Sprites\\PrvkyMapy\\enemy_lehky");
            EnemyStredni = _content.Load<Texture2D>("Sprites\\PrvkyMapy\\enemy_stredni");
            EnemyTezky = _content.Load<Texture2D>("Sprites\\PrvkyMapy\\enemy_tezky");
            Kamen = _content.Load<Texture2D>("Sprites\\PrvkyMapy\\kamen");
            LahvickaMana = _content.Load<Texture2D>("Sprites\\PrvkyMapy\\lahvicka_mana");
            LahvickaZdravi = _content.Load<Texture2D>("Sprites\\PrvkyMapy\\lahvicka_zdravi");
            PostavaDolu = _content.Load<Texture2D>("Sprites\\PrvkyMapy\\postava_dolu");
            PostavaLevo = _content.Load<Texture2D>("Sprites\\PrvkyMapy\\postava_levo");
            PostavaNahoru = _content.Load<Texture2D>("Sprites\\PrvkyMapy\\postava_nahoru");
            PostavaPravo = _content.Load<Texture2D>("Sprites\\PrvkyMapy\\postava_pravo");
            Strom = _content.Load<Texture2D>("Sprites\\PrvkyMapy\\strom");
            Trava = _content.Load<Texture2D>("Sprites\\PrvkyMapy\\trava");
            PrazdnaTexturaBila = new Texture2D(_game.GraphicsDevice, 1, 1);
            PrazdnaTexturaBila.SetData(new Color[] { Color.White });
            PrazdnaTexturaCerna = new Texture2D(_game.GraphicsDevice, 1, 1);
            PrazdnaTexturaCerna.SetData(new Color[] { Color.Black });
            Intro = _content.Load<Texture2D>("Sprites\\GUI\\intro");
            HlavniMenu= _content.Load<Texture2D>("Sprites\\GUI\\hlavni_menu");
            Informace = _content.Load<Texture2D>("Sprites\\GUI\\info");
            ButtonNovaHra = _content.Load<Texture2D>("Sprites\\GUI\\nova_hra");
            Button1v1 = _content.Load<Texture2D>("Sprites\\GUI\\1 v 1");
            ButtonKonec = _content.Load<Texture2D>("Sprites\\GUI\\konec");

            //načtení zvuků
            Fireball = _content.Load<SoundEffect>("Sound\\fireball");
            Frostbolt = _content.Load<SoundEffect>("Sound\\ledove_kopi");
            BowPull = _content.Load<SoundEffect>("Sound\\luk_natah");
            BowMiss = _content.Load<SoundEffect>("Sound\\netrefil_sip");
            Hit = _content.Load<SoundEffect>("Sound\\zasah");
            Regen = _content.Load<SoundEffect>("Sound\\regen");
            Battlecry = _content.Load<SoundEffect>("Sound\\battlecry");
            Click = _content.Load<SoundEffect>("Sound\\click");


            //načtení hudby
            MenuMusic = _content.Load<Song>("Music\\intromusic");
            MapMusic = _content.Load<Song>("Music\\mapmusic");
            BattleMusic = _content.Load<Song>("Music\\battlemusic");
            CreateMusic = _content.Load<Song>("Music\\createmusic");

            //nastavení přehrávače hudby
            MediaPlayer.Volume = 0.2f;
            MediaPlayer.IsRepeating = true;

        }

        public Texture2D DejPortret(Pohlavi pohlavi, Trida trida)
        {
            switch (pohlavi)
            {
                case Pohlavi.Muz:
                    switch (trida)
                    {
                        case Trida.Valecnik: return Valecnik;
                        case Trida.Lucistnik: return Lucistnik;
                        case Trida.Kouzelnik: return Kouzelnik;

                    }
                    break;
                case Pohlavi.Zena:
                    switch (trida)
                    {
                        case Trida.Valecnik: return Valecnice;
                        case Trida.Lucistnik: return Lucistnice;
                        case Trida.Kouzelnik: return Kouzelnice;
                    }
                    break;
            }
            return null;
        }

        public void MusicPlayer()
        {
            if (_game.CurrentState == ZacarovanyLes.menuState && MediaPlayer.State != MediaState.Playing)
            {
                MediaPlayer.Play(MenuMusic);

            }
            if (_game.CurrentState == ZacarovanyLes.mapState && MediaPlayer.State != MediaState.Playing)
            {
                MediaPlayer.Play(MapMusic);

            }
            if (_game.CurrentState == ZacarovanyLes.gameState && MediaPlayer.State != MediaState.Playing)
            {
                MediaPlayer.Play(BattleMusic);
            }
        }
    }
}
