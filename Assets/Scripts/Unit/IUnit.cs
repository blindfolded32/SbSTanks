using System;
using System.Collections.Generic;
using UnityEngine;

namespace SbSTanks
{
    public interface IUnit
    {
        public IParameters Parameters { get; }
        public Action<GameObject, IDamagebleUnit> ShellHit { get; set; }

        public Transform GetShotPoint { get; }
        public Transform Transform { get; }
        

    }
}