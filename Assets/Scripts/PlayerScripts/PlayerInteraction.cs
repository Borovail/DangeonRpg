using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private IInteractable interactableGameObject;
    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            interactableGameObject?.Interact(_player);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var interactable = collision.gameObject.GetComponent<IInteractable>();
        if (interactable != null)
        {
            interactableGameObject = interactable;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        interactableGameObject = null;
    }

}
