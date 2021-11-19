using System;
using UnityEngine;

namespace SbSTanks
{
    public class InputController : IExecute
    {
        private IPCInputSpace _keyBoardInput;
        private IPCInputSpace _UIInput;
        public Action<KeyCode> SkillUsed;

        public InputController(IPCInputSpace keyBoardInput, IPCInputSpace uiInput)
        {
            _keyBoardInput = keyBoardInput;
            _UIInput = uiInput;
            _keyBoardInput.ButtonDown +=(keycodein)=> SkillUsed.Invoke(keycodein);
            _UIInput.ButtonDown +=(keycodein)=> SkillUsed.Invoke(keycodein);
            
        }

        public void Execute(float deltaTime)
        {
            _keyBoardInput.CheckButtons();
            _UIInput.CheckButtons();
        }
    }
}

