using System.Collections.Generic;
using Controllers.Model;
using Markers;
using Unit;
using Unit.Model;
using UnityEngine;

namespace Spawners
{
    public class EnemySpawner
    {
        internal readonly List<Enemy> Enemies = new List<Enemy>();
        public EnemySpawner(EnemySpawnPoint[] points)
        {
            var fabric = new EnemyFabric();;
            foreach (var point in points)
            {
               Enemies.Add(EnemyFabric.Create(point.transform, 
                   new UnitInitializationData(new Health(10, 10), 1, 0)));
            }
        }
    }
}