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
    public class SplashScreen : GameScreen
    {
        Texture2D _texture;
        String _string;
        bool switchable = false;
        #region Constructor

        public SplashScreen(Texture2D _texture,String _string)
        {
            ComboManager.GetInstance().ResetCombo(); //Reset any combos that may have been triggered as the last level ended
            this._texture = _texture;
            this._string = _string;
        }

        #endregion

        #region Load/Unload

        public override void LoadContent(ContentManager content)
        {
        }

        public override void UnloadContent()
        {
        }

        #endregion

        #region Update

        public override void Update(GameTime gameTime)
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

            if (switchable)
            {
                if (gamePadState.IsButtonDown(Buttons.A) || Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    _screenManager.CurrentState++;
                }
            }
            else if (gamePadState.IsButtonUp(Buttons.A) && Keyboard.GetState().IsKeyUp(Keys.Enter))
            {
                switchable = true;
            }
        }

        #endregion

        #region Draw

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont spriteFont = ScreenManager.SpriteFont;

            _screenManager.GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            //spriteBatch.Draw(_background, ScreenManager.Game.GraphicsDevice.Viewport.Bounds, _background.Bounds, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);
            spriteBatch.Draw(_texture, new Rectangle(0, 0, 640, 480), Color.White);
            Vector2 loc = new Vector2(0, 380);
            loc.X = _screenManager.Game.GraphicsDevice.Viewport.Width / 2 - spriteFont.MeasureString(_string).X / 2;
            spriteBatch.DrawString(spriteFont, _string, loc, Color.Gainsboro);

           
            spriteBatch.End();
        }

        #endregion
    }
}
