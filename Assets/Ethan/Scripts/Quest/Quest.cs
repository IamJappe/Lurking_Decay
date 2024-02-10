using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest System/Quest")]
public class Quest : ScriptableObject
{
    public bool isActive;
    public string title;
    public string description;
    public int xpReward;
    public int goldReward;
    public List<QuestGoal> goals = new List<QuestGoal>();

    public void Complete()
    {
        isActive = false;
    }
}
