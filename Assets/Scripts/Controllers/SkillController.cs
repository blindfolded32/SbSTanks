using System.Collections.Generic;
using System.Linq;
using Interfaces;
using Unit;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class SkillController : IController
    {
        private readonly PlayerController _player;
        private readonly StepController _stepController;
        private readonly List<Enemy> _enemies;
        public SkillController(PlayerController player,  List<Enemy> enemies)
        {
            _player = player;
            _enemies = enemies;
        }
        protected internal void EarthSkill()
        {
            _player.IsPlayerTurn = true;
            var transformPosition = _enemies.
                ElementAt(Random.Range(0, _enemies.FindAll(x=>!x.isDead).Count))
                .transform;
            _player.RotatePlayer(transformPosition);
            _player.GetView.Shot(_player,0);
        }
        protected internal void WaterSkill()
        {
            _player.IsPlayerTurn = true;
            _player.GetView.Shot(_player,2);
        }
        protected internal void FireSkill()
        {
            _player.IsPlayerTurn = true;
            foreach (var enemy in _enemies.Where(enemy => !enemy.isDead))
            {
                enemy.TakingDamage(10,1);
            }
            _player.IsPlayerTurn = false;
        }
        
    }
}