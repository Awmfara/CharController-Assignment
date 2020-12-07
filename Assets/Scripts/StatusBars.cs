using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StatusBars : MonoBehaviour
{
   private Player player;
    public Image healthBar;
    public Image staminaBar;
    public Image manaBar;
    
    private float curHealth, maxHealth, curStamina, maxStamina, curMana, maxMana;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }
    // Update is called once per frame
    private void Start()
    {
        maxHealth = player.playerStats.maxHealth;
        maxMana = player.playerStats.maxMana;
        maxStamina = player.playerStats.maxStamina;

        curHealth = player.playerStats.currentHealth;
        curStamina = player.playerStats.currentStamina;
        curMana = player.playerStats.currentMana;
    }
    void Update()
    {
        if (curHealth!=player.playerStats.currentHealth)
        {
            curHealth = player.playerStats.currentHealth;
            StatusChange(curHealth, maxHealth, healthBar);
        }

        if (curStamina != player.playerStats.currentStamina)
        {
            curStamina = player.playerStats.currentStamina;
            StatusChange(curStamina, maxStamina, staminaBar);
        }
        if (curMana!=player.playerStats.currentMana)
        {
            StatusChange(curMana, maxMana, manaBar);
        }
      
     

    }
    void StatusChange(float curStatus,float maxStaus,Image bar)
    {
        float amount = Mathf.Clamp01(curStatus / maxStaus);
        bar.fillAmount = amount;
    }
}
