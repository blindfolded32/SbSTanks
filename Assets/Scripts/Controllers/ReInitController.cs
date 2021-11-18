using System.Collections.Generic;
using UnityEngine;

namespace SbSTanks
{
    public class ReInitController
    {
        private readonly List<Enemy> _enemies;
        public ReInitController(List<Enemy> enemies)
        {
            _enemies = enemies;
        }

        public void ReInit()
        {
          // _player.SetUnitElement(Random.Range(0,2));
           // Debug.Log($"Player new element is {_player.GetUnitElement}");
            foreach (var enemy in _enemies)
            {
                enemy.SetUnitElement(Random.Range(0,2));
               // Debug.Log($"Enemy new element is {enemy.GetUnitElement}");
            }
        }
    }
}