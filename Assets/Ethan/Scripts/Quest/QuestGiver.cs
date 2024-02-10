using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestGiver : MonoBehaviour
{
    public List<Quest> quests = new List<Quest>();
    public PlayerMovement player;

    public GameObject questWindow, questAccept;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI xpText;
    public TextMeshProUGUI goldText;
    public GameObject textBox;
    public PlayerCamera cam;
    public PlayerMovement movement;

    private Quest currentQuest;

    public void OpenQuestWindow(int questIndex)
    {
        questWindow.SetActive(true);
        if (questIndex >= 0 && questIndex < quests.Count)
        {
            currentQuest = quests[questIndex];
            titleText.text = currentQuest.title;
            descriptionText.text = currentQuest.description;
            xpText.text = currentQuest.xpReward.ToString();
            goldText.text = currentQuest.goldReward.ToString();
        }
        else
        {
            Debug.LogError("Invalid quest index.");
        }
    }

    public void AcceptQuest()
    {
        if (currentQuest != null)
        {
            textBox.SetActive(false);
            questWindow.SetActive(false);
            questAccept.SetActive(true);
            currentQuest.isActive = true;
            player.activeQuests.Add(currentQuest);
            UnLockPlayer();
        }
        else
        {
            Debug.LogError("No quest selected to accept.");
        }
    }

    public void UnLockPlayer()
    {
        cam.enabled = true;
        movement.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}