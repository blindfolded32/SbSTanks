using UnityEngine;

namespace SbSTanks
{
    public class Enemy : Unit
    {
        public bool isShotReturn = false;

        protected override void OnCollisionEnter(Collision collision)
        {
            ShellHit?.Invoke(collision.gameObject, this);
            _shellController.ReturnShell(collision.gameObject);
            _stepController.EnemiesTurn();
        }
        public void ReturnShot()
        {
            var shell = _shellController.GetShell(_parameters.Damage, _shotStartPoint,_parameters.ElementId);
            var shellRb = shell.GetComponent<Rigidbody>();
            shellRb.AddForce(shell.transform.forward * SHOT_FORCE, ForceMode.Impulse);
        }
    }
}
