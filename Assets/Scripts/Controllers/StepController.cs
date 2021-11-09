using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SbSTanks
{
    public class StepController: IExecute
    {
        public bool isPlayerTurn = true;

        private TimerData _startTurnTimer;
        private TimerData _shotDelayTimer;
        private TimerData _endTurnTimer;
        private bool _isDelay = false;
        private List<Enemy> _enemies;
        private TimerController _timerController;

        public StepController(List<Enemy> enemies, TimerController timerController)
        {
            _enemies = enemies;
            _timerController = timerController;
        }

        public void EnemiesTurn()
        {
            _startTurnTimer = new TimerData(1f, Time.time);
            _timerController.AddTimer(_startTurnTimer);
        }

        public void Execute(float deltaTime)
        {
            CheckStartTurn();

            CheckDelay();

            CheckEndTurn();
        }

        private void CheckEndTurn()
        {
            if (!(_endTurnTimer is null))
            {
               
                    foreach (var enemy in _enemies)
                    {
                        enemy.isShotReturn = false;
                    }
                    _endTurnTimer = null;
                    _isDelay = false;
                    _startTurnTimer = null;
            }
        }

        private void CheckDelay()
        {
            if (!(_shotDelayTimer is null))
            {
                if (_shotDelayTimer.IsTimerEnd)
                {
                    _isDelay = false;
                    _shotDelayTimer = null;
                }
            }
        }

        private void CheckStartTurn()
        {
            if (!(_startTurnTimer is null) && isPlayerTurn == false)
            {
                if (_startTurnTimer.IsTimerEnd)
                {
                    if (!_isDelay && !_enemies.Contains(_enemies.Find(enemy =>!enemy.isShotReturn)))
                    {
                        _endTurnTimer = new TimerData(1f, Time.time);
                        Debug.Log("No ammo");
                    }
                   
                    else
                    {
                        _shotDelayTimer = new TimerData(1f, Time.time);
                        EnemyShot(_enemies.FindIndex(enemy => !enemy.isShotReturn),_shotDelayTimer);
                        isPlayerTurn = true;
                    }
                    
                    /*  for (int i = 0; i < _enemies.Count; i++)
                      {
                          if (!_isDelay && !_enemies[i].isShotReturn && i < _enemies.Count - 1)
                          {
                              _shotDelayTimer = new TimerData(1f, Time.time);
                              EnemyShot(i, _shotDelayTimer);
                              break;                            
                          }
                          else if (!_isDelay && i == _enemies.Count - 1)
                          {
                              _endTurnTimer = new TimerData(4f, Time.time);
                              EnemyShot(i, _endTurnTimer);
                              break;
                          }
                      }*/
                }
            }
        }

        private void EnemyShot(int index, TimerData timer)
        {
          //  Debug.Log($"index is {index} and shot{_enemies[index].ElementId}");
            _enemies[index].ReturnShot(_enemies[index].ElementId);
            _enemies[index].isShotReturn = true;
            _isDelay = true;
            _timerController.AddTimer(timer);
        }
    }
}
