using System;
using Unit;
using UnityEngine;

namespace Interfaces
{
    public interface IUnitController : IController
    {
        public IModel Model {get; }
        public Transform GetShotPoint {get; }
        public Transform GetTransform {get; }
        public NameManager.State State{ get;}
        public UnitHealthBar HealthBar { get; }
        public event Action StateChanged;
        public void SetParams(IModel parameters);
        public void ChangeState(NameManager.State state);
    }
}