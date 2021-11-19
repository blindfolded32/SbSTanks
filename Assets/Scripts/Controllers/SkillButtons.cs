using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SbSTanks
{
    public class SkillButtons : IPCInputSpace
    {
        readonly KeyCode[] keycodes = new KeyCode[]{KeyCode.Q, KeyCode.W, KeyCode.E};
        private readonly Dictionary<KeyCode, Button> _skillButtonsDict = new Dictionary<KeyCode, Button>();

        private readonly StepController _stepController;
        
        public event Action<KeyCode> ButtonDown;
        public SkillButtons(UIModel uiModel,StepController stepController)
        {
            _stepController = stepController;
            var canvas = uiModel.GetCanvases;
            var buttonArray = canvas.Find(x => x.name == "SkillCanvas").GetComponentsInChildren<Button>();//uiModel.GetCanvases[1].GetComponentsInChildren<Button>();
             Debug.Log(buttonArray.Length);
            for (int i = 0; i < buttonArray.Length; i++)
            {
                _skillButtonsDict.Add(keycodes[i],buttonArray[i]);
            }
            foreach (var button in _skillButtonsDict)
            {
                button.Value.onClick.AddListener(delegate
                {
                    Debug.Log("Click");
                    ButtonDown?.Invoke(button.Key);
                });
            }
        }
        public void CheckButtons()
        {
            if (_stepController.GetTurnNumber % 3 != 0)
            {
                Debug.Log("Q is incactive");
                _skillButtonsDict[KeyCode.Q].interactable = false;
            }
            else
            {
                _skillButtonsDict[KeyCode.Q].interactable = true;
            }
            if (_stepController.GetTurnNumber % 2 != 0)
            {
                _skillButtonsDict[KeyCode.E].interactable = false;
            }
            else
            {
                _skillButtonsDict[KeyCode.E].interactable = true;
            }
        }
    }

}


