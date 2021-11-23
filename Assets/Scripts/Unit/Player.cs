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
        protected override void OnCollisionEnter(Collision collision)
        {
            ShellHit?.Invoke(collision.gameObject, this);
           // _shellController.ReturnShell(collision.gameObject);
           // if (_parameters.Hp.GetCurrentHp<=0) PlayerDead?.Invoke(); 
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
            
          /*  var shell = _shellController.GetShell(_parameters.Damage, _shotStartPoint,shotElement);
            var shellRb = shell.GetComponent<Rigidbody>();
            shellRb.AddForce(shell.transform.forward * SHOT_FORCE, ForceMode.Impulse);
            GetComponentInChildren<ParticleSystemShotIdentificator>().GetComponent<ParticleSystem>().Play();
            GetHitStatus = true;
            playerController.IsPlayerTurn = false;*/
        }
    }
}
