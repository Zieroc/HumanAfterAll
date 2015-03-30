using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

namespace HumanAfterAll
{
    public class Soldier : NPC
    {
        #region Variables

        Vector2 _targetPos;
        bool _hasTarget;
       
        int _minX, _maxX;
        Vector2 _direction;
        float _angle;
        ContentManager _content;
        List<Bullet> _bulletsInScene = new List<Bullet>();
        #endregion

        #region Constructor

        public Soldier(Vector2 _position, World _world, ContentManager _content, float _speed, int _bloodReturn, int _minX, int _maxX, Player _player, TextureManager _manager, Camera2D _camRefrence)
        {
            this._world = _world;
            this._content = _content;
            this._sound = _content.Load<SoundEffect>("explo4");
            this._manager = _manager;
            this._camRefrence = _camRefrence;
            this._player = _player;
            //_texture = _content.Load<Texture2D>("solBot-w-Camo");
            //_body = BodyFactory.CreateRoundedRectangle(_world, _texture.Width * Game1.pixelToUnit, _texture.Height * Game1.pixelToUnit, (_texture.Width * Game1.pixelToUnit) / 4, (_texture.Height * Game1.pixelToUnit) / 4, 4, 1f);
            _body.Restitution = 0;
            _body.BodyType = BodyType.Dynamic;
            _body.FixedRotation = true;
            this._body.Position = _position * Game1.pixelToUnit;
            this._speed = _speed;
            this._bloodReturn = _bloodReturn;
            _alive = true;
            //_reverse = false;
            this._minX = _minX;
            this._maxX = _maxX;
            _bloodReturn = 100;
            this._body.OnCollision += this.Body_OnCollision;
        }

        #endregion

        #region Update

        public override void Update()
        {
            _position = _body.Position * Game1.unitToPixel;
            _direction = (this._body.Position * Game1.unitToPixel) - (_player._body.Position * Game1.unitToPixel);
            _angle = (float)Math.Atan2(_direction.Y, _direction.X) + 3.14159268f;
               
            _bulletsInScene.Add(new Bullet(_content, this._body.Position - new Vector2(0, (38 * Game1.pixelToUnit)), (_direction* 2) * Game1.pixelToUnit, _world, _player, _manager, false));
               
            if (_bulletsInScene.Count > 15)
            {
                _world.RemoveBody(_bulletsInScene[0]._body);
                _bulletsInScene.RemoveAt(0);
            }
        }
        #endregion
        public override void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.DrawString(ScreenManager._spriteFont, "Num Bullets : " + _bulletsInScene.Count, _position, Color.Black);
            foreach (Bullet item in _bulletsInScene)
            {
                item.Draw(_spriteBatch);
            }
            if (_angle >1.57)
            {
                //_spriteBatch.Draw(_texture, new Rectangle((int)(_body.Position.X * Game1.unitToPixel), (int)(_body.Position.Y * Game1.unitToPixel), 55, 67), null, Color.White, _angle, new Vector2(_texture.Width / 2, _texture.Height / 2), SpriteEffects.None, 0f);
            }
            else
            {
               // _spriteBatch.Draw(_texture, new Rectangle((int)(_body.Position.X * Game1.unitToPixel), (int)(_body.Position.Y * Game1.unitToPixel), 55, 67), null, Color.White, _angle, new Vector2(_texture.Width / 2, _texture.Height / 2), SpriteEffects.None, 0f);
            }
            _spriteBatch.DrawString(ScreenManager._spriteFont, ""+ _direction* Game1.pixelToUnit, Vector2.Zero, Color.Black);
        }
    }
}
