using System.Collections.Generic;
using System.Linq;
using  Object = UnityEngine.Object;

namespace SbSTanks


{
    public class MainInitializator
    {
        private List<Enemy> _enemies;
        public MainInitializator(GameController mainController)
        {
            var uiModel = new UIModel();
            var timerController = new TimerController();
            var pcinput = new PCInputSpace();
            var timerActionInvoker = new TimerActionInvoker();
            _enemies = Object.FindObjectsOfType<Enemy>().ToList();
            //var playerModel = new PlayerModel(timerController, data.Player);
            var playerController = new PlayerController(new PlayerModel(timerController), Object.FindObjectOfType<Player>());//, stepController);
            var stepController = new StepController(_enemies,playerController, timerController);
            var shellController = new ShellController(playerController,_enemies);
            var skillUI = new SkillButtons(uiModel, stepController);
            var buttonActivationController = new ButtonActivationController(uiModel, stepController, _enemies, playerController);
            new TimerSetsInitialization(playerController, timerActionInvoker);
            new ParticlesInitialization(playerController, _enemies);
            new SkillControler(playerController,stepController,skillUI,pcinput,buttonActivationController);
          
            mainController.Add(shellController);
            mainController.Add(stepController);
          
            mainController.Add(new InputController(pcinput));
            mainController.Add(playerController);
            mainController.Add(timerController);
            mainController.Add(buttonActivationController);
            mainController.Add(skillUI);
        }
        
        
     /*   public MainInitializator(GameInitializationData data, GameController mainController)
        {
            var uiModel = new UIModel();
            new ParticlesInitialization(data.Player, data.Enemies);
            var timerController = new TimerController();
           
            var pcinput = new PCInputSpace();
            var timerActionInvoker = new TimerActionInvoker();
            var playerModel = new PlayerModel(timerController, data.Player);
            var playerController = new PlayerController(playerModel);//, stepController);
            var stepController = new StepController(data.Enemies,playerController, timerController);
            var shellController = new ShellController(data.Player, data.Enemies);
            var skillUI = new SkillButtons(uiModel, stepController);
            var buttonActivationController = new ButtonActivationController(uiModel, stepController, data.Enemies, playerController);
            new TimerSetsInitialization(playerModel, timerActionInvoker);

          
            mainController.Add(shellController);
           
            mainController.Add(stepController);
          
            mainController.Add(new InputController(pcinput));
            mainController.Add(playerController);
            mainController.Add(timerController);
          
            mainController.Add(buttonActivationController);
            new SkillControler(playerController,stepController,skillUI,pcinput,buttonActivationController);
            mainController.Add(skillUI);

            for (int i = 0; i < data.Enemies.Count; i++)
            {
                data.Enemies[i].Init(data.EnemyInitializationData, shellController, stepController); 
            }
            
            data.Player.Init(data.PlayerInitializationData, shellController, stepController); 
        }*/
    }
}