
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace HumanAfterAll
{
    public class Bullet
    {
        Texture2D _texture;
        public Body _body;
        Vector2 _velocity;
        World _world;
        public bool _shouldBeDestroyed;
        TextureManager _manager;
        public Bullet(ContentManager _content, Vector2 _position, Vector2 _velocity, World _world,Player _player,TextureManager _textureManager,bool _isPlayer)
        {
            this._manager = _textureManager;
            this._texture = _content.Load<Texture2D>("Blood");
            this._world = _world;
            _body = BodyFactory.CreateCircle(_world, (_texture.Width / 2) * Game1.pixelToUnit, 1f);
            //_body = BodyFactory.CreateRectangle(_world, _texture.Width * Game1.pixelToUnit, _texture.Height * Game1.pixelToUnit, 1f);
            _body.BodyType = BodyType.Dynamic;
            _body.Position = _position ;
            this._velocity = _velocity;
            this._body.FixedRotation = true;
            this._body.Restitution = 0.5f;
            
            if (_isPlayer)
            {
                _body.IgnoreCollisionWith(_player._body);
                _body.ApplyForce((new Vector2(GamePad.GetState(_player._index).ThumbSticks.Right.X, GamePad.GetState(_player._index).ThumbSticks.Right.Y * -1) * 20) * Game1.pixelToUnit);
            }
            else
            {
                _body.ApplyForce(_velocity);
            }
            _body.OnCollision += Body_OnCollision;
        }
        public bool Body_OnCollision(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact)
        {
            _shouldBeDestroyed = true;
           
            return false;
        }
        public void Update()
        {
            
        }
        public  void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_texture, new Rectangle((int)(_body.Position.X * Game1.unitToPixel), (int)(_body.Position.Y * Game1.unitToPixel), _texture.Width, _texture.Height), null, Color.Red, _body.Rotation, new Vector2(_texture.Width / 2, _texture.Height / 2), SpriteEffects.None, 0.5f);
            //_spriteBatch.DrawString(ScreenManager._spriteFont, "Velocity : " + _velocity, Vector2.Zero, Color.Black);
        }
       
    }
}


