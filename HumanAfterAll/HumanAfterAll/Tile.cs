using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace HumanAfterAll
{
    public abstract class Tile
    {
        #region _Variables
        public Color _colour = Color.White;
        protected Texture2D _texture;
        protected bool _shouldHaveBloodOnTop;
        protected bool _shouldHaveBloodOnBottom;
        protected bool _shouldHaveBloodOnLeft;
        protected bool _shouldHaveBloodOnRight;
        protected Texture2D _bloodLeft;
        protected Texture2D _bloodRight;
        protected Texture2D _bloodTop;
        protected Texture2D _bloodBottom;
        
        public Body _body;
        protected World _world;
        
        #endregion

        #region Functions
        public abstract void Update();
        public abstract void Draw(SpriteBatch _spriteBatch);
        public abstract bool Body_OnCollision(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact);
        
        #endregion

    }
}
