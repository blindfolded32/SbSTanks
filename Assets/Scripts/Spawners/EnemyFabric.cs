using Bullet;
using Markers;
using Shell;
using Unit;
using Unit.Model;
using UnityEngine;

namespace Spawners
{
    public class EnemyFabric
    {
        public static Enemy Create(Transform transform, UnitInitializationData parameters)
        {
            var enemy = Object.Instantiate(Resources.Load<Enemy>("Prefabs/Enemy"), transform);
            enemy.ShotPoint = enemy.GetComponentInChildren<ShotPoint>().transform;
           // enemy.UnitInitializationData = parameters;
           enemy.Element = parameters.Element;
            enemy.BulletPool = ServiceLocator.Resolve<BulletPool>();
            enemy.TakeDamage += enemy.GetDamage;
            enemy.transform.LookAt(Object.FindObjectOfType<Player>().transform);
            return enemy;
        }
    }
}