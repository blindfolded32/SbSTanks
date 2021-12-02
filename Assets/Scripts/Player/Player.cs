using System;
using Unit;

namespace Player
{
    [Serializable]
    public class Player : AbstractUnit
    {
        public event Action PlayerDead;
        public void ConfirmDeath()
        {
            PlayerDead?.Invoke();
        }
    }
}
