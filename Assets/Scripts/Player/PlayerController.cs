using System;
using Controllers;
using Interfaces;
using Markers;
using Unit;
using UnityEngine;

namespace Player
{
    public class PlayerController : IUnitController
    {
        private UnitModel _playerModel;
        private Player GetView { get; }
        private readonly StepController _stepController;
        public IModel Model { get => _playerModel; set => _playerModel = value as UnitModel; }
        public bool IsFired { get; set; } = false;
        private NameManager.State State { get; set; }
        public Transform GetShotPoint => GetView.ShotPoint; 
        public Transform GetTransform => GetView.transform;
        public NameManager.State GetState => State;
        public void SetParams(IModel parameters)
        {
            _playerModel.Damage = parameters.Damage;
            _playerModel.Element = parameters.Element;
            _playerModel.HP = parameters.HP;
            _playerModel.UnitPosition = parameters.UnitPosition;
        }
        public event Action StateChanged;
        public void ChangeState(NameManager.State state)
        {
            if (GetState == state) return;
            State = state;
           if (state == NameManager.State.Fired) StateChanged?.Invoke();
        }
        public PlayerController(UnitModel model, Player player)
     {
         _playerModel = model;
         GetView =player;
         player.TakeDamage+=GetDamage;
         State = NameManager.State.Idle;
         Model.HP.IsDead += () => ChangeState(NameManager.State.Dead);
     }
       private void GetDamage(float damage)
        {
            _playerModel.HP.ChangeCurrentHealth(damage);
          //  Debug.Log($"My hp is {_playerModel.HP.GetCurrentHp}");
            if (_playerModel.HP.GetCurrentHp <= 0)
            {
                GetView.IsDead = true;
                GetView.ConfirmDeath();
            }
        }
    }
}

