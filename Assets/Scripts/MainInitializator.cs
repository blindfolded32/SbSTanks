﻿namespace SbSTanks
{
    public class MainInitializator
    {
        public MainInitializator(GameInitializationData data, GameController mainController)
        {
            var uiModel = new UIModel();
            
            var timerController = new TimerController();
            mainController.Add(timerController);
    
            var stepController = new StepController(data.Enemies,data.Player, timerController);
            mainController.Add(stepController);

            new ParticlesInitialization(data.Player, data.Enemies);
            var pcinput = new PCInputSpace();
            var timerActionInvoker = new TimerActionInvoker();

            var playerModel = new PlayerModel(timerController, data.Player);
            new TimerSetsInitialization(playerModel, timerActionInvoker);

            var shellController = new ShellController(data.Player, data.Enemies);
            mainController.Add(shellController);
            var playerController = new PlayerController(playerModel, stepController);
           var skillUI = new SkillButtons(uiModel, stepController);
            mainController.Add(new InputController(pcinput));
            mainController.Add(playerController);
            var buttonActivationController = new ButtonActivationController(uiModel, stepController, data.Enemies, playerController);
            mainController.Add(buttonActivationController);
            new SkillControler(playerController,stepController,skillUI,pcinput,buttonActivationController);
            mainController.Add(skillUI);

            for (int i = 0; i < data.Enemies.Count; i++)
            {
                data.Enemies[i].Init(data.EnemyInitializationData, shellController, stepController); 
            }
            
            data.Player.Init(data.PlayerInitializationData, shellController, stepController); 
        }
    }
}