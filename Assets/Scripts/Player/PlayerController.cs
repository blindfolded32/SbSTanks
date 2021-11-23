using Controllers;
using Interfaces;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

namespace Player
{
    public class PlayerController : IPlayerController
    {
        public PlayerModel PlayerModel { get; private set; }
        public Player GetView { get; }
        public bool IsDead => GetView.IsDead;
        private readonly StepController _stepController;
        private Quaternion _targetRotation;
        private const float ROTATION_TIME = 0.5f;
        private float _lerpProgress;
        private Quaternion _startRotation;
        public bool IsPlayerTurn;
        public IModel Model { get => PlayerModel; set => PlayerModel = value as PlayerModel; }
        public bool IsFired { get; set; } = false;
        public Transform GetShotPoint  => GetView.ShotPoint; 
        public Transform GetTransform => GetView.transform;
       public bool GetOrSetHit
        {
            get => GetView.GetHitStatus;
            set => GetView.GetHitStatus = value;
        }
       public PlayerController(PlayerModel model, Player player)
     {
         PlayerModel = model;
         GetView =player;
         IsPlayerTurn = true;
         player.TakeDamage+=GetDamage;
     }
        
        public void RotatePlayer(Transform targetTransform)
        {
            _lerpProgress += Time.deltaTime / ROTATION_TIME;
            var targetRotation = Quaternion.LookRotation(targetTransform.position - GetTransform.position);
            GetTransform.rotation = Quaternion.Lerp(_startRotation, targetRotation, _lerpProgress);
        }
        private void GetDamage(float damage)
        {
            PlayerModel.HP.ChangeCurrentHealth(damage);
            Debug.Log($"My hp is {PlayerModel.HP.GetCurrentHp}");
            if (PlayerModel.HP.GetCurrentHp <= 0)
            {
                GetView.IsDead = true;
            }
        }
        
    }
}

