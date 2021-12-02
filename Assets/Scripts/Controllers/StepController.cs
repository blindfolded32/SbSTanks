using System;
using System.Collections.Generic;
using System.Linq;
using Controllers.Model;
using Interfaces;
using Player;
using Unit;
using UnityEngine;
using static NameManager;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class StepController
    {
        public bool IsPlayerTurn { get; private set; }
        public IUnitController AttackingPlayer;
        private readonly List<IUnitController> _players;
        private readonly List<IUnitController> _enemies;
        private readonly TimerController _timerController;
        public readonly IReInit ReInitController;
        private readonly List<IUnitController> _unitList = new List<IUnitController>();
        public int TurnNumber { get;  set; }
        public event Action<int> NewTurn;
        public event Action<int> NewRound;

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
            ReInitController.StartAgain += () => { TurnNumber = 1;};
            CountTurnOrder();
            timerController.AddTimer(new TimerData(TurnCoolDown,Time.time));
        }
        public void AddTimer()
        {
            _timerController.AddTimer(new TimerData(TurnCoolDown,Time.time));//TurnState;
        }
        private bool CheckDead(List<IUnitController> units)
        {
            return units.Contains(units.Find(x => x.State != State.Dead));
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
            var unit = _unitList.First(x => x.State == State.Idle);
            unit.ChangeState(State.Attack);
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

        private void TurnState()
        { 
            if (ReInitController.Lost) return;
            if (!CheckDead(_players))
            {
                ReInitController.NewTry();
                //TODO Game Restart Logic
                return;
            }
            if (!CheckDead(_enemies))
            {
                NewRound?.Invoke(ReInitController.RoundNumber);
                ReInitController.NewRound();
                TurnNumber = 1;
                ChooseABeliever();
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
            return _unitList.Contains(_unitList.Find(x => x.State == State.Idle));
        }
        private void RotateEnemy(IUnitController unit)
        {
            Transform playerPos;
            var activePlayers = _players.FindAll(x => x.State != State.Dead);
            playerPos = activePlayers.Count > 1 ? activePlayers[Random.Range(0, activePlayers.Count - 1)].GetTransform : activePlayers[0].GetTransform;
            UnitRotation.RotateUnit(unit,playerPos);
        }

        private void ChooseABeliever()
        {
            var alive = _enemies.FindAll(x => x.State != State.Dead).Count;
            if (alive < 2 ) return;
            var count = Random.Range(1, alive);
            for (int i = 0; i < count; i++)
            {
              _enemies[Random.Range(0,alive)].ChangeState(State.Levitate);
              Debug.Log($"Believers count {count}");
            }
        }
    }
}