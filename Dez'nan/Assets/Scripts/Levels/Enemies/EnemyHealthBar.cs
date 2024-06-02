using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
            slider.value = currentHealth / maxHealth;
    }

}
