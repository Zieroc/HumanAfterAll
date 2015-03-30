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
    public abstract class NPC
    {
        #region Variables

        protected World _world;
        public Body _body;
        protected Vector2 _position;
        protected float _speed = 5;
        protected int _bloodReturn;
        protected bool _alive;
        public Player _player;
        public TextureManager _manager;
        public Camera2D _camRefrence;
        public SoundEffect _sound;
        protected AnimationManager _animation;
        #endregion

        #region Properties

        public bool Alive
        {
            get { return _alive; }
            set { _alive = value; }
        }

        public int BloodReturn
        {
            get { return _bloodReturn; }
        }

        #endregion

        #region Update

        public abstract void Update();

        #endregion

        #region Draw

        public virtual void Draw(SpriteBatch _spriteBatch)
        {
            _animation.Draw(_spriteBatch, _body.Position, _body.Rotation);
        }

        #endregion

        public bool Body_OnCollision(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact)
        {
            //foreach (Bullet item in _player._liveBullets)
            //{
            //    if (fixtureB.Body.BodyId == item._body.BodyId && fixtureA.Body.BodyId == this._body.BodyId)
            //    {
            //        _alive = false;
            //    }
            //}

            /*
            foreach (Bullet item in _player._liveBullets)
            {
                if (fixtureB.Body.BodyId == item._body.BodyId && fixtureA.Body.BodyId == this._body.BodyId)
                {
                    this.Alive = false;
                    _manager.AddSomeSplats(fixtureB.Body.Position * Game1.unitToPixel);
                    _camRefrence._shouldShake = true;
                    _camRefrence._divisor = 50;
                    _sound.Play();
                    Player._blood += 2;
                    EnemyManager.GetInstance(_world).AddPoints(new Points(_body.Position * Game1.unitToPixel, "" + 2));
                    fixtureB.Body.CollidesWith = Category.None;
                }
            }
                */
            return true;

        }
    }
}
