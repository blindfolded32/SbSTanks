using UnityEngine;
using System;

namespace SbSTanks
{
    [Serializable]
    public struct GameInitializationData
    {
        [SerializeField] private Player _player;
        [SerializeField] private Enemy _enemy;
        [SerializeField] private UnitInitializationData _playerInitializationData;
        [SerializeField] private UnitInitializationData _enemyInitializationData;

        public Player Player { get => _player; }
        public Enemy Enemy { get => _enemy; }
        public UnitInitializationData PlayerInitializationData { get => _playerInitializationData; }
        public UnitInitializationData EnemyInitializationData { get => _enemyInitializationData; }
    }
}