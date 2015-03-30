using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HumanAfterAll
{
    public class ComboManager
    {
        #region Variables

        private float _comboTime; //How long before the combo ends 
        private float _timer;   
        private bool _inCombo;  //Is the player currently in a combo
        private int _numKills;  //How many kills the player has for this combo
        private Player _player; //Reference to the player
        private static ComboManager _instance;
        private List<FinishedCombo> _finishedCombos = new List<FinishedCombo>();

        #endregion

        #region Constructor

        private ComboManager()
        {
            _comboTime = 1250f; //Needs testing
            _inCombo = false;
            _numKills = 0;
        }

        public static ComboManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ComboManager();
            }

            return _instance;
        }

        #endregion

        #region Properties

        public Player Player
        {
            set { _player = value; }
        }

        #endregion

        #region Update

        public void Update(GameTime _gameTime)
        {
            if (_inCombo)
            {
                _timer += (float)_gameTime.ElapsedGameTime.TotalMilliseconds;

                if (_timer >= _comboTime)
                {
                    EndCombo();
                }
            }

            for (int i = _finishedCombos.Count - 1; i >= 0; i--)
            {
                if (_finishedCombos[i].Active)
                {
                    _finishedCombos[i].Update(_gameTime);
                }
                else
                {
                    _finishedCombos.RemoveAt(i);
                }
            }
        }

        #endregion

        #region Draw

        public void Draw(SpriteBatch _spriteBatch)
        {

            //Console.WriteLine("INCOMBO: " + _inCombo);
            foreach (FinishedCombo item in _finishedCombos)
            {
                item.Draw(_spriteBatch);
            }
        }

        #endregion

        #region ComboMethods

        public void ResetCombo()
        {
            _finishedCombos.Clear();
            _numKills = 0;
            _inCombo = false;
            _timer = 0f;
        }

        public void EndCombo()
        {
            //Have we really got a combo?
            if (_numKills > 1)
            {
                string _text;
                FinishedCombo _combo;
                if (_numKills == 2)
                {
                    _text = "DOUBLE KILL!";
                }
                else if (_numKills == 3)
                {
                    _text = "TRIPLE KILL!";
                }
                else
                {
                    _text = _numKills + "X KILL!";
                }
                
                _combo = new FinishedCombo(_text, _player);
                _finishedCombos.Add(_combo);
            }

            //Reset Combos
            _inCombo = false;
            _numKills = 0;
            _timer = 0f;
        }

        public void NewKill()
        {
            //Are we already in a combo
            if (_inCombo)
            {
                _timer = 0f;
            }
            else
            {
                _inCombo = true; //If not start one
            }

            _numKills++;
        }

        #endregion
    }
}
