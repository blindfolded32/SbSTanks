using System;
using Interfaces;
using Unit;
using Unit.Model;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

namespace Controllers
{
    public class PlayerController : IController
    {
        public PlayerModel PlayerModel { get;}
        private readonly Player _player;
        private readonly StepController _stepController;
        private Quaternion _targetRotation;
        private const float ROTATION_TIME = 0.5f;
        private float _lerpProgress;
        private Quaternion _startRotation;
        public bool IsPlayerTurn;
        public PlayerController(PlayerModel model, Player player)
        {
            PlayerModel = model;
            _player =player;
            IsPlayerTurn = true;
            player.TakeDamage+=GetDamage;
        }
        public Transform GetTransform => _player.transform;
       public bool GetOrSetHit
        {
            get => _player.GetHitStatus;
            set => _player.GetHitStatus = value;
        }
     /*   public Action<GameObject, IDamagebleUnit> ShellHit
        {
            get => _player.ShellHit;
            set => _player.ShellHit = value;
        }*/
        public Player GetView  => _player;
        public void RotatePlayer(Transform targetTransform)
        {
            _lerpProgress += Time.deltaTime / ROTATION_TIME;
            var targetRotation = Quaternion.LookRotation(targetTransform.position - GetTransform.position);
            GetTransform.rotation = Quaternion.Lerp(_startRotation, targetRotation, _lerpProgress);
        }
        private void GetDamage(float damage)
        {
            _player.UnitInitializationData.Hp.ChangeCurrentHealth(damage);
            Debug.Log($"My hp is {GetView.UnitInitializationData.Hp.GetCurrentHp}");
            if (_player.UnitInitializationData.Hp.GetCurrentHp <= 0)
            {
                _player.IsDead = true;
            }
        }
        
        
    }
}

