using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace SbSTanks
{
    public class TimerActionInvoker
    {
        public event Action TimerSet = delegate () { };
        public event Action TimerDelete = delegate () { };
        

        public void SetTimer()
        {
            TimerSet?.Invoke();
        }

        public void DeleteTimer()
        {
            TimerDelete?.Invoke();
        }


    }
}

