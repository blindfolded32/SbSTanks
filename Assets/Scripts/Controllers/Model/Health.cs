﻿using System;

namespace Controllers.Model
{
    [Serializable]
    public class Health
    {
        public float Max { get; set; }
        private float Current { get; set; }
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