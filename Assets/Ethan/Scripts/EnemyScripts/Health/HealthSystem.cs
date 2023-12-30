using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void Damage(int dmg)
    {
        currentHealth -= dmg;
        
        if(currentHealth <= 0)
        {
            Destroy(this.gameObject);
            print("Enemy Dead!");
        }
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if(other.CompareTag("Hands"))
    //     {
    //         print("Working");
    //         Damage(1);
    //     }
    // }
}
