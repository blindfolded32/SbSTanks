using System;
using System.Collections.Generic;
using Controllers.Model;
using Interfaces;
using Player;
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
        private readonly List<Enemy.Enemy> _enemies;
        private readonly PlayerController _player;
        private readonly TimerController _timerController;
        public readonly IReInit ReInitController;
        public int GetTurnNumber { get; private set; }
        public bool PlayerTurn => !_player.IsFired;
        public event Action<int> NewTurn;

        public StepController(List<Enemy.Enemy> enemies, IUnitController player, TimerController timerController)
        {
            _enemies = enemies;
            _player = player as PlayerController;
            _timerController = timerController;
            _isDelay = false;
            GetTurnNumber = 1;
            ReInitController = new ReInitController(_player, enemies);
            ReInitController.StartAgain += () => { GetTurnNumber = 1; };
        }

        public void Execute(float deltaTime)
        {
            if (ReInitController.Lost) return;
            if (CheckDead())
            {
                Debug.Log("Battle over");
                ReInitController.NewRound(_enemies);
                GetTurnNumber = 1;
                return;
            }
            CheckStartTurn();
            CheckDelay();
            CheckEndTurn();
        }

        private void CheckEndTurn()
        {
            if (_endTurnTimer is null || !_endTurnTimer.IsTimerEnd || !_player.IsFired) return;
            ReInitController.ReInit(_enemies);
            _isDelay = false;
            _endTurnTimer = null;
            _shotDelayTimer = null;
            _player.IsFired = false;
            GetTurnNumber++;
            NewTurn?.Invoke(GetTurnNumber);
            Debug.Log($"Turn {GetTurnNumber}");
        }

        private void CheckDelay()
        {
            if (!_isDelay || !_shotDelayTimer.IsTimerEnd) return;
            //if (_shotDelayTimer is null|| !_shotDelayTimer.IsTimerEnd ) return;
            _isDelay = false;
            EnemyShot();
        }

        private bool CheckDead()
        {
            return !_enemies.Find(enemy => !enemy.IsDead);
        }

        private void CheckStartTurn()
        {
            if (!_player.IsFired || _shotDelayTimer is not null || ReInitController.Lost) return;
            _isDelay = true;
            _shotDelayTimer = new TimerData(3.0f, Time.time);
            _timerController.AddTimer(_shotDelayTimer);
        }

        private void EnemyShot()
        {
            if (_endTurnTimer is not null) return;
            foreach (var enemy in _enemies.FindAll(x => !x.IsDead && !x.Controller.IsFired))
            {
                UnitShoot.Shot(enemy.Controller, enemy.ShotPoint, enemy.Controller.Model.Damage, enemy.Element);
                enemy.Controller.IsFired = true;
            }
            _endTurnTimer = new TimerData(4.0f, Time.time);
            _timerController.AddTimer(_endTurnTimer);
        }
    }
}