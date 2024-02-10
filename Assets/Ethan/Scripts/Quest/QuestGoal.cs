using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal 
{
    public GoalType goalType;
    public int requiredAmount;
    public int currentAmount;

    public bool IsReached()
    {
        return (currentAmount >= requiredAmount);
    }

    public void EnemyKilled()
    {
        if (goalType == GoalType.Kill)
            currentAmount++;
    }

    public void Damage()
    {
        //Test type
        if (goalType == GoalType.TakeDamage)
            currentAmount++;
    }
}

public enum GoalType
{
    Kill,
    Gathering,
    TakeDamage
    
    //We can add more types as needed
}