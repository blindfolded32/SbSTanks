using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SbSTanks
{
    public class TimeData
    {
        private float _startTime;
        private float _deltaTime;
        private bool _isTimerEnd;

        public float GetStartTime { get => _startTime; }
        public float GetDeltaTime { get => _deltaTime; }
        public bool IsTimerEnd { get => _isTimerEnd; set => _isTimerEnd = value; }

        public TimeData(float deltatime, float currentTime)
        {
            _startTime = currentTime;
            _deltaTime = deltatime;
            _isTimerEnd = false; //поменять на событие
        }
    }
}

