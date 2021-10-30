using UnityEngine;

namespace SbSTanks
{
    public class MainInitializator
    {
        public MainInitializator(GameInitializationData data, GameController mainController)
        {
            var timerController = new TimerController();
            new ParticlesInitialization(data.Player, data.Enemies);
            var pcinputinitialization = new PCInputSpaceInitialization();
            var timerActionInvoker = new TimerActionInvoker();

            var playerModel = new PlayerModel(pcinputinitialization.GetInputSpace(), timerController);
            var timerSetsInitialization = new TimerSetsInitialization(playerModel, timerActionInvoker);


            mainController.Add(timerController);
            var shellController = new ShellController(data.Player, data.Enemies);
            mainController.Add(shellController);

            mainController.Add(new InputController(pcinputinitialization.GetInputSpace()));
            mainController.Add(new PlayerController(playerModel, timerActionInvoker));

            for (int i = 0; i < data.Enemies.Length; i++)
            {
                data.Enemies[i].Init(data.EnemyInitializationData, shellController);
            }
            
            data.Player.Init(data.PlayerInitializationData, shellController);
        }
    }
}