using System;
using Interfaces;
using UnityEngine;

namespace Controllers
{
    public class InputController : IExecute
    {
        private IPCInputSpace _keyBoardInput;
        public SkillButtons UIInput;
        public event Action<KeyCode> SkillUsed;

        public InputController(IPCInputSpace keyBoardInput, SkillButtons uiInput)
        {
            _keyBoardInput = keyBoardInput;
            UIInput = uiInput;
            _keyBoardInput.ButtonDown +=(keycodein)=> SkillUsed.Invoke(keycodein);
            UIInput.ButtonDown +=(keycodein)=> SkillUsed.Invoke(keycodein);
        }

        public void Execute(float deltaTime)
        {
            _keyBoardInput.CheckButtons();
            UIInput.CheckButtons();
        }
    }
}

