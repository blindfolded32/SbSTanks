using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputInitialization
{
    public event Action<bool> OnSpaceChange;

    public void GetAxis()
    {
        OnSpaceChange?.Invoke(Input.GetKeyDown(KeyCode.Space));
    }
}
