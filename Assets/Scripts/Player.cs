using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //references player stat scripts
    public PlayerStats playerStats;
    public PlayerProfession profession;

   

    public PlayerProfession Profession
        
    {
        get
        {
            return profession;
        }
        set
        {
            ChangeProfession(value);
        }
    }
    public void ChangeProfession(PlayerProfession cProfession)
    {
        this.profession = cProfession;
        SetupProfession();
    }
    public void SetupProfession()
    {
        for (int i = 0; i < playerStats.basestats.Length; i++)
        {
            if (profession.defaultStats.Length < i)
            {
                playerStats.basestats[i].defaultStat = profession.defaultStats[i].defaultStat;
            }

        }
    }
   


}
