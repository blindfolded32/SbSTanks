using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIModel
{
    private List<Canvas> _canvases;

    public List<Canvas> GetCanvases { get => _canvases; }

    public UIModel()
    {
        _canvases = new List<Canvas>();
        _canvases.AddRange(GameObject.FindObjectsOfType<Canvas>());
    }
}
