using Controllers;
using Interfaces;
using Markers;
using Unit;
using UnityEngine;

namespace Player
{
    public class PlayerController : IUnitController
    {
        private PlayerModel _playerModel;
        public Player GetView { get; }
        public bool IsDead => GetView.IsDead;
        private readonly StepController _stepController;
        public IModel Model { get => _playerModel; set => _playerModel = value as PlayerModel; }
        public bool IsFired { get; set; } = false;
        public NameManager.State State { get; set; }
        public Transform GetShotPoint => GetView.ShotPoint; 
        public Transform GetTransform => GetView.transform;
        public void SetParams(IModel parameters)
        {
            _playerModel.Damage = parameters.Damage;
            _playerModel.Element = parameters.Element;
            _playerModel.HP = parameters.HP;
            _playerModel.UnitPosition = parameters.UnitPosition;
        }

        public PlayerController(PlayerModel model, Player player)
     {
         _playerModel = model;
         GetView =player;
         player.TakeDamage+=GetDamage;
         State = NameManager.State.Idle;
     }
       private void GetDamage(float damage)
        {
            _playerModel.HP.ChangeCurrentHealth(damage);
            Debug.Log($"My hp is {_playerModel.HP.GetCurrentHp}");
            if (_playerModel.HP.GetCurrentHp <= 0)
            {
                GetView.IsDead = true;
                GetView.ConfirmDeath();
            }
        }
    }
}

