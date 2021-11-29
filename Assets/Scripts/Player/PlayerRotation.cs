using Interfaces;
using UnityEngine;

namespace Player
{
    public static class PlayerRotation
    {
        private static Quaternion _targetRotation;
        private const float ROTATION_TIME = 0.5f;
        private static float _lerpProgress;
        private static Quaternion _startRotation;
        
        public static void RotatePlayer(IUnitController controller,Transform targetTransform)
        {
            _lerpProgress += Time.deltaTime / ROTATION_TIME;
            var targetRotation = Quaternion.LookRotation(targetTransform.position - controller.GetTransform.position);
           controller.GetTransform.rotation = Quaternion.Lerp(_startRotation, targetRotation, _lerpProgress);
        }
    }
}