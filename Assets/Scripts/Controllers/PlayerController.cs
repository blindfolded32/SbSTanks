using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SbSTanks
{
    public class PlayerController : IExecute, IDisposable
    {
        private PlayerModel _playerModel;
        private StepController _stepController;
        private Dictionary<Button, Enemy> _switchEnemyButtonsMatching = new Dictionary<Button, Enemy>();
        private bool _isOnRotation;
        private Quaternion _targetRotation;

        private const float ROTATION_TIME = 0.5f;
        private float _lerpProgress = 0;
        private Quaternion _startRotation;
        public PlayerController(PlayerModel model, StepController stepController, UIModel uIModel, List<Enemy> enemies, List<Button> switchEnemyButtons)
        {
            _stepController = stepController;
            _playerModel = model;
            _playerModel.GetpcInputSpace.OnSpaceDown += GetSpaceKey;

            for (int i = 0; i < enemies.Count; i++)
            {
                _switchEnemyButtonsMatching.Add(switchEnemyButtons[i], enemies[i]);
            }

            foreach(var element in _switchEnemyButtonsMatching)
            {
                element.Key.onClick.AddListener(
                    delegate
                    {
                        _targetRotation = Quaternion.LookRotation(element.Value.transform.position - _playerModel.GetPlayer.transform.position);
                        _isOnRotation = true; 
                        _startRotation = _playerModel.GetPlayer.transform.rotation;
                        _lerpProgress = 0; 
                    });
            }
        }

        public void GetSpaceKey(bool f)
        {
            _playerModel.IsSpaceDown = f;
        }

        public void Execute(float deltaTime)
        {
            if (_stepController.isPlayerTurn && _playerModel.IsSpaceDown)
            {
                _stepController.isPlayerTurn = false;
            //    Debug.Log("Shot!!!!");
                _playerModel.GetShotEvent.Play();
                _playerModel.GetPlayer.Shot(_playerModel);
            }
            if (_isOnRotation)
            {
                RotatePlayer();
            }
        }

        private void RotatePlayer()
        {
            _lerpProgress += Time.deltaTime / ROTATION_TIME;
            _playerModel.GetPlayer.transform.rotation = Quaternion.Lerp(_startRotation, _targetRotation, _lerpProgress);

            if (_lerpProgress >= 1)
            {
                _isOnRotation = false;
                _lerpProgress = 0;
            }
        }

        public void Dispose()
        {
            foreach (var element in _switchEnemyButtonsMatching)
            {
                element.Key.onClick.RemoveAllListeners();
            }
        }
    }
}

