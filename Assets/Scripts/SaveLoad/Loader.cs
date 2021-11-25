using UnityEngine.SceneManagement;

namespace SaveLoad
{
    public class Loader
    {
        private GameController _gameController;
        public void Load(Saver saver)
        {
            SceneManager.LoadScene("Scenes/SampleScene");
            _gameController = ServiceLocator.Resolve<GameController>();
            if (_gameController._model.ExecuteControllers.Count > 0)
            {
                _gameController._model.ExecuteControllers = null;
                _gameController._model.FixedControllers = null;
                _gameController._model.LateExecuteControllers = null;
            }

            new MainInitializator(_gameController);
            
            
        }
    }
}