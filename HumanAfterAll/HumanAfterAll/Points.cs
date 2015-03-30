using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HumanAfterAll
{
    public class Points
    {
        #region Variables

        private Vector2 _position;
        private string _text;
        private float _scale;
        private bool _active;
        private float _timer;
        private float _interval;

        #endregion

        #region Properties

        public bool Active
        {
            get { return _active; }
        }

        #endregion

        #region Constructor

        public Points(Vector2 _position, string _text)
        {
            this._position =_position;
            this._text = _text;
            _scale = 0.1f;
            _active = true;
            _interval = 1000f;
            _timer = 0f;
        }

        #endregion

        #region Update

        public void Update(GameTime gameTime)
        {
            if (_timer < _interval)
            {
                _position.Y--;
                _scale += 0.1f;
                _timer += gameTime.ElapsedGameTime.Milliseconds;
            }
            else
            {
                _active = false;
            }
        }

        #endregion

        #region Draw

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(ScreenManager._spriteFont, _text, _position, Color.Gainsboro, 0f, Vector2.Zero, _scale, SpriteEffects.None, 0);
        }
        
        #endregion
    }
}
