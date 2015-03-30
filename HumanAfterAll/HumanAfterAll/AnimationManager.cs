using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HumanAfterAll
{
    public class AnimationManager
    {
        #region AnimationStateEnum

        public enum AnimationState
        {
            TEST = 0,
            IDLE,
            RUNNING,
            JUMPING,
            FALLING,
            HARVESTING
        }

        #endregion

        #region Variables

        private List<Animation> _animations;
        private List<Texture2D> _textures;
        private AnimationState _state;
        private int _animationIndex;
        private bool _reverse;
        //private bool falling;

        #endregion

        #region Constructor

        public AnimationManager()
        {
            _animations = new List<Animation>();
            _textures = new List<Texture2D>();
            _state  = AnimationState.TEST;
            _animationIndex = 0;
        }

        #endregion

        #region Properties

        public List<Animation> Animations
        {
            get { return _animations; }
        }

        public List<Texture2D> Textures
        {
            get { return _textures; }
        }

        public int AnimationIndex
        {
            get { return _animationIndex; }
            set { _animationIndex = value; }
        }

        public Animation CurrentAnimation
        {
            get { return _animations[_animationIndex]; }
        }

        public Texture2D CurrentTexture
        {
            get { return _textures[_animationIndex]; }
        }

        public AnimationState State
        {
            get { return _state; }
            set { _state = value; }
        }

        #endregion

        #region List Methods

        public void AddAnimation(Animation _animation, Texture2D _texture)
        {
            _animations.Add(_animation);
            _textures.Add(_texture);
        }

        #endregion

        #region Update

        public void Update(Vector2 _velocity)
        {
            switch(_state)
            {
                case AnimationState.TEST:
                    SwitchAnimation(_velocity);
                    break;
                case AnimationState.FALLING:
                    if (_velocity.Y > 0.5)
                    {
                        _animations[_animationIndex].Update();
                    }
                    else
                    {
                        SwitchAnimation(_velocity);
                    }
                    break;
                case AnimationState.HARVESTING:
                    if (!CurrentAnimation.FinishedPlaying())
                    {
                        _animations[_animationIndex].Update();
                    }
                    else
                    {
                        SwitchAnimation(_velocity);
                    }
                    break;
                case AnimationState.IDLE:
                    if ((_velocity.X > -0.5f && _velocity.X < 0.5f) && (_velocity.Y > -0.5f && _velocity.Y < 0.5f))
                    {
                        _animations[_animationIndex].Update();
                    }
                    else
                    {
                        SwitchAnimation(_velocity);
                    }
                    break;
                case AnimationState.JUMPING:
                    if (_velocity.Y < -0.5f)
                    {
                        _animations[_animationIndex].Update();
                    }
                    else
                    {
                        SwitchAnimation(_velocity);
                    }
                    break;
                case AnimationState.RUNNING:
                    if (_velocity.Y == 0 && _velocity.X != 0)
                    {
                        _animations[_animationIndex].Update();
                    }
                    else
                    {
                        SwitchAnimation(_velocity);
                    }
                    break;
            }

            if (_velocity.X > 0.5f)
            {
                _reverse = false;
            }
            else if (_velocity.X < -0.5f)
            {
                _reverse = true;
            }
        }

        #endregion

        #region Draw

        public void Draw(SpriteBatch _spriteBatch, Vector2 _position, float _rotation)
        {
            if (_reverse)
            {
                _spriteBatch.Draw(_textures[_animationIndex], new Rectangle((int)(_position.X * Game1.unitToPixel), (int)(_position.Y * Game1.unitToPixel), CurrentTexture.Width / CurrentAnimation.NumberOfFrames, CurrentTexture.Height), _animations[_animationIndex].GetCurrentSprite(), Color.White, _rotation, new Vector2((CurrentTexture.Width / CurrentAnimation.NumberOfFrames) / 2, CurrentTexture.Height / 2), SpriteEffects.FlipHorizontally, 0f);
            }
            else
            {
                _spriteBatch.Draw(_textures[_animationIndex], new Rectangle((int)(_position.X * Game1.unitToPixel), (int)(_position.Y * Game1.unitToPixel), CurrentTexture.Width / CurrentAnimation.NumberOfFrames, CurrentTexture.Height), _animations[_animationIndex].GetCurrentSprite(), Color.White, _rotation, new Vector2((CurrentTexture.Width / CurrentAnimation.NumberOfFrames) / 2, CurrentTexture.Height / 2), SpriteEffects.None, 0f);
            }
        }

        #endregion

        #region Other

        public void SwitchAnimation(Vector2 _velocity)
        {

            if (_velocity.Y < 0.5f && _velocity.Y > -0.5f)
            {
                if (_velocity.X < 0.5f && _velocity.X > -0.5f)
                {
                    if (!(_state == AnimationState.JUMPING))
                    {
                        if (_animations.Count >= 2)
                        {
                            _state = AnimationState.IDLE;
                        }
                    }
                }
                else if (_velocity.X > 0.5f)
                {
                    if (_animations.Count >= 3)
                    {
                        _state = AnimationState.RUNNING;
                    }
                }
                else if (_velocity.X < -0.5f)
                {
                    if (_animations.Count >= 3)
                    {
                        _state = AnimationState.RUNNING;
                    }
                }
            }
            else
            {
                if (_velocity.Y < -0.5f)
                {
                    if (_animations.Count >= 4)
                    {
                        _state = AnimationState.JUMPING;
                    }
                }
                else if (_velocity.Y > 0.5f)
                {
                    if (_animations.Count >= 5)
                    {
                        _state = AnimationState.FALLING;
                    }
                }
            }

            //Only reset animation if it changed
            if (_animationIndex != (int)_state)
            {
                _animationIndex = (int)_state;
                CurrentAnimation.ResetAnimation();
            }

        }

        #endregion
    }
}
