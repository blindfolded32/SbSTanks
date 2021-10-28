using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SbSTanks
{
    public class Timer : MonoBehaviour
    {
        private bool _timerStatus;

        public bool GetTimerStatus { get => _timerStatus; }

        public void Awake()
        {
            _timerStatus = true;
        }

        public void StartTimer()
        {
            StartCoroutine(TimerCoroutine());
        }

        public IEnumerator TimerCoroutine()
        {
            _timerStatus = false;
            yield return new WaitForSecondsRealtime(5f);
            _timerStatus = true;
        }
    }
}

