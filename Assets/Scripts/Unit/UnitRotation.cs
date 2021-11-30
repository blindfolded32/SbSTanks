using Interfaces;
using UnityEngine;
using static NameManager;

namespace Unit
{
    public static class UnitRotation
    {
        private static Quaternion _targetRotation;
        private static float _lerpProgress;
        private static Quaternion _startRotation;
        
        public static void RotateUnit(IUnitController controller,Transform targetTransform)
        {
            _lerpProgress += Time.deltaTime / ROTATION_TIME;
            var targetRotation = Quaternion.LookRotation(targetTransform.position - controller.GetTransform.position);
           controller.GetTransform.rotation = Quaternion.Lerp(_startRotation, targetRotation, _lerpProgress);
        }
    }
}