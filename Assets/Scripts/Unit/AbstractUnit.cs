using System;
using System.Collections;
using IdentificationElements;
using Interfaces;
using Markers;
using UnityEngine;

namespace Unit
{
    public abstract class AbstractUnit : MonoBehaviour, IDamagebleUnit
    {
        public IUnitController Controller;
        internal Transform ShotPoint;
        public Action<float> TakeDamage { get; set; }
        internal UnitHealthBar HealthBar;
        public void ChildCouroutine(IEnumerator enumerable) => StartCoroutine(enumerable);
        public void TakingDamage(float damage, NameManager.ElementList element)
        {
            
            if (Controller.GetState ==NameManager.State.Dead) return;
            GetComponentInChildren<ParticleSystemShotIdentificator>().GetComponent<ParticleSystem>().Play();
            if (element == Controller.Model.Element || element - Controller.Model.Element == -1) TakeDamage?.Invoke(damage);
            else TakeDamage?.Invoke(damage*2);
        }
    }
}