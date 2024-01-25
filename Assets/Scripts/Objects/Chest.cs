using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public Sprite emptyChest;
    public int coinsAmount = 5;

    private bool isLooted = false;
    public void Interact(Player player)
    {
        if (isLooted) return;
        isLooted = true;

       gameObject.GetComponent<SpriteRenderer>().sprite = emptyChest;
       GameManager.Instance.PlayerCoinsChanged(coinsAmount);
    }
}