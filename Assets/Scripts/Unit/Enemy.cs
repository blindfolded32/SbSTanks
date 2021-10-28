using System;
using UnityEngine;

namespace SbSTanks
{
    public class Enemy : Unit
    {
        private void Start()
        {
            ReturnShot();
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

            shellRb.AddForce(shell.transform.forward * SHOT_FORCE, ForceMode.Impulse);
        }
    }
}
