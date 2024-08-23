using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float maxStamina;
    public float currentStamina;
    public float maxHunger;
    public float currentHunger;

    public Image healthBar;
    public Image staminaBar;
    public Image hungerBar;

    public float healthRegen;
    public float staminaRegen;
    public float hungerRegen;
    private void Start()
    {
        maxHealth = 100;
        maxStamina = 100;
        maxHunger = 100;
        currentHealth = 100;
        currentStamina = 100;
        currentHunger = 100;

        healthRegen = 0;
        staminaRegen = 5;
        hungerRegen = -.5f;
        
    }
    private void UpdateBars()
    {
        healthBar.fillAmount = currentHealth/maxHealth;
        staminaBar.fillAmount = currentStamina/maxStamina;
        hungerBar.fillAmount = currentHunger/maxHunger;
    }
    private void Regeneration()
    {
        currentHealth += healthRegen * Time.deltaTime;
        currentStamina += staminaRegen * Time.deltaTime;
        currentHunger += hungerRegen * Time.deltaTime;
        if(currentHealth >= 100) currentHealth = 100;
        if(currentStamina >= 100) currentStamina = 100;
        if(currentHunger >= 100) currentHunger = 100;
        if(currentHunger <= 0) healthRegen = -5;
        if (currentHealth <= 0) Application.Quit();
    }
    private void Update()
    {
        UpdateBars();
        Regeneration();
    }
}
