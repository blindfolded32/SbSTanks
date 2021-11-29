using System;
using Controllers.Model;
using Interfaces;
using Markers;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class PlayerModel : IModel
    {
        public Health HP { get; set;}
        public float Damage { get; set; }
        public NameManager.ElementList Element { get; set;}
        public event Action<NameManager.ElementList> OnElementChange;
        public Transform UnitPosition { get; set; } = default;
        public int Initiative { get; set; }

        public PlayerModel(IModel parameters) 
        {
            HP = parameters.HP;
            Damage = parameters.Damage;
            Element = parameters.Element;
            UnitPosition = parameters.UnitPosition;
            
        }
    }
}