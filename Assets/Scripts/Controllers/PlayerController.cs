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
        public Dictionary<Button, Enemy> SwitchEnemyButtonsMatching = new Dictionary<Button, Enemy>();
        private bool _isOnRotation;
        private Quaternion _targetRotation;
        private const float ROTATION_TIME = 0.5f;
        private float _lerpProgress = 0;
        private Quaternion _startRotation;

        public bool isPlayerTurn;
        public PlayerController(PlayerModel model, StepController stepController, UIModel uIModel, List<Enemy> enemies, List<Button> switchEnemyButtons)
        {
            _stepController = stepController;
            _playerModel = model;

            for (int i = 0; i < enemies.Count; i++)
            {
                SwitchEnemyButtonsMatching.Add(switchEnemyButtons[i], enemies[i]);
            }

            foreach(var element in SwitchEnemyButtonsMatching)
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

        public Vector3 GetPosition() => _playerModel.GetPlayer.transform.position;

        public int GetPlayerElement() => _playerModel.GetPlayer.GetUnitElement;

        public void GetSpaceKey(bool f)
        {
            _playerModel.IsSpaceDown = f;
        }

        public void Execute(float deltaTime)
        {
            if (_stepController.isPlayerTurn && isPlayerTurn)
            {
                _stepController.isPlayerTurn = false;
                _playerModel.GetShotEvent.Play();
                _playerModel.GetPlayer.Shot(_playerModel);
            }
            if (_isOnRotation)
            {
                RotatePlayer(_targetRotation);
            }

            isPlayerTurn = false;
        }

        public void RotatePlayer(Quaternion targetrotation)
        {
            _lerpProgress += Time.deltaTime / ROTATION_TIME;
            _playerModel.GetPlayer.transform.rotation = Quaternion.Lerp(_startRotation, targetrotation, _lerpProgress);

            if (_lerpProgress >= 1)
            {
                _isOnRotation = false;
                _lerpProgress = 0;
            }
        }
        public void Dispose()
        {
            foreach (var element in SwitchEnemyButtonsMatching)
            {
                element.Key.onClick.RemoveAllListeners();
            }
        }
    }
}

