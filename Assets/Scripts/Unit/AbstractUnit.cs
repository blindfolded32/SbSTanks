using System;
using System.Collections;
using Bullet;
using IdentificationElements;
using Interfaces;
using Markers;
using UnityEngine;

namespace Unit
{
    public abstract class AbstractUnit : MonoBehaviour, IDamagebleUnit
    {
        public bool IsDead { get; set; } = false;
        public IUnitController Controller;
       // internal BulletPool BulletPool;
        internal Transform ShotPoint;
       // internal NameManager.ElementList Element;
        public Action<float> TakeDamage { get; set; }

        internal UnitHealthBar HealthBar;

        public void ChildCouroutine(IEnumerator enumerable) => StartCoroutine(enumerable);
       // public Action<GameObject, IDamagebleUnit> ShellHit { get; set; }
       // internal UnitInitializationData UnitInitializationData;
      //  protected abstract void OnCollisionEnter(Collision collision);
        public void TakingDamage(float damage, NameManager.ElementList element)
        {
            
            if (IsDead) return;
            GetComponentInChildren<ParticleSystemShotIdentificator>().GetComponent<ParticleSystem>().Play();
            if (element == Controller.Model.Element || element - Controller.Model.Element == -1) TakeDamage?.Invoke(damage);
            else TakeDamage?.Invoke(damage*2);
        }
    }
}