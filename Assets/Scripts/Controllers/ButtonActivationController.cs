using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

namespace SbSTanks
{
    public class ButtonActivationController : IExecute
    {
         
        private List<Button> _canvasButtons;
        private StepController _stepController;
        private bool _isPreviousPlayerTurn;
        private Text _textNewRound;

        private const int REQUIRED_CANVAS = 0;

        private IEnumerable<Button> CanvasButtons => _canvasButtons;
        public ButtonActivationController(UIModel uIModel, StepController stepController)
        {
            _isPreviousPlayerTurn = true;
            _stepController = stepController;
            _canvasButtons = new List<Button>();
            
            _canvasButtons.AddRange(uIModel.GetCanvases[REQUIRED_CANVAS].GetComponentsInChildren<Button>());
            _textNewRound = uIModel.GetCanvases[REQUIRED_CANVAS].GetComponentInChildren<Text>();
        }
        public void Execute(float deltaTime)
        {
            if (_stepController.isPlayerTurn == _isPreviousPlayerTurn) return;
            foreach (var canvasButton in CanvasButtons)
            {
                canvasButton.interactable = !canvasButton.interactable;
            }
            _isPreviousPlayerTurn = !_isPreviousPlayerTurn;
            bool enabled;
            var enabler = CanvasButtons.ElementAt(0).interactable ? enabled = true : enabled = false;
//            _textNewRound.enabled = enabled;

        }
    }
}

