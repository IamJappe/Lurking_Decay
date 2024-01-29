using UnityEngine;
using UnityEngine.UI;

public class RayCastInteractable : MonoBehaviour
{
    public float interactionDistance = 4.5f;
    public bool changeCrossHair = true;

    private Image crossHair;

    private void Start()
    {
        GameObject crossHairObject = GameObject.Find("Crosshair");

        if (crossHairObject != null)
        {
            crossHair = crossHairObject.GetComponent<Image>();
        }
        else
        {
            Debug.LogError("Crosshair GameObject not found!");
        }
    }

    void Update()
    {
        int interactableLayerMask = 1 << LayerMask.NameToLayer("Interactable");

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactionDistance, interactableLayerMask))
        {
            if (changeCrossHair && crossHair != null)
                crossHair.color = Color.red;

            InteractableTalking interactable = hit.collider.GetComponent<InteractableTalking>();

            if (interactable != null && Input.GetMouseButtonDown(0))
            {
                interactable.Interact();
            }
        }
        else
        {
            if (crossHair != null)
                crossHair.color = Color.white;
        }
    }
}