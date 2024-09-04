using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider healthBar;

    public void updateHealthBar(float currentHealth, float maxHealth)
    {
        healthBar.value = currentHealth / maxHealth;
    }
}
