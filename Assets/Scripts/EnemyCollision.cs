using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    Player player;
    #region Variables
    [SerializeField]
    private bool damageBool;
    [SerializeField]
    private GameObject damageHUD;
    [SerializeField]
    private float damageCounter = 1f;
    #endregion
    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }
    void Start()
    {
        damageCounter = 0.5f;
    }

    private void Update()
    {
        if (damageBool)
        {
            if (damageCounter <= 0)
            {
                damageBool = false;
                damageHUD.SetActive(false);
                player.playerStats.currentHealth -= 10;
                damageCounter = 0.5f;
            }
            else
            {
                damageCounter -= Time.deltaTime;
             
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        
            damageBool = true;
            damageHUD.SetActive(true);    
    }
    
}
