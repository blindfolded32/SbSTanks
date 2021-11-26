using System;
using System.Collections.Generic;
using Controllers.Model;
using Interfaces;
using Markers;
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
        public int TurnNumber { get;  set; }
        public bool PlayerTurn => !_player.IsFired;
        public event Action<int> NewTurn;

        public StepController(List<Enemy.Enemy> enemies, IUnitController player, TimerController timerController)
        {
            _enemies = enemies;
            _player = player as PlayerController;
            _timerController = timerController;
            _isDelay = false;
            TurnNumber = 0;
            ReInitController = new ReInitController(_player, enemies);
            ReInitController.StartAgain += () => { TurnNumber = 0; };
        }

        public void Execute(float deltaTime)
        {
            if (ReInitController.Lost) return;
            if (CheckDead())
            {
                Debug.Log("Battle over");
                ReInitController.NewRound(_enemies);
                TurnNumber = 0;
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
            TurnNumber++;
            NewTurn?.Invoke(TurnNumber);
            Debug.Log($"Turn {TurnNumber}");
        }

        private void CheckDelay()
        {
            if (!_isDelay || !_shotDelayTimer.IsTimerEnd) return;
            _isDelay = false;
            EnemyShot();
        }

        private bool CheckDead()
        {
            return !_enemies.Find(enemy => enemy.Controller.State != NameManager.State.Dead);
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
            _endTurnTimer = new TimerData(1.0f, Time.time);
            _timerController.AddTimer(_endTurnTimer);
        }
    }
}