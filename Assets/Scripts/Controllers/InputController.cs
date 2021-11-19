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
            _keyBoardInput.ButtonDown += SkillUsed;
            _UIInput.ButtonDown +=(keycodein)=>
            {
                Debug.Log(keycodein.ToString());
                SkillUsed(keycodein);
            };
        }

        public void Execute(float deltaTime)
        {
            _keyBoardInput.CheckButtons();
            _UIInput.CheckButtons();
        }
    }
}

