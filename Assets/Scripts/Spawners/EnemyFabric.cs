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
        public Enemy Create(Transform transform, UnitInitializationData parameters)
        {
            var enemy = Object.Instantiate(Resources.Load<Enemy>("Prefabs/Enemy"), transform);
            enemy.shotPoint = enemy.GetComponentInChildren<ShotPoint>().transform;
            enemy.unitInitializationData = parameters;
            enemy.BulletPool = ServiceLocator.Resolve<BulletPool>();
            return enemy;
        }
    }
}