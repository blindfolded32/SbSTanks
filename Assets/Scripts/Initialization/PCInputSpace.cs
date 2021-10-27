using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Leo_Part
{
    public class PCInputSpace : IPCInputSpace
    {
        public event Action<bool> OnSpaceChange;

        public void GetAxis()
        {
            OnSpaceChange?.Invoke(Input.GetKeyDown(KeyCode.Space));
        }
    }
}

