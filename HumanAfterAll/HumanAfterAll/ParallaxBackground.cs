using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HumanAfterAll
{
    public class ParallaxBackground
    {
        #region Variables

        Vector2 _position;
        Texture2D _background;
        Camera2D _camRef;
        Player _player;

        #endregion

        #region Constructor

        public ParallaxBackground(Texture2D _background, Camera2D _camRef, Player _player, int i)
        {
            this._background = _background;
            if (i == 0)
            {
                _position = new Vector2(-320, 0);
            }
            else
            {
                _position = new Vector2(-320 + _background.Width, 0);
            }
            this._camRef = _camRef;
            this._player = _player;
        }

        #endregion

        #region Update

        public void Update(GameTime gameTime)
        {
          // _position.X += (_player._body.LinearVelocity.X * 0.002f) - 320;
        }

        #endregion

        #region Draw

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_background, new Rectangle((int)_position.X, (int)_camRef._pos.Y - 240, _background.Width, _background.Height), _background.Bounds, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1);
        }

        #endregion
    }
}
