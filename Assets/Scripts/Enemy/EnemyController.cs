using Interfaces;
using Markers;
using Unit;
using UnityEngine;

namespace Enemy
{
    public class EnemyController : IUnitController
    {
        private readonly Enemy _enemy;
        public NameManager.State State { get; set; }
        public Transform GetShotPoint => _enemy.ShotPoint;
        public Transform GetTransform => _enemy.transform;
        public void SetParams(IModel parameters) => Model = parameters;
        public bool IsDead => _enemy.IsDead;
        public IModel Model { get; private set; }
        public bool IsFired{ get; set; } = false;
        public EnemyController(IModel unitModel, Enemy enemy)
        {
            Model = unitModel;
            _enemy = enemy;
            State = NameManager.State.Idle;
        }

    }
}