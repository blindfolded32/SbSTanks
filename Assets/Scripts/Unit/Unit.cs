using System;
using IdentificationElements;
using Interfaces;
using Markers;
using Shell;
using Unit.Model;
using UnityEngine;

namespace Unit
{
    public abstract class Unit : MonoBehaviour, IDamagebleUnit, IUnit
    {
        public Action<float> TakeDamage { get; set; }
        public Action<GameObject, IDamagebleUnit> ShellHit { get; set; }
        protected UnitParameters _parameters;
        protected Transform _shotStartPoint;
        protected ShellController _shellController;
        protected const float SHOT_FORCE = 180f;
        public IParameters Parameters
        {
            get => _parameters;
            set => _parameters = value as UnitParameters;
        }
        public Transform GetShotPoint => _shotStartPoint;
        public Transform Transform => gameObject.transform;
        public bool isDead;
        public void SetUnitElement(int value) => _parameters.ElementId = value;
        public void Init(UnitInitializationData data, ShellController shellController)
        {             
            _parameters = new UnitParameters(this, data.Hp,data.Element,data.Damage); //changed
            _shellController = shellController;
            isDead = false;
            _shotStartPoint = GetComponentInChildren<ShotPoint>().transform;
           _parameters.ConfirmDeath +=KillUnit;
        }
        protected abstract void OnCollisionEnter(Collision collision);
        public void TakingDamage(float damage, int elementId)
        {
            if (isDead) return;
            GetComponentInChildren<ParticleSystemShotIdentificator>().GetComponent<ParticleSystem>().Play();
            if (elementId == _parameters.ElementId || elementId - _parameters.ElementId == -1) TakeDamage?.Invoke(damage);
            else TakeDamage?.Invoke(damage*2);
        }
        private void KillUnit(bool val) => isDead = val;
    }
}
