using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Leo_Part
{
    public interface IPCInputSpace
    {
        public event Action<bool> OnSpaceDown;

        public void GetButton();
    }
}

