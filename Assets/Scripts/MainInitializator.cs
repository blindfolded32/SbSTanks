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
    private EnemySpawner _enemySpawn;
    private StepController stepController;
    private PlayerFabric _playerFabric;
    private SkillArbitr skillArbiter;

    private TimerController timerController;
    private InputController inputController;
    private GameController _gameController;

    private Camera camera;

    private TargetSelectionController targetSelectionController;

    private RoundCanvas RoundCanvas;
    private SkillController SkillControl;

    private Player.Player _player;
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
    public MainInitializator(GameController mainController)
    {
        _playerFabric = new PlayerFabric();
        inputController = new InputController(new KeyBoardInput(), new SkillButtons());
        timerController = new TimerController();
        
            _player= _playerFabric.Create(Object.FindObjectOfType<PlayerSpawnPoint>().transform,
                new UnitModel(new Health(100,100),1,0 ));
            _enemySpawn = new EnemySpawner(Object.FindObjectsOfType<EnemySpawnPoint>());
            SkillControl = new SkillController(_player.Controller, _enemySpawn.Enemies);
            stepController = new StepController(_enemySpawn.Enemies,_player.Controller as IPlayerController, timerController);
            skillArbiter = new SkillArbitr(stepController, inputController, SkillControl);//TODO here! default values
            InitControllers();
        new SaveStruct(inputController,skillArbiter);
        
        mainController.Add(stepController);
        mainController.Add(inputController);
        mainController.Add(timerController);
        mainController.Add(targetSelectionController);
        mainController.Add(RoundCanvas);
    }

    private void InitControllers()
    {
        camera = Camera.main; // Может быть взять из GameStater.cs? 
        targetSelectionController = new TargetSelectionController(camera, _player.Controller,_enemySpawn.Enemies);
        RoundCanvas = new RoundCanvas(stepController);
        new ParticlesInitialization(_player.Controller as IPlayerController, _enemySpawn.Enemies);
        
    }
    public void GameLoad(Saver save)
    {
        _gameController = ServiceLocator.Resolve<GameController>();
        _gameController._model.ExecuteControllers.Clear();
        _gameController._model.LateExecuteControllers.Clear();
        _gameController._model.FixedControllers.Clear();
        stepController = null;
        skillArbiter = null;
        foreach (var unit in Object.FindObjectsOfType<AbstractUnit>())
        {
            unit.gameObject.SetActive(false);
            Object.Destroy(unit.gameObject);
        }
    _enemySpawn.Enemies.Clear();
    _enemySpawn = null;
        _player =_playerFabric.Create(Object.FindObjectOfType<PlayerSpawnPoint>().transform,
            new UnitModel(new Health(save.PlayerModel.HP.Max, save.PlayerModel.HP.GetCurrentHp),
                save.PlayerModel.Damage, save.PlayerModel.Element));
        _enemySpawn = new EnemySpawner(Object.FindObjectsOfType<EnemySpawnPoint>());
        SkillControl = new SkillController(_player.Controller, _enemySpawn.Enemies);
        stepController = new StepController(_enemySpawn.Enemies,_player.Controller as IPlayerController, timerController);
        skillArbiter = new SkillArbitr(stepController, inputController, SkillControl);//TODO here! default values
        skillArbiter.SetSkills(save.SkillCDs);

        
        InitControllers();

        _gameController.Add(stepController);
        _gameController.Add(inputController);
        _gameController.Add(timerController);
        _gameController.Add(targetSelectionController);
        _gameController.Add(RoundCanvas);
        
    }
}