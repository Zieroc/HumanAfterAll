using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Input;

namespace HumanAfterAll
{
    public class Level3 : GameScreen
    {
        #region Variables

        World _world;
        TileManager _tileManager;
        Texture2D _floorTileTexture;
        Texture2D _backgroundTexture;
        int[,] _testMap;
        Player _player;
        Camera2D _cam;
        EnemyManager _enemyManager;
        TextureManager _splat;
        Texture2D _overlay;
        ParallaxBackground _background;

        #endregion

        #region Constructor

        public Level3()
        {

            _world = new World(new Vector2(0, 9.8f));
            _testMap = new int[30, 30]
            {
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,1},
{1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,1},
{1,1,1,1,0,0,1,1,1,1,0,0,0,1,1,1,1,0,0,0,0,0,0,0,1,0,0,0,0,1},
{1,0,0,1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,1},
{1,0,0,1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1},
{1,0,0,1,0,0,1,0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,1},
{1,0,0,1,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,1},
{1,0,0,1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,1,1,1,1,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,1},
{1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,1},
{1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,1},
{1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,1},
{1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,1},
{1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
                };
        }

        #endregion

        #region Load/Unload

        public override void LoadContent(ContentManager content)
        {
            _cam = new Camera2D();
            _floorTileTexture = content.Load<Texture2D>("TronTiles");
            _splat = new TextureManager(content);
            _player = new Player(new Vector2(35, 896), _world, content, _splat, _cam, PlayerIndex.One);
            _tileManager = new TileManager(_testMap, 30, 30, content, _world, _player);
            _player.SetTiles(_tileManager.TilesInGame);
            _splat.SetPlayer(_player);
            _overlay = content.Load<Texture2D>("Vignette");
            _enemyManager = EnemyManager.GetInstance(_world);
            _enemyManager.AddEnemy(new Robot(new Vector2(160, 928), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));
            _enemyManager.AddEnemy(new Robot(new Vector2(200, 928), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));
            _enemyManager.AddEnemy(new FatMan(new Vector2(250, 928), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));
            _enemyManager.AddEnemy(new Robot(new Vector2(300, 928), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));
            _enemyManager.AddEnemy(new Robot(new Vector2(350, 928), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));
            _enemyManager.AddEnemy(new FatMan(new Vector2(400, 928), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));

            _enemyManager.AddEnemy(new Robot(new Vector2(704, 512), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));
            _enemyManager.AddEnemy(new Robot(new Vector2(576, 416), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));
            _enemyManager.AddEnemy(new FatMan(new Vector2(448, 320), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));
            _enemyManager.AddEnemy(new Robot(new Vector2(384, 544), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));
            _enemyManager.AddEnemy(new Robot(new Vector2(256, 416), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));
            _enemyManager.AddEnemy(new FatMan(new Vector2(832, 352), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));

            Player._blood = 100;
            _backgroundTexture = content.Load<Texture2D>("testBackground3");
            _background = new ParallaxBackground(_backgroundTexture, _cam, _player);

        }

        public override void UnloadContent()
        {
        }

        #endregion

        #region Update

        public override void Update(GameTime gameTime)
        {

            _world.Step((float)gameTime.ElapsedGameTime.TotalSeconds);
            _cam._pos = _player._body.Position * Game1.unitToPixel;

            _player.Update(gameTime);
            _splat.Update();
            _enemyManager.Update(gameTime);
            _cam.ShakeCamera();

            _background.Update(gameTime);

            if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Back))
            {
                _screenManager.CurrentState = HumanAfterAll.ScreenManager.GameState.TITLE;
            }
            if (Player._blood < 0)
            {
                _screenManager.CurrentState = HumanAfterAll.ScreenManager.GameState.TITLE;
            }
            if (_enemyManager.Enemies.Count <= 0)
            {
                _screenManager.CurrentState = HumanAfterAll.ScreenManager.GameState.CREDITS;
            }
        }

        #endregion

        #region Draw

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = _screenManager.SpriteBatch;
            spriteBatch.Begin(SpriteSortMode.BackToFront,
                        BlendState.AlphaBlend,
                        null,
                        null,
                        null,
                        null,
                        _cam.get_transformation(ScreenManager.Game.GraphicsDevice));

            _tileManager.Draw(spriteBatch);
            _player.Draw(spriteBatch);
            _enemyManager.Draw(spriteBatch);
            _splat.Draw(spriteBatch);
            _background.Draw(spriteBatch);
            spriteBatch.Draw(_overlay, new Vector2((_player._body.Position.X * Game1.unitToPixel) - 320, (_player._body.Position.Y * Game1.unitToPixel) - 240), Color.White);
            //spriteBatch.DrawString(ScreenManager._spriteFont, "StartTime : " + _cam._startTime + " _currentTime :" + _cam._currentTime + " ShakeTime :" + _cam._shakeTime, _player._body.Position * Game1.unitToPixel, Color.Black);
            spriteBatch.End();
        }

        #endregion
    }
}
