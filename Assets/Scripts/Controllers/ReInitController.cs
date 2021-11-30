using System;
using System.Collections.Generic;
using Interfaces;
using Player;
using Unit;
using static UnityEngine.Object;
using Random = UnityEngine.Random;
using static NameManager;


namespace Controllers
{
    public class ReInitController : IReInit, IController
    {
        public event Action StartAgain;
        public event Action GameOver;
        public event Action<int> NewRoundStart;
        public bool Lost { get; private set; } = false;
        private int RoundNumber { get; set; }
        private readonly IEnumerable<IUnitController> _unitControllers;
        private List<Enemy.Enemy> _defParams;
        private int _triesCount;
        public ReInitController(IEnumerable<IUnitController> unitControllers)
        {
            _unitControllers = unitControllers;
            _triesCount = TriesCount;
            RoundNumber = 1;
        }
        public void StartNewTurn()
        {
            foreach (var unit in _unitControllers)
            {
                if (unit.State == State.Dead) continue;
                if (unit is PlayerController) unit.ChangeState(State.Idle);
                else
                {
                    unit.Model.Element = (ElementList) (Random.Range(0, 2));
                    unit.ChangeState(State.Idle);
                }
            }
        }
        public void NewRound()
        {
            foreach (var unit in _unitControllers)
            {
                if (unit is PlayerController)
                {
                    unit.ChangeState(State.Idle);
                    unit.Model.HP.InjectNewHp(unit.Model.HP.Max);
                }
                else
                {
                    unit.ChangeState(State.Idle);
                    unit.Model.Damage *= RoundModifier;
                    unit.Model.HP.InjectNewHp(unit.Model.HP.Max * RoundModifier);
                   // Debug.Log($"{unit.GetTransform.name} health is {unit.Model.HP.GetCurrentHp}");
                }
                unit.GetTransform.GetComponentInChildren<UnitHealthBar>().ResetBar(1.0f);
                unit.Model.Element = (ElementList) (Random.Range(0, 2));
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
            foreach (var t in currentEnemy)
            {
                t.Controller.ChangeState(State.Idle);
                // currentEnemy[i].UnitInitializationData.Damage = _defParams[i].UnitInitializationData.Damage;
                // currentEnemy[i].UnitInitializationData.Hp.InjectNewHp(_defParams[i].UnitInitializationData.Hp.Max);
                t.GetComponentInChildren<UnitHealthBar>().foregroundImage.fillAmount =1;
            }
        }
    }
}