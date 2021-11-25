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
        private  AbstractUnit _player;
        private  StepController _stepController;
        private  List<Enemy.Enemy> _enemies;
        public SkillController(AbstractUnit player,  List<Enemy.Enemy> enemies)
        {
            _player = player ;
            _enemies = enemies;
            Debug.Log($" constructor player at {_player.Controller.GetShotPoint.position}");
        }
         internal void EarthSkill()
        {
            var transformPosition = _enemies.
                ElementAt(Random.Range(0, _enemies.FindAll(x=>!x.IsDead).Count))
                .transform;
            PlayerRotation.RotatePlayer(_player.Controller,transformPosition);
            UnitShoot.Shot(_player.Controller,_player.Controller.GetShotPoint,_player.Controller.Model.Damage, ElementList.Earth);
        }
         internal void WaterSkill()
        {
            Debug.Log($"player at {_player.Controller.GetShotPoint.position}");
            UnitShoot.Shot(_player.Controller,_player.Controller.GetShotPoint,_player.Controller.Model.Damage,ElementList.Water);
        }
         internal void FireSkill()
        {
            foreach (var enemy in _enemies.Where(enemy => !enemy.IsDead))
            {
                enemy.TakingDamage(10,ElementList.Fire);
            }
            _player.Controller.IsFired = true;
        }
    }
}