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
    public class GameplayScreen : GameScreen
    {
        #region Variables

        World _world;
        TileManager _tileManager;
        Texture2D _floorTileTexture;
        Texture2D _backgroundTexture;
        int[,] _testMap;
        Player _player;
        Camera2D _cam;
        
        TextureManager _splat;
        Texture2D _overlay;
        ParallaxBackground _background;
        ParallaxBackground _background2;
        Switch _switch1;
        Switch _switch2;
        Switch _switch3;
        Switch _switch4;
        #endregion

        #region Constructor

        public GameplayScreen()
        {

            _world = new World(new Vector2(0, 9.8f));
            _testMap = new int[30, 50]
            {
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                {1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,1,4,4,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,1,5,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                {1,0,0,0,1,1,1,1,1,1,1,1,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,1},
                {1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,1},
                {1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,0,0,0,1},
                {1,0,0,0,0,0,1,1,1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,1,0,0,0,0,0,1,1,1,0,0,1,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,1,1,1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,1,1,1,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,1},
                {1,0,0,0,0,0,0,0,0,0,0,1,4,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,6,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,1,1,1},
                {1,0,0,0,0,0,0,0,0,0,0,1,4,4,0,0,0,0,0,0,0,0,0,0,6,1,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,1,1,1,1},
                {1,1,1,1,1,1,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1},
                {1,0,0,0,0,1,1,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1},
                {1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1},
                {1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1},
                {1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
            };
              
        }

        #endregion

        #region Load/Unload

        public override void LoadContent(ContentManager content)
        {
            
             _cam = new Camera2D();
            _floorTileTexture = content.Load<Texture2D>("TronTiles");
           
            _splat = new TextureManager(content);
            _player = new Player(new Vector2(33, 864), _world, content,_splat,_cam,PlayerIndex.One);
            _tileManager = new TileManager(_testMap, 50,30, content, _world,_player,ScreenManager);
            _player.SetTiles(_tileManager.TilesInGame);
            _splat.SetPlayer(_player);
            _overlay = content.Load<Texture2D>("Vignette");
            _enemyManager = EnemyManager.GetInstance(_world);
            _enemyManager.AddEnemy(new Robot(new Vector2(33, 320), _world, content, 2f, 5, 0, 800,_player,_splat,_cam));
            _enemyManager.AddEnemy(new Robot(new Vector2(160, 64), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));
            _enemyManager.AddEnemy(new Robot(new Vector2(640, 704), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));
            _enemyManager.AddEnemy(new Robot(new Vector2(640, 865), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));
            _enemyManager.AddEnemy(new Robot(new Vector2(768, 865), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));
            _enemyManager.AddEnemy(new Robot(new Vector2(960, 865), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));
            _enemyManager.AddEnemy(new Robot(new Vector2(1160, 864), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));
            _enemyManager.AddEnemy(new Robot(new Vector2(1088, 672), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));
            _enemyManager.AddEnemy(new Robot(new Vector2(1312, 192), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));
            _enemyManager.AddEnemy(new Robot(new Vector2(640, 192), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));
            _enemyManager.AddEnemy(new Robot(new Vector2(864, 192), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));

            _switch1 = new Switch(_screenManager.Game.Content.Load<Texture2D>("switch"), new Vector2(768, 896), _screenManager.Game.Content, _world, _player, _tileManager, Color.Green);
            _switch1._tilesToBeToggled.Add(_tileManager.TilesInGame[_tileManager._easierSwitch[261]]);
            _switch1._tilesToBeToggled.Add(_tileManager.TilesInGame[_tileManager._easierSwitch[311]]);
            _switch1._tilesToBeToggled.Add(_tileManager.TilesInGame[_tileManager._easierSwitch[361]]);
            _switch1._tilesToBeToggled.Add(_tileManager.TilesInGame[_tileManager._easierSwitch[411]]);

            _switch2 = new Switch(_screenManager.Game.Content.Load<Texture2D>("switch"), new Vector2(768, 576), _screenManager.Game.Content, _world, _player, _tileManager, Color.Yellow);
            _switch2._tilesToBeToggled.Add(_tileManager.TilesInGame[_tileManager._easierSwitch[708]]);
            _switch2._tilesToBeToggled.Add(_tileManager.TilesInGame[_tileManager._easierSwitch[709]]);
            _switch2._tilesToBeToggled.Add(_tileManager.TilesInGame[_tileManager._easierSwitch[710]]);

            _switch3 = new Switch(_screenManager.Game.Content.Load<Texture2D>("switch"), new Vector2(32, 416), _screenManager.Game.Content, _world, _player, _tileManager, Color.Red);
            _switch3._tilesToBeToggled.Add(_tileManager.TilesInGame[_tileManager._easierSwitch[1261]]);
            _switch3._tilesToBeToggled.Add(_tileManager.TilesInGame[_tileManager._easierSwitch[1311]]);
            _switch3._tilesToBeToggled.Add(_tileManager.TilesInGame[_tileManager._easierSwitch[1361]]);
            _switch3._tilesToBeToggled.Add(_tileManager.TilesInGame[_tileManager._easierSwitch[1411]]);

            _switch4 = new Switch(_screenManager.Game.Content.Load<Texture2D>("switch"), new Vector2(1248, 608), _screenManager.Game.Content, _world, _player, _tileManager, Color.Blue);
            _switch4._tilesToBeToggled.Add(_tileManager.TilesInGame[_tileManager._easierSwitch[445]]);
            _switch4._tilesToBeToggled.Add(_tileManager.TilesInGame[_tileManager._easierSwitch[446]]);
            _switch4._tilesToBeToggled.Add(_tileManager.TilesInGame[_tileManager._easierSwitch[447]]);
            _switch4._tilesToBeToggled.Add(_tileManager.TilesInGame[_tileManager._easierSwitch[448]]);
            
            Player._blood = 100;
            _backgroundTexture = content.Load<Texture2D>("testBackground3");
            _background = new ParallaxBackground(_backgroundTexture, _cam, _player, 0);
            _background2 = new ParallaxBackground(_backgroundTexture, _cam, _player, 1);
         
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
            if (Player._blood<0)
            {
                _screenManager.CurrentState = HumanAfterAll.ScreenManager.GameState.TITLE;
            }
            //if (_enemyManager.Enemies.Count <= 0)
            //{
            //    _screenManager.CurrentState++;
            //}
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
            _background2.Draw(spriteBatch);
            spriteBatch.Draw(_overlay, new Vector2((_player._body.Position.X * Game1.unitToPixel) - 320, (_player._body.Position.Y * Game1.unitToPixel) - 240), Color.White);
            //spriteBatch.DrawString(ScreenManager._spriteFont, "StartTime : " + _cam._startTime + " _currentTime :" + _cam._currentTime + " ShakeTime :" + _cam._shakeTime, _player._body.Position * Game1.unitToPixel, Color.Black);
            _switch1.Draw(spriteBatch);
            _switch2.Draw(spriteBatch);
            _switch3.Draw(spriteBatch);
            _switch4.Draw(spriteBatch);
            spriteBatch.End();
        }

        #endregion
    }
}
