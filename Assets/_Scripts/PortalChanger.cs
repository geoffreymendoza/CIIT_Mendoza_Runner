using UnityEngine;

public class PortalChanger : MonoBehaviour
{
    [SerializeField] private bool starterTiles = false;
    [SerializeField] private Portal leftPortal;
    [SerializeField] private Portal rightPortal;

    // Start is called before the first frame update
    void Start() {
        if (starterTiles) {
            starterTiles = false;
            leftPortal.gameObject.SetActive(false);
            rightPortal.gameObject.SetActive(false);
            return;
        }
        
        float chance = Random.value;
        if (chance >= 0.5f) {
            leftPortal.AssignPortalColour(PortalColour.Green);
            rightPortal.AssignPortalColour(PortalColour.Red);
            return;
        }
        leftPortal.AssignPortalColour(PortalColour.Red);
        rightPortal.AssignPortalColour(PortalColour.Green);
    }
}
