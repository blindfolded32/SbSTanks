using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator
{
        
    private static readonly Dictionary<Type, object> ServiceСontainer = 
        new Dictionary<Type, object>();

    public static void SetService<T>(T value) where T : class
    {
        var typeValue = typeof(T);
        if (!ServiceСontainer.ContainsKey(typeValue))
        {
            ServiceСontainer[typeValue] = value;
        }
    }
 
    public static T Resolve<T>()
    { 
        var type = typeof(T);
        if (ServiceСontainer.ContainsKey(type))
        {
            return (T) ServiceСontainer[type];
        }
        return default;
    }
}