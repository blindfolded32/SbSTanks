using UnityEngine;
using System;

namespace SbSTanks
{
    public class KeyBoardInput : IPCInputSpace
    {
        public event Action<KeyCode> ButtonDown;

        public void CheckButtons()
        {
            if (Input.GetKeyUp(KeyCode.Q)) ButtonDown?.Invoke(KeyCode.Q);
           if (Input.GetKeyUp(KeyCode.W)) ButtonDown?.Invoke(KeyCode.W);
           if (Input.GetKeyUp(KeyCode.E)) ButtonDown?.Invoke(KeyCode.E);
        }
    }
}

