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
    public class HowTo : GameScreen
    {
        #region Constructor

        public HowTo()
        {
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

            if (gamePadState.IsButtonDown(Buttons.B))
            {
                _screenManager.CurrentState = HumanAfterAll.ScreenManager.GameState.TITLE;
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

            Vector2 loc = new Vector2(0, 100);
            loc.X = _screenManager.Game.GraphicsDevice.Viewport.Width / 2 - spriteFont.MeasureString("").X / 2;
            spriteBatch.DrawString(spriteFont, "", loc, Color.Yellow);

            loc.Y += spriteFont.MeasureString("---Controls---").Y;
            loc.X = _screenManager.Game.GraphicsDevice.Viewport.Width / 2 - spriteFont.MeasureString("---Controls---").X / 2;
            spriteBatch.DrawString(spriteFont, "---Controls---", loc, Color.Gainsboro);

            loc.Y += spriteFont.MeasureString("Left Analog to Control Robot").Y + 5;
            loc.X = _screenManager.Game.GraphicsDevice.Viewport.Width / 2 - spriteFont.MeasureString("Left Analog to Control Robot").X / 2;
            spriteBatch.DrawString(spriteFont, "Left Analog to Control Robot", loc, Color.Yellow);

            loc.Y += spriteFont.MeasureString("A to jump and wall jump :D Hold A for easy Climbing").Y;
            loc.X = _screenManager.Game.GraphicsDevice.Viewport.Width / 2 - spriteFont.MeasureString("A to jump and wall jump :D Hold A for easy Climbing").X / 2;
            spriteBatch.DrawString(spriteFont, "A to jump and wall jump :D Hold A for easy Climbing", loc, Color.Gainsboro);

            loc.Y += spriteFont.MeasureString("Right Analog to use your blasters :D Pyew pyew").Y + 5;
            loc.X = _screenManager.Game.GraphicsDevice.Viewport.Width / 2 - spriteFont.MeasureString("Right Analog to use your blasters :D Pyew pyew").X / 2;
            spriteBatch.DrawString(spriteFont, "Right Analog to use your blasters :D Pyew pyew", loc, Color.Yellow);

            loc.Y += spriteFont.MeasureString("Kill All The Enemies In The Level Before You Run Out Of Fuel").Y;
            loc.X = _screenManager.Game.GraphicsDevice.Viewport.Width / 2 - spriteFont.MeasureString("Kill All The Enemies In The Level Before You Run Out Of Fuel").X / 2;
            spriteBatch.DrawString(spriteFont, "Kill All The Enemies In The Level Before You Run Out Of Fuel", loc, Color.Gainsboro);

            loc.Y += spriteFont.MeasureString("B To Return to Menu").Y + 5;
            loc.X = _screenManager.Game.GraphicsDevice.Viewport.Width / 2 - spriteFont.MeasureString("B To Return to Menu").X / 2;
            spriteBatch.DrawString(spriteFont, "B To Return to Menu", loc, Color.Yellow);

           

            spriteBatch.End();
        }

        #endregion
    }
}
