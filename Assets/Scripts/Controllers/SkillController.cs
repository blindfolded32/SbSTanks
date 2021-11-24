using System.Collections.Generic;
using System.Linq;
using Interfaces;
using Player;
using Unit;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class SkillController : IController
    {
        private readonly PlayerController _player;
        private readonly StepController _stepController;
        private readonly List<Enemy.Enemy> _enemies;
        public SkillController(IController player,  List<Enemy.Enemy> enemies)
        {
            _player = player as PlayerController;
            _enemies = enemies;
        }
        protected internal void EarthSkill()
        {
            var transformPosition = _enemies.
                ElementAt(Random.Range(0, _enemies.FindAll(x=>!x.IsDead).Count))
                .transform;
            PlayerRotation.RotatePlayer(_player,transformPosition);
            UnitShoot.Shot(_player,_player.GetView.ShotPoint,_player.Model.Damage,2);
        }
        protected internal void WaterSkill()
        {
            UnitShoot.Shot(_player,_player.GetView.ShotPoint,_player.Model.Damage,1);
        }
        protected internal void FireSkill()
        {
            foreach (var enemy in _enemies.Where(enemy => !enemy.IsDead))
            {
                enemy.TakingDamage(10,0);
            }
            _player.IsFired = true;
        }
    }
}