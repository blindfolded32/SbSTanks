using UnityEngine;
using System;

namespace SbSTanks
{
    [Serializable]
    public struct GameInitializationData
    {
        [SerializeField] private Player _player;
        [SerializeField] private Enemy[] _enemies;
        [SerializeField] private UnitInitializationData _playerInitializationData;
        [SerializeField] private UnitInitializationData _enemyInitializationData;

        public Player Player { get => _player; }
        public Enemy[] Enemies { get => _enemies; }
        public UnitInitializationData PlayerInitializationData { get => _playerInitializationData; }
        public UnitInitializationData EnemyInitializationData { get => _enemyInitializationData; }
    }
}