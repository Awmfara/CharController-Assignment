using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Player player;
    AudioDir audioDir;
    [SerializeField]
    private GameObject deathScreen;
    [SerializeField]
    private GameObject uiCanvas;

    [SerializeField]
    private GameObject inventoryDisplay;
   
    private void Awake()
    {
        player = FindObjectOfType<Player>();
        audioDir = FindObjectOfType<AudioDir>();
     

    }
    private void Start()
    {
        audioDir.MusicPlay();
    }
    void Update()
    {
        if (player.playerStats.currentHealth<=0)
        {
            Time.timeScale = 0;
            deathScreen.SetActive(true);
            uiCanvas.SetActive(false);
            
        }
        if (Input.GetKeyDown(KeyCode.I)) 
        {
            if (!inventoryDisplay.activeSelf)
            {
                inventoryDisplay.SetActive(true);
            }
            else
            {
                inventoryDisplay.SetActive(false);
            }
        }
          
    }
}
