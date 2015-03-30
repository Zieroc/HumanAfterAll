using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HumanAfterAll
{
    class Splat
    {
        Texture2D _texture;
        Vector2 _position;
        Player _player;
        public Splat(Texture2D _texture, Vector2 _position,Player _player)
        {
            this._player = _player;
            this._texture = _texture;
            this._position = _position;
        }
        public void Update()
        {
           // _position.X += (_player._body.LinearVelocity.X * 0.2f) - 320;
            //_position.Y += (_player._body.LinearVelocity.Y);
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_texture, new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height),null,Color.Black * .8f,0f,Vector2.Zero,SpriteEffects.None,0.7f);
        }
    }
}
