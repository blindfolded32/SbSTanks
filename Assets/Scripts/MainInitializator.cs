using UnityEngine;

namespace SbSTanks
{
    public class MainInitializator
    {
        public MainInitializator(GameInitializationData data, GameController mainController)
        {
            var timerController = new TimerController();
            var particleInitialization = new ParticlesInitialization();
            var pcinputinitialization = new PCInputSpaceInitialization();
            var timerActionInvoker = new TimerActionInvoker();

            var playerModel = new PlayerModel(pcinputinitialization.GetInputSpace(), timerController);
            var timerSetsInitialization = new TimerSetsInitialization(playerModel, timerActionInvoker);


            mainController.Add(timerController);
            var shellController = new ShellController(data.Player, data.Enemy);
            mainController.Add(shellController);



            

            mainController.Add(new InputController(pcinputinitialization.GetInputSpace()));
            mainController.Add(new PlayerController(playerModel, timerActionInvoker));

            data.Enemy.Init(data.EnemyInitializationData, shellController);
            data.Player.Init(data.PlayerInitializationData, shellController);
        }
    }
}