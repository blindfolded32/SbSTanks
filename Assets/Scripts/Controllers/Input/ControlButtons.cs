using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using Markers;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Controllers.Input
{
    public class ControlButtons : IPCInputSpace
    {
        public event Action<KeyCode> ButtonDown;
        private readonly KeyCode[] _keycodes = {KeyCode.R, KeyCode.L, KeyCode.Z};
        private readonly Dictionary<KeyCode, Button> _skillButtonsDict = new Dictionary<KeyCode, Button>();
        private readonly StepController _stepController;
        
        public ControlButtons()
        {
            var buttonArray = Object.FindObjectOfType<ControlCanvas>().GetComponentsInChildren<Button>();
//            Debug.Log(buttonArray.Length);
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