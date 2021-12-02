using System.Collections.Generic;
using Controllers.Model;
using Interfaces;
using Markers;
using SaveLoad;
using Unit;
using UnityEngine;

namespace Enemy
{
    public class EnemySpawner
    {
        internal readonly List<Enemy> Enemies = new List<Enemy>();
        internal readonly List<IUnitController> UnitControllers = new List<IUnitController>();
        public EnemySpawner(IEnumerable<EnemySpawnPoint> points)
        {
            //var fabric = new EnemyFabric();
            foreach (var point in points)
            {
                var enemy = EnemyFabric.Create(point.transform,
                    new UnitModel(new Health(10, 10), 4, (NameManager.ElementList) Random.Range(0,3)));
                Enemies.Add(enemy);
                UnitControllers.Add(enemy.Controller);
            }
        }

        public void LoadEnemies(Saver save)
        {
            for (int i = 0; i <Enemies.Count; i++)
            {
                Enemies[i].Controller.SetParams(save.AbstractUnits[i]);
            }
        }
    }
}