using Controllers.Model;
using Interfaces;

namespace Unit
{
    public class UnitModel: IModel
    {
        public Health HP { get; set; }
        public float Damage { get; set; }
        public int Element { get; set; }

        public UnitModel(Health hp, float damage, int element)
        {
            HP = hp;
            Damage = damage;
            Element = element;
        }

        
    }
}

