using UnityEngine;

namespace SbSTanks
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private GameInitializationData _dataForInitialization;
        private GameController _mainController;

        void Start()
        {
            _mainController = new GameController();
            new MainInitializator(_dataForInitialization, _mainController);
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
}