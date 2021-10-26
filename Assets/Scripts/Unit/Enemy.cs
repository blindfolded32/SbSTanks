using System;
using UnityEngine;

namespace SbSTanks
{
    public class Enemy : Unit
    {
        protected override void OnCollisionEnter(Collision collision)
        {
            shellHit?.Invoke(collision.gameObject);
            _shellController.Destroy(collision.gameObject);
            ReturnShot();
        }

        private void ReturnShot()
        { 
            var shell = _shellController.InitShell(_parameters.Damage, _shotStartPoint);
            var shellRb = shell.GetComponent<Rigidbody>();

            shellRb.AddForce(shell.transform.forward * SHOT_FORCE, ForceMode.Force);
        }
    }
}
