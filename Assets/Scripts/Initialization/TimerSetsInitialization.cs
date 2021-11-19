using UnityEngine;


namespace SbSTanks
{
    public class TimerSetsInitialization
    {
        private PlayerController _playerController;

        private const float DELTA_TIME_BETWEEN_SHOT = 4f;

        public TimerSetsInitialization(PlayerController playerController, TimerActionInvoker timerActionInvoker)
        {
            _playerController = playerController;
            timerActionInvoker.TimerSet += SetTimer;
            timerActionInvoker.TimerDelete += DeleteTimer;
        }
        public void SetTimer()
        {
            if (_playerController.GetOrSetHit)
            {
                _playerController.PlayerModel.GetAndSetTimeData = new TimerData(DELTA_TIME_BETWEEN_SHOT, Time.time);
                _playerController.PlayerModel.GetAndSetTimerController.AddTimer(_playerController.PlayerModel.GetAndSetTimeData);
                _playerController.PlayerModel.GetAndSetIndexOfTimer = _playerController.PlayerModel.GetAndSetTimerController.Count() - 1;
                _playerController.GetOrSetHit = false;
            }

        }
        public void DeleteTimer()
        {
            if ((_playerController.PlayerModel.GetAndSetIndexOfTimer != -1))
            {
                _playerController.PlayerModel.GetAndSetTimerController.DeleteTimer(_playerController.PlayerModel.GetAndSetTimeData);
                _playerController.PlayerModel.GetAndSetTimeData = null;
                _playerController.PlayerModel.GetAndSetIndexOfTimer = -1;
            }

        }
    }
}

