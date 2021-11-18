using System;
using UnityEngine;

namespace SbSTanks
{
    public abstract class Unit : MonoBehaviour, IDamagebleUnit, IUnit
    {
        public Action<int> TakeDamage { get; set; }
        public Action<GameObject, IDamagebleUnit> ShellHit { get; set; }
        [SerializeField] protected UnitParameters _parameters;
        [SerializeField] protected Transform _shotStartPoint;
        protected ShellController _shellController;
        protected StepController _stepController;
        protected const float SHOT_FORCE = 180f;
        public IParameters Parameters { get => _parameters; }
        public Transform GetShotPoint { get => _shotStartPoint; }
        public Transform Transform { get => gameObject.transform; }
        public int GetUnitElement => _parameters.ElementId;

        public bool isDead;
        public void SetUnitElement(int value) => _parameters.ElementId = value;
        public void Init(UnitInitializationData data, ShellController shellController, StepController stepController)
        {             
            _parameters = new UnitParameters(this, data.hp,data.element,data.damage); //changed
            _shellController = shellController;
            _stepController = stepController;
            isDead = false;
           _parameters.ConfirmDeath +=KillUnit;
        }
        protected abstract void OnCollisionEnter(Collision collision);
        public void TakingDamage(int damage, int elementId)
        {
            GetComponentInChildren<ParticleSystemShotIdentificator>().GetComponent<ParticleSystem>().Play();
            if (elementId == _parameters.ElementId || elementId - _parameters.ElementId == -1) TakeDamage?.Invoke(damage);
            else TakeDamage?.Invoke(damage*2);
        }
        private void KillUnit(bool val)
        {
            isDead = val;
        }
       
    }
}
