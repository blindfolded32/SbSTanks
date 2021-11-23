using System;
using System.Collections.Generic;
using System.Linq;
using Controllers.Model;
using Interfaces;
using Markers;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;


namespace Controllers
{
    public class SkillButtons : IPCInputSpace
    {
        public event Action<KeyCode> ButtonDown;
        private readonly KeyCode[] _keycodes = new KeyCode[]{KeyCode.Q, KeyCode.W, KeyCode.E};
        private readonly Dictionary<KeyCode, Button> _skillButtonsDict = new Dictionary<KeyCode, Button>();
        private readonly StepController _stepController;
        public SkillButtons()
        {
            var buttonArray = Object.FindObjectOfType<SkillCanvas>().GetComponentsInChildren<Button>();
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


