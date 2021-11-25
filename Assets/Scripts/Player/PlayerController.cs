using Controllers;
using Interfaces;
using Unit;
using UnityEngine;

namespace Player
{
    public class PlayerController : IUnitController
    {
        public PlayerModel PlayerModel { get; private set; }
        public Player GetView { get; }
        public bool IsDead => GetView.IsDead;
        private readonly StepController _stepController;
        public IModel Model { get => PlayerModel; set => PlayerModel = value as PlayerModel; }
        public bool IsFired { get; set; } = false;
        public Transform GetShotPoint => GetView.ShotPoint; 
        public Transform GetTransform => GetView.transform;
        public void SetParams(UnitModel parameters)
        {
            PlayerModel.Damage = parameters.Damage;
            PlayerModel.Element = parameters.Element;
            PlayerModel.HP = parameters.HP;
        }

        public PlayerController(PlayerModel model, Player player)
     {
         PlayerModel = model;
         GetView =player;
         player.TakeDamage+=GetDamage;
     }
       private void GetDamage(float damage)
        {
            PlayerModel.HP.ChangeCurrentHealth(damage);
            Debug.Log($"My hp is {PlayerModel.HP.GetCurrentHp}");
            if (PlayerModel.HP.GetCurrentHp <= 0)
            {
                GetView.IsDead = true;
                GetView.ConfirmDeath();
            }
        }
    }
}

