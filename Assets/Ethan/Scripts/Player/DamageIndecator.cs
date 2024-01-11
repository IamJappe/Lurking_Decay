using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndecator : MonoBehaviour
{
    public Slider healthSlider;

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;

            healthSlider.value = currentHealth;
        }
    }
}
