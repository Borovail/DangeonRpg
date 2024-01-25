using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text playerCoins;

    public GridLayoutGroup playerHealthBar;
    public Image playerHealthPrefab;
    public GridLayoutGroup playerArmorBar;
    public Image playerArmorPrefab;

    public Player Player;

    private void Start()
    {
        Player.OnHealthChanged += (health) => UpdatePlayerAttribute(health, playerHealthBar, playerHealthPrefab);
        Player.OnArmorChanged += (armor) => UpdatePlayerAttribute(armor, playerArmorBar, playerArmorPrefab);
        GameManager.Instance.OnPlayerCoinsChanged +=(amount)=> UpdatePlayerCoins(amount);

        UpdatePlayerAttribute(Player.Health, playerHealthBar, playerHealthPrefab);
        UpdatePlayerAttribute(Player.Armor, playerArmorBar, playerArmorPrefab);
        UpdatePlayerCoins(Player.Coins);
    }

    private void UpdatePlayerAttribute(int value, GridLayoutGroup bar, Image prefab)
    {
        int currentCount = bar.transform.childCount;
        int requiredCount = Mathf.Clamp(value, 0, int.MaxValue);

        for (int i = currentCount; i != requiredCount;)
        {
            if (i < requiredCount)
            {
                Instantiate(prefab, bar.transform);
                i++;
            }
            else
            {
                Destroy(bar.transform.GetChild(--i).gameObject);
            }
        }
    }

    private void UpdatePlayerCoins(int coins)
    {
        playerCoins.text = coins.ToString();
    }
}
