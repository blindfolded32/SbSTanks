using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SbSTanks
{
    public class TimerController : IExecute
    {
        private List<TimerData> _timers = new List<TimerData>();

        public void AddTimer(TimerData timeData)
        {
            _timers.Add(timeData);
        }

        public int Count()
        {
            return _timers.Count;
        }

        public void DeleteTimer(TimerData item)
        {
            _timers.Remove(item);
        }

        public TimerData this[int i]
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

