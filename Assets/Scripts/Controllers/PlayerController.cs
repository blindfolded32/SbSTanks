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
        private Timer _timer;

        public PlayerController(IPCInputSpace pCInputSpace)
        {
            _timer = GameObject.FindObjectOfType<Timer>();
            _pcInputSpace = pCInputSpace;
            _pcInputSpace.OnSpaceDown += GetSpaceKey;
            _player = GameObject.FindObjectOfType<Player>();
        }

        public void GetSpaceKey(bool f)
        {
            _isSpdaceDown = f;
        }

        public void Execute(float deltaTime)
        {
            if (_player.GetHitStatus)
            {
                _timer.StartTimer();
                _player.GetHitStatus = false;
            }
            if (_isSpdaceDown && _timer.GetTimerStatus)
            {
                Debug.Log("Shot!!!!");
                _player.Shot();
            }
        }

    }
}

