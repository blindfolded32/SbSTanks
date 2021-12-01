using Controllers;
using Controllers.Input;
using Enemy;
using Initialization;
using Markers;
using Player;
using SaveLoad;
using UnityEngine;
using  Object = UnityEngine.Object;

public class MainInitializator 
{
    private EnemySpawner _enemySpawn;
    private StepController stepController;
    private PlayerSpawner _playerSpawner;
    private SkillArbitr skillArbiter;

    private TimerController timerController;
    private InputController inputController;
    private GameController _gameController;

    private Camera camera;

    private TargetSelectionController targetSelectionController;

    private RoundCanvas RoundCanvas;
    private SkillController SkillControl;

    public MainInitializator(GameController mainController)
    {
        _playerSpawner = new PlayerSpawner(Object.FindObjectsOfType<PlayerSpawnPoint>());
        inputController = new InputController(new KeyBoardInput(), new SkillButtons(), new ControlButtons());
        timerController = new TimerController();
        _enemySpawn = new EnemySpawner(Object.FindObjectsOfType<EnemySpawnPoint>());
            SkillControl = new SkillController(_enemySpawn.Enemies);
            stepController = new StepController(_enemySpawn.UnitControllers,_playerSpawner.PlayerControllers, timerController);
            skillArbiter = new SkillArbitr(stepController, inputController, SkillControl);
            InitUIControllers();
        new SaveStruct(inputController,skillArbiter,stepController);
       
        mainController.Add(inputController);
        mainController.Add(timerController);
        mainController.Add(targetSelectionController);
        RoundCanvas.Init(stepController);
        
        stepController.AddTimer();
    }

    private void InitUIControllers()
    {
        camera = Camera.main; // Может быть взять из GameStater.cs? 
        targetSelectionController = new TargetSelectionController(camera, _playerSpawner.PlayerControllers,_enemySpawn.Enemies);
        new ParticlesInitialization(_playerSpawner.Players, _enemySpawn.Enemies);
    }
    public void GameLoad(Saver save)
    {
     /*   _gameController = ServiceLocator.Resolve<GameController>();
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
    _playerFabric = null;
    _player = null;
    Resources.UnloadUnusedAssets();
    _playerFabric = new PlayerFabric();
        _player =_playerFabric.Create(save.PlayerModel.UnitPosition,
            new UnitModel(new Health(save.PlayerModel.HP.Max, save.PlayerModel.HP.GetCurrentHp),
                save.PlayerModel.Damage, save.PlayerModel.Element,save.PlayerModel.UnitPosition));
        _enemySpawn = new EnemySpawner(Object.FindObjectsOfType<EnemySpawnPoint>());
        SkillControl = new SkillController(_player, _enemySpawn.Enemies);
        stepController = new StepController(_enemySpawn.Enemies,_player.Controller, timerController);
        stepController.ReInitController.ReInit(_enemySpawn.Enemies);
        stepController.ReInitController.NewRound(_enemySpawn.Enemies);

        skillArbiter = new SkillArbitr(stepController, inputController, SkillControl);//TODO here! default values
        skillArbiter.SetSkills(save.SkillCDs);
      
        InitControllers();
        _gameController.Add(stepController);
        _gameController.Add(inputController);
        _gameController.Add(timerController);
        _gameController.Add(targetSelectionController);
        _gameController.Add(RoundCanvas);*/
     _playerSpawner.LoadPlayers(save);
     _enemySpawn.LoadEnemies(save);
        skillArbiter.SetSkills(save.SkillCDs);
        stepController.TurnNumber = save.turnNumber;
        stepController.ReInitController.Renew();
        stepController.AddTimer();
    }
}