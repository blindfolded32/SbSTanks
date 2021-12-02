using System.Collections.Generic;
using System.Linq;
using Interfaces;
using Unit;
using static NameManager;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class SkillController : IController
    {
        
        private  StepController _stepController;
        private readonly List<Enemy.Enemy> _enemies;
        public SkillController(List<Enemy.Enemy> enemies)
        {
            _enemies = enemies;
        }
         internal void EarthSkill(IUnitController player)
        {
            var transformPosition = _enemies.
                ElementAt(Random.Range(0, _enemies.FindAll(x=> x.Controller.State != State.Dead &&  
                                                               x.Controller.State != State.Levitate).Count))
                .transform;
            UnitRotation.RotateUnit(player,transformPosition);
            UnitShoot.Shot(player,player.GetShotPoint,player.Model.Damage, ElementList.Earth);
        }
         internal void WaterSkill(IUnitController player)
        {
            UnitShoot.Shot(player,player.GetShotPoint,player.Model.Damage,ElementList.Water);
        }
         internal void FireSkill(IUnitController player)
        {
            foreach (var enemy in _enemies.Where(x =>  x.Controller.State != State.Dead &&  
                                                               x.Controller.State != State.Levitate))
            {
                enemy.TakingDamage(player.Model.Damage*3,ElementList.Fire);
            }
            player.ChangeState(State.Fired);
        }
    }
}