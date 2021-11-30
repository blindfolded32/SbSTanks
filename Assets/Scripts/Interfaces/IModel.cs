using System;
using Controllers.Model;
using UnityEngine;

namespace Interfaces
{
    public interface IModel
    {
        public Health HP { get; set; }
        public float Damage { get; set; }
        public NameManager.ElementList Element { get; set; }
        public event Action<NameManager.ElementList> OnElementChange;
        public Transform UnitPosition { get; set; }
        public int Initiative { get; set; }

    }
}