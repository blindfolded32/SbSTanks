using UnityEngine;

namespace SbSTanks
{
    public class MainInitializator
    {
        public MainInitializator(GameInitializationData data, GameController mainController)
        {
            var shellController = new ShellController(data.Player, data.Enemy);
            var pcinputinitialization = new PCInputSpaceInitialization();

            mainController.Add(new InputController(pcinputinitialization.GetInputSpace()));
            mainController.Add(new PlayerController(pcinputinitialization.GetInputSpace()));

            data.Enemy.Init(data.EnemyInitializationData, shellController);
            data.Player.Init(data.PlayerInitializationData, shellController);
        }
    }
}