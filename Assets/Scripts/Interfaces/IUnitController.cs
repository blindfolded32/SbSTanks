using Markers;
using Unit;
using UnityEngine;

namespace Interfaces
{
    public interface IUnitController : IController
    {
        public bool IsDead {get; }
        public IModel Model {get; }
        public bool IsFired {get; set; }
        public NameManager.State State { get; set; }
        public Transform GetShotPoint {get; }
        public Transform GetTransform {get; }
        public void SetParams(IModel parameters);
    }
}