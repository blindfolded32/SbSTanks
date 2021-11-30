using System;
using Interfaces;
using UnityEngine;

namespace Enemy
{
    public class EnemyController : IUnitController
    {
        private Enemy _enemy;

        public IModel Model { get; private set; }
        public Transform GetShotPoint => _enemy.ShotPoint;
        public Transform GetTransform => _enemy.transform;
        public NameManager.State State { get; private set; }
        public void SetParams(IModel parameters) => Model = parameters;
        public event Action StateChanged;
        public void ChangeState(NameManager.State state)
        {
            if (State == state) return;
            switch (state)
            {
                case NameManager.State.Idle:
                //    EnemyLevitate.ReturnToGround(_enemy);
               // _enemy.gameObject.SetActive(true);
                    break;
                case NameManager.State.Attack:
                    break;
                case NameManager.State.Fired:
                    StateChanged?.Invoke();
                    break;
                case NameManager.State.Levitate:
                    EnemyLevitate.Levitate(_enemy);
                    break;
                case NameManager.State.Dead:
                  //  _enemy.gameObject.SetActive(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
           State = state;
          /* if(state == NameManager.State.Fired) StateChanged?.Invoke();
           if (state == NameManager.State.Levitate) EnemyLevitate.Levitate(_enemy);*/
           //  if (State == NameManager.State.Levitate) EnemyLevitate.ReturnToGround(_enemy);
          
        }
        public EnemyController(IModel unitModel, Enemy enemy)
        {
            Model = unitModel;
            _enemy = enemy;
            State = NameManager.State.Idle;
            enemy.TakeDamage+=GetDamage;
            Model.HP.IsDead += () => ChangeState(NameManager.State.Dead);
        }
        private void GetDamage(float damage)
        {
            Model.HP.ChangeCurrentHealth(damage);
            if (  Model.HP.GetCurrentHp <= 0)
            {
                ChangeState(NameManager.State.Dead);
            }
        }
    }
}