using UnityEngine;
using UnityEngine.UI;

namespace SbSTanks
{
    public class PlayerRotationEvent
    {
        private Transform _player;
        private Transform _enemyPosition;

        private float _time = 20f;
        private bool _isComplete;
        private float _lerpProgress = 0;
        private Quaternion _startRotation;

        public PlayerRotationEvent(PlayerModel playerModel, Transform enemyPosition, Button button)
        {
            _player = playerModel.GetPlayer.transform;

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

