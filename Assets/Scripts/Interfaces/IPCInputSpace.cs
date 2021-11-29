using System;
using UnityEngine;

namespace Interfaces
{
    public interface IPCInputSpace
    {
        public event Action<KeyCode> ButtonDown;
        public void CheckButtons();
    }
}

