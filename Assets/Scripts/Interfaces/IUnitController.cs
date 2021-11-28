using System;
using Markers;
using Unit;
using UnityEngine;

namespace Interfaces
{
    public interface IUnitController : IController
    {
        public IModel Model {get; }
        public NameManager.State State { get; set; }
        public Transform GetShotPoint {get; }
        public Transform GetTransform {get; }
        public NameManager.State GetState{ get;}
        public void SetParams(IModel parameters);
        public event Action StateChanged;
        public void ChangeState(NameManager.State state);
    }
}