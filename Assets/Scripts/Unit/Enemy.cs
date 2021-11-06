using System;
using UnityEngine;

namespace SbSTanks
{
   // [RequireComponent(typeof(Animator))]
    public class Enemy : Unit
    {
        private ParticleSystem _shotEnemy;
        public bool isShotReturn = false;

        public void Start()
        {
            _shotEnemy = GetComponentInChildren<ParticleSystemShotIdentificator>().GetComponent<ParticleSystem>();
        }

        protected override void OnCollisionEnter(Collision collision)
        {
            ShellHit?.Invoke(collision.gameObject, this,ElementId);
            _shellController.ReturnShell(collision.gameObject);
            _stepController.EnemiesTurn();
        }

        public void ReturnShot(int elementId)
        { 

            var shell = _shellController.GetShell(_parameters.Damage, _shotStartPoint, elementId);
            var shellRb = shell.GetComponent<Rigidbody>();
            _shotEnemy.Play();

            shellRb.AddForce(shell.transform.forward * SHOT_FORCE, ForceMode.Impulse);
        }
    }
}
