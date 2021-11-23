using Interfaces;
using MainModels;

public class GameController : IController
{
    private GameControllerModel _model;

    public GameController()
    {
        _model = new GameControllerModel();
    }

    public void Add(IController controller)
    {
        if (controller is IExecute executeController)
        {
            _model.ExecuteControllers.Add(executeController);
        }

        if (controller is ILateExecute lateExecuteController)
        {
            _model.LateExecuteControllers.Add(lateExecuteController);
        }

        if (controller is IFixedExecute fixedController)
        {
            _model.FixedControllers.Add(fixedController);
        }
    }

    public void Execute(float deltaTime)
    {
        for (var element = 0; element < _model.ExecuteControllers.Count; ++element)
        {
            _model.ExecuteControllers[element].Execute(deltaTime);
        }
    }

    public void LateExecute(float deltaTime)
    {
        for (var element = 0; element < _model.LateExecuteControllers.Count; ++element)
        {
            _model.LateExecuteControllers[element].LateExecute(deltaTime);
        }
    }

    public void FixedExecute(float fixedTime, float fixedDeltaTime)
    {
        for (var element = 0; element < _model.FixedControllers.Count; ++element)
        {
            _model.FixedControllers[element].FixedExecute(fixedTime, fixedDeltaTime);
        }
    }

    public IModel Model { get; set; }
}