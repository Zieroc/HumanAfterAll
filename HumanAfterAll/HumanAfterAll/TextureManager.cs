using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HumanAfterAll
{
    public class TextureManager
    {
        List<Splat> _splatsInGame = new List<Splat>();
        Texture2D []_texture = new Texture2D[10];
        ContentManager _content;
        Player _player;
        public TextureManager(ContentManager _content)
        {
            
            this._content = _content;
            _texture[0] = _content.Load<Texture2D>("splat");
            _texture[1] = _content.Load<Texture2D>("splat_2");
            _texture[2] = _content.Load<Texture2D>("splat_3");
            _texture[3] = _content.Load<Texture2D>("splat_4");
            _texture[4] = _content.Load<Texture2D>("splat_5");
            _texture[5] = _content.Load<Texture2D>("splat_6");
            _texture[6] = _content.Load<Texture2D>("splat_7");
            _texture[7] = _content.Load<Texture2D>("splat_8");
            _texture[8] = _content.Load<Texture2D>("splat_9");
            _texture[9] = _content.Load<Texture2D>("splat");
        }

        public void Update()
        {
            foreach (Splat item in _splatsInGame)
            {
                item.Update();
            }
           
        }
        public void SetPlayer(Player _player)
        {
            this._player = _player;
        }
        public void AddSomeSplats(Vector2 _place)
        {
            Random _r1 = new Random();
            _splatsInGame.Add(new Splat(_texture[_r1.Next(9)], (_place) + new Vector2(-64, -64), _player));
            
        }
        public void Draw(SpriteBatch _spriteBatch)
        {
            foreach (Splat item in _splatsInGame)
            {
                item.Draw(_spriteBatch);
            }
        }
    }
}
