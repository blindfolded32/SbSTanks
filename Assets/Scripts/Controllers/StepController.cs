using System;
using System.Collections.Generic;
using System.Linq;
using Controllers.Model;
using Interfaces;
using Markers;
using Player;
using Unit;
using UnityEngine;
using static NameManager;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class StepController 
    {
        public bool IsPlayerTurn;
        public IUnitController AttackingPlayer;
        private readonly List<IUnitController> _players;
        private readonly List<IUnitController> _enemies;
        private readonly TimerController _timerController;
        public readonly IReInit ReInitController;
        private readonly List<IUnitController> _unitList = new List<IUnitController>();
        public int TurnNumber { get;  set; }
      
        public event Action<int> NewTurn;
        public StepController(List<IUnitController> enemies, List<IUnitController> player, TimerController timerController)
        {
            _players = player;
            _enemies = enemies;
            _timerController = timerController;
            _timerController.IsEnd += TurnState;
            TurnNumber = 1;
            foreach (var unitController in player)
            {
                
                _unitList.Add(unitController);
                unitController.StateChanged += () =>
                {
                    IsPlayerTurn = false;
                    AddTimer();
                };
            }
            
            foreach (var enemy in _enemies)
            {
               _unitList.Add(enemy);
               enemy.StateChanged += AddTimer;
            }
            ReInitController = new ReInitController(_unitList);
            ReInitController.StartAgain += () => { TurnNumber = 0; };
            CountTurnOrder();
            timerController.AddTimer(new TimerData(TurnCoolDown,Time.time));
        }
        public void AddTimer()
        {
            _timerController.AddTimer(new TimerData(TurnCoolDown,Time.time));//TurnState;
        }
        private bool CheckDead()
        {
            return _enemies.Contains(_enemies.Find(x => x.GetState != NameManager.State.Dead));
            //Если содержит кого-то не мертвого, то трушка
        }
        private void CountTurnOrder()
        {
            foreach (var unit in _unitList)
            {
                unit.Model.Initiative = Random.Range(0, 100);
            }
            _unitList.Sort((u1,u2)=>u1.Model.Initiative.CompareTo(u2.Model.Initiative));
        }
        private IUnitController GetUnitForShoot()
        {
            var unit = _unitList.First(x => x.GetState == NameManager.State.Idle);
            unit.ChangeState(NameManager.State.Attack);
            if (unit is PlayerController)
            {
                IsPlayerTurn = true;
                AttackingPlayer = unit;
            }
            return unit;
        }
        private void UnitTurn(IUnitController unit)
        {
            if (IsPlayerTurn) return;
            RotateEnemy(unit);
            UnitShoot.Shot(unit, unit.GetShotPoint, unit.Model.Damage, unit.Model.Element);
        }
        public void TurnState()
        {
            if (ReInitController.Lost) return;
          if (!CheckDead())
            {
                Debug.Log("Battle over");
                ReInitController.NewRound();
                TurnNumber = 0;
                CountTurnOrder();
            }
            if (!CheckIdle())
            {
                TurnNumber++;
                NewTurn?.Invoke(TurnNumber);
                Debug.Log($"Turn {TurnNumber}");
                AddTimer();
                ReInitController.StartNewTurn();
                CountTurnOrder();
            }
            if (IsPlayerTurn) return;
            UnitTurn(GetUnitForShoot());
        }
        private bool CheckIdle()
        {
            return _unitList.Contains(_unitList.Find(x => x.GetState == NameManager.State.Idle));
        }

        private void RotateEnemy(IUnitController unit)
        {
            var playerPos = _players[Random.Range(0, _players.FindAll(x => x.GetState != NameManager.State.Dead).Count)].GetTransform;
            
                PlayerRotation.RotatePlayer(unit,playerPos);
           
        }
    }
}