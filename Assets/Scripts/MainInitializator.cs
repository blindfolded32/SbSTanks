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
            var timerActionInvoker = new TimerActionInvoker();
            
            
            
            _enemies = Object.FindObjectsOfType<Enemy>().ToList();
            var playerController = new PlayerController(new PlayerModel(timerController), Object.FindObjectOfType<Player>());
            var stepController = new StepController(_enemies,playerController, timerController);
            var shellController = new ShellController(playerController,_enemies);
            var skillUI = new SkillButtons(uiModel, stepController);
            KeyBoardInput keyBoardInput = new KeyBoardInput();;
            InputController inputController = new InputController(keyBoardInput, skillUI);
            var buttonActivationController = new ButtonActivationController(uiModel, stepController, _enemies, playerController);
            new TimerSetsInitialization(playerController, timerActionInvoker);
            new ParticlesInitialization(playerController, _enemies);
            new SkillControler(playerController,stepController,keyBoardInput,buttonActivationController);
          
            mainController.Add(shellController);
            mainController.Add(stepController);
            mainController.Add(inputController);
            mainController.Add(playerController);
            mainController.Add(timerController);
            mainController.Add(buttonActivationController);
           
        }
    }
}