using System.Collections.Generic;
using UnityEngine;

namespace SbSTanks
{
    public class ReInitController
    {
        private List<Enemy> _enemies;
        private Player _player;

        public ReInitController(List<Enemy> enemies, Player player)
        {
            _enemies = enemies;
            _player = player;
        }

        public void ReInit()
        {
            _player.SetUnitElement(Random.Range(0,2));
           // Debug.Log($"Player new element is {_player.GetUnitElement}");
            foreach (var enemy in _enemies)
            {
                enemy.SetUnitElement(Random.Range(0,2));
               // Debug.Log($"Enemy new element is {enemy.GetUnitElement}");
            }
        }
    }
}