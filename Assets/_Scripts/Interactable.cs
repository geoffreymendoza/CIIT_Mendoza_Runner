using System;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] protected InteractableType type;
    public InteractableType Type => type;
    private bool enable = true;

    public virtual void Interact(IHealthHandler handler) {
        if (!enable) return;
        enable = false;
        
        switch (Type) {
            case InteractableType.Balloon:
                //TODO object pooling
                handler.Apply(ApplyType.Regen);
                Destroy(this.gameObject);
                break;
            case InteractableType.Obstacle:
                handler.Apply(ApplyType.Damage);
                break;
        }
    }
}