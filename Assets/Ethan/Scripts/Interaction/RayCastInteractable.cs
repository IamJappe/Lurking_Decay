using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayCastInteractable : MonoBehaviour
{
    public float interactionDistance = 4.5f;
    public Image crossHair;
    public bool changeCrossHair = true;


    void Update()
    {   
        int interactableLayerMask = 1 << LayerMask.NameToLayer("Interactable");

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactionDistance, interactableLayerMask))
        {   
            if(changeCrossHair == true)
                crossHair.color = Color.red;

            InteractableTalking interactable = hit.collider.GetComponent<InteractableTalking>();
 
            if (interactable != null && Input.GetMouseButtonDown(0))
            {
                interactable.Interact();
            }
        }
        else
        {
            crossHair.color = Color.white;
        }
    }

}
