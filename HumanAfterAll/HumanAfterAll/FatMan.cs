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

namespace HumanAfterAll
{
    public class FatMan : NPC
    {
        #region Variables

        Vector2 _targetPos;
        bool _hasTarget;
        int _direction;
        int _minX, _maxX;

        #endregion

        #region Constructor

        public FatMan(Vector2 _position, World _world, ContentManager _content, float _speed, int _bloodReturn, int _minX, int _maxX, Player _player, TextureManager _manager, Camera2D _camRefrence)
        {
            _animation = new AnimationManager();
            //Add Animations
            _animation.AddAnimation(new Animation(700, new Vector2(55, 67), 8, true), _content.Load<Texture2D>("BigbotSprite")); //IDLE

            this._sound = _content.Load<SoundEffect>("explo4");
            this._manager = _manager;
            this._camRefrence = _camRefrence;
            this._player = _player;
            _body = BodyFactory.CreateCircle(_world, 27 * Game1.pixelToUnit, 1f);
            _body.Restitution = 1;
            _body.BodyType = BodyType.Dynamic;
            _body.FixedRotation = false;
            this._body.Position = _position * Game1.pixelToUnit;
            this._speed = _speed;
            this._bloodReturn = _bloodReturn;
            _alive = true;
            _direction = 1;
            this._minX = _minX;
            this._maxX = _maxX;
            this._body.OnCollision += this.Body_OnCollision;
            _body.ApplyTorque(1); 
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

            _animation.Update(_body.LinearVelocity);
        }
        #endregion

    }
}
