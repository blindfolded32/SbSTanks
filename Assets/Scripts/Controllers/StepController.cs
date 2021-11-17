using System.Collections.Generic;
using UnityEngine;

namespace SbSTanks
{
    public class StepController: IExecute
    {
        public bool isPlayerTurn = true;

        private TimerData _startTurnTimer;
        private TimerData _shotDelayTimer;
        private TimerData _endTurnTimer;
        private bool _isDelay;
        private List<Enemy> _enemies;
        private Player _player;
        private TimerController _timerController;
        private ReInitController _reInitController;
        public int GetTurnNumber { get; private set; }

        public StepController(List<Enemy> enemies, Player player, TimerController timerController)
        {
            _enemies = enemies;
            _player = player;
            _timerController = timerController;
            _reInitController = new ReInitController(enemies, player);
            _isDelay = false;
            GetTurnNumber = 1;
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

            _reInitController.ReInit();
            isPlayerTurn = true;
            _endTurnTimer = null;
            _isDelay = false;
            GetTurnNumber++;
            Debug.Log($"Turn {GetTurnNumber}");
        }
        private void CheckDelay()
        {
            if (_shotDelayTimer is null || !_shotDelayTimer.IsTimerEnd) return;
            _isDelay = false;
            _shotDelayTimer = null;
            EnemyShot();
        }
        private void CheckStartTurn()
        {
            if (isPlayerTurn|| _isDelay || !_enemies.Contains(_enemies.Find(enemy => !enemy.isShotReturn))) return; 
            _isDelay = true;
          _shotDelayTimer = new TimerData(3f, Time.time);
          _timerController.AddTimer(_shotDelayTimer);
        }
        private void EnemyShot()
        {
            foreach (var enemy in _enemies)
            {
                enemy.ReturnShot();
                enemy.isShotReturn = true;
            }
            _endTurnTimer = new TimerData(4f, Time.time);
            _timerController.AddTimer(_endTurnTimer);
        }
    }
}
