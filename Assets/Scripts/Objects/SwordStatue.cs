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
        SetSword(1);
        SetPrice(1);
    }

    private void SetSword(int index)
    {
        sword.sprite = GameManager.Instance.swords[index].skin;
    }

    private void SetPrice(int index)
    {
        upgradePrice.text = GameManager.Instance.swords[ index].price.ToString();
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

        if (GameManager.Instance.swords[GameManager.Instance.currentSwordId+1].price <=player.Coins)
        {

            GameManager.Instance.currentSwordId++;
            player.BuySword();

            if (GameManager.Instance.swords.Length == GameManager.Instance.currentSwordId + 1)
            {
                SetMaxPrice();
                return;
            }

            SetSword(GameManager.Instance.currentSwordId+1);
            SetPrice(GameManager.Instance.currentSwordId + 1);

        }
    }
}
