using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SbSTanks
{
    public class PlayerController : IExecute
    {
        public PlayerModel GetPlayerModel { get; private set; }

        private StepController _stepController;
        
        private bool _isOnRotation;
        private Quaternion _targetRotation;
        private const float ROTATION_TIME = 0.5f;
        private float _lerpProgress;
        private Quaternion _startRotation;
        public bool isPlayerTurn;
        public PlayerController(PlayerModel model, StepController stepController)
        {
            _stepController = stepController;
            GetPlayerModel = model;
        }
        public Transform GetTransform() => GetPlayerModel.GetPlayer.transform;
        public int GetPlayerElement() => GetPlayerModel.GetPlayer.GetUnitElement;
        public void Execute(float deltaTime)
        {
            if (_stepController.isPlayerTurn && isPlayerTurn)
            {
                _stepController.isPlayerTurn = false;
            }
           
            isPlayerTurn = false;
        }
        public void RotatePlayer(Transform targetTransform)
        {
            _lerpProgress += Time.deltaTime / ROTATION_TIME;
            var targetRotation = Quaternion.LookRotation(targetTransform.position - GetPlayerModel.GetPlayer.transform.position);
            GetPlayerModel.GetPlayer.transform.rotation = Quaternion.Lerp(_startRotation, targetRotation, _lerpProgress);

            if (_lerpProgress >= 1)
            {
                _isOnRotation = false;
                _lerpProgress = 0;
            }
        }
        
    }
}

