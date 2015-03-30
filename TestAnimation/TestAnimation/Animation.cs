using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TestAnimation
{

class Animation
{

float _timeIncrement;
float _currentTime;
float _previousTime;
Vector2 _sprite;
int i = 0;
bool _play;
int _numberOfFrames;
public Animation(float _increment,Vector2 _spriteSize,int _numFrames)
{
_timeIncrement = _increment;
_sprite = _spriteSize;
_numberOfFrames = _numFrames;
_previousTime = System.Environment.TickCount;
_currentTime = _previousTime;
}

public void Update()
{
_currentTime = System.Environment.TickCount;
    float _diff = _currentTime - _previousTime;
if (_diff> _timeIncrement)
{
_previousTime = System.Environment.TickCount;
i++;
}
if (i > _numberOfFrames-1)
{
i = 0;
}
}

public Rectangle GetCurrentSprite()
{
return new Rectangle((int)(i * _sprite.X), (int)0, (int)_sprite.X, (int)_sprite.Y);
}
}
}

