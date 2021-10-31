using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SbSTanks
{
    public class TimerSetsInitialization
    {
        private PlayerModel _playerModel;

        private const float DELTA_TIME_BETWEEN_SHOT = 4f;

        public TimerSetsInitialization(PlayerModel playerModel, TimerActionInvoker timerActionInvoker)
        {
            _playerModel = playerModel;
            timerActionInvoker.TimerSet += SetTimer;
            timerActionInvoker.TimerDelete += DeleteTimer;
        }

        public void SetTimer()
        {
            if (_playerModel.GetPlayer.GetHitStatus)
            {
                _playerModel.GetAndSetTimeData = new TimeData(DELTA_TIME_BETWEEN_SHOT, Time.time);
                _playerModel.GetAndSetTimerController.AddTimer(_playerModel.GetAndSetTimeData);
                _playerModel.GetAndSetIndexOfTimer = _playerModel.GetAndSetTimerController.Count() - 1;
                _playerModel.GetPlayer.GetHitStatus = false;
            }

        }

        public void DeleteTimer()
        {
            if ((_playerModel.GetAndSetIndexOfTimer != -1))
            {
                _playerModel.GetAndSetTimerController.DeleteTimer(_playerModel.GetAndSetTimeData);
                _playerModel.GetAndSetTimeData = null;
                _playerModel.GetAndSetIndexOfTimer = -1;
            }

        }
    }
}

