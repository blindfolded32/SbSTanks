using Unit;
using UnityEngine;

namespace Enemy
{
    public class Enemy : AbstractUnit
    {
        public bool isShotReturn = false;
        private readonly float SHOT_FORCE = 250.0f;

        protected override void OnCollisionEnter(Collision collision)
        {
            ShellHit?.Invoke(collision.gameObject, this);
          // if (UnitInitializationData.Hp.GetCurrentHp <= 0) IsDead = true;
        }
        public void ReturnShot()
        {
            if (IsDead)
            {
                isShotReturn = true;
                return;
            }
            var shell = BulletPool.GetItem("Bullet");
            var shellRb = shell.GetComponent<Rigidbody>();
          //  shell.Damage = UnitInitializationData.Damage;
          //  shell.Element = UnitInitializationData.Element;
            shell.Transform.position = ShotPoint.position;
            shell.transform.rotation = ShotPoint.rotation;
            shell.gameObject.SetActive(true);
            shellRb.AddForce(shell.transform.forward * SHOT_FORCE, ForceMode.Impulse);
        }
        
        public void GetDamage(float damage)
        {
          /*  UnitInitializationData.Hp.ChangeCurrentHealth(damage);
            Debug.Log($"My hp is {UnitInitializationData.Hp.GetCurrentHp}");
            if (UnitInitializationData.Hp.GetCurrentHp <= 0)
            {
                IsDead = true;
            }*/
        }
        
    }
}
