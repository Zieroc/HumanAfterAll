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
            PLAY,
            CREDITS
        }

        #endregion

        #region Variables

        private GameScreen _screen;
        private GameState _currentState;
        private GameState _lastState;
        private SpriteBatch _spriteBatch;
        private SpriteFont _spriteFont;
        private bool _isInitialized;

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
            // Load content belonging to the screen manager.
            ContentManager content = Game.Content;

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _spriteFont = content.Load<SpriteFont>("GameFont");

            _screen.LoadContent(content);
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
                case GameState.PLAY:
                    if (_lastState != _currentState)
                    {
                        SwitchScreen(new GameplayScreen());
                    }
                break;
                case GameState.CREDITS:
                if (_lastState != _currentState)
                {
                    SwitchScreen(new CreditsScreen());
                }
                break;
            }

            _screen.Update(gameTime);
            _lastState = _currentState;
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

            if (_isInitialized)
            {
                screen.LoadContent(Game.Content);

                if (_screen != null)
                {
                    _screen.UnloadContent();
                }
            }

            _screen = screen;
        }

        #endregion
    }
}
