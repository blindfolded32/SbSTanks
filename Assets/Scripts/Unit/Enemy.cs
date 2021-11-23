using Bullet;
using Spawners;
using UnityEngine;

namespace Unit
{
    public class Enemy : AbstractUnit
    {
        public bool isShotReturn = false;
        private readonly float SHOT_FORCE = 280.0f;

      /*  public Enemy()
        {
            _bulletPool = new BulletPool(5);
        }*/
        protected override void OnCollisionEnter(Collision collision)
        {
            ShellHit?.Invoke(collision.gameObject, this);
        }
        public void ReturnShot()
        {
            if (IsDead)
            {
                isShotReturn = true;
                return;
            }

            var shell = BulletPool.GetItem("Bullet");
            
           // var shell = _shellController.GetShell(_parameters.Damage, _shotStartPoint,_parameters.ElementId);
            var shellRb = shell.GetComponent<Rigidbody>();
            shellRb.AddForce(shell.transform.forward * SHOT_FORCE, ForceMode.Impulse);
        }
    }
}
