using System;
using UnityEngine;

namespace Controllers.Model
{
    [Serializable]
    public class Health
    {
         [SerializeField]private float max;
         [SerializeField]private float current;
        public float Max { get=> max; set=> max = value; }
        public event Action IsDead;
        private float Current
        {
            get=>current;
            set
            {
                current = value;
                Debug.Log($"HP is {current}");
                if (current <= 0)
                {
                    IsDead?.Invoke();
                    Debug.Log("Is Dead");
                }
            }
        }

        public Health(float max, float current = default)
        {
            Max = max;
            Current = Current == default ? max : current;
        }
        public void ChangeCurrentHealth(float value) => Current -= value;
        public void InjectNewHp(float value)
        {
            Max = value;
            Current = value;
        }
        public float GetCurrentHp => Current;//TODO Remove prop
    }
}