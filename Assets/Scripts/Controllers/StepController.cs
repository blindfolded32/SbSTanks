using System;
using System.Collections.Generic;
using Controllers.Model;
using Interfaces;
using Unit;
using UnityEngine;

namespace Controllers
{
    public class StepController : IExecute
    {
        private TimerData _startTurnTimer;
        private TimerData _shotDelayTimer;
        private TimerData _endTurnTimer;
        private bool _isDelay;
        private List<Enemy> _enemies;
        private PlayerController _player;
        private TimerController _timerController;
        private ReInitController _reInitController;
        public int GetTurnNumber { get; private set; }
        public bool PlayerTurn => _player.IsPlayerTurn;
        public event Action<int> NewTurn;
        public StepController(List<Enemy> enemies, PlayerController player, TimerController timerController)
        {
            _enemies = enemies;
            _player = player;
            _timerController = timerController;
            _isDelay = false;
            GetTurnNumber = 1;
            _reInitController = new ReInitController(_player,enemies);
            _reInitController.StartAgain += () =>
            {
                GetTurnNumber = 1;
            };
        }
        public void Execute(float deltaTime)
        {
            if (_reInitController.Lost) return;
            if (CheckDead())
            {
                Debug.Log("Battle over");
                _reInitController.NewRound(_enemies);
                 return;
            }
            CheckStartTurn();
            CheckDelay();
            CheckEndTurn();
        }

        private void CheckEndTurn()
        {
            if (_endTurnTimer is null || !_endTurnTimer.IsTimerEnd) return;
            foreach (var enemy in _enemies.FindAll(x =>!x.IsDead))
            {
                enemy.isShotReturn = false;
            }
            ReInitController.ReInit(_enemies);
            _player.IsPlayerTurn = true;
            _endTurnTimer = null;
            _isDelay = false;
            GetTurnNumber++;
            NewTurn?.Invoke(GetTurnNumber);
            Debug.Log($"Turn {GetTurnNumber}");
        }
        private void CheckDelay()
        {
            if (_shotDelayTimer is null || !_shotDelayTimer.IsTimerEnd) return;
            _isDelay = false;
            _shotDelayTimer = null;
            EnemyShot();
        }

        private bool CheckDead()
        {
            return !_enemies.Find(enemy => !enemy.IsDead);
        }
        
        private void CheckStartTurn()
        {
            if (_player.IsPlayerTurn|| _isDelay || !_enemies.Contains(_enemies.Find(enemy => !enemy.isShotReturn && !enemy.IsDead))) return; 
            _isDelay = true;
          _shotDelayTimer = new TimerData(3f, Time.time);
          _timerController.AddTimer(_shotDelayTimer);
        }
        private void EnemyShot()
        {
            foreach (var enemy in _enemies.FindAll(x =>!x.IsDead))
            {
                Debug.Log("Backshot");
                enemy.ReturnShot();
                enemy.isShotReturn = true;
            }
            _endTurnTimer = new TimerData(4f, Time.time);
            _timerController.AddTimer(_endTurnTimer);
        }
    }
}
