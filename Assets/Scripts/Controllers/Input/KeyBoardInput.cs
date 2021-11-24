using System;
using Interfaces;
using UnityEngine;

namespace Initialization
{
    public class KeyBoardInput : IPCInputSpace
    {
        public event Action<KeyCode> ButtonDown;

        public void CheckButtons()
        {
            if (Input.GetKeyUp(KeyCode.Q)) ButtonDown?.Invoke(KeyCode.Q);
           if (Input.GetKeyUp(KeyCode.W)) ButtonDown?.Invoke(KeyCode.W);
           if (Input.GetKeyUp(KeyCode.E)) ButtonDown?.Invoke(KeyCode.E);
           if (Input.GetKeyUp(KeyCode.R)) ButtonDown?.Invoke(KeyCode.R);
        }
    }
}

