using System;
using UnityEngine;

namespace SbSTanks
{
    public abstract class Unit : MonoBehaviour, IDamagebleUnit, IUnit
    {
        public Action<float> TakeDamage { get; set; }
        public Action<float> HealthChanged { get; set; }
        public Action<GameObject, IDamagebleUnit> ShellHit { get; set; }
        [SerializeField] protected UnitParameters _parameters;
        [SerializeField] protected Transform _shotStartPoint;
        protected ShellController _shellController;
        protected const float SHOT_FORCE = 180f;
        public IParameters Parameters => _parameters;
        public Transform GetShotPoint => _shotStartPoint;
        public Transform Transform => gameObject.transform;
        public bool isDead;
        public void SetUnitElement(int value) => _parameters.ElementId = value;
        public void Init(UnitInitializationData data, ShellController shellController, StepController stepController)
        {             
            _parameters = new UnitParameters(this, data.Hp,data.Element,data.Damage); //changed
            _shellController = shellController;
            isDead = false;
           _parameters.ConfirmDeath +=KillUnit;
        }
        protected abstract void OnCollisionEnter(Collision collision);
        public void TakingDamage(float damage, int elementId)
        {
            GetComponentInChildren<ParticleSystemShotIdentificator>().GetComponent<ParticleSystem>().Play();
            if (elementId == _parameters.ElementId || elementId - _parameters.ElementId == -1) TakeDamage?.Invoke(damage);
            else TakeDamage?.Invoke(damage*2);
            HealthChanged(_parameters.Hp.GetCurrentHp);
        }
        private void KillUnit(bool val)
        {
            isDead = val;
        }
       
    }
}
