using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HumanAfterAll
{
    public class ScreenManager : DrawableGameComponent
    {
        #region GameStateEnum

        public enum GameState
        {
            TITLE,
            CODE,
            HOWTO,
            SPLASH,
            PLAY,
            SPLASH1,
            LEVEL2,
            SPLASH2,
            LEVEL3,
            SPLASH3,
            LEVEL4,
            SPLASH4,
            LEVEL5,
            SPLASH5,
            SPLASH6,
            CREDITS
        }

        #endregion

        #region Variables

        private GameScreen _screen;
        private GameState _currentState;
        private GameState _lastState;
        private SpriteBatch _spriteBatch;
        public static SpriteFont _spriteFont;
        private bool _isInitialized;

        //Splash Screens
        Texture2D _splashTexture1;
        Texture2D _splashStart;

        #endregion

        #region Properties

        public GameScreen Screen
        {
            get { return _screen; }
        }

        public GameState CurrentState
        {
            get { return _currentState; }
            set { _currentState = value; }
        }

        public GameState LastState
        {
            get { return _lastState; }
        }

        public SpriteBatch SpriteBatch
        {
            get { return _spriteBatch; }
        }

        public SpriteFont SpriteFont
        {
            get { return _spriteFont; }
        }

        #endregion

        #region Constructor

        public ScreenManager(Game game) : base(game)
        {
            _currentState = GameState.TITLE;
            _lastState = GameState.CREDITS; //Make last state different to so the title screen will be auto-created
            _screen = new TitleScreen();
        }

        #endregion

        #region Initialize

        public override void Initialize()
        {
            base.Initialize();

            _isInitialized = true;
        }

        #endregion

        #region Load/Unload

        protected override void LoadContent()
        {

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _spriteFont = Game.Content.Load<SpriteFont>("GameFont");
            _splashTexture1 = Game.Content.Load<Texture2D>("Splash");
            _splashStart = Game.Content.Load<Texture2D>("SplashStart");
            _screen.LoadContent(Game.Content);
        }

        protected override void UnloadContent()
        {
            _screen.UnloadContent();
        }

        #endregion
        
        #region Update

        public override void Update(GameTime gameTime)
        {
            switch (_currentState)
            {
                case GameState.TITLE:
                    if (_lastState != _currentState)
                    {
                        SwitchScreen(new TitleScreen());
                    }
                break;
                case GameState.CODE:
                if (_lastState != _currentState)
                {
                    SwitchScreen(new CodeScreen());
                }
                break;
                case GameState.PLAY:
                    if (_lastState != _currentState)
                    {
                        SwitchScreen(new GameplayScreen());
                    }
                break;
                case GameState.LEVEL2:
                if (_lastState != _currentState)
                {
                    SwitchScreen(new Level2());
                }
                break;
                case GameState.LEVEL3:
                if (_lastState != _currentState)
                {
                    SwitchScreen(new Level3());
                }
                break;

                case GameState.LEVEL4:
                if (_lastState != _currentState)
                {
                    SwitchScreen(new Level4());
                }
                break;
                case GameState.HOWTO:
                if (_lastState != _currentState)
                {
                    SwitchScreen(new HowTo());
                }
                break;
                case GameState.LEVEL5:
                if (_lastState != _currentState)
                {
                    SwitchScreen(new Level5());
                }
                break;
        
                case GameState.CREDITS:
                if (_lastState != _currentState)
                {
                    SwitchScreen(new CreditsScreen());
                }
                break;
                case GameState.SPLASH:
                if (_lastState != _currentState)
                {
                    SwitchScreen(new SplashScreen(_splashStart, "This is Charlie.\nHe spends his day activating switches to open doors for the other robots\nHe likes helping them and justs wants to be their friend\nBut whenever he talks to an other robot they just walk away"));
                }
                break;
                case GameState.SPLASH1:
                if (_lastState != _currentState)
                {
                    SwitchScreen(new SplashScreen(Game.Content.Load<Texture2D>("Splash2"), "It didn't matter what he said, they would always run\nThey jumped on the springs and bounced away\nMaybe if he bounced up with them he would find a friend"));
                }

                break;
                case GameState.SPLASH2:
                if (_lastState != _currentState)
                {
                    SwitchScreen(new SplashScreen(Game.Content.Load<Texture2D>("Splash3"), "He jumped on the spring and bounced up to them\nEven up here they ran away\nThey jumped into the water and floated even higher\nMaybe they wanted to go swimming with him\nCharlie found a new robot up here, surely this one would be his friend"));
                }

                break;
                case GameState.SPLASH3:
                if (_lastState != _currentState)
                {
                    SwitchScreen(new SplashScreen(Game.Content.Load<Texture2D>("Splash4"), "This new robot ran away too, he didn't want to be Charlie's friend\nCharlie decided to follow them up the water\nHe reached the top and saw more robots than he had ever imagined\nMaybe up here he would find a friend"));
                }

                break;
                case GameState.SPLASH4:
                if (_lastState != _currentState)
                {
                    SwitchScreen(new SplashScreen(Game.Content.Load<Texture2D>("Splash5"), "Charlie had never been so far into the city.\nThere were robots everywhere but they all ran when he came close\nHe found himself in a strange place without switches and springs\nThen he heard a beating sound coming from somewhere below the ground"));
                }

                break;
                case GameState.SPLASH5:
                if (_lastState != _currentState)
                {
                    SwitchScreen(new SplashScreen(Game.Content.Load<Texture2D>("Splash6"), "Charlie followed the beating sound and made a great discovery\nA working heart that could fix his leak and make him safe\nWith his new heart in place the other robots no longer ran\nCharlie fixed the leak and finally found some friends"));
                }

                break;
                case GameState.SPLASH6:
                if (_lastState != _currentState)
                {
                    SwitchScreen(new SplashScreen(Game.Content.Load<Texture2D>("LastSplash"), ""));
                }

                break;
                
            }

            _screen.Update(gameTime);
        }

        #endregion

        #region Draw

        public override void Draw(GameTime gameTime)
        {
            _screen.Draw(gameTime);
        }

        #endregion

        #region SwitchScreen

        public void SwitchScreen(GameScreen screen)
        {
            screen.ScreenManager = this;
            if (_screen._enemyManager != null)
            {
                _screen._enemyManager.Enemies = new List<NPC>();
                
            }
            if (_isInitialized)
            {
                screen.LoadContent(Game.Content);

                if (_screen != null)
                {
                    _screen.UnloadContent();
                }
            }

            _screen = screen;
            _lastState = _currentState;
        }

        #endregion
    }
}
