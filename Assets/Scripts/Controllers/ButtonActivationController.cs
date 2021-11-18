using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SbSTanks
{
    public class ButtonActivationController : IExecute, IDisposable
    {
        public Dictionary<Button, Enemy> SwitchEnemyButtonsMatching = new Dictionary<Button, Enemy>(); 
        private readonly List<Button> _canvasButtons;
        private readonly List<Enemy> _enemies;
        private readonly PlayerController _playerController;
        private readonly StepController _stepController;
        private bool _isPreviousPlayerTurn;
        private Text _textNewRound;

        private const int RequiredCanvas = 0;

     //   private IEnumerable<Button> CanvasButtons => _canvasButtons;
        public ButtonActivationController(UIModel uIModel, StepController stepController, List<Enemy> enemies, PlayerController playerController)
        {
            _isPreviousPlayerTurn = true;
            _stepController = stepController;
            _enemies = enemies;
            _playerController = playerController;
            _canvasButtons = new List<Button>();
            _canvasButtons.AddRange(uIModel.GetCanvases[RequiredCanvas].GetComponentsInChildren<Button>());
            _textNewRound = uIModel.GetCanvases[RequiredCanvas].GetComponentInChildren<Text>();
            CreateDictionary(_canvasButtons,_enemies);
            AddListeners();
        }
        private void CreateDictionary(List<Button> buttons, List<Enemy> enemies)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                SwitchEnemyButtonsMatching.Add(buttons[i],enemies[i]);
            }
        }

        private void AddListeners()
        {
            foreach(var element in SwitchEnemyButtonsMatching)
            {
                element.Key.onClick.AddListener(
                    delegate
                    {
                        _playerController.RotatePlayer(element.Value.transform);
                    //   _playerController.GetPlayerModel.GetPlayer.transform.LookAt(element.Value.transform.position, Vector3.up);
                    });
            }
        }
        public void Execute(float deltaTime)
        {
            if (_stepController.isPlayerTurn == _isPreviousPlayerTurn) return;
            foreach (var element in SwitchEnemyButtonsMatching.Where(element => element.Value.isDead))
            {
                element.Key.interactable = false;
            }
            foreach (var canvasButton in _canvasButtons.FindAll(button => button.interactable))
            {
                canvasButton.interactable = !canvasButton.interactable;
            }
            _isPreviousPlayerTurn = !_isPreviousPlayerTurn;
            bool enabled;
            var enabler = _canvasButtons.ElementAt(0).interactable ? enabled = true : enabled = false;
//            _textNewRound.enabled = enabled;

        }

        public void Dispose()
        {
            foreach (var element in SwitchEnemyButtonsMatching)
            {
                element.Key.onClick.RemoveAllListeners();
            }
        }
    }
}

