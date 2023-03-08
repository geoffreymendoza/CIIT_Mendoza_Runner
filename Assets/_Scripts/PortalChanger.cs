using UnityEngine;

public class PortalChanger : MonoBehaviour
{
    [SerializeField] private bool starterTiles = false;
    [SerializeField] private Portal leftPortal;
    [SerializeField] private Portal rightPortal;

    // Start is called before the first frame update
    void Start() {
        RandomAssignPortals();
    }

    public void RandomAssignPortals() {
        if (starterTiles) {
            starterTiles = false;
            ActivatePortals(false);
            return;
        }

        if (!leftPortal.gameObject.activeInHierarchy) {
            ActivatePortals(true);
        }
        float chance = Random.value;
        ActivatePortals(chance >= .75f);

        chance = Random.value;
        if (chance >= 0.5f) {
            leftPortal.AssignPortalColour(PortalColour.Green);
            rightPortal.AssignPortalColour(PortalColour.Red);
            return;
        }
        leftPortal.AssignPortalColour(PortalColour.Red);
        rightPortal.AssignPortalColour(PortalColour.Green);
    }

    private void ActivatePortals(bool activate) {
        leftPortal.gameObject.SetActive(activate);
        rightPortal.gameObject.SetActive(activate);
    }
}
