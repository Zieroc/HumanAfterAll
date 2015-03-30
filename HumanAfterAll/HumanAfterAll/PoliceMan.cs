using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

namespace HumanAfterAll
{
    public class PoliceMan : NPC
    {
        #region Variables

        Vector2 _targetPos;
        bool _hasTarget;
        int _direction;
        int _minX, _maxX;

        #endregion

        #region Constructor

        public PoliceMan(Vector2 _position, World _world, ContentManager _content, float _speed, int _bloodReturn, int _minX, int _maxX)
        {
            //_texture = _content.Load<Texture2D>("manBot");
            //_body = BodyFactory.CreateRoundedRectangle(_world, _texture.Width * Game1.pixelToUnit, _texture.Height * Game1.pixelToUnit, (_texture.Width * Game1.pixelToUnit) / 4, (_texture.Height * Game1.pixelToUnit) / 4, 4, 1f);
            _body.Restitution = 0;
            _body.BodyType = BodyType.Dynamic;
            _body.FixedRotation = true;
            this._body.Position = _position * Game1.pixelToUnit;
            this._speed = _speed;
            this._bloodReturn = _bloodReturn;
            _alive = true;
            _direction = 1;
            this._minX = _minX;
            this._maxX = _maxX;
        }

        #endregion

        #region Update

        public override void Update()
        {
            if (_direction == -1)
            {
                _body.ApplyForce(new Vector2(-_speed, 0));
                if (_body.Position.X * Game1.unitToPixel <= _minX)
                {
                    _direction *= -1;
                }
            }
            else
            {
                _body.ApplyForce(new Vector2(_speed, 0));
                if (_body.Position.X * Game1.unitToPixel >= _maxX)
                {
                    _direction *= -1;
                }
            }

            /*
            if (_body.LinearVelocity.X > 0.5)
            {
                _reverse = false;
            }
            else if (_body.LinearVelocity.X < -0.5f)
            {
                _reverse = true;
            }
             * */
        }
        #endregion
    }
}
