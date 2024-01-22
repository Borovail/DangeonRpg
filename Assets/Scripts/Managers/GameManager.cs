using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }

        instance = this;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);  ///Lobby
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    //Temporary solution

    string[] phrases = new string[] {
    "Something went wrong, try again",
    "Nah",
    "Seems like that stuff is not implemented yet, but go ahead, try again",
    "It is definitely not implemented yet!",
    "Alright, alright, stop it, enough.",
    "Seriously, stop!!!",
    "Okay, one more, and I'm leaving.",
    "Bye!"};

    int clickCounter = 0;

    public Transform position;

    public void OpenSettings()
    {
        if( clickCounter == phrases.Length)   return;
        
        System.Random random = new System.Random();
        Vector3 motion = Vector3.zero;

        int number = random.Next(4);

        switch (number)
        {
            case 0:
                motion = Vector3.up;
                break;
            case 1:
                motion = Vector3.down;
                break;
            case 2:
                motion = Vector3.left;
                break;
            case 3:
                motion = Vector3.right;
                break;
        }

        FloatingTextManager.instance.Show(new FloatingTextSettings(phrases[clickCounter], 5f, 20, Color.red,motion/3, position.position, FloatingTextType.WorldSpaceFloatingText));

        clickCounter++;


        if (clickCounter == 8)
        {
            TheLastStraw();
        }

    }

    IEnumerator TheLastStraw()
    {
        yield return new WaitForSeconds(7f);
        QuitGame();
    }
    //Temporary solution


    //Player
    public Player player;
    public int playerCoins;
    public int playerHealth;





    //Sword Stats
    public Weapon[] swords;

    [HideInInspector] public int currentSwordId = 0;

}
