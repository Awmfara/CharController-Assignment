using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
[System.Serializable]
public struct BaseStats
{
    public string baseStatName;
    public int defaultStat;//stat from the class
    public int additionalStat;//additionstat from


    //final stats will be
    //default+current
    public int FinalStat
    {
        get
        {
            return defaultStat + additionalStat;
        }
    }
}
[System.Serializable]
public class PlayerStats
{
    [Header("Player Movement")]

    [SerializeField, Tooltip("Speed of Character Movement")]
    public float speed = 6f;
    [SerializeField,Tooltip("Speed while Sprinting")]
    public float sprintSpeed = 6f;
    [SerializeField,Tooltip("Speed of Character Movement")] 
    public float crouchSpeed = 6f;
    [SerializeField,Tooltip("Jump Height")] 
    public float jumpHeight = 1.0f;

    [Header("Player Stats")]

    [Tooltip("Players Current Health"), Range(0, 100)]
    public float currentHealth = 100;
    [Tooltip("Players Max Health"),Range(0,100)] 
    public float maxHealth = 100;
    [Tooltip("Players Current Mana"), Range(0, 100)] 
    public float currentMana = 100;
    [Tooltip("Players Max Mana"), Range(0, 100)] 
    public float maxMana = 100;
    [Tooltip("Players Current Stamina"), Range(0, 100)] 
    public float currentStamina = 100;
    [Range (1,20)]
    public float staminaDecreaseRate;
    [Range(1, 20)]
    public float staminaIncreaseRate;
    [Tooltip("Players Max Stamina"), Range(0, 100)] 
    public int maxStamina = 100;
  
    
    [Tooltip("Players Level")] public int playerLevel = 1;
    
  
    [Header("BaseStats")]
    public BaseStats[] basestats;
    public int baseStatPoints = 10;


    public bool SetStats(int statIndex, int amount)
    {
        if (amount > 0 && baseStatPoints - amount < 0)
        {
            return false;
        }
        else if (amount < 0)
        {
            return false;
        }
        basestats[statIndex].additionalStat += amount;
        baseStatPoints -= amount;
        return true;
    }
    public void ResetStats()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
        currentStamina = maxStamina;
    }




}
