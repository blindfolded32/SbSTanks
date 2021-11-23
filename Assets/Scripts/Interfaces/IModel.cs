using Controllers.Model;

namespace Interfaces
{
    public interface IModel
    {
        public Health HP { get; set; }
        public float Damage { get; set; }

        public int Element { get; set; }
    }
}