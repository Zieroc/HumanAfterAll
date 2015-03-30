using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;

namespace HumanAfterAll
{
    public class ParticleEmitter
    {
        public enum Type
        {
            BLOOD,
            
        }
        public List<Particle> _liveParticles = new List<Particle>();
        Vector2 _position;
        Vector2 _velocity;
        Texture2D _texture;
        World _world;
        Player _player;

        public ParticleEmitter(ContentManager _content, Vector2 _position, Vector2 _velocity, Type _type,World _world,Player _player)
        {
            switch (_type)
            {
                case Type.BLOOD:
                    _texture = _content.Load<Texture2D>("Blood");
                    break;
            }
            this._position = _position;
            this._velocity = _velocity;
            this._world = _world;
            this._player = _player;
        }

        public void Update()
        {
            _position = (_player._body.Position)* Game1.unitToPixel + _player._offset;
            _velocity = _player._body.LinearVelocity * -1;
            _liveParticles.Add(new Particle(_texture, _position, _velocity, _world));
            

            if (_liveParticles.Count > 40)
            {
                if (_liveParticles[0]._body != null)
                {

                    _world.RemoveBody(_liveParticles[0]._body);
                }
                _liveParticles.RemoveAt(0);
            }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            //_spriteBatch.DrawString(ScreenManager._spriteFont, "Num :" + _liveParticles.Count, new Vector2(0, 0), Color.White);
            foreach (Particle item in _liveParticles)
            {

                item.Draw(_spriteBatch);
            }
        }
    }
}
