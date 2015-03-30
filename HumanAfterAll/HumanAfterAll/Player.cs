using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Content;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace HumanAfterAll
{
    public class Player
    {
        World _world;
        SoundEffect _jumpSound;
        SoundEffect _explosiveSound;
        Song _song1;
        public Body _body;
        Vector2 _position;
        //Texture2D _texture;
        float _speed = 5;
        //bool _lookingLeft = false;
        public Vector2 _offset = new Vector2(-20,0);
        public bool _canJump;
        bool _jumping;
        //bool _onWall;
        //public float _oldX;
        float _timer;
        float _jumpInterval;
        List<bool> wallHits = new List<bool>();
        List<Tile> _tiles;
        TextureManager _manager;
        Camera2D _camRefrence;
        public static float _blood = 100;
        public bool _dead = false;
        public PlayerIndex _index;
        public ParticleEmitter _emitter;
        Vector2 _direction;
        float _angle;
        public List<Particle> _liveBullets = new List<Particle>();
        ContentManager _content;
        AnimationManager _animation;
        ComboManager _comboMan;
        Texture2D _oilBarTexture;
        public Player(Vector2 _position,World _world,ContentManager _content,TextureManager _manager,Camera2D _cam,PlayerIndex _index)
        {
            _animation = new AnimationManager();
            //Add Animations
            _animation.AddAnimation(new Animation(700, new Vector2(37, 64), 1, true), _content.Load<Texture2D>("IMG_2127_PT2")); //TEST
            _animation.AddAnimation(new Animation(500, new Vector2(37, 64), 5, true), _content.Load<Texture2D>("Standing-Sprite-64x185")); //IDLE
            _animation.AddAnimation(new Animation(700, new Vector2(37, 64), 1, true), _content.Load<Texture2D>("IMG_2127_PT2")); //RUNNING
            _animation.AddAnimation(new Animation(150, new Vector2(50, 64), 8, false), _content.Load<Texture2D>("jump_sprite_64x397")); //JUMPING
            _animation.AddAnimation(new Animation(125, new Vector2(50, 64), 2, true), _content.Load<Texture2D>("fall_sprite_64x100")); //FALLING
            _animation.AddAnimation(new Animation(700, new Vector2(37, 64), 1, true), _content.Load<Texture2D>("IMG_2127_PT2")); //HARVESTING

            _comboMan = ComboManager.GetInstance();
            _comboMan.Player = this;

            this._world = _world;
            this._index = _index;
            this._content = _content;
            this._camRefrence = _cam;
            //_texture = _content.Load<Texture2D>("IMG_2127_PT2");
            _jumpSound = _content.Load<SoundEffect>("jump6");
            _explosiveSound = _content.Load<SoundEffect>("explo4");
            _song1 = _content.Load<Song>("searching");
            _oilBarTexture = _content.Load<Texture2D>("OilBar");
            _body = BodyFactory.CreateRoundedRectangle(_world, _animation.CurrentTexture.Width * Game1.pixelToUnit, _animation.CurrentTexture.Height * Game1.pixelToUnit, (_animation.CurrentTexture.Width * Game1.pixelToUnit) / 4, (_animation.CurrentTexture.Height * Game1.pixelToUnit) / 4, 4, 1f);
            _body.Restitution = 0;
            _body.BodyType = BodyType.Dynamic;
            _body.FixedRotation = true;
            
            this._body.Position = _position * Game1.pixelToUnit;
            _canJump = true;
            _jumping = false;
            //_onWall = false;
            _timer = 0f;
            _jumpInterval = 500f;
            _body.OnCollision += Body_OnCollision;
            this._manager = _manager;
            MediaPlayer.Play(_song1);
            MediaPlayer.IsRepeating = true;
            

            _emitter = new ParticleEmitter(_content, new Vector2(150, 100), new Vector2(1, -3), ParticleEmitter.Type.BLOOD, _world,this);
            
        }

        public void SetTiles(List<Tile> _tiles)
        {
            this._tiles = _tiles;
        }

        public void Update(GameTime gameTime)
        {
            if (_body.LinearVelocity.X > 10)
            {
                _body.LinearVelocity = new Vector2(10, _body.LinearVelocity.Y);
            }
            if (_body.LinearVelocity.X < -10)
            {
                _body.LinearVelocity = new Vector2(-10, _body.LinearVelocity.Y);
            }
            if (_body.LinearVelocity.Y > 10)
            {
                _body.LinearVelocity = new Vector2(_body.LinearVelocity.X,10);
            }
            if (_body.LinearVelocity.Y < -10)
            {
                _body.LinearVelocity = new Vector2(_body.LinearVelocity.X ,- 10);
            }
            if (_blood < 20)
            {
                if (_camRefrence._isShaking == false)
                {
                    //_camRefrence.ShakeCamera();
                    _camRefrence._shouldShake = true;
                }
            }
            if (_liveBullets.Count > 60)
            {
                _world.RemoveBody(_liveBullets[0]._body);
                _liveBullets.RemoveAt(0);
            }

            _direction = new Vector2((int)_position.X, (int)_position.Y - 12) - (((_body.Position * Game1.unitToPixel) + new Vector2(0, -20)) + new Vector2(GamePad.GetState(_index).ThumbSticks.Right.X, -GamePad.GetState(_index).ThumbSticks.Right.Y));
            _angle = (float)Math.Atan2(_direction.Y, _direction.X) + 3.14159268f;

            if ((GamePad.GetState(_index).ThumbSticks.Right.X>0.5 ||GamePad.GetState(_index).ThumbSticks.Right.X<-0.5 )||(GamePad.GetState(_index).ThumbSticks.Right.Y>0.5 ||GamePad.GetState(_index).ThumbSticks.Right.Y<-0.5 ))
            {

                _liveBullets.Add(new Particle(_content.Load<Texture2D>("Blood"), _position, _direction, _world));
            }
            
            _emitter.Update();
            _blood -= 0.05f;
            if (_blood > 100)
            {
                _blood = 100;
            }
            if (_blood < 0)
            {
                _dead = true;
            }
            _body.ApplyForce(new Vector2(GamePad.GetState(_index).ThumbSticks.Left.X * _speed,0));
            if(Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                _body.ApplyForce(new Vector2( _speed, 0));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                _body.ApplyForce(new Vector2(-_speed, 0));
            }

            
            if (_body.LinearVelocity.X > 0.5)
            {
              //  _lookingLeft = false;
                _offset = new Vector2(-25, 20);
            }
            else if(_body.LinearVelocity.X < -0.5f)
            {
            //    _lookingLeft = true;
                _offset = new Vector2(25, 20);
            }

            if (_jumping)
            {
                
                if (_timer > _jumpInterval)
                {
                    _jumping = false;
                    _timer = 0f;
                    _canJump = true;
                    
                }
                
                else
                {
                    _body.ApplyForce(new Vector2(0, -7.2f));
                    _timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                }
                if (_timer > _jumpInterval/2)
                {
                    _body.ApplyForce(new Vector2(0, 9.8f));
                    
                    //_jumpRest = true;
                }
            }
            
            if ((GamePad.GetState(_index).IsButtonDown(Buttons.A) || Keyboard.GetState().IsKeyDown(Keys.Space)) && _canJump)
            {
                _body.ApplyForce(new Vector2(0, -5));
                _canJump = false;
                _jumping = true;
                _jumpSound.Play();
            }


            
            if (wallHits.Count < 5)
            {
                wallHits.Add(false);
            }
            else
            {
                wallHits.RemoveAt(0);
                wallHits.Add(false);
            }

            _animation.Update(_body.LinearVelocity);
            _comboMan.Update(gameTime);
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            foreach (Particle item in _liveBullets)
            {
                item.Draw(_spriteBatch);
            }
            
            _emitter.Draw(_spriteBatch);

            _animation.Draw(_spriteBatch, _body.Position, _body.Rotation);

            /*
            if (_lookingLeft == false)
            {

                _spriteBatch.Draw(_texture, new Rectangle((int)(_body.Position.X * Game1.unitToPixel), (int)(_body.Position.Y * Game1.unitToPixel), _texture.Width, _texture.Height), null, Color.White, _body.Rotation, new Vector2(_texture.Width / 2, _texture.Height / 2), SpriteEffects.None, 0f);
            }
            else
            {
                _spriteBatch.Draw(_texture, new Rectangle((int)(_body.Position.X * Game1.unitToPixel), (int)(_body.Position.Y * Game1.unitToPixel), _texture.Width, _texture.Height), null, Color.White, _body.Rotation, new Vector2(_texture.Width / 2, _texture.Height / 2), SpriteEffects.FlipHorizontally, 0f);
            
            }*/

            _spriteBatch.DrawString(ScreenManager._spriteFont,"     Health :" + Player._blood, _body.Position * Game1.unitToPixel, Color.Black);
            _comboMan.Draw(_spriteBatch);

            /*switch (_animation.State)
            {
                case AnimationManager.AnimationState.IDLE:
                    //Console.WriteLine("IDLE");
                    break;
                case AnimationManager.AnimationState.RUNNING:
                 
                    break;
                case AnimationManager.AnimationState.JUMPING:
                   // Console.WriteLine("JUMPING");
                    break;
                case AnimationManager.AnimationState.FALLING:
                    Console.WriteLine("FALLING");
                    break;
                case AnimationManager.AnimationState.HARVESTING:
                      break;
                default:
                  
                    break;
            }*/
            _spriteBatch.Draw(_oilBarTexture, new Rectangle((int)_camRefrence._pos.X - 180, (int)_camRefrence._pos.Y + 160, (int)_blood * 4, (int)_oilBarTexture.Height), Color.White);
        }

        public bool Body_OnCollision(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact)
        {
            foreach (Tile tile in _tiles)
            {
                if (fixtureB.Body.BodyId == tile._body.BodyId && fixtureA.Body.BodyId == this._body.BodyId)
                {
                    Vector2 _colNorm = contact.Manifold.LocalNormal;
                }
            }

            
            foreach(NPC enemy in EnemyManager.GetInstance(_world).Enemies)
            {
                if (fixtureB.Body.BodyId == enemy._body.BodyId && fixtureA.Body.BodyId == this._body.BodyId)
                {
                    enemy.Alive = false;
                    _manager.AddSomeSplats(enemy._body.Position * Game1.unitToPixel);
                    _camRefrence._shouldShake = true;
                    _camRefrence._divisor = 50;
                    _explosiveSound.Play();
                    fixtureB.Body.CollidesWith = Category.None;
                    Player._blood += 5;
                    EnemyManager.GetInstance(_world).AddPoints(new Points(fixtureB.Body.Position * Game1.unitToPixel, "" + 5));
                    _comboMan.NewKill();
                 }
                    //
                
            }
            return true;
        }
    }
}
