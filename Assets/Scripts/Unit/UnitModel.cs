using System;
using Controllers.Model;
using Interfaces;
using UnityEngine;

namespace Unit
{
    [Serializable]
    public class UnitModel: IModel
    {
        [SerializeField]private Health _health;
        [SerializeField]private float _damage;
        [SerializeField]private NameManager.ElementList _element;
        [SerializeField] private Transform _position;
        public Health HP { get => _health; set=>_health =value; }
        public event Action<NameManager.ElementList> OnElementChange;
        public float Damage { get=> _damage; set=> _damage=value; }
        public NameManager.ElementList Element
        {
            get=> _element;
            set
            {
                _element = value;
                OnElementChange?.Invoke(_element);
            }
        }
        public Transform UnitPosition
        {
            get => _position;
            set => _position = value;
        }     
        public int Initiative { get; set; }
        public UnitModel(Health hp, float damage, NameManager.ElementList element,  Transform position = default)
        {
            HP = hp;
            Damage = damage;
            Element = element;
            UnitPosition = position;
            
        }
    }
}

