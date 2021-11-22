using System.Linq;
using UnityEngine;
using  Object = UnityEngine.Object;

namespace SbSTanks


{
    public class MainInitializator
    {
        public MainInitializator(GameController mainController)
        {
            var camera = Camera.main; // Может быть взять из GameStater.cs? 
            var enemies = Object.FindObjectsOfType<Enemy>().ToList();
            var uiModel = new UIModel();
            var timerController = new TimerController();
            var timerActionInvoker = new TimerActionInvoker();
            var playerController = new PlayerController(new PlayerModel(timerController), Object.FindObjectOfType<Player>());
            var stepController = new StepController(enemies,playerController, timerController);
            var shellController = new ShellController(playerController,enemies);
            var inputController = new InputController(new KeyBoardInput(), new SkillButtons(uiModel));
            var targetSelectionController = new TargetSelectionController(camera, playerController, enemies);
            
            mainController.Add(shellController);
            mainController.Add(stepController);
            mainController.Add(inputController);
            mainController.Add(playerController);
            mainController.Add(timerController);
            mainController.Add(targetSelectionController);
            
            new TimerSetsInitialization(playerController, timerActionInvoker);
            new ParticlesInitialization(playerController, enemies);
            new SkillArbitr(stepController, inputController, new SkillController(playerController,enemies));


            foreach (var enemy in enemies)
            {
                enemy.Init(new UnitInitializationData(new Health(30.0f, 30.0f), 1.0f, 1),
                    shellController); 
            }

            playerController.GetView.Init(new UnitInitializationData(new Health(100.0f,100.0f),2.0f,0 ),
                    shellController);
        }
    }
}