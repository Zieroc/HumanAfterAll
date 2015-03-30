using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;

namespace HumanAfterAll
{
    public class EnemyManager
    {
        World _world;
        List<NPC> _enemies;
        List<Points> _points;
        private static EnemyManager _instance;

        private EnemyManager(World _world)
        {
            this._world = _world;
            _enemies = new List<NPC>();
            _points = new List<Points>();
        }

        public static EnemyManager GetInstance(World _world)
        {
            
            if (_instance == null)
            {
                _instance = new EnemyManager(_world);
            }

            return _instance;
        }

        public List<NPC> Enemies
        {
            get { return _enemies; }
            set { _enemies = value; }
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < _enemies.Count; i++ )
            {
                if (_enemies[i].Alive)
                {
                    _enemies[i].Update();
                }
                else
                {
                    _enemies[i]._body.CollisionCategories = Category.None;
                   _world.RemoveBody(_enemies[i]._body);
                    _enemies.RemoveAt(i);
                }
            }
            for (int i = 0; i < _points.Count; i++)
            {
                if (_points[i].Active)
                {
                    _points[i].Update(gameTime);
                }
                else
                {
                    _points.RemoveAt(i);
                }
            }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            foreach (NPC item in _enemies)
            {
                item.Draw(_spriteBatch);
            }
            foreach (Points item in _points)
            {
                item.Draw(_spriteBatch);
            }
        }

        public void AddEnemy(NPC enemy)
        {
            _enemies.Add(enemy);
        }

        public void AddPoints(Points points)
        {
            _points.Add(points);
        }
    }
}
