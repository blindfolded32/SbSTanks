using Controllers.Model;
using Markers;
using UnityEngine;

namespace Interfaces
{
    public interface IModel
    {
        public Health HP { get; set; }
        public float Damage { get; set; }
        public NameManager.ElementList Element { get; set; }
        public Transform UnitPosition { get; set; }
    }
}