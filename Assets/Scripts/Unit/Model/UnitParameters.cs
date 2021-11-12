using System;
using System.Collections.Generic;
using UnityEngine;


namespace SbSTanks
{
    [Serializable]
    public struct UnitParameters : IParameters
    {
        [SerializeField] private int _hp;
        [SerializeField] private int _damage;
        private int _elementId;
        public int Hp
        {
            get => _hp;
            set => _hp = value;
        }
        public int Damage
        {
            get => _damage;
            set => _damage = value;
        }
        public int ElementId
        {
            get => _elementId;
            set => _elementId = value;
        }
        public UnitParameters(Unit unit, int hp, int elementId, int damage)
        {
            _hp = hp;
            _damage = damage;
            _elementId = elementId;
            unit.TakeDamage += GetDamage;
        }
        public void GetDamage(int damage)
        {
            _hp -= damage;
        }

    }
}
