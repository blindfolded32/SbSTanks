using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SbSTanks
{
    public class PlayerController : IExecute
    {
        private PlayerModel _playerModel;
        private TimerActionInvoker _timerActionInvoker;

        public PlayerController(PlayerModel model, TimerActionInvoker timerActionInvoker)
        {
            _timerActionInvoker = timerActionInvoker;
            _playerModel = model;
            _playerModel.GetAndSetIndexOfTimer = -1;
            _playerModel.GetpcInputSpace.OnSpaceDown += GetSpaceKey;
            Debug.Log(_playerModel.GetShotEvent);
        }

        public void GetSpaceKey(bool f)
        {
            _playerModel.IsSpaceDown = f;
        }

        public void Execute(float deltaTime)
        {
            _timerActionInvoker.SetTimer();
            if ((_playerModel.GetAndSetTimeData == null || _playerModel.GetAndSetTimeData.IsTimerEnd) && _playerModel.IsSpaceDown)
            {
                Debug.Log("Shot!!!!");
                _timerActionInvoker.DeleteTimer();
                _playerModel.GetShotEvent.Play();
                _playerModel.GetPlayer.Shot();
            }
        }

    }
}

