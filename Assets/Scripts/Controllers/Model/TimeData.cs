using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SbSTanks
{
    public class TimeData
    {
        private float _startTime;
        private float _deltaTime;
        private bool _isEndOfTimer;

        public float GetStartTime { get => _startTime; }
        public float GetDeltaTime { get => _deltaTime; }
        public bool GetAndSetStatusTimer { get => _isEndOfTimer; set => _isEndOfTimer = value; }

        public TimeData(float deltatime, float currentTime)
        {
            _startTime = currentTime;
            _deltaTime = deltatime;
            _isEndOfTimer = false; //поменять на событие
        }
    }
}

