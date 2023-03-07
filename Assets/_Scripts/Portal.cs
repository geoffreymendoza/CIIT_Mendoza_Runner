using UnityEngine;

public class Portal : Interactable
{
    [SerializeField] private PortalColour portalColour;
    [SerializeField] private Renderer renderer;
    [SerializeField] private Material greenPortalMaterial;
    [SerializeField] private Material redPortalMaterial;
    
    public void AssignPortalColour(PortalColour colour) {
        this.portalColour = colour;
        switch (this.portalColour) {
            case PortalColour.Green:
                renderer.material = greenPortalMaterial;
                break;
            case PortalColour.Red:
                renderer.material = redPortalMaterial;
                break;
        }
    }
    
    public override void Interact(IHealthHandler handler) {
        base.Interact(handler);
        EventManager.InvokeEnterPortal(portalColour);
    }
}

public enum PortalColour
{
    Unassigned,
    Green,
    Red
}
