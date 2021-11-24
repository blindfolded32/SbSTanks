using Unit;
using UnityEngine;

namespace Enemy
{
    public class Enemy : AbstractUnit
    {
        public bool isShotReturn = false;
        private readonly float SHOT_FORCE = 250.0f;
        public void GetDamage(float damage)
        {
           Controller.Model.HP.ChangeCurrentHealth(damage);
            Debug.Log($"My hp is {  Controller.Model.HP.GetCurrentHp}");
            if (  Controller.Model.HP.GetCurrentHp <= 0)
            {
                IsDead = true;
                Controller.IsFired = true;
            }
        }
        protected override void OnCollisionEnter(Collision collision)
        {
            ShellHit?.Invoke(collision.gameObject, this);
        }
    }
}
