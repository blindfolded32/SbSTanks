using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace SbSTanks
{
    public class PlayerRotatioInitialization
    {
        private List<Button> _buttonsChangeTanks;
        private Enemy[] _enemyTanksPositions;

        private const int REQUIRED_CANVAS = 0;

        public PlayerRotatioInitialization(UIModel model, PlayerModel playerModel, GameInitializationData data)
        {
            _buttonsChangeTanks = new List<Button>();
            _buttonsChangeTanks.AddRange(model.GetCanvases[REQUIRED_CANVAS].GetComponentsInChildren<Button>());
            _enemyTanksPositions = data.Enemies;

            for(int i = 0; i< _buttonsChangeTanks.Count; i++)
            {
                new PlayerRotationEvent( playerModel, _enemyTanksPositions[i].Transform, _buttonsChangeTanks[i]);
            }

        }
    }
}

