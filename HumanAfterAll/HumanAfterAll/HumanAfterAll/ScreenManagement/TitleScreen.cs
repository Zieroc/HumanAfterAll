using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HumanAfterAll
{
    public class TitleScreen : GameScreen
    {
        #region Variables

        private Texture2D _background;
        private int _selectedItem;

        #endregion

        #region Constructor

        public TitleScreen()
        {
            _selectedItem = 1;
        }

        #endregion

        #region Load/Unload

        public override void LoadContent(ContentManager content)
        {
            //_background = content.Load<Texture2D>("SOMETEXTURE");
        }

        public override void UnloadContent()
        {
        }

        #endregion

        #region Update

        public override void Update(GameTime gameTime)
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

            if (gamePadState.DPad.Down == ButtonState.Pressed || gamePadState.ThumbSticks.Left.Y == -1f)
            {
                _selectedItem++;
                
                if(_selectedItem > 3)
                {
                    _selectedItem = 1;
                }
            }
            if (gamePadState.DPad.Up == ButtonState.Pressed || gamePadState.ThumbSticks.Left.Y == 1f)
            {
                _selectedItem--;

                if (_selectedItem < 1)
                {
                    _selectedItem = 3;
                }
            }
            if (gamePadState.IsButtonDown(Buttons.A))
            {
                
            }
        }

        #endregion

        #region Draw

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont spriteFont = ScreenManager.SpriteFont;

            spriteBatch.Begin();
            //spriteBatch.Draw(_background, ScreenManager.Game.GraphicsDevice.Viewport.Bounds, _background.Bounds, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);
            
            Vector2 loc = new Vector2(0, 100);
            loc.X = _screenManager.Game.GraphicsDevice.Viewport.Width / 2 - spriteFont.MeasureString("PLAY GAME").X;
            if (_selectedItem == 1)
            {
                spriteBatch.DrawString(spriteFont, "PLAY GAME", loc, Color.White);
            }
            else
            {
                spriteBatch.DrawString(spriteFont, "PLAY GAME", loc, Color.Yellow);
            }

            loc.Y += spriteFont.MeasureString("CREDITS").Y;
            loc.X = _screenManager.Game.GraphicsDevice.Viewport.Width / 2 - spriteFont.MeasureString("CREDITS").X;
            if (_selectedItem == 2)
            {
                spriteBatch.DrawString(spriteFont, "CREDITS", loc, Color.White);
            }
            else
            {
                spriteBatch.DrawString(spriteFont, "CREDITS", loc, Color.Yellow);
            }

            loc.Y += spriteFont.MeasureString("EXIT").Y;
            loc.X = _screenManager.Game.GraphicsDevice.Viewport.Width / 2 - spriteFont.MeasureString("EXIT").X;
            if (_selectedItem == 1)
            {
                spriteBatch.DrawString(spriteFont, "EXIT", loc, Color.White);
            }
            else
            {
                spriteBatch.DrawString(spriteFont, "EXIT", loc, Color.Yellow);
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
                    _screenManager.CurrentState = HumanAfterAll.ScreenManager.GameState.PLAY;
                    break;
                case 2:
                    _screenManager.CurrentState = HumanAfterAll.ScreenManager.GameState.CREDITS;
                    break;
                case 3:
                    _screenManager.Game.Exit();
                    break;
            }
        }

        #endregion
    }
}
