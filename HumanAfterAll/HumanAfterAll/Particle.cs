using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using FarseerPhysics.Factories;

namespace HumanAfterAll
{
    public class Particle
    {
        Texture2D _texture;
        public Body _body;
        Vector2 _velocity;
        
        public Particle(Texture2D _texture,Vector2 _position,Vector2 _velocity,World _world)
        {
            _body = BodyFactory.CreateCircle(_world, (_texture.Width / 2) * Game1.pixelToUnit, 1f);
            //_body = BodyFactory.CreateRectangle(_world, _texture.Width * Game1.pixelToUnit, _texture.Height * Game1.pixelToUnit, 1f);
            _body.BodyType = BodyType.Dynamic;
            _body.Position = _position * Game1.pixelToUnit;
            this._velocity = _velocity;
            this._texture = _texture;
            this._body.FixedRotation = true;
            this._body.Restitution = 0.5f;
            //_body.ApplyForce(_velocity * Game1.pixelToUnit);
        }
        public void Update()
        {
            
        }
        public  void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_texture, new Rectangle((int)(_body.Position.X * Game1.unitToPixel), (int)(_body.Position.Y * Game1.unitToPixel), _texture.Width, _texture.Height), null, Color.Black, _body.Rotation, new Vector2(_texture.Width / 2, _texture.Height / 2), SpriteEffects.None, 0.5f);

        }
       
    }
}
