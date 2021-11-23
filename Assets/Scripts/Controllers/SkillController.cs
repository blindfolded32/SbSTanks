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
            _player.IsPlayerTurn = true;
            var transformPosition = _enemies.
                ElementAt(Random.Range(0, _enemies.FindAll(x=>!x.IsDead).Count))
                .transform;
            _player.RotatePlayer(transformPosition);
            UnitShoot.Shot(_player,_player.GetView.ShotPoint,_player.Model.Damage,2);
        }
        protected internal void WaterSkill()
        {
            _player.IsPlayerTurn = true;
            UnitShoot.Shot(_player,_player.GetView.ShotPoint,_player.Model.Damage,1);
        }
        protected internal void FireSkill()
        {
            _player.IsPlayerTurn = true;
            foreach (var enemy in _enemies.Where(enemy => !enemy.IsDead))
            {
                enemy.TakingDamage(10,0);
            }
            _player.IsPlayerTurn = false;
        }
    }
}