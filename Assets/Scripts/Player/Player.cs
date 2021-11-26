using System;
using Unit;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class Player : AbstractUnit
    {
        public event Action PlayerDead;
        public void ConfirmDeath() => PlayerDead?.Invoke();
    }
}
