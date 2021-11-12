using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SbSTanks
{
   // [RequireComponent(typeof(Animator))]
    public class Player : Unit
    {
        private bool _hitStatus = false;
        public bool GetHitStatus { get => _hitStatus; set => _hitStatus = value; }

        protected override void OnCollisionEnter(Collision collision)
        {
            ShellHit?.Invoke(collision.gameObject, this, this.GetType());
            _shellController.ReturnShell(collision.gameObject);
        }

        public void Shot(PlayerModel playerModel)
        {
          //  Debug.Log($"Player has {playerModel.ElementId} in model");
            var shell = _shellController.GetShell(_parameters.Damage, _shotStartPoint);
            var shellRb = shell.GetComponent<Rigidbody>();

            shellRb.AddForce(shell.transform.forward * SHOT_FORCE, ForceMode.Impulse);
            _hitStatus = true;
        }
    }
}
