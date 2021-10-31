using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SbSTanks
{
    public class TimerController : IExecute
    {
        private List<TimeData> _timers = new List<TimeData>();

        public void AddTimer(TimeData timeData)
        {
            _timers.Add(timeData);
        }

        public int Count()
        {
            return _timers.Count;
        }

        public void DeleteTimer(TimeData item)
        {
            _timers.Remove(item);
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
                    _timers[i].IsTimerEnd = true;
                    //if ((Time.time -(currentTime + _timers[i].GetStartTime)) > 60f)
                    //{
                    //    DeleteTimer(_timers[i]);
                    //}
                }
            }
        }
    }
}

