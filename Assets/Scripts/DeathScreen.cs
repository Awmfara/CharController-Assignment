using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DeathScreen : MonoBehaviour
{
    #region Components and Objects
    Player player;
    ThirdPersonMovement thirdPersonMovement;
    AudioDir audioDir;
    #endregion
    #region Fields
    [SerializeField]
    private Vector3 respawnPoint;
    [SerializeField]
    private GameObject HUD=default;
    #endregion
    private void Awake()
    {
        player = FindObjectOfType<Player>();
        audioDir = FindObjectOfType<AudioDir>();
        thirdPersonMovement = FindObjectOfType<ThirdPersonMovement>();

    }
    private void OnEnable()
    {
        audioDir.MusicStop();
        audioDir.DeathSoundPlay();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
           
            Time.timeScale = 1;
            player.playerStats.ResetStats();
            thirdPersonMovement.Respawn = true;
            audioDir.DeathSoundStop();
            audioDir.MusicPlay();
            HUD.SetActive(true);
            this.gameObject.SetActive(false);


            
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            {
#if UNITY_EDITOR // if In Unity Editor
                EditorApplication.ExitPlaymode(); //Exits Playmode
#endif
                Application.Quit(); //Quits Application
            }
        }
    }
}
