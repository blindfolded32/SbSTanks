using UnityEngine;
using UnityEngine.UI;

namespace SbSTanks
{
    public class PlayerRotationEvent
    {
        private Transform _player;
        private Transform _enemyPosition;

        private Quaternion _startRotation;

        public PlayerRotationEvent(PlayerController player, Transform enemyPosition, Button button)
        {
            _player =player.GetTransform;
            _enemyPosition = enemyPosition;
            button.onClick.AddListener(RotatePlayerTank);
        }
        public void RotatePlayerTank()
        {
            //_startRotation = _player.rotation;
            //while (_isComplete == false)
            //{
            //    _lerpProgress += Time.deltaTime / _time;
            //    _player.rotation = Quaternion.Lerp(_startRotation, _enemyPosition.rotation, _lerpProgress);

            //    if (_lerpProgress >= 1)
            //    {
            //        _isComplete = true;
            //    }
            //}
            //_isComplete = false;
            _player.LookAt(_enemyPosition, Vector3.up);
        }

    }
}

