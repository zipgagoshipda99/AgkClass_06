using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    public static UI_Controller uiController;
    void Awake()
    {
        if (uiController != null)
        {
            Destroy(gameObject);
        }
        else
        {
            uiController = this;
        }
    }
    [SerializeField] private Slider energySlider;
    [SerializeField] private TMP_Text energyText;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText;
    public TMP_Text energyLowText;

    
    public void UpdEnergySlider(float currentEnergy, float maxEnergy)
    {
        energySlider.maxValue = maxEnergy;
        energySlider.value = currentEnergy;
        energyText.text = $"{Mathf.RoundToInt(currentEnergy)} / {Mathf.RoundToInt(maxEnergy)}";
    }
    public void UpdHealthSlider(float currentHealth, float maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        healthText.text = $"{Mathf.RoundToInt(currentHealth)} / {Mathf.RoundToInt(maxHealth)}";
    }
}
