using Interfaces;
using Unit;

namespace Enemy
{
    public class EnemyController : IController
    {
        public UnitModel UnitModel { get; set; }
        public Enemy Enemy;
        
        public EnemyController(UnitModel unitModel, Enemy enemy)
        {
            UnitModel = unitModel;
            Enemy = enemy;
        }

        public IModel Model { get => UnitModel; set => UnitModel = value as UnitModel; }
    }
}