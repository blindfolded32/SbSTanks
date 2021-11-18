using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SbSTanks
{
    public class PlayerController : IController
    {
        public PlayerModel PlayerModel { get;}

        private readonly StepController _stepController;
        private Quaternion _targetRotation;
        private const float ROTATION_TIME = 0.5f;
        private float _lerpProgress;
        private Quaternion _startRotation;
        public bool IsPlayerTurn;
        public PlayerController(PlayerModel model)
        {
            PlayerModel = model;
            IsPlayerTurn = true;
        }
        public Transform GetTransform() => PlayerModel.GetPlayer.transform;
        public void RotatePlayer(Transform targetTransform)
        {
            _lerpProgress += Time.deltaTime / ROTATION_TIME;
            var targetRotation = Quaternion.LookRotation(targetTransform.position - PlayerModel.GetPlayer.transform.position);
            PlayerModel.GetPlayer.transform.rotation = Quaternion.Lerp(_startRotation, targetRotation, _lerpProgress);
        }
        
    }
}

