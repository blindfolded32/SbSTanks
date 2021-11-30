using System;
using Interfaces;
using Unit;
using UnityEngine;

namespace Player
{
    public class PlayerController : IUnitController
    {
        private UnitModel _playerModel;
        private Player GetView { get; }
        public IModel Model { get => _playerModel; set => _playerModel = value as UnitModel; }
        public Transform GetShotPoint => GetView.ShotPoint; 
        public Transform GetTransform => GetView.transform;
        public NameManager.State State { get; private set; }
        public event Action StateChanged;
        public void SetParams(IModel parameters)
        {
            _playerModel.Damage = parameters.Damage;
            _playerModel.Element = parameters.Element;
            _playerModel.HP = parameters.HP;
            _playerModel.UnitPosition = parameters.UnitPosition;
        }
        public void ChangeState(NameManager.State state)
        {
            if (State == state) return;
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
                ChangeState(NameManager.State.Dead);
                GetView.ConfirmDeath();
            }
        }
    }
}

