using System;
using UnityEngine;

namespace Interfaces
{
    public interface IUnit
    {
        public IParameters Parameters { get; set; }
        public Action<GameObject, IDamagebleUnit> ShellHit { get; set; }
        public Transform GetShotPoint { get; }
    }
}