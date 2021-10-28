using UnityEngine;

namespace SbSTanks
{
    public class MainInitializator
    {
        public MainInitializator(GameInitializationData data, GameController mainController)
        {
            var particleInitialization = new ParticlesInitialization();
            var timerController = new TimerController();
            mainController.Add(timerController);
            var shellController = new ShellController(data.Player, data.Enemy);
            mainController.Add(shellController);



            var pcinputinitialization = new PCInputSpaceInitialization();

            mainController.Add(new InputController(pcinputinitialization.GetInputSpace()));
            mainController.Add(new PlayerController(pcinputinitialization.GetInputSpace(), timerController));

            data.Enemy.Init(data.EnemyInitializationData, shellController);
            data.Player.Init(data.PlayerInitializationData, shellController);
        }
    }
}