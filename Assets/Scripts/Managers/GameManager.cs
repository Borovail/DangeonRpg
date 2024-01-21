using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if(instance != null)
        {
           Destroy(instance);
        }

        instance = this;
    }



    //Player
    public Player player;
    public int playerCoins;

   



//Sword Stats
public Weapon[] swords;

    [HideInInspector] public int currentSwordId = 0;

}
