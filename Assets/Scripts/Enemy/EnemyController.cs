using Interfaces;
using Unit;
using UnityEngine;

namespace Enemy
{
    public class EnemyController : IUnitController
    {
        private UnitModel UnitModel { get; set; }
        private readonly Enemy _enemy;
        public Transform GetShotPoint => _enemy.ShotPoint;
        public Transform GetTransform => _enemy.transform;
        public bool IsDead => _enemy.IsDead;
        public IModel Model { get => UnitModel; set => UnitModel = value as UnitModel; }
        public bool IsFired{ get; set; } = false;
        public EnemyController(UnitModel unitModel, Enemy enemy)
        {
            UnitModel = unitModel;
            _enemy = enemy;
        }

    }
}