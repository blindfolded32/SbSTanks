using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SbSTanks
{
    public class PlayerController : IExecute
    {
        private PlayerModel _playerModel;
        private StepController _stepController;

        public PlayerController(PlayerModel model, StepController stepController)
        {
            _stepController = stepController;
            _playerModel = model;
            _playerModel.GetpcInputSpace.OnSpaceDown += GetSpaceKey;
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
                Debug.Log("Shot!!!!");
                _playerModel.GetShotEvent.Play();
                _playerModel.GetPlayer.Shot();
            }
        }

    }
}

