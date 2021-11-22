using System;
using UnityEngine;


namespace SbSTanks
{
    [Serializable]
    public class UnitParameters : IParameters
    {
        private Health _hp;
        private float _damage;
        public Action<bool> ConfirmDeath { get; set; }
        public bool IsDead { get; internal set; }
        private int _elementId;
        public Health Hp
        {
            get => _hp;
            set => _hp = value;
        }
        public float Damage
        {
            get => _damage;
            set => _damage = value;
        }
        public int ElementId
        {
            get => _elementId;
            set => _elementId = value;
        }
        public UnitParameters(Unit unit, Health hp, int elementId, float damage)
        {
            _hp = hp;
            _damage = damage;
            _elementId = elementId;
            IsDead = false;
            unit.TakeDamage += GetDamage;
        }
        private void GetDamage(float damage)
        {
            _hp.ChangeCurrentHealth(damage);
          //  Debug.Log($"My hp is {_hp.GetCurrentHp}");
           if (_hp.GetCurrentHp <= 0)
            {
                Debug.Log("killed");
               IsDead = true;
               ConfirmDeath?.Invoke(true);
            }
        }
       
    }
}
