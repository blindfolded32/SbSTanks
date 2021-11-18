using System;
using UnityEngine;


namespace SbSTanks
{
    [Serializable]
    public class UnitParameters : IParameters
    {
        [SerializeField] private int _hp;
        [SerializeField] private int _damage;
        public bool isDead { get; private set; }
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
        public Action<bool> ConfirmDeath { get; set; }

        public UnitParameters(Unit unit, int hp, int elementId, int damage)
        {
            _hp = hp;
            _damage = damage;
            _elementId = elementId;
            isDead = false;
            unit.TakeDamage += GetDamage;
        }
        private void GetDamage(int damage)
        {
            _hp -= damage;
            Debug.Log($"My hp is {_hp}");
           if (_hp <= 0)
            {
                Debug.Log("killed");
               isDead = true;
               ConfirmDeath?.Invoke(true);
            }
        }
       
    }
}
