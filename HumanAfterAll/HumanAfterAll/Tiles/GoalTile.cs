using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace HumanAfterAll
{
    class GoalTile : Tile
    {
       
        ParticleEmitter _blood;
        Player _player;
        SoundEffect _soundDrop;
        SoundEffectInstance _instance;
        ScreenManager _screenManager;
        bool _once = false;
        public GoalTile(Texture2D _texture, Vector2 _position, ContentManager _content, World _world, Player _player,ScreenManager _screenManager)
        {
            this._screenManager = _screenManager;
            this._player = _player;
            this._texture = _texture;
            _bloodBottom = _content.Load<Texture2D>("BloodBottom");
            _bloodLeft = _content.Load<Texture2D>("BloodLeft");
            _bloodRight = _content.Load<Texture2D>("BloodRight");
            _bloodTop = _content.Load<Texture2D>("BloodTop");
            _soundDrop = _content.Load<SoundEffect>("waterDroplet");
            this._body = BodyFactory.CreateRectangle(_world, _texture.Width * Game1.pixelToUnit, _texture.Height * Game1.pixelToUnit, 1f);
            this._body.BodyType = BodyType.Kinematic;
            this._body.Position = _position * Game1.pixelToUnit;
            this._body.Restitution = 1f;
            this._blood = _player._emitter;
            _body.OnCollision += Body_OnCollision;
            _instance = _soundDrop.CreateInstance();
        }
        public override void Update()
        {

        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            _body.Friction = 1;
            _spriteBatch.Draw(_texture, new Rectangle((int)(_body.Position.X * Game1.unitToPixel), (int)(_body.Position.Y * Game1.unitToPixel), _texture.Width, _texture.Height), null, _colour, _body.Rotation, new Vector2(_texture.Width / 2, _texture.Height / 2), SpriteEffects.None, 0.5f);
            if (_shouldHaveBloodOnBottom)
            {
                _spriteBatch.Draw(_bloodBottom, new Rectangle((int)(_body.Position.X * Game1.unitToPixel), (int)(_body.Position.Y * Game1.unitToPixel), _texture.Width, _texture.Height), null, Color.Black, _body.Rotation, new Vector2(_texture.Width / 2, _texture.Height / 2), SpriteEffects.None, 0.4f);

            }
            if (_shouldHaveBloodOnTop)
            {
                _spriteBatch.Draw(_bloodTop, new Rectangle((int)(_body.Position.X * Game1.unitToPixel), (int)(_body.Position.Y * Game1.unitToPixel), _texture.Width, _texture.Height), null, Color.Black, _body.Rotation, new Vector2(_texture.Width / 2, _texture.Height / 2), SpriteEffects.None, 0.4f);
                _body.Friction = 0f;
            }
            if (_shouldHaveBloodOnLeft)
            {
                _spriteBatch.Draw(_bloodLeft, new Rectangle((int)(_body.Position.X * Game1.unitToPixel), (int)(_body.Position.Y * Game1.unitToPixel), _texture.Width, _texture.Height), null, Color.Black, _body.Rotation, new Vector2(_texture.Width / 2, _texture.Height / 2), SpriteEffects.None, 0.4f);

            }
            if (_shouldHaveBloodOnRight)
            {
                _spriteBatch.Draw(_bloodRight, new Rectangle((int)(_body.Position.X * Game1.unitToPixel), (int)(_body.Position.Y * Game1.unitToPixel), _texture.Width, _texture.Height), null, Color.Black, _body.Rotation, new Vector2(_texture.Width / 2, _texture.Height / 2), SpriteEffects.None, 0.4f);

            }
        }
        public override bool Body_OnCollision(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact)
        {
            if (fixtureB.Body == _player._body && _once == false)
            {
                _screenManager.CurrentState++;
                _once = true;
            }
            return false;
        }
    }
}
