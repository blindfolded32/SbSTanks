using Bullet;
using Controllers.Model;
using Markers;
using Unit;
using UnityEngine;

namespace Enemy
{
    public class EnemyFabric
    {
        public static Enemy Create(Transform transform, UnitModel parameters)
        {
            var enemy = Object.Instantiate(Resources.Load<Enemy>("Prefabs/Enemy"), transform);
            enemy.ShotPoint = enemy.GetComponentInChildren<ShotPoint>().transform;
            enemy.Controller = new EnemyController(new UnitModel(new Health(10,10),1,0 ), enemy);
            enemy.Element = parameters.Element;
            enemy.TakeDamage += enemy.GetDamage;
            enemy.transform.LookAt(Object.FindObjectOfType<Player.Player>().transform);
            return enemy;
        }
    }
}