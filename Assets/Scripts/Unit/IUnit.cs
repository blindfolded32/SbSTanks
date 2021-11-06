using System;
using System.Collections.Generic;
using UnityEngine;

namespace SbSTanks
{
    public interface IUnit
    {
        public IParameters Parameters { get; }
        public Action<GameObject, IDamagebleUnit, int> ShellHit { get; set; }
       public int ElementId { get; set; }//added

        public Transform GetShotPoint { get; }
        public Transform Transform { get; }
    }
}