using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SbSTanks
{
    public class PlayerRotationEvent
    {
        private Transform _player;
        private Transform _enemyPosition;



        public PlayerRotationEvent(PlayerModel playerModel, Transform enemyPosition, Button button)
        {
            _player = playerModel.GetPlayer.transform;

            _enemyPosition = enemyPosition;
            button.onClick.AddListener(RotatePlayerTank);
   
        }

        public void RotatePlayerTank()
        {
            _player.LookAt(_enemyPosition, Vector3.up);
        }

    }
}

