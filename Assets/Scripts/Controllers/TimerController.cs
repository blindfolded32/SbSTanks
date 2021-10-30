using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SbSTanks
{
    public class TimerController : IExecute
    {
        private List<TimeData> _timers;

        public TimerController()
        {
            _timers = new List<TimeData>(); 
        }

        public void AddTimer(TimeData timeData)
        {
            _timers.Add(timeData);
        }

        public int Count()
        {
            return _timers.Count;
        }

        public void DeleteTimer(int index)
        {
            _timers.RemoveAt(index);
        }

        public TimeData this[int i]
        {
            get
            {
                return _timers[i];
            }
        }

        public void Execute(float deltaTime)
        {
            for(int i = 0; i<_timers.Count; i++)
            {
                var currentTime = Time.time;
                if((currentTime - _timers[i].GetStartTime) > _timers[i].GetDeltaTime)
                {
                    _timers[i].GetAndSetStatusTimer = true;
                }
            }
        }
    }
}

