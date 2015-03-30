using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HumanAfterAll
{
    public class FinishedCombo
    {
        #region Variables

        private Vector2 _position;
        private string _text;
        private float _scale;
        private bool _active;
        private float _timer;
        private float _interval;
        private Player _player;

        #endregion

        #region Properties

        public bool Active
        {
            get { return _active; }
        }

        #endregion

        #region Constructor

        public FinishedCombo(string _text, Player _player)
        {
            this._text = _text;
            _scale = 0.1f;
            _active = true;
            _interval = 750f;
            _timer = 0f;
            _active = true;
            this._player = _player;
        }

        #endregion

        #region Update

        public void Update(GameTime gameTime)
        {
            if (_timer < _interval)
            {
                if (_timer < 250f)
                {
                    _scale += 0.2f;
                    _position.X = (_player._body.Position.X * Game1.unitToPixel) - (ScreenManager._spriteFont.MeasureString(_text).X * _scale) / 2;
                    _position.Y = (_player._body.Position.Y * Game1.unitToPixel) - (ScreenManager._spriteFont.MeasureString(_text).Y * _scale) / 2 - 50;
                }
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
