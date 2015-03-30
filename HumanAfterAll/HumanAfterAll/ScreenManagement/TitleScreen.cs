using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace HumanAfterAll
{
    public class TitleScreen : GameScreen
    {
        #region Variables

        private Texture2D _background;
        private int _selectedItem;
        private bool _downPressed;
        private bool _upPressed;
        Song _titleTheme;
        #endregion

        #region Constructor

        public TitleScreen()
        {
            GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
            _selectedItem = 1;
            _downPressed = false;
            _upPressed = false;
        }

        #endregion

        #region Load/Unload

        public override void LoadContent(ContentManager content)
        {
            _background = content.Load<Texture2D>("hugMe");
            _titleTheme = content.Load<Song>("TitleTheme");
           // MediaPlayer.Play(_titleTheme);
            MediaPlayer.Volume = 0.5f;
            MediaPlayer.IsRepeating = true;
        }

        public override void UnloadContent()
        {
        }

        #endregion

        #region Update

        public override void Update(GameTime gameTime)
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                _selectedItem++;
                if (_selectedItem > 4)
                {
                    _selectedItem = 1;
                }
            }
            if ((gamePadState.DPad.Down == ButtonState.Pressed || gamePadState.ThumbSticks.Left.Y == -1f) && !_downPressed)
            {
                _selectedItem++;

                if (_selectedItem > 4)
                {
                    _selectedItem = 1;
                }

                _downPressed = true;
            }
            else
            {
                if (!(gamePadState.DPad.Down == ButtonState.Pressed || gamePadState.ThumbSticks.Left.Y == -1f))
                {
                    _downPressed = false;
                }
            }

            if ((gamePadState.DPad.Up == ButtonState.Pressed || gamePadState.ThumbSticks.Left.Y == 1f) && !_upPressed)
            {
                _selectedItem--;

                if (_selectedItem < 1)
                {
                    _selectedItem = 3;
                }

                _upPressed = true;
            }
            else
            {
                if (!(gamePadState.DPad.Up == ButtonState.Pressed || gamePadState.ThumbSticks.Left.Y == 1f))
                {
                    _upPressed = false;
                }
            }
            if (gamePadState.IsButtonDown(Buttons.A) || Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                MenuControl();
            }
        }

        #endregion

        #region Draw

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont spriteFont = ScreenManager.SpriteFont;

            spriteBatch.Begin();
            spriteBatch.Draw(_background, new Rectangle(0,0,640,480), _background.Bounds, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);
            
            Vector2 loc = new Vector2(0, 100);
            loc.X = _screenManager.Game.GraphicsDevice.Viewport.Width / 2 - spriteFont.MeasureString("PLAY GAME").X/2;
            if (_selectedItem == 1)
            {
                spriteBatch.DrawString(spriteFont, "Play Game", loc, Color.Red);
            }
            else
            {
                spriteBatch.DrawString(spriteFont, "Play Game", loc, Color.Gainsboro);
            }

            loc.Y += spriteFont.MeasureString("CREDITS").Y;
            loc.X = _screenManager.Game.GraphicsDevice.Viewport.Width / 2 - spriteFont.MeasureString("CREDITS").X/2;
            if (_selectedItem == 2)
            {
                spriteBatch.DrawString(spriteFont, "Credits", loc, Color.Red);
            }
            else
            {
                spriteBatch.DrawString(spriteFont, "Credits", loc, Color.Gainsboro);
            }

            loc.Y += spriteFont.MeasureString("HOW TO").Y;
            loc.X = _screenManager.Game.GraphicsDevice.Viewport.Width / 2 - spriteFont.MeasureString("HOW TO").X/2;
            if (_selectedItem == 3)
            {
                spriteBatch.DrawString(spriteFont, "How To", loc, Color.Red);
            }
            else
            {
                spriteBatch.DrawString(spriteFont, "How To", loc, Color.Gainsboro);
            }
            loc.Y += spriteFont.MeasureString("CODE").Y;
            loc.X = _screenManager.Game.GraphicsDevice.Viewport.Width / 2 - spriteFont.MeasureString("CODE").X / 2;
            if (_selectedItem == 4)
            {
                spriteBatch.DrawString(spriteFont, "Code", loc, Color.Red);
            }
            else
            {
                spriteBatch.DrawString(spriteFont, "Code", loc, Color.Gainsboro);
            }
            spriteBatch.End();
        }

        #endregion

        #region MenuControl

        public void MenuControl()
        {
            switch (_selectedItem)
            {
                case 1:
                    _screenManager.CurrentState = HumanAfterAll.ScreenManager.GameState.SPLASH;
                    //_screenManager.CurrentState = HumanAfterAll.ScreenManager.GameState.LEVEL3;
                    break;
                case 2:
                    _screenManager.CurrentState = HumanAfterAll.ScreenManager.GameState.CREDITS;
                    break;
                case 3:
                   _screenManager.CurrentState = HumanAfterAll.ScreenManager.GameState.HOWTO;
                    break;
                case 4:
                    _screenManager.CurrentState = HumanAfterAll.ScreenManager.GameState.CODE;
                    break;
                default :
                    _screenManager.CurrentState = HumanAfterAll.ScreenManager.GameState.PLAY;
                    
                    break;
            }
        }

        #endregion
    }
}
