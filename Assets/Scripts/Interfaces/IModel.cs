using Controllers.Model;
using Markers;

namespace Interfaces
{
    public interface IModel
    {
        public Health HP { get; set; }
        public float Damage { get; set; }

        public NameManager.ElementList Element { get; set; }
    }
}