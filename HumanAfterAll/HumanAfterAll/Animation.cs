using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HumanAfterAll
{
    public class Animation
    {
        float _timeIncrement;
        float _currentTime;
        float _previousTime;
        Vector2 _sprite;
        int i = 0;
        bool _play;
        bool _looping;
        int _numberOfFrames;

        public Animation(float _increment, Vector2 _spriteSize, int _numFrames, bool _looping)
        {
            _timeIncrement = _increment;
            _sprite = _spriteSize;
            _numberOfFrames = _numFrames;
            _previousTime = System.Environment.TickCount;
            _currentTime = _previousTime;
            this._looping = _looping;
        }

        public void Update()
        {
            _currentTime = System.Environment.TickCount;
            float _difference = _currentTime - _previousTime;
            if (_difference > _timeIncrement)
            {
                _previousTime = System.Environment.TickCount;
                i++;
            }
            if (i > _numberOfFrames - 1 )
            {
                if (_looping)
                {
                    i = 0;
                }
                else
                {
                    i = _numberOfFrames - 1;
                }
            }
        }

        public Rectangle GetCurrentSprite()
        {
            return new Rectangle((int)(i * _sprite.X), (int)0, (int)_sprite.X, (int)_sprite.Y);
        }

        public void ResetAnimation()
        {
            i = 0;
            _currentTime = System.Environment.TickCount;
            _previousTime = _currentTime;
        }

        public bool FinishedPlaying()
        {
            if (!_looping && i >= _numberOfFrames - 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int NumberOfFrames
        {
            get { return _numberOfFrames; }
        }
    }

}
