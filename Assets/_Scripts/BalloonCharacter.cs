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
        AlterBodyParts(type);
        if (CurrentHealth == MaxHealth) {
            anim.CrossFade(Data.RUNNING_ANIM, 0.5f);
            return;
        }
        anim.CrossFade(Data.CRAWLING_ANIM, .25f);
        // TODO popup balloon death
        // if(!IsAlive)
    }

    private void AlterBodyParts(ApplyType type) {
        int bodyIndex = 0;
        switch (type) {
            case ApplyType.Damage:
                bodyIndex = CurrentHealth;
                bodyParts[bodyIndex].ModifyBodyParts(false);
                break;
            case ApplyType.Regen:
                bodyIndex = CurrentHealth;
                bodyIndex -= 1;
                bodyParts[bodyIndex].ModifyBodyParts(true);
                break;
        }
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
        // Debug.Log($"{BodyPartName} : {activate}");
        foreach (var part in Parts) {
            part.gameObject.SetActive(activate);
        }
    }
}
