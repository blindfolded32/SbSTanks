using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SbSTanks
{
    public abstract class Unit : MonoBehaviour, IDamagebleUnit, IUnit
    {
        //My code
        private readonly List<string> _elementsList = new List<string>{"Fire","Water","Earth"};
        private int _elementId;
        public int ElementId { get => _elementId; set=> _elementId=value; }
        //end of my code    
            
        public Action<int> TakeDamage { get; set; }
        public Action<GameObject, IDamagebleUnit> ShellHit { get; set; }

        [SerializeField] protected UnitParameters _parameters;
        [SerializeField] protected Transform _shotStartPoint;

        protected ShellController _shellController;
        protected StepController _stepController;

        protected const float SHOT_FORCE = 180f;

        public IParameters Parameters { get => _parameters; }

        public Transform GetShotPoint { get => _shotStartPoint; }
        public Transform Transform { get => gameObject.transform; }
        public int GetUnitElement => _parameters.ElementId;

        public void SetUnitElement(int value) => _parameters.ElementId = value;
        public void Init(UnitInitializationData data, ShellController shellController, StepController stepController)
        {             
            _parameters = new UnitParameters(this, data.hp,data.element,data.damage); //changed
            _shellController = shellController;
            _stepController = stepController;
        }
        protected abstract void OnCollisionEnter(Collision collision);

        public void TakingDamage(int damage, int elementId)
        {
            //Debug.Log($"enemy element is {elementId} target is {_parameters.ElementId} diff is {Math.Abs(elementId - _parameters.ElementId)}");
            if (elementId == _parameters.ElementId || Math.Abs(elementId - _parameters.ElementId) != 1) TakeDamage?.Invoke(damage);
            else
            {
                Debug.Log("Double Damage");
                TakeDamage?.Invoke(damage*2);
            }
         //   Debug.Log("Auch!");
        }
    }
}
