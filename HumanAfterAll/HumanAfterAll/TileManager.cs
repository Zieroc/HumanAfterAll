using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace HumanAfterAll
{
    class TileManager
    {
        int _width;
        int _height;
        Texture2D _floorTexture;
        Texture2D _springTexture;
        Texture2D _waterTexture;
        Texture2D _crateTexture;
        Texture2D _goalTexture;
        Texture2D _fillerTexture;
        List<Tile> _tilesInGame = new List<Tile>();
        ContentManager _content;
        ParticleEmitter _emitterRefrence;
        public World _world;

        public List<int> _easierSwitch = new List<int>();

        public TileManager(int[,] _tileMap,int _height,int _width,ContentManager _content,World _world,Player _player,ScreenManager _screenManager)
        {
            this._world = _world;
            this._content = _content;
            this._emitterRefrence =_player._emitter;
            _floorTexture = _content.Load<Texture2D>("TronTiles");
            _springTexture = _content.Load<Texture2D>("spring");
            _waterTexture = _content.Load<Texture2D>("Water");
            _crateTexture = _content.Load<Texture2D>("Crate");
            _goalTexture = _content.Load<Texture2D>("door");
            
            _fillerTexture = _content.Load<Texture2D>("Filler");
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    _easierSwitch.Add(0);
                    if (_tileMap[i,j] == 1)
                    {
                        _tilesInGame.Add(new FloorTile(_floorTexture, new Vector2(j * 32, i * 32), _content, _world,_player));
                        _easierSwitch[_easierSwitch.Count - 1] = _tilesInGame.Count - 1;
                    }
                    if (_tileMap[i, j] == 2)
                    {
                        _tilesInGame.Add(new SpringTile(_springTexture, new Vector2(j * 32, i * 32), _content, _world, _player));
                        _easierSwitch[_easierSwitch.Count - 1] = _tilesInGame.Count - 1;
                    }
                    if (_tileMap[i, j] == 3)
                    {
                        _tilesInGame.Add(new WaterTile(_waterTexture, new Vector2(j * 32, i * 32), _content, _world, _player));
                        _easierSwitch[_easierSwitch.Count - 1] = _tilesInGame.Count - 1;
                    }
                    if (_tileMap[i, j] == 4)
                    {
                        _tilesInGame.Add(new Crate(_crateTexture, new Vector2(j * 32, i * 32), _content, _world, _player));
                        _easierSwitch[_easierSwitch.Count - 1] = _tilesInGame.Count - 1;
                    }
                    if (_tileMap[i, j] == 5)
                    {
                        _tilesInGame.Add(new GoalTile(_goalTexture, new Vector2(j * 32, i * 32), _content, _world, _player,_screenManager));
                        _easierSwitch[_easierSwitch.Count - 1] = _tilesInGame.Count - 1;
                    }
                    if (_tileMap[i, j] == 6)
                    {
                        _tilesInGame.Add(new OilFiller(_fillerTexture, new Vector2(j * 32, i * 32), _content, _world, _player, _screenManager));
                        _easierSwitch[_easierSwitch.Count - 1] = _tilesInGame.Count - 1;
                    }
                 }
            }
        }

        public List<Tile> TilesInGame
        {
            get { return _tilesInGame; }
        }

        public void Update()
        {
            foreach (Tile item in _tilesInGame)
            {
                item.Update();
            }
        }
        public void Draw(SpriteBatch _spriteBatch)
        {
            foreach (Tile item in _tilesInGame)
            {
                item.Draw(_spriteBatch);
            }
        }
    }
}
