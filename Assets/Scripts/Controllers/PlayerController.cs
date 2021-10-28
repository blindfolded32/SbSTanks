using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SbSTanks
{
    public class PlayerController : IExecute
    {
        private IPCInputSpace _pcInputSpace;
        private bool _isSpdaceDown;
        private Player _player;
        private TimerController _timerController;
        private ParticleSystem _shotEvent;
        private readonly int _indexOfTimer;

        private const float DELTA_TIME_BETWEEN_SHOT = 5f;

        public PlayerController(IPCInputSpace pCInputSpace, TimerController timerController)
        {
            _timerController = timerController;
            _timerController.AddTimer(new TimeData());
            _indexOfTimer = _timerController.Count() - 1;

            _pcInputSpace = pCInputSpace;
            _pcInputSpace.OnSpaceDown += GetSpaceKey;
            _player = GameObject.FindObjectOfType<Player>();
            _shotEvent = _player.gameObject.GetComponentInChildren<ParticleSystemShotIdentificator>().GetComponent<ParticleSystem>();
            Debug.Log(_shotEvent);
        }

        public void GetSpaceKey(bool f)
        {
            _isSpdaceDown = f;
        }

        public void Execute(float deltaTime)
        {
            if (_player.GetHitStatus)
            {
                _timerController[_indexOfTimer].SetNewTimer(DELTA_TIME_BETWEEN_SHOT, Time.time);
                _player.GetHitStatus = false;
            }
            if (_isSpdaceDown && _timerController[_indexOfTimer].GetAndSetStatusTimer)
            {
                Debug.Log("Shot!!!!");
                _shotEvent.Play();
                _player.Shot();
            }
        }

    }
}

