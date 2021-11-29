using Unit;
using UnityEngine;

namespace Enemy
{
    public class EnemyLevitate
    {
        public static void Levitate(AbstractUnit unit)
        {
            var transformPosition = unit.transform.parent.position;
            
            transformPosition.y  += 5.0f;
            Debug.Log($"Believer {unit.transform.parent} position is {transformPosition}");
        }

        public static void ReturnToGround(AbstractUnit unit)
        {
            var transformPosition = unit.transform.position;
            transformPosition.y  -= 5.0f;
            Debug.Log("BACK");
        }
    }
}