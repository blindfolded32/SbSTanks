using System;
using Interfaces;
using UnityEngine;

namespace Controllers.Input
{
    public class InputController : IExecute
    {
        private readonly IPCInputSpace _keyBoardInput;
        public readonly SkillButtons UIInput;
        public readonly ControlButtons UIControl;
        public event Action<KeyCode> SkillUsed;
        public event Action<KeyCode> ControlButtonPressed;

        public InputController(IPCInputSpace keyBoardInput, SkillButtons uiInput, ControlButtons uiControl)
        {
            _keyBoardInput = keyBoardInput;
            UIInput = uiInput;
            UIControl = uiControl;
            _keyBoardInput.ButtonDown +=(keycodein)=>
            {
                ControlButtonPressed?.Invoke(keycodein);
                SkillUsed?.Invoke(keycodein);
            };
            
            UIInput.ButtonDown +=(keycodein)=> SkillUsed?.Invoke(keycodein);
            UIControl.ButtonDown += (keycodein) => ControlButtonPressed?.Invoke(keycodein);
        }
        public void Execute(float deltaTime)
        {
            _keyBoardInput.CheckButtons();
            UIInput.CheckButtons();
            UIControl.CheckButtons();
        }
    }
}

