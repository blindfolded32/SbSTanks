using System;
using System.Collections.Generic;
using Interfaces;
using Markers;
using Player;
using Unit;
using UnityEngine;
using static UnityEngine.Object;
using Random = UnityEngine.Random;
using static Markers.NameManager;


namespace Controllers
{
    public class ReInitController : IReInit, IController
    {
        public event Action StartAgain;
        public event Action GameOver;
        public event Action<int> NewRoundStart;
        public bool Lost { get; private set; } = false;
        private int RoundNumber { get; set; }
        
      //  private readonly PlayerController _playerController;
      //  private readonly List<Enemy.Enemy> _enemies;

        private readonly IEnumerable<IUnitController> _unitControllers;
        private List<Enemy.Enemy> _defParams;
        private int _triesCount;
        public ReInitController(IEnumerable<IUnitController> unitControllers)
        {
          //  _playerController = playerController;
          //  _enemies = enemies;
          _unitControllers = unitControllers;
        //    _defParams = _enemies;
            //_playerController.GetView.PlayerDead += NewTry;
            _triesCount = TriesCount;
            RoundNumber = 1;
        }
        public void StarnNewTurn()
        {
            foreach (var unit in _unitControllers)
            {
                if (unit.GetState == State.Dead) continue;
                if (unit is PlayerController) unit.State = State.Idle;
                else
                {
                    unit.Model.Element = (ElementList) (Random.Range(0, 2)); //TODO count elements in enum
                    unit.State = State.Idle;
                }
            }
        }
        public void NewRound()
        {
            foreach (var unit in _unitControllers)
            {
                if (unit is PlayerController)
                {
                    unit.State = State.Idle;
                    unit.Model.Element = (ElementList) (Random.Range(0, 2));
                }
                else
                {
                    unit.State = State.Idle;
                    unit.Model.Damage *= RoundModifier;
                    unit.Model.HP.InjectNewHp(unit.Model.HP.Max * RoundModifier);
                    unit.GetTransform.GetComponentInChildren<UnitHealthBar>().ResetBar(1.0f);
                    unit.Model.Element = (ElementList) (Random.Range(0, 2));
                    Debug.Log($"{unit.GetTransform.name} health is {unit.Model.HP.GetCurrentHp}");
                }
            }
            RoundNumber++;
            NewRoundStart?.Invoke(RoundNumber);
        }
        public void Renew()
        {
            foreach (var unitController in _unitControllers)
            {
                unitController.GetTransform.GetComponentInChildren<UnitHealthBar>().RenewBar(unitController);
            }
        /*    _playerController.GetView.GetComponentInChildren<UnitHealthBar>().RenewBar(_playerController.GetView);
            foreach (var enemy in _enemies)
            {
                enemy.GetComponentInChildren<UnitHealthBar>().RenewBar(enemy);
            }*/
        }

        public void NewTry()
        {
            if (_triesCount == 0)
            {
                GameOver?.Invoke();
                return;
            }
            StartAgain?.Invoke();
            _triesCount--;
            RoundNumber = 1;
            NewRoundStart?.Invoke(RoundNumber);
            Lost = true;
        }
        private void RestartGame()
        {
            var currentEnemy = FindObjectsOfType<Enemy.Enemy>();
            for (int i = 0; i < currentEnemy.Length; i++)
            {
                currentEnemy[i].IsDead = false;
               // currentEnemy[i].UnitInitializationData.Damage = _defParams[i].UnitInitializationData.Damage;
               // currentEnemy[i].UnitInitializationData.Hp.InjectNewHp(_defParams[i].UnitInitializationData.Hp.Max);
                currentEnemy[i].GetComponentInChildren<UnitHealthBar>()._foregroundImage.fillAmount =1;
            }
            
        }
    }
}