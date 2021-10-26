﻿using UnityEngine;

namespace SbSTanks
{
    public class MainInitializator
    {
        public MainInitializator(GameInitializationData data, GameController mainController)
        {
            var shellController = new ShellController(data.Player, data.Enemy);

            data.Enemy.Init(data.EnemyInitializationData, shellController);
            data.Player.Init(data.PlayerInitializationData, shellController);
        }
    }
}