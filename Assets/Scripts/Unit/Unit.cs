using System;
using UnityEngine;

namespace SbSTanks
{
    public abstract class Unit : MonoBehaviour, IUnit
    {
        public Action<int> takeDamage;
        public Action<GameObject> shellHit;

        [SerializeField] protected UnitParameters _parameters;
        [SerializeField] protected Transform _shotStartPoint;

        protected ShellController _shellController;

        protected const float SHOT_FORCE = 50f;

        public IParameters Parameters { get => _parameters; }

        public void Init(UnitInitializationData data, ShellController shellController)
        {
            _parameters = new UnitParameters(this, data.hp, data.damage);
            _shellController = shellController;
        }

        protected abstract void OnCollisionEnter(Collision collision);

        public void TakingDamage(int damage)
        {
            Debug.Log("Auch!");
            takeDamage?.Invoke(damage);
        }
    }
}
