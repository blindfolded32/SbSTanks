using System;
using UnityEngine;

namespace Controllers.Model
{
    [Serializable]
    public class Health
    {
         [SerializeField]private float _max;
         [SerializeField]private float _current;
        public float Max { get=> _max; set=> _max = value; }
        private float Current { get=>_current; set=>_current = value; }
        public Health(float max, float current = default)
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