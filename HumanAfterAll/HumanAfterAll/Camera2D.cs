using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HumanAfterAll
{
     public class Camera2D
    {
        protected float _zoom; 
        public Matrix _transform;
        public Vector2 _pos; 
        public float _rotation;
        public float _startTime;
        public float _currentTime;
        public float _shakeTime;
        public bool _isShaking = false;
        public bool _shouldShake = false;
        public float _divisor = 100;
        public Camera2D()
        {
            _zoom = 1f;
            _rotation = 0.0f;
            _pos = Vector2.Zero;
        }
        public Matrix get_transformation(GraphicsDevice graphicsDevice)
        {
            _transform =       // Thanks to o KB o for this solution
              Matrix.CreateTranslation(new Vector3(-_pos.X, -_pos.Y, 0)) *
                                         Matrix.CreateRotationZ(_rotation) *
                                         Matrix.CreateScale(new Vector3(1f, 1f, 1f)) *
                                         Matrix.CreateTranslation(new Vector3(640 * 0.5f, 480 * 0.5f, 0));
            return _transform;
        }
        public void ShakeCamera()
        {
            GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
                 
            if (_shouldShake == true)
            {
                if (_isShaking == false)
                {
                    _startTime = System.Environment.TickCount / 1000;
                    _currentTime = _startTime;
                    _shakeTime = 1f;
                    _isShaking = true;
                }
                if (_isShaking == true)
                {
                    _rotation = 0;
                    _currentTime = System.Environment.TickCount / 1000;
                    if (_currentTime - _startTime > _shakeTime)
                    {
                        _isShaking = false;
                        _shouldShake = false;
                    }
                        Random _random = new Random();
                        GamePad.SetVibration(PlayerIndex.One, 1f, 1f);
                 
                        _rotation += (float)_random.NextDouble() / _divisor;
                        if (System.Environment.TickCount % 2 == 0)
                        {
                            _rotation *= -1;
                        }
                    
                    
                }
            }
            _divisor = 100;
        }
    }
}
