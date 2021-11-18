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
            ShellHit?.Invoke(collision.gameObject, this);
            _shellController.ReturnShell(collision.gameObject);
        }
        public void Shot(PlayerController playerController, int shotElement)
        {
            var shell = _shellController.GetShell(_parameters.Damage, _shotStartPoint,shotElement);
            var shellRb = shell.GetComponent<Rigidbody>();
            shellRb.AddForce(shell.transform.forward * SHOT_FORCE, ForceMode.Impulse);
            GetComponentInChildren<ParticleSystemShotIdentificator>().GetComponent<ParticleSystem>().Play();
            _hitStatus = true;
            playerController.IsPlayerTurn = false;
        }
    }
}
