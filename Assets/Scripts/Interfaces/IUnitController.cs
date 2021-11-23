using UnityEngine;

namespace Interfaces
{
    public interface IUnitController : IController
    {
        public bool IsDead { get; }
        public IModel Model { get; set; }
        public bool IsFired { get; set; }
        public Transform GetShotPoint { get; }
        
    }
}