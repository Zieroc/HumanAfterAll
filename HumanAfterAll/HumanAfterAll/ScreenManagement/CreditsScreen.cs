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
    public class CreditsScreen : GameScreen
    {
        #region Constructor

        public CreditsScreen()
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
            loc.X = _screenManager.Game.GraphicsDevice.Viewport.Width / 2 - spriteFont.MeasureString("Lead Programmer").X/2;
            spriteBatch.DrawString(spriteFont, "Lead Programmer", loc, Color.Yellow);

            loc.Y += spriteFont.MeasureString("Joseph Bentley").Y;
            loc.X = _screenManager.Game.GraphicsDevice.Viewport.Width / 2 - spriteFont.MeasureString("Joseph Bentley").X / 2;
            spriteBatch.DrawString(spriteFont, "Joseph Bentley", loc, Color.Gainsboro);

            loc.Y += spriteFont.MeasureString("Programmer").Y + 5;
            loc.X = _screenManager.Game.GraphicsDevice.Viewport.Width / 2 - spriteFont.MeasureString("Programmer").X / 2;
            spriteBatch.DrawString(spriteFont, "Programmer", loc, Color.Yellow);

            loc.Y += spriteFont.MeasureString("Christy Carroll").Y;
            loc.X = _screenManager.Game.GraphicsDevice.Viewport.Width / 2 - spriteFont.MeasureString("Christy Carroll").X / 2;
            spriteBatch.DrawString(spriteFont, "Christy Carroll", loc, Color.Gainsboro);

            loc.Y += spriteFont.MeasureString("Artist").Y + 5;
            loc.X = _screenManager.Game.GraphicsDevice.Viewport.Width / 2 - spriteFont.MeasureString("Artist").X / 2;
            spriteBatch.DrawString(spriteFont, "Artist", loc, Color.Yellow);

            loc.Y += spriteFont.MeasureString("Mark Luna").Y;
            loc.X = _screenManager.Game.GraphicsDevice.Viewport.Width / 2 - spriteFont.MeasureString("Mark Luna").X / 2;
            spriteBatch.DrawString(spriteFont, "Mark Luna", loc, Color.Gainsboro);

            loc.Y += spriteFont.MeasureString("Audio").Y + 5;
            loc.X = _screenManager.Game.GraphicsDevice.Viewport.Width / 2 - spriteFont.MeasureString("Audio").X / 2;
            spriteBatch.DrawString(spriteFont, "Audio", loc, Color.Yellow);

            loc.Y += spriteFont.MeasureString("David Morton").Y;
            loc.X = _screenManager.Game.GraphicsDevice.Viewport.Width / 2 - spriteFont.MeasureString("David Morton").X / 2;
            spriteBatch.DrawString(spriteFont, "David Morton", loc, Color.Gainsboro);

            spriteBatch.End();
        }

        #endregion
    }
}
