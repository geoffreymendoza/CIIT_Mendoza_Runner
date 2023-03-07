using UnityEngine;

public static class Data
{
    public static readonly int FLOATING_ANIM = Animator.StringToHash("Floating");
    public static readonly int CRAWLING_ANIM = Animator.StringToHash("Crawling");
    public static readonly int IDLE_ANIM = Animator.StringToHash("Idle");
    public static readonly int RUNNING_ANIM = Animator.StringToHash("Running");
    
    private static readonly int InteractableLayer = LayerMask.NameToLayer("Interactable");
    public static readonly int InteractableLayerMask = 1 << InteractableLayer;
}

public enum SceneID
{
    Unassigned,
    MainMenu,
    InGame,
    Result,
}