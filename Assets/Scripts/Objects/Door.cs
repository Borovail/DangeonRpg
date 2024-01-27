using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public bool isClosed = true;
    public bool isKeyRequired = false;

    public Sprite closedDoor;
    public Sprite openDoor;

    private BoxCollider2D _doorCollider;
    private SpriteRenderer _doorRenderer;


    private void Awake()
    {
        _doorCollider = GetComponent<BoxCollider2D>();
        _doorRenderer = GetComponent<SpriteRenderer>();

        if (!isClosed)
        {
            _doorCollider.enabled = false;
        }
           
    }

    public void Interact(Player player)
    {
        if (isClosed)
        {
            if(!isKeyRequired)
            OpenDoor();
            else
            {
                if (player.hasKey)
                    OpenDoor();
                else
                    FloatingTextManager.Instance.Show(new FloatingTextSettings("You need a key to open this door", 2f, 20, Color.red, Vector3.up*60, transform.position, FloatingTextType.UIRelativeFloatingText));
            }
        }
        else
            CloseDoor();
    }

    private void OpenDoor()
    {
        _doorCollider.enabled = false;
        isClosed = false;
        _doorRenderer.sprite = openDoor;

    }

    private void CloseDoor()
    {
        _doorCollider.enabled = true;
        isClosed = true;
        _doorRenderer.sprite = closedDoor;

    }
}
