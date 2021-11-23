using Bullet;
using Shell;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    private GameController _mainController;

    private void Awake()
    {
        _mainController = new GameController();
        new MainInitializator(_mainController);
        ServiceLocator.SetService<BulletPool>( new BulletPool(5));
    }
    void Update()
    {
        var time = Time.deltaTime;
        _mainController.Execute(time);
    }
    private void LateUpdate()
    {
        var time = Time.deltaTime;
        _mainController.LateExecute(time);
    }
    private void FixedUpdate()
    {
        var fixedTime = Time.fixedTime;
        var fixedDeltaTime = Time.fixedDeltaTime;
        _mainController.FixedExecute(fixedTime, fixedDeltaTime);
    }
}