using System;
using UnityEngine;

namespace SbSTanks
{
    public abstract class Unit : MonoBehaviour, IUnit
    {
        public Action<int> takeDamage;
        public Action<GameObject> shellHit;

        [SerializeField] protected UnitParameters _parameters;

        public IParameters Parameters { get => _parameters; }

        public void Init(UnitInitializationData data)
        {
            _parameters = new UnitParameters(this, data.hp, data.damage);
        }

        protected abstract void OnCollisionEnter(Collision collision);

        public void TakingDamage(int damage)
        {
            Debug.Log("Auch!");
            takeDamage?.Invoke(damage);
        }
    }
}
