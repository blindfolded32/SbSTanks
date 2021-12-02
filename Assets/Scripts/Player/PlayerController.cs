using System;
using Interfaces;
using Unit;
using UnityEngine;

namespace Player
{
    public class PlayerController : IUnitController
    {
        private readonly UnitModel _playerModel;
        private Player GetView { get; }
        public IModel Model  => _playerModel;
        public Transform GetShotPoint => GetView.ShotPoint; 
        public Transform GetTransform => GetView.transform;
        public NameManager.State State { get; private set; }
        public UnitHealthBar HealthBar => GetView.HealthBar;
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
            switch (state)
            {
                case NameManager.State.Idle:
                  //  GetView.StopSmoke();
                    break;
                case NameManager.State.Attack:
                    break;
                case NameManager.State.Fired:
                    StateChanged?.Invoke();
                    break;
                case NameManager.State.Dead:
                    GetView.ConfirmDeath();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
            State = state;
        }
        public PlayerController(UnitModel model, Player player)
     {
         _playerModel = model;
         GetView =player;
         player.TakeDamage+=GetDamage;
         State = NameManager.State.Idle;
         Model.HP.IsDead += () => ChangeState(NameManager.State.Dead);
     }
       private void GetDamage(float damage) => _playerModel.HP.ChangeCurrentHealth(damage);
    }
}

