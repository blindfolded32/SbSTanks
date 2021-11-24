using Controllers;
using Controllers.Model;
using Unit;

namespace Player
{
    public class PlayerModel : UnitModel
    { 
        public PlayerModel(UnitModel parameters ) : base(parameters.HP,parameters.Damage, parameters.Element)
        {
            HP = parameters.HP;
            Damage = parameters.Damage;
            Element = parameters.Element;
        }

    }
}