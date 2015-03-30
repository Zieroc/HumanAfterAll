using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace HumanAfterAll
{
    public class Level4 : GameScreen
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
        ParallaxBackground _background2;

        #endregion

        #region Constructor

        public Level4()
        {
            _world = new World(new Vector2(0, 9.8f));
            _testMap = new int[30, 100]
            {
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,3,3,3,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,0,0,0,1,3,3,3,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,1},
            {1,3,3,3,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,1,3,3,3,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,1,0,0,0,0,1},
            {1,3,3,3,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,1,3,3,3,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,1},
            {1,3,3,3,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,1,3,3,3,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,3,3,3,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,1,3,3,3,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,3,3,3,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,1,3,3,3,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,1},
            {1,3,3,3,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,1,0,0,0,1,0,0,0,0,0,0,0,1,3,3,3,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,1},
            {1,3,3,3,1,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,1,0,0,0,0,0,0,0,1,3,3,3,1,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1},
            {1,3,3,3,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,3,3,3,1,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,3,3,3,1,1,1,0,0,0,0,4,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,3,3,3,1,0,0,0,1,0,0,1,0,0,4,0,0,0,0,0,0,0,0,0,4,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,3,3,3,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,3,3,3,1,0,0,0,1,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,3,3,3,1,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,3,3,3,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,3,3,3,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,3,3,3,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,3,3,3,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,3,3,3,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,3,3,3,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,1,3,3,3,1,0,0,0,1,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,3,3,3,3,3,3,3,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,1,3,3,3,1,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,3,3,3,3,3,3,3,1,4,4,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,1,3,3,3,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,3,3,3,3,3,3,3,1,4,4,0,0,0,0,0,4,4,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,1,2,0,0,0,0,0,0,0,0,0,0,6,1,3,3,3,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,1,3,3,3,3,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,3,3,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,1,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,1,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,1,4,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,1,2,2,0,0,0,0,0,0,0,0,0,0,0,6,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
            };

        }

        #endregion

        #region Load//Unload

        public override void LoadContent(ContentManager content)
        {
            _cam = new Camera2D();
            _floorTileTexture = content.Load<Texture2D>("TronTiles");
            _splat = new TextureManager(content);
            _player = new Player(new Vector2(33, 864), _world, content, _splat, _cam, PlayerIndex.One);
            _tileManager = new TileManager(_testMap, 100, 30, content, _world, _player, ScreenManager);
            _player.SetTiles(_tileManager.TilesInGame);
            _splat.SetPlayer(_player);
            _overlay = content.Load<Texture2D>("Vignette");
            _enemyManager = EnemyManager.GetInstance(_world);
            _enemyManager.AddEnemy(new FatMan(new Vector2(480, 832), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));
            _enemyManager.AddEnemy(new Robot(new Vector2(320, 864), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));
            _enemyManager.AddEnemy(new Robot(new Vector2(640, 864), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));
            _enemyManager.AddEnemy(new Robot(new Vector2(512, 640), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));
            _enemyManager.AddEnemy(new FatMan(new Vector2(512, 384), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));
            _enemyManager.AddEnemy(new FatMan(new Vector2(800, 384), _world, content, 2f, 5, 400, 1000, _player, _splat, _cam));
            _enemyManager.AddEnemy(new Robot(new Vector2(256, 416), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));
            _enemyManager.AddEnemy(new Robot(new Vector2(288, 160), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));
            _enemyManager.AddEnemy(new Robot(new Vector2(608, 160), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));
            _enemyManager.AddEnemy(new Robot(new Vector2(1536, 352), _world, content, 2f, 5, 1378, 1566, _player, _splat, _cam));
            _enemyManager.AddEnemy(new Robot(new Vector2(1344, 640), _world, content, 2f, 5, 1200, 1450, _player, _splat, _cam));
            _enemyManager.AddEnemy(new Robot(new Vector2(1216, 608), _world, content, 2f, 5, 1100, 1350, _player, _splat, _cam));
            _enemyManager.AddEnemy(new FatMan(new Vector2(1216, 800), _world, content, 2f, 5, 1100, 1500, _player, _splat, _cam));
            _enemyManager.AddEnemy(new Robot(new Vector2(1824, 128), _world, content, 2f, 5, 1800, 2900, _player, _splat, _cam));
            _enemyManager.AddEnemy(new Robot(new Vector2(2124, 128), _world, content, 2f, 5, 1800, 2900, _player, _splat, _cam));
            _enemyManager.AddEnemy(new Robot(new Vector2(2544, 128), _world, content, 2f, 5, 1800, 2900, _player, _splat, _cam));
            _enemyManager.AddEnemy(new Robot(new Vector2(2750, 128), _world, content, 2f, 5, 1800, 2900, _player, _splat, _cam));
            _enemyManager.AddEnemy(new FatMan(new Vector2(2000, 96), _world, content, 2f, 5, 1800, 2900, _player, _splat, _cam));
            _enemyManager.AddEnemy(new Robot(new Vector2(2816, 864), _world, content, 2f, 5, 2800, 3100, _player, _splat, _cam));
            _enemyManager.AddEnemy(new Robot(new Vector2(2980, 864), _world, content, 2f, 5, 2800, 3100, _player, _splat, _cam));
            _enemyManager.AddEnemy(new Robot(new Vector2(2208, 864), _world, content, 2f, 5, 2200, 2400, _player, _splat, _cam));
            _enemyManager.AddEnemy(new FatMan(new Vector2(1984, 832), _world, content, 2f, 5, 1800, 2500, _player, _splat, _cam));
            _enemyManager.AddEnemy(new FatMan(new Vector2(2450, 832), _world, content, 2f, 5, 1800, 2500, _player, _splat, _cam));
            _enemyManager.AddEnemy(new FatMan(new Vector2(2200, 832), _world, content, 2f, 5, 1800, 2500, _player, _splat, _cam));

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
            if (Player._blood < 0)
            {
                _screenManager.CurrentState = HumanAfterAll.ScreenManager.GameState.TITLE;
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
            _background2.Draw(spriteBatch);
            spriteBatch.Draw(_overlay, new Vector2((_player._body.Position.X * Game1.unitToPixel) - 320, (_player._body.Position.Y * Game1.unitToPixel) - 240), Color.White);
            //spriteBatch.DrawString(ScreenManager._spriteFont, "StartTime : " + _cam._startTime + " _currentTime :" + _cam._currentTime + " ShakeTime :" + _cam._shakeTime, _player._body.Position * Game1.unitToPixel, Color.Black);
            spriteBatch.End();
        }

        #endregion
    }
}
