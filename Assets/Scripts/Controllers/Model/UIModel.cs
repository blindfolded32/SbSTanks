using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIModel
{
    private List<Canvas> _canvases;

    public List<Canvas> GetCanvases { get => _canvases; }

    public UIModel()
    {
        _canvases = new List<Canvas>();
        _canvases.AddRange(GameObject.FindObjectsOfType<Canvas>());
        foreach (var canvase in _canvases)
        {
           Debug.Log($"Name {canvase.name} and {canvase.GetComponentsInChildren<Button>().Length}");
        }
    }
}
