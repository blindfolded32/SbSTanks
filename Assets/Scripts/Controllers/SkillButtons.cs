using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SbSTanks
{
    public class SkillButtons : IPCInputSpace
    {
        public event Action<KeyCode> ButtonDown;
        
        
        private readonly KeyCode[] _keycodes = new KeyCode[]{KeyCode.Q, KeyCode.W, KeyCode.E};
        private readonly Dictionary<KeyCode, Button> _skillButtonsDict = new Dictionary<KeyCode, Button>();
        private readonly StepController _stepController;
        

        public SkillButtons(UIModel uiModel)
        {
            var canvas = uiModel.GetCanvases;
            var buttonArray = canvas.Find(x => x.name == "SkillCanvas").GetComponentsInChildren<Button>();
             Debug.Log(buttonArray.Length);
            for (int i = 0; i < buttonArray.Length; i++)
            {
                _skillButtonsDict.Add(_keycodes[i],buttonArray[i]);
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
         
        }

        public void ButtonState(KeyCode keyCode, bool state)
        {
            _skillButtonsDict.Single(x => x.Key == keyCode).Value.interactable = state;
        }
    }

}


