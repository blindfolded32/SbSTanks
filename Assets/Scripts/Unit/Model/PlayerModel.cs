using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SbSTanks
{
    public class PlayerModel
    {



        private IPCInputSpace _pcInputSpace;//
        private Player _player;//
        private ParticleSystem _shotEvent;//
        private int _indexOfTimer;
        private TimerController _timerController;//
        private bool _isSpdaceDown;
        private TimeData _timeData;

        public IPCInputSpace GetpcInputSpace { get => _pcInputSpace; } 
        public Player GetPlayer { get => _player; }
        public ParticleSystem GetShotEvent { get => _shotEvent; }
        public int GetAndSetIndexOfTimer { get => _indexOfTimer; set => _indexOfTimer = value; }
        public TimerController GetAndSetTimerController { get => _timerController; set => _timerController = value; }
        public bool IsSpaceDown { get => _isSpdaceDown; set => _isSpdaceDown = value; }
        public TimeData GetAndSetTimeData { get => _timeData; set => _timeData = value; }

        public PlayerModel(IPCInputSpace pCInputSpace, TimerController timerController)
        {
            _pcInputSpace = pCInputSpace;
            _timerController = timerController;
            _player = GameObject.FindObjectOfType<Player>();
            _shotEvent = _player.gameObject.GetComponentInChildren<ParticleSystemShotIdentificator>().GetComponent<ParticleSystem>();
        }

    }
}

