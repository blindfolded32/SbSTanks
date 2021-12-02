using System;
using Interfaces;
using Unit;
using UnityEngine;

namespace Enemy
{
    public class EnemyController : IUnitController
    {
        private readonly Enemy _enemy;

        public IModel Model { get; private set; }
        public Transform GetShotPoint => _enemy.ShotPoint;
        public Transform GetTransform => _enemy.transform;
        public NameManager.State State { get; private set; }
        public UnitHealthBar HealthBar => _enemy.HealthBar;
        public void SetParams(IModel parameters) => Model = parameters;
        public event Action StateChanged;
        public void ChangeState(NameManager.State state)
        {
            if (State == state) return;
            switch (state)
            {
                case NameManager.State.Idle:
                    if(State == NameManager.State.Levitate) _enemy.ReturnToGround();
                _enemy.StopSmoke();
                _enemy.ChangeTargetState(true);
                    break;
                case NameManager.State.Attack:
                    break;
                case NameManager.State.Fired:
                    StateChanged?.Invoke();
                    break;
                case NameManager.State.Levitate:
                    _enemy.FlyCanI();
                    break;
                case NameManager.State.Dead:
                    _enemy.ConfirmDeath();
                    if(State == NameManager.State.Levitate) _enemy.ReturnToGround();
                   _enemy.ChangeTargetState(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
           State = state;
        }
        public EnemyController(IModel unitModel, Enemy enemy)
        {
            Model = unitModel;
            _enemy = enemy;
            State = NameManager.State.Idle;
            enemy.TakeDamage+=GetDamage;
            Model.HP.IsDead += () => ChangeState(NameManager.State.Dead);
        }
        private void GetDamage(float damage) => Model.HP.ChangeCurrentHealth(damage);
        
    }
}