using System;
using Interfaces;
using UnityEngine;

namespace Controllers.Input
{
    public class KeyBoardInput : IPCInputSpace
    {
        public event Action<KeyCode> ButtonDown;

        public void CheckButtons()
        {
            if (UnityEngine.Input.GetKeyUp(KeyCode.Q)) ButtonDown?.Invoke(KeyCode.Q);
           if (UnityEngine.Input.GetKeyUp(KeyCode.W)) ButtonDown?.Invoke(KeyCode.W);
           if (UnityEngine.Input.GetKeyUp(KeyCode.E)) ButtonDown?.Invoke(KeyCode.E);
           if (UnityEngine.Input.GetKeyUp(KeyCode.R)) ButtonDown?.Invoke(KeyCode.R);
           if (UnityEngine.Input.GetKeyUp(KeyCode.L)) ButtonDown?.Invoke(KeyCode.L);
           if (UnityEngine.Input.GetKeyUp(KeyCode.Z)) ButtonDown?.Invoke(KeyCode.Z);
        }
    }
}

