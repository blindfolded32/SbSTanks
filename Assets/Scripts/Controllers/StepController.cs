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
            if (_endTurnTimer is null || !_endTurnTimer.IsTimerEnd) return;
            foreach (var enemy in _enemies)
            {
                enemy.isShotReturn = false;
            }
            _endTurnTimer = null;
            _isDelay = false;
        }

        private void CheckDelay()
        {
            if (_shotDelayTimer is null || !_shotDelayTimer.IsTimerEnd) return;
         
            _isDelay = false;
            _shotDelayTimer = null;
            isPlayerTurn = true;
        }

        private void CheckStartTurn()
        {
            if (_startTurnTimer is null || isPlayerTurn || !_startTurnTimer.IsTimerEnd) return;
            if (_isDelay || !_enemies.Contains(_enemies.Find(enemy => !enemy.isShotReturn))) return;
          _shotDelayTimer = new TimerData(2f, Time.time);
          EnemyShot(_enemies.FindIndex(enemy => !enemy.isShotReturn),_shotDelayTimer);
            
        }

      private void EnemyShot(int index, TimerData timer)
        {
            _enemies[index].ReturnShot();
            _enemies[index].isShotReturn = true;
            _isDelay = true;
            if (index == _enemies.Count - 1)
            {
                _endTurnTimer = new TimerData(4f, Time.time);
                _timerController.AddTimer(_endTurnTimer);
                Debug.Log("TurnEnd");
            }
            else _timerController.AddTimer(timer);
        }
    }
}
