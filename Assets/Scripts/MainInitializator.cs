using Controllers;
using Controllers.Model;
using Enemy;
using Initialization;
using Markers;
using Player;
using SaveLoad;
using Unit;
using UnityEngine;
using  Object = UnityEngine.Object;

public class MainInitializator
{
    private readonly EnemySpawner _enemySpawn;
    private StepController stepController;

    private SkillArbitr skillArbiter;
   /* public MainInitializator(GameController mainController)
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
        var skillArbiter = new SkillArbitr(stepController, inputController, 
            new SkillController(playerFabric.Player.Controller, enemySpawn.Enemies));
        mainController.Add(stepController);
        mainController.Add(inputController);
        mainController.Add(timerController);
        mainController.Add(targetSelectionController);
        mainController.Add(RoundCanvas);

        new ParticlesInitialization(playerFabric.Player.Controller as IPlayerController, enemySpawn.Enemies);
        new SaveStruct(inputController,skillArbiter);
    }*/
    public MainInitializator(GameController mainController, Saver save = default)
    {
        
        var timerController = new TimerController();
        var playerFabric = new PlayerFabric();
        var inputController = new InputController(new KeyBoardInput(), new SkillButtons());
        if (!save.Equals(null))
        {
            playerFabric.Create(Object.FindObjectOfType<PlayerSpawnPoint>().transform,
            new UnitModel(new Health(save.PlayerModel.HP.Max, save.PlayerModel.HP.GetCurrentHp),
                save.PlayerModel.Damage, save.PlayerModel.Element));
         //   new UnitModel(new Health(100,100),1,0 ));
        _enemySpawn = new EnemySpawner(Object.FindObjectsOfType<EnemySpawnPoint>());
        stepController = new StepController(_enemySpawn.Enemies,playerFabric.Player.Controller as IPlayerController, timerController);
        skillArbiter = new SkillArbitr(stepController, inputController, 
            new SkillController(playerFabric.Player.Controller, _enemySpawn.Enemies));//TODO here! default values
        }
        else
        {
            playerFabric.Create(Object.FindObjectOfType<PlayerSpawnPoint>().transform,
                new UnitModel(new Health(100,100),1,0 ));
            _enemySpawn = new EnemySpawner(Object.FindObjectsOfType<EnemySpawnPoint>());
            stepController = new StepController(_enemySpawn.Enemies,playerFabric.Player.Controller as IPlayerController, timerController);
           skillArbiter = new SkillArbitr(stepController, inputController, 
                new SkillController(playerFabric.Player.Controller, _enemySpawn.Enemies));
        }
        var camera = Camera.main; // Может быть взять из GameStater.cs? 

        var targetSelectionController = new TargetSelectionController(camera, playerFabric.Player.Controller,_enemySpawn.Enemies);
        var RoundCanvas = new RoundCanvas(stepController);
       
        mainController.Add(stepController);
        mainController.Add(inputController);
        mainController.Add(timerController);
        mainController.Add(targetSelectionController);
        mainController.Add(RoundCanvas);

        new ParticlesInitialization(playerFabric.Player.Controller as IPlayerController, _enemySpawn.Enemies);
        new SaveStruct(inputController,skillArbiter);
    }
}