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
    public class Human : NPC
    {
        #region Variables

        Vector2 _targetPos;
        bool _hasTarget;

        #endregion

        #region Constructor

        public Human(Vector2 _position, World _world, ContentManager _content, float _speed, int _bloodReturn)
        {
            _texture = _content.Load<Texture2D>("floor");
            _body = BodyFactory.CreateRoundedRectangle(_world, _texture.Width * Game1.pixelToUnit, _texture.Height * Game1.pixelToUnit, (_texture.Width * Game1.pixelToUnit) / 4, (_texture.Height * Game1.pixelToUnit) / 4, 4, 1f);
            _body.Restitution = 0;
            _body.BodyType = BodyType.Dynamic;
            this._body.Position = _position * Game1.pixelToUnit;
            this._speed = _speed;
            this._bloodReturn = _bloodReturn;
        }

        #endregion

        #region Update

        public override void Update()
        {
            if (!_hasTarget)
            {
                Random _numGen = new Random();
                int _distance = _numGen.Next(60) + 1 - 30;
                if(_distance < 0 /*&& _distance > -10*/)
                {
                    _distance = -30;
                }
                if (_distance >= 0 /*&& _distance < 10*/)
                {
                    _distance = 30;
                }
                _targetPos = new Vector2(_position.X + _distance, _position.Y);
                if (_distance < 0)
                    _reverse = true;
                else
                    _reverse = false;

                _hasTarget = true;
            }
            else
            {
                Vector2 _oldPos = _position;

                if (_reverse)
                {
                    _body.ApplyForce(new Vector2(-_speed, 0));
                    if (_position.X < _targetPos.X)
                    {
                        _hasTarget = false;
                    }
                }
                else
                {
                    _body.ApplyForce(new Vector2(_speed, 0));
                    if (_position.X > _targetPos.X)
                    {
                        _hasTarget = false;
                    }
                }

                if (_oldPos == _position)
                {
                    _hasTarget = false;
                }
            }
        }
        #endregion

    }
}
