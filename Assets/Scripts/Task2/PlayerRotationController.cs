using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SbSTanks
{
    public class PlayerRotationInitialization
    {
        private List<Button> _buttonsChangeTanks;
        private List<Transform> _enemyTanksPositions;

        private Transform _player;
        private Transform _enemyPosition;

        private const int REQUIRED_CANVAS = 0;

        public PlayerRotationInitialization(UIModel model, PlayerModel playerModel)
        {
            _player = GameObject.FindObjectOfType<Player>().transform;
            _buttonsChangeTanks = new List<Button>();
            _enemyTanksPositions = new List<Transform>();
            _buttonsChangeTanks.AddRange(model.GetCanvases[REQUIRED_CANVAS].GetComponentsInChildren<Button>());
            //_enemyTanksPositions
            for(int i = 0; i <  _buttonsChangeTanks.Count; i++)
            {
                _enemyPosition = _enemyTanksPositions[i].transform;
                _buttonsChangeTanks[i].onClick.AddListener(RotatePlayerTank);
            }
   
        }

        public void RotatePlayerTank()
        {
            _player.LookAt(_enemyPosition, Vector3.up);
        }

    }
}

