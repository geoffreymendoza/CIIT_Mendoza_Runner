using System;
using UnityEngine;

public class BalloonCharacter : Entity
{
    [Header("Body Parts")] 
    [SerializeField] private BodyPart[] bodyParts;
    [Header("Renderer stuffs")]
    [SerializeField] private Renderer[] renderers;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material greenMaterial;
    [SerializeField] private Material redMaterial;
    
    protected override void Awake() {
        base.Awake();
        EventManager.OnEnterPortal += OnEnterPortal;
    }

    protected override void OnDestroy() {
        EventManager.OnEnterPortal -= OnEnterPortal;
        base.OnDestroy();
    }

    private void OnEnterPortal(PortalColour colour) {
        switch (colour) {
            case PortalColour.Green:
                ApplyMaterial(greenMaterial);
                break;
            case PortalColour.Red:
                ApplyMaterial(redMaterial);
                break;
        }
    }

    private void ApplyMaterial(Material material) {
        foreach (var r in renderers) {
            r.material = material;
        }
    }

    public override void Apply(ApplyType type) {
        if (type == ApplyType.Regen && CurrentHealth == MaxHealth) {
            anim.CrossFade(Data.FLOATING_ANIM, 0.5f);
            return;
        }
        base.Apply(type);
        //TODO fixed body parts issue
        int bodyIndex = Mathf.Clamp(CurrentHealth, 0, bodyParts.Length - 1);
        // int bodyIndex = CurrentHealth - 1;
        //bodyParts[CurrentHealth].ModifyBodyParts();
        switch (type) {
            case ApplyType.Damage:
                bodyParts[bodyIndex].ModifyBodyParts(false);
                break;
            case ApplyType.Regen:
                bodyParts[bodyIndex].ModifyBodyParts(true);
                break;
        }
        
        if (CurrentHealth == MaxHealth) {
            anim.CrossFade(Data.RUNNING_ANIM, 0.5f);
            return;
        }
        anim.CrossFade(Data.CRAWLING_ANIM, .75f);
        // if(!IsAlive)
        // TODO popup balloon
    }

    private void OnTriggerEnter(Collider other) {
        int layer = other.gameObject.layer;
        if (Data.InteractableLayerMask != (Data.InteractableLayerMask | (1 << layer)))
            return;
        if (other.TryGetComponent(out IInteractable interactable)) 
            interactable.Interact(this);
    }
}

[System.Serializable]
public class BodyPart
{
    public string BodyPartName;
    public Transform[] Parts;
    private bool enableParts = true;

    public void ModifyBodyParts(bool activate) {
        if (enableParts == activate) 
            return;
        enableParts = activate;
        foreach (var part in Parts) {
            part.gameObject.SetActive(activate);
        }
    }
}
