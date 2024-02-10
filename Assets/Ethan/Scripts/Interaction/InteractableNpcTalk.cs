using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNpcTalk : MonoBehaviour
{
    public GameObject questTalk, talkText;
    public  QuestGiver giver;
    public PlayerCamera cam;
    public PlayerMovement movement;
    public bool givesQuest;
   
    public void InteractTalk()
    {

        if(givesQuest == true)
        {
            LockPlayer();
            questTalk.SetActive(true);
        }
        else
        {
            talkText.SetActive(true);
        }
        //giver.OpenQuestWindow();
        //giver.AcceptQuest();
    }

    public void LockPlayer()
    {
        cam.enabled = false;
        movement.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}