using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace HumanAfterAll
{
    public abstract class GameScreen
    {
        #region Variables

        protected ScreenManager _screenManager;

        #endregion

        #region Properties

        public ScreenManager ScreenManager
        {
            get { return _screenManager; }
            set { _screenManager = value; }
        }

        #endregion

        #region Virtual Methods

        public abstract void LoadContent(ContentManager content);
        public abstract void UnloadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);

        #endregion
    }
}
