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
        sword.sprite = GameManager.Instance.swords[GameManager.Instance.currentSwordId].skin;
    }

    private void SetPrice()
    {
        upgradePrice.text = GameManager.Instance.swords[GameManager.Instance.currentSwordId].price.ToString();
    }

    private void SetMaxPrice()
    {
        // TODO  change the font  and if it will be possible apply some lightning around price

        upgradePrice.text = "MAX";
    }

    public void Interact(Player player)
    {
        if (GameManager.Instance.swords.Length == GameManager.Instance.currentSwordId+1)
        {
            SetMaxPrice();
            return;
        }

        if (GameManager.Instance.swords[GameManager.Instance.currentSwordId].price <=player.Coins)
        {
            player.BuySword();
            GameManager.Instance.currentSwordId++;

            SetSword();
            SetPrice();

        }
    }
}
