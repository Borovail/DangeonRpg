using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Text playerCoins;

    public GridLayoutGroup playerHealthBar;
    public Image playerHealthPrefab;
    public GridLayoutGroup playerArmorBar;
    public Image playerArmorPrefab;



    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }

        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < GameManager.instance.playerHealth; i++)
        {
            AddPlayerHp();
        }

        for (int i = 0; i < GameManager.instance.playerArmor; i++)
        {
            AddPlayerArmor();
        }

        UpdatePlayerCoins(GameManager.instance.playerCoins);
    }



    public void UpdatePlayerCoins(int coins)
    {
        playerCoins.text = coins.ToString();
    }

    public void AddPlayerHp()
    {
      Instantiate(playerHealthPrefab, playerHealthBar.transform);
    }

    public void RemovePlayerHp()
    {
        Destroy(playerHealthBar.transform.GetChild(playerHealthBar.transform.childCount - 1).gameObject);
    }

    public void AddPlayerArmor()
    {
        Instantiate(playerArmorPrefab, playerArmorBar.transform);
    }

    public void RemovePlayerArmor()
    {
        Destroy(playerArmorBar.transform.GetChild(playerArmorBar.transform.childCount - 1).gameObject);
    }

   
}
