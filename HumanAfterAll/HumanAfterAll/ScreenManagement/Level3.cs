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
        ParallaxBackground _background2;
        Switch _switch1;
        Switch _switch2;
        Switch _switch3;

        #endregion

        #region Constructor

        public Level3()
        {

            _world = new World(new Vector2(0, 9.8f));
            _testMap = new int[30, 100] {
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0,1,0,0,0,0,6,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,4,4,1,0,0,0,1,1,1,1,1,1,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,1,0,0,0,0,0,0,1,5,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,1,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,1,4,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,1,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,2,0,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,1,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,1,1,1,1,3,3,3,1,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,3,3,3,1,0,0,0,0,0,6,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,4,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,1,3,3,3,1,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,3,3,3,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,3,3,3,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,3,3,3,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,1},
            {1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,3,3,3,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,1},
            {1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,3,3,3,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,4,0,0,0,0,0,1},
            {1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,3,3,3,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,1},
            {1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,3,3,3,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,3,3,3,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,3,3,3,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,3,3,3,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,3,3,3,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,3,3,3,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,0,0,1,1,1,1,1,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,1,1,3,3,3,3,3,3,3,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,4,0,0,1,1,1,1,1,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,1,1,3,3,3,3,3,3,3,3,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,4,0,0,1,1,1,1,1,1,0,0,2,1,2,0,0,0,0,0,0,0,0,0,4,4,0,0,0,0,0,1,1,3,3,3,3,3,3,3,3,3,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
            };
        }

        #endregion

        #region Load/Unload

        public override void LoadContent(ContentManager content)
        {
            _cam = new Camera2D();
            _floorTileTexture = content.Load<Texture2D>("TronTiles");
            _splat = new TextureManager(content);
            _player = new Player(new Vector2(35, 128), _world, content, _splat, _cam, PlayerIndex.One);
            _tileManager = new TileManager(_testMap, 100, 30, content, _world, _player,ScreenManager);
            _player.SetTiles(_tileManager.TilesInGame);
            _splat.SetPlayer(_player);
            _overlay = content.Load<Texture2D>("Vignette");
            _enemyManager = EnemyManager.GetInstance(_world);
            _enemyManager.AddEnemy(new Robot(new Vector2(2272, 832), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));
            _enemyManager.AddEnemy(new Robot(new Vector2(2872, 832), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));
            _enemyManager.AddEnemy(new FatMan(new Vector2(2452,832), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));
            _enemyManager.AddEnemy(new Robot(new Vector2(3008, 832), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));
            _enemyManager.AddEnemy(new Robot(new Vector2(2600, 832), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));
            _enemyManager.AddEnemy(new FatMan(new Vector2(2389,832), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));

            _enemyManager.AddEnemy(new Robot(new Vector2(2464, 130), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));
            _enemyManager.AddEnemy(new Robot(new Vector2(1632, 130), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));
            _enemyManager.AddEnemy(new Robot(new Vector2(1536, 130), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));
            _enemyManager.AddEnemy(new Robot(new Vector2(1504, 352), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));
            _enemyManager.AddEnemy(new Robot(new Vector2(288, 832), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));
            _enemyManager.AddEnemy(new FatMan(new Vector2(544, 832), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));
            _enemyManager.AddEnemy(new FatMan(new Vector2(1334, 832), _world, content, 2f, 5, 0, 800, _player, _splat, _cam));

            _switch1 = new Switch(_screenManager.Game.Content.Load<Texture2D>("switch"), new Vector2(992, 32), _screenManager.Game.Content, _world, _player, _tileManager,Color.Blue);
            _switch1._tilesToBeToggled.Add(_tileManager.TilesInGame[_tileManager._easierSwitch[2505]]);
            _switch1._tilesToBeToggled.Add(_tileManager.TilesInGame[_tileManager._easierSwitch[2605]]);
            _switch1._tilesToBeToggled.Add(_tileManager.TilesInGame[_tileManager._easierSwitch[2705]]);
            _switch1._tilesToBeToggled.Add(_tileManager.TilesInGame[_tileManager._easierSwitch[2805]]);

            _switch2 = new Switch(_screenManager.Game.Content.Load<Texture2D>("switch"), new Vector2(32, 896), _screenManager.Game.Content, _world, _player, _tileManager,Color.Yellow);
            _switch2._tilesToBeToggled.Add(_tileManager.TilesInGame[_tileManager._easierSwitch[132]]);
            _switch2._tilesToBeToggled.Add(_tileManager.TilesInGame[_tileManager._easierSwitch[232]]);
            _switch2._tilesToBeToggled.Add(_tileManager.TilesInGame[_tileManager._easierSwitch[332]]);
            _switch2._tilesToBeToggled.Add(_tileManager.TilesInGame[_tileManager._easierSwitch[432]]);

            _switch3 = new Switch(_screenManager.Game.Content.Load<Texture2D>("switch"), new Vector2(1056, 896), _screenManager.Game.Content, _world, _player, _tileManager,Color.Green);
            _switch3._tilesToBeToggled.Add(_tileManager.TilesInGame[_tileManager._easierSwitch[2558]]);
            _switch3._tilesToBeToggled.Add(_tileManager.TilesInGame[_tileManager._easierSwitch[2559]]);
            _switch3._tilesToBeToggled.Add(_tileManager.TilesInGame[_tileManager._easierSwitch[2560]]);

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
            _switch1.Draw(spriteBatch);
            _switch2.Draw(spriteBatch);
            _switch3.Draw(spriteBatch);
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
