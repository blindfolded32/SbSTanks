using System;
using Controllers.Model;
using Interfaces;
using Markers;
using UnityEngine;

namespace Unit
{
    [Serializable]
    public class UnitModel: IModel
    {
        [SerializeField]private Health _health;
        [SerializeField]private float _damage;
        [SerializeField]private NameManager.ElementList _element;
        public Health HP { get => _health; set=>_health =value; }
        public float Damage { get=> _damage; set=> _damage=value; }
        public NameManager.ElementList Element { get=> _element; set=> _element = value; }
        public UnitModel(Health hp, float damage, NameManager.ElementList element)
        {
            HP = hp;
            Damage = damage;
            Element = element;
        }

        
    }
}

