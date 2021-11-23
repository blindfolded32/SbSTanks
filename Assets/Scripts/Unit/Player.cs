using System;
using Controllers;
using IdentificationElements;
using Interfaces;
using Spawners;
using UnityEngine;

namespace Unit
{
   // [RequireComponent(typeof(Animator))]
    public class Player : AbstractUnit
    {
        public event Action PlayerDead;
        public bool GetHitStatus { get; set; } = false;
        
        private readonly float SHOT_FORCE = 180.0f;
        protected override void OnCollisionEnter(Collision collision)
        {
            ShellHit?.Invoke(collision.gameObject, this);
            if (unitInitializationData.Hp.GetCurrentHp <= 0) PlayerDead?.Invoke();
        }
        public void Shot(PlayerController playerController, int shotElement)
        {
        /*  _shellController.shellobjectpool.Get(out var sh);
          sh.Activate(true);
          var rb = sh.ShellObject.GetComponent<Rigidbody>();
          sh.ShellObject.transform.position = playerController.GetView._shotStartPoint.position;
          sh.ShellObject.transform.rotation = playerController.GetView._shotStartPoint.rotation;
          rb.AddForce(sh.ShellObject.transform.forward * SHOT_FORCE, ForceMode.Impulse);*/
        Debug.Log("WANNA FIRE");
        var shell = ShellController.GetItem("Bullet");
           // var shell = ShellController.GetShell(unitInitializationData.Damage, ShotPoint,shotElement);
            var shellRb = shell.GetComponent<Rigidbody>();
            shellRb.AddForce(shell.transform.forward * SHOT_FORCE, ForceMode.Impulse);
           // GetComponentInChildren<ParticleSystemShotIdentificator>().GetComponent<ParticleSystem>().Play();
            GetHitStatus = true;
            playerController.IsPlayerTurn = false;
        }
    }
}
