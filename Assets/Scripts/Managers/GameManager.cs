using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public event Action<int> OnPlayerCoinsChanged;
    public event Action<int,int> OnHealthChanged;
    public event Action<int,int> OnArmorChanged;
    public event Action<GameObject> OnPlayerGetsEffect;
    public event Action<GameObject> OnPlayerEffectEnds;

    public event Action OnPlayerDie;

    public int MaxHealth = 10;
    public int MaxArmor = 10;

    public void PlayerCoinsChanged(int amount)
    {
        OnPlayerCoinsChanged?.Invoke(amount);
    }
    
    public void PlayerHealthChanged(int amount)
    {
        OnHealthChanged?.Invoke(amount,MaxHealth);
    }

    public void PlayerArmorChanged(int amount)
    {
        OnArmorChanged?.Invoke(amount,MaxArmor);
    }

    public void PlayerGetsEffect(GameObject effectIcon)
    {
        OnPlayerGetsEffect?.Invoke(effectIcon);
    }

    public void PlayerEffectEnds(GameObject effectIcon)
    {
        OnPlayerEffectEnds?.Invoke(effectIcon);
    }

    public void PlayerDie()
    {
        OnPlayerDie?.Invoke();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);  ///Lobby
        ResumeGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }


    //Temporary solution

    string[] phrases = new string[] {
    "Something went wrong, try again",
    "Nah",
    "Seems like that stuff is not implemented yet",
    "It is definitely not implemented yet!",
    "Alright, alright, stop it, enough.",
    "Seriously, stop!!!",
    "Okay, one more, and I'm leaving.",
    "Bye!"};

    int clickCounter = 0;

    public Transform position;

    public void OpenSettings()
    {
        if (clickCounter == phrases.Length) return;

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

        FloatingTextManager.Instance.Show(new FloatingTextSettings(phrases[clickCounter], 2f, 20, Color.red, motion *60, position.position, FloatingTextType.UIRelativeFloatingText));

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



    //Sword Stats
    public Weapon[] swords;

    [HideInInspector] public int currentSwordId = 0;







    public void LoadUserStart() 
    { 
        PlayerCoinsChanged(0);
        PlayerHealthChanged(3);
        PlayerArmorChanged(3);
    }


}
