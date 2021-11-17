using UnityEngine;
using System;

namespace SbSTanks
{
    public interface IPCInputSpace
    {
        public event Action<KeyCode> ButtonDown;

        public void CheckButtons();
    }
}

