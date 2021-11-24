using Controllers;
using Controllers.Model;
using Enemy;
using Initialization;
using Markers;
using Player;
using Unit;
using UnityEngine;
using  Object = UnityEngine.Object;

public class MainInitializator
{
    public MainInitializator(GameController mainController)
    {
        var timerController = new TimerController();
        var playerFabric = new PlayerFabric();
        playerFabric.Create(Object.FindObjectOfType<PlayerSpawnPoint>().transform,
            new UnitModel(new Health(100,100),1,0 ));
        var enemySpawn = new EnemySpawner(Object.FindObjectsOfType<EnemySpawnPoint>());
        var camera = Camera.main; // Может быть взять из GameStater.cs? 
        var stepController = new StepController(enemySpawn.Enemies,playerFabric.Player.Controller as IPlayerController, timerController);
        var inputController = new InputController(new KeyBoardInput(), new SkillButtons());
        var targetSelectionController = new TargetSelectionController(camera, playerFabric.Player.Controller,enemySpawn.Enemies);
        var RoundCanvas = new RoundCanvas(stepController);
            
        mainController.Add(stepController);
        mainController.Add(inputController);
        mainController.Add(timerController);
        mainController.Add(targetSelectionController);
        mainController.Add(RoundCanvas);

        new ParticlesInitialization(playerFabric.Player.Controller as IPlayerController, enemySpawn.Enemies);
        new SkillArbitr(stepController, inputController, 
                        new SkillController(playerFabric.Player.Controller, enemySpawn.Enemies));
    }
}