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
            enemy.unitInitializationData = parameters;
            return enemy;
        }
    }
}