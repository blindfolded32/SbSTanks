using Markers;
using Unit;

namespace Enemy
{
    public class Enemy : AbstractUnit
    {
        public void GetDamage(float damage)
        {
           Controller.Model.HP.ChangeCurrentHealth(damage);
           if (  Controller.Model.HP.GetCurrentHp <= 0)
           {
               Controller.ChangeState(NameManager.State.Dead);
           }
        }
    }
}
