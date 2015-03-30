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
    public class CodeScreen : GameScreen
    {
        #region Constructor
        Texture2D _blank;
        Texture2D _x;
        Texture2D _y;
        Texture2D _b;
        Texture2D _a;
        bool _once = false;
        bool _once2 = false;
        bool _once3 = false;
        bool _once4 = false;
      
        List<Texture2D> _codes = new List<Texture2D>();
        public CodeScreen()
        {
          
        }

        #endregion

        #region Load/Unload

        public override void LoadContent(ContentManager content)
        {
            _blank = content.Load<Texture2D>("empty");
            _x = content.Load<Texture2D>("x");
            _y = content.Load<Texture2D>("y");
            _a = content.Load<Texture2D>("a");
            _b = content.Load<Texture2D>("b");
            _codes.Add(_blank);
            _codes.Add(_blank);
            _codes.Add(_blank);
            _codes.Add(_blank);
        }

        public override void UnloadContent()
        {
        }

        #endregion

        #region Update

        public override void Update(GameTime gameTime)
        {
            if ((Keyboard.GetState().IsKeyDown(Keys.A)) && _once == false)
            {
                _codes.Add(_a);
                _once = true;
            }
            else if ((Keyboard.GetState().IsKeyUp(Keys.A) ) && _once == true)
            {
                Console.WriteLine("Up");
                _once = false;
            }

            if ((Keyboard.GetState().IsKeyDown(Keys.B)) && _once2 == false)
            {
                _codes.Add(_b);
                _once2 = true;
            }
            else if ((Keyboard.GetState().IsKeyUp(Keys.B)) && _once2 == true)
            {
                Console.WriteLine("Up");
                _once2 = false;
            }

            if ((Keyboard.GetState().IsKeyDown(Keys.X)) && _once3 == false)
            {
                _codes.Add(_x);
                _once3 = true;
            }
            else if ((Keyboard.GetState().IsKeyUp(Keys.X)) && _once3 == true)
            {
                Console.WriteLine("Up");
                _once3 = false;
            }

            if ((Keyboard.GetState().IsKeyDown(Keys.Y)) && _once4 == false)
            {
                _codes.Add(_y);
                _once4 = true;
            }
            else if ((Keyboard.GetState().IsKeyUp(Keys.Y)) && _once4 == true)
            {
                Console.WriteLine("Up");
                _once4 = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) || GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed)
            {
                if (_codes[0] == _x)
                {
                    _screenManager.CurrentState = HumanAfterAll.ScreenManager.GameState.PLAY;
                }
                else if (_codes[0] == _y)
                {
                    _screenManager.CurrentState = HumanAfterAll.ScreenManager.GameState.LEVEL2;
                }
                else if (_codes[0] == _a)
                {
                    _screenManager.CurrentState = HumanAfterAll.ScreenManager.GameState.LEVEL3;
                }
                else if (_codes[0] == _b)
                {
                    _screenManager.CurrentState = HumanAfterAll.ScreenManager.GameState.LEVEL4;
                }
                else if (_codes[0] == _blank)
                {
                    _screenManager.CurrentState = HumanAfterAll.ScreenManager.GameState.LEVEL4;
                }
            }
            if (_codes.Count > 4)
            {
                _codes.RemoveAt(0);
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
            loc.X = _screenManager.Game.GraphicsDevice.Viewport.Width / 2 - spriteFont.MeasureString("Enter Super Secret Passcodes..").X / 2;
            spriteBatch.DrawString(spriteFont, "Enter Super Secret Passcodes..", loc, Color.Gainsboro);

            for (int i = 0; i < _codes.Count; i++)
            {
               spriteBatch.Draw(_codes[i], new Vector2((460 - (i * 128)), 300), Color.White);
             //   spriteBatch.Draw(_codes[i], new Vector2(((i * 128) + 100), 300), Color.White);
            }

            spriteBatch.End();
        }

        #endregion
    }
}
