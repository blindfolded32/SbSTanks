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
            enemy.Controller = new EnemyController(new UnitModel(parameters.HP,parameters.Damage,parameters.Element), enemy);
            enemy.Controller.Model.Element = parameters.Element;
            enemy.TakeDamage += enemy.GetDamage;
            enemy.transform.LookAt(Object.FindObjectOfType<Player.Player>().transform);
            enemy.HealthBar = enemy.gameObject.GetComponentInChildren<UnitHealthBar>();
            enemy.HealthBar.Init(enemy);
            return enemy;
        }
    }
}