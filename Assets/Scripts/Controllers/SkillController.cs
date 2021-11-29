using System.Collections.Generic;
using System.Linq;
using Interfaces;
using Player;
using Unit;
using UnityEngine;
using static Markers.NameManager;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class SkillController : IController
    {
        
        private  StepController _stepController;
        private  List<Enemy.Enemy> _enemies;
        public SkillController(List<Enemy.Enemy> enemies)
        {
            _enemies = enemies;
        }
         internal void EarthSkill(IUnitController player)
        {
            var transformPosition = _enemies.
                ElementAt(Random.Range(0, _enemies.FindAll(x=>!x.IsDead).Count))
                .transform;
            PlayerRotation.RotatePlayer(player,transformPosition);
            UnitShoot.Shot(player,player.GetShotPoint,player.Model.Damage, ElementList.Earth);
        }
         internal void WaterSkill(IUnitController player)
        {
            UnitShoot.Shot(player,player.GetShotPoint,player.Model.Damage,ElementList.Water);
        }
         internal void FireSkill(IUnitController player)
        {
            foreach (var enemy in _enemies.Where(enemy => !enemy.IsDead))
            {
                enemy.TakingDamage(10,ElementList.Fire);
            }
            player.ChangeState(State.Fired);
        }
    }
}