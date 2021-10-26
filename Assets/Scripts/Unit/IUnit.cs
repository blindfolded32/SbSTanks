using System;
using UnityEngine;

namespace SbSTanks
{
    public interface IUnit
    {
        public IParameters Parameters { get; }
        public Action<GameObject, IDamagebleUnit> ShellHit { get; set; }
    }
}