using System;
using UnityEngine;

namespace SbSTanks
{
    public class StepController: IExecute
    {
        public bool isPlayerTurn = true;

        private TimeData _startTurnTimer;
        private TimeData _shotDelayTimer;
        private TimeData _endTurnTimer;
        private bool _isDelay = false;
        private Enemy[] _enemies;
        private TimerController _timerController;

        public StepController(Enemy[] enemies, TimerController timerController)
        {
            _enemies = enemies;
            _timerController = timerController;
        }

        public void EnemiesTurn()
        {
            _startTurnTimer = new TimeData(2f, Time.time);
            _timerController.AddTimer(_startTurnTimer);
        }

        public void Execute(float deltaTime)
        {
            if (!(_startTurnTimer is null) && isPlayerTurn == false)
            {
                if (_startTurnTimer.IsTimerEnd)
                {
                    for (int i = 0; i < _enemies.Length; i++)
                    {
                        if (!_isDelay && !_enemies[i].isShotReturn && i < _enemies.Length - 1)
                        {                           
                            _shotDelayTimer = new TimeData(1f, Time.time);
                            EnemyShot(i, _shotDelayTimer);
                        }
                        else if (!_isDelay &&  i == _enemies.Length - 1)
                        {                           
                            _endTurnTimer = new TimeData(4f, Time.time);
                            EnemyShot(i, _endTurnTimer);
                        }
                    }
                }
            }

            if (!(_shotDelayTimer is null))
            {
                if(_shotDelayTimer.IsTimerEnd)
                {
                    _isDelay = false;
                    _shotDelayTimer = null;
                }
            }

            if (!(_endTurnTimer is null))
            {
                if (_endTurnTimer.IsTimerEnd)
                {
                    isPlayerTurn = true;
                    for (int i = 0; i < _enemies.Length; i++)
                    {
                        _enemies[i].isShotReturn = false;
                    }
                    _endTurnTimer = null;
                    _isDelay = false;
                }
                _startTurnTimer = null;
            }
        }

        private void EnemyShot(int index, TimeData timer)
        {
            _enemies[index].ReturnShot();
            _enemies[index].isShotReturn = true;
            _isDelay = true;
            _timerController.AddTimer(timer);
        }
    }
}
