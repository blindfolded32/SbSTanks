using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SbSTanks
{
    public interface IPCInputSpace
    {
        public event Action<bool> OnSpaceDown;

        public void CheckButtons();
    }
}

