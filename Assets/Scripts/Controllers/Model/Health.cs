using System;
using UnityEngine;

namespace SbSTanks
{
    [Serializable]
    public class Health
    {
      public float Max { get; set; }
     private float Current { get; set; }
        public Health(int max, int current = default)
        {
            Max = max;
            Current = current;
        }
        public void ChangeCurrentHealth(float value) => Current -= value;
        public void InjectNewHp(float value)
        {
            Max = value;
            Current = value;
        }
        public float GetCurrentHp => Current;
    }
}