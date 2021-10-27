using UnityEngine;

namespace SbSTanks
{
    public class MainInitializator
    {
        public MainInitializator(GameInitializationData data, GameController mainController)
        {
            var shellController = new ShellController(data.Player, data.Enemy);
            mainController.Add(shellController);

            data.Enemy.Init(data.EnemyInitializationData, shellController);
            data.Player.Init(data.PlayerInitializationData, shellController);
        }
    }
}