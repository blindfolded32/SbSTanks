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
        var timerController = new TimerController();
        var timerActionInvoker = new TimerActionInvoker();
        
        var playerFabric = new PlayerFabric();
        playerFabric.Create(Object.FindObjectOfType<PlayerSpawnPoint>().transform,
            new UnitInitializationData(new Health(10,10),0,10 ), timerController);
        var enemySpawn = new EnemySpawner(Object.FindObjectsOfType<EnemySpawnPoint>());
        var camera = Camera.main; // Может быть взять из GameStater.cs? 
        var stepController = new StepController(enemySpawn.Enemies,playerFabric.Player.PlayerController, timerController);
        var shellController = new ShellController(playerFabric.Player.PlayerController,enemySpawn.Enemies);
        var inputController = new InputController(new KeyBoardInput(), new SkillButtons());
        var targetSelectionController = new TargetSelectionController(camera, playerFabric.Player.PlayerController,enemySpawn.Enemies);
            
        mainController.Add(shellController);
        mainController.Add(stepController);
        mainController.Add(inputController);
        mainController.Add(playerFabric.Player.PlayerController);
        mainController.Add(timerController);
        mainController.Add(targetSelectionController);
            
        new TimerSetsInitialization(playerFabric.Player.PlayerController, timerActionInvoker);
        new ParticlesInitialization(playerFabric.Player.PlayerController, enemySpawn.Enemies);
        new SkillArbitr(stepController, inputController, 
                        new SkillController(playerFabric.Player.PlayerController, enemySpawn.Enemies));
    }
}