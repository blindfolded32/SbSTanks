using System.Linq;
using Controllers;
using Controllers.Model;
using Initialization;
using Markers;
using Shell;
using Spawners;
using Unit;
using Unit.Model;
using UnityEngine;
using  Object = UnityEngine.Object;

public class MainInitializator
{
    public MainInitializator(GameController mainController)
    {
        var playerFabric = new PlayerFabric();
        playerFabric.Create(Object.FindObjectOfType<PlayerSpawnPoint>().transform,
            new UnitInitializationData(new Health(10,10),0,10 ));
        var enemySpawn = new EnemySpawner(Object.FindObjectsOfType<EnemySpawnPoint>());
        
        var camera = Camera.main; // Может быть взять из GameStater.cs? 
        var uiModel = new UIModel();
        var timerController = new TimerController();
        var timerActionInvoker = new TimerActionInvoker();
        var playerController = new PlayerController(new PlayerModel(timerController), Object.FindObjectOfType<Player>());
        var stepController = new StepController(enemySpawn.Enemies,playerController, timerController);
        var shellController = new ShellController(playerController,enemySpawn.Enemies);
        var inputController = new InputController(new KeyBoardInput(), new SkillButtons(uiModel));
        var targetSelectionController = new TargetSelectionController(camera, playerController,enemySpawn.Enemies);
            
        mainController.Add(shellController);
        mainController.Add(stepController);
        mainController.Add(inputController);
        mainController.Add(playerController);
        mainController.Add(timerController);
        mainController.Add(targetSelectionController);
            
        new TimerSetsInitialization(playerController, timerActionInvoker);
        new ParticlesInitialization(playerController, enemySpawn.Enemies);
        new SkillArbitr(stepController, inputController, new SkillController(playerController, enemySpawn.Enemies));
    }
}