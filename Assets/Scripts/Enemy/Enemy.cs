using Markers;
using Unit;
using UnityEngine;

namespace Enemy
{
    public class Enemy : AbstractUnit
    {
        public void GetDamage(float damage)
        {
           Controller.Model.HP.ChangeCurrentHealth(damage);
           if (  Controller.Model.HP.GetCurrentHp <= 0)
           {
               Controller.State = NameManager.State.Dead;
           }
        }
    }
}
