using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace SbSTanks
{
    [Serializable]
    public struct GameInitializationData
    {
        [SerializeField] private Player _player;
        [SerializeField] private List<Enemy> _enemies;
        [SerializeField] private UnitInitializationData _playerInitializationData;
        [SerializeField] private UnitInitializationData _enemyInitializationData;
        [SerializeField] private List<Button> _enemiesSwitchButtons;
        [SerializeField] private Text _textNewRound;

        public Player Player => _player;
        public List<Enemy> Enemies => _enemies;
        public UnitInitializationData PlayerInitializationData => _playerInitializationData;
        public UnitInitializationData EnemyInitializationData => _enemyInitializationData;
        public List<Button> EnemiesSwitchButtons => _enemiesSwitchButtons;
        public Text Text => _textNewRound;
    }
}