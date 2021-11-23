using System;
using Controllers.Model;

namespace Unit
{
    [Serializable]
    public struct UnitInitializationData
    {
        public Health Hp;
        public float Damage;
        public int Element;

        public UnitInitializationData(Health hp, float damage, int element) : this()
        {
            Hp = hp;
            Damage = damage;
            Element = element;
        }
    }
}