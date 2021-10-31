using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SbSTanks
{
    public class PCInputSpace : IPCInputSpace
    {
        public event Action<bool> OnSpaceDown;

        public void CheckButtons()
        {
            OnSpaceDown?.Invoke(Input.GetKeyDown(KeyCode.Space));
        }
    }
}

