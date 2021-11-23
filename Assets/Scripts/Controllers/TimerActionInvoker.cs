using System;

namespace Controllers
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

