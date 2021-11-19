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
        private const int RequiredCanvas = 0;

        public ButtonActivationController(UIModel uIModel, StepController stepController, List<Enemy> enemies, PlayerController playerController)
        {
            _isPreviousPlayerTurn = true;
            _stepController = stepController;
            _enemies = enemies;
            _playerController = playerController;
            _canvasButtons = new List<Button>();
            _canvasButtons.AddRange(uIModel.GetCanvases.Find(x =>x.name == "EnemyCanvas").GetComponentsInChildren<Button>());
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
                    });
            }
        }
        public void Execute(float deltaTime)
        {
            if (_playerController.IsPlayerTurn == _isPreviousPlayerTurn) return;
            ActiveCheck();
            _isPreviousPlayerTurn = !_isPreviousPlayerTurn;
        }

        private void ActiveCheck()
        {
            foreach (var element in SwitchEnemyButtonsMatching)
            {
                element.Key.interactable = !element.Value.isDead && _playerController.IsPlayerTurn;
            }
          
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

