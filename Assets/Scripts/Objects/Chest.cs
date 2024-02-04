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
       gameObject.GetComponent<CircleCollider2D>().enabled = false;
       GameManager.Instance.PlayerCoinsChanged(coinsAmount);
        FloatingTextManager.Instance.Show(new FloatingTextSettings($"+{coinsAmount} coins", 2f, 4, Color.yellow, Vector3.up/2, transform.position, FloatingTextType.WorldSpaceFloatingText));
    }
}