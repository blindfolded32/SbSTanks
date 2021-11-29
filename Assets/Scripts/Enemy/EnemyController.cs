using System;
using System.Collections.Generic;
using Interfaces;
using Markers;
using Player;
using Unit;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemyController : IUnitController
    {
        private readonly Enemy _enemy;
        private readonly List<Player.Player> _player;
        private NameManager.State State { get; set; }
        public IModel Model { get; private set; }
        public Transform GetShotPoint => _enemy.ShotPoint;
        public Transform GetTransform => _enemy.transform;
        public NameManager.State GetState => State;
        public void SetParams(IModel parameters) => Model = parameters;
        public event Action StateChanged;
        public void ChangeState(NameManager.State state)
        {
           if (GetState == state) return;
            State = state;
            if(state == NameManager.State.Fired) StateChanged?.Invoke();
        }
        public EnemyController(IModel unitModel, Enemy enemy)
        {
            Model = unitModel;
            _enemy = enemy;
            State = NameManager.State.Idle;
            Model.HP.IsDead += () => ChangeState(NameManager.State.Dead);
        }
        public void RandomRotate()
        {
            PlayerRotation.RotatePlayer(this, _player[Random.Range(0, _player.Count)].Controller.GetTransform);
        }
    }
}