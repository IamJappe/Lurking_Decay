using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTalking : MonoBehaviour
{
    public GameObject textTalk;
    public GameObject openInventory;
    public bool friendCutScene;
    public RayCastInteractable interactable;
    public PlayerCamera cam;
    public PlayerMovement movement;
    public GameObject swarmCamera;
    public GameObject player, textAttack, hands;
    public GameObject canavs;
    public StashWeapons stashWeapons;
    bool introFinished = false;

    public void Interact()
    {
        if (friendCutScene && introFinished == false)
        {
            textTalk.SetActive(true);
            interactable.changeCrossHair = true;
            cam.enabled = false;
            movement.enabled = false;
            StartCoroutine(AcquireKnife());
        }
        else if(friendCutScene && introFinished == true)
        {
            interactable.changeCrossHair = false;
        }
    }

    IEnumerator AcquireKnife()
    {
        yield return new WaitForSeconds(4f);

        openInventory.SetActive(true);

        while (!Input.GetKeyDown(KeyCode.I))
        {
            yield return null;
            introFinished = true;
            StartCoroutine(ZombieSwarmCutScene());
        }

        textTalk.SetActive(false);
        openInventory.SetActive(false);

        cam.enabled = true;
        movement.enabled = true;
    }

    IEnumerator ZombieSwarmCutScene()
    {
        yield return new WaitForSeconds(10f);
        player.SetActive(false);
        swarmCamera.SetActive(true);
        canavs.SetActive(false);
        yield return new WaitForSeconds(10f);
        stashWeapons.enabled = true;
        player.SetActive(true);
        swarmCamera.SetActive(false);
        canavs.SetActive(true);
        textAttack.SetActive(true);
        hands.SetActive(true);
        yield return new WaitForSeconds(5f);
        textAttack.SetActive(false);
    }
}