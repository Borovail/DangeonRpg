using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordStatue :MonoBehaviour
{
    public SpriteRenderer sword;
    public Text upgradePrice;

    private void Start()
    {
        SetSword();
        SetPrice();
    }

    private void SetSword()
    {
        sword.sprite = GameManager.instance.swords[GameManager.instance.currentSwordId].skin;
    }

    private void SetPrice()
    {
        upgradePrice.text = GameManager.instance.swords[GameManager.instance.currentSwordId].price.ToString();
    }

    private void SetMaxPrice()
    {
        // TODO  change the font  and if it will be possible apply some lightning around price

        upgradePrice.text = "MAX";
    }

    public void Interact()
    {
        if (GameManager.instance.swords.Length == GameManager.instance.currentSwordId+1)
        {
            SetMaxPrice();
            return;
        }

        if (GameManager.instance.swords[GameManager.instance.currentSwordId].price <= GameManager.instance.playerCoins)
        {
            GameManager.instance.player.BuySword();
            GameManager.instance.currentSwordId++;

            SetSword();
            SetPrice();

        }
    }
}
