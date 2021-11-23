using System;
using Controllers;
using IdentificationElements;
using Interfaces;
using Unit;
using Unit.Model;
using UnityEngine;

namespace Spawners
{
    public abstract class AbstractUnit : MonoBehaviour, IDamagebleUnit
    {
        public bool isDead { get; set; }
        public Action<float> TakeDamage { get; set; }
        public Action<GameObject, IDamagebleUnit> ShellHit { get; set; }
        public UnitInitializationData unitInitializationData;
        private PlayerController _playerController;
        private Enemy _enemy;
        protected abstract void OnCollisionEnter(Collision collision);
        public void TakingDamage(float damage, int element)
        {
            if (isDead) return;
            GetComponentInChildren<ParticleSystemShotIdentificator>().GetComponent<ParticleSystem>().Play();
            if (element == unitInitializationData.Element || element - unitInitializationData.Element == -1) TakeDamage?.Invoke(damage);
            else TakeDamage?.Invoke(damage*2);
        }
    }
}