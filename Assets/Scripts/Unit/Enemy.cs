using System;
using UnityEngine;

namespace SbSTanks
{
   // [RequireComponent(typeof(Animator))]
    public class Enemy : Unit, IUnit
    {
        private ParticleSystem _shotEnemy;

        public Transform GetShotPoint { get => _shotStartPoint; }
        public Transform Transform { get => gameObject.transform; }
        public void Start()
        {
            _shotEnemy = GetComponentInChildren<ParticleSystemShotIdentificator>().GetComponent<ParticleSystem>();
        }

        protected override void OnCollisionEnter(Collision collision)
        {
            ShellHit?.Invoke(collision.gameObject, this);
            _shellController.ReturnShell(collision.gameObject);
            ReturnShot();
        }

        private void ReturnShot()
        { 

            var shell = _shellController.GetShell(_parameters.Damage, _shotStartPoint);
            var shellRb = shell.GetComponent<Rigidbody>();
            _shotEnemy.Play();

            shellRb.AddForce(shell.transform.forward * SHOT_FORCE, ForceMode.Impulse);
        }
    }
}
