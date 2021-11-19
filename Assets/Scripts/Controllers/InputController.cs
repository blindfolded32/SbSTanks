using System;
using UnityEngine;

namespace SbSTanks
{
    public class InputController : IExecute
    {
        private IPCInputSpace _keyBoardInput;
        private IPCInputSpace _UIInput;

        public Action<KeyCode> SkillUsed;

        public InputController(KeyBoardInput keyBoardInput, SkillButtons UIInput)
        {
            _keyBoardInput = keyBoardInput;
            _UIInput = UIInput;
            _keyBoardInput.ButtonDown += SkillUsed;
            _UIInput.ButtonDown += SkillUsed;

        }

        public void Execute(float deltaTime)
        {
            _keyBoardInput.CheckButtons();
            _UIInput.CheckButtons();
        }
    }
}

