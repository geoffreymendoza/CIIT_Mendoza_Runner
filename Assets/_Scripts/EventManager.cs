using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public static event Action<SceneID> OnInitializeScene;
    public static void InvokeInitializeScene(SceneID sceneID) => OnInitializeScene?.Invoke(sceneID);

    public static event Action<int> OnInteractObject;
    public static void InvokeInteractObject(int damage) => OnInteractObject?.Invoke(damage);
    
    public static event Action<PortalColour> OnEnterPortal; 
    public static void InvokeEnterPortal(PortalColour colour) => OnEnterPortal?.Invoke(colour);

}