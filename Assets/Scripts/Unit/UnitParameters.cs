using System;
using UnityEngine;

namespace SbSTanks
{
    [Serializable]
    public struct UnitParameters : IParameters
    {
        [SerializeField] private int _hp;
        [SerializeField] private int _damage;

        public int Hp { get => _hp; set => _hp = value; }
        public int Damage { get => _damage; set => _damage = value; }

        public UnitParameters(Unit unit, int hp, int damage)
        {
            _hp = hp;
            _damage = damage;
            unit.takeDamage += GetDamage;
        }

        public void GetDamage(int damage)
        {
            _hp -= damage;
        }
    }
}
