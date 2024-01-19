using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    //переделать всё под триггер зоны




    public bool isClosed = true;

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

    public void Interact()
    {
        if (isClosed)
            OpenDoor();
        else
            CloseDoor();
    }


    private void OpenDoor()
    {
        _doorCollider.enabled = false;
        _doorRenderer.sprite = openDoor;

    }

    private void CloseDoor()
    {
        _doorCollider.enabled = false;
        _doorRenderer.sprite = closedDoor;

    }
}
