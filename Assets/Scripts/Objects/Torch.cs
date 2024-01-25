using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Torch : MonoBehaviour, IInteractable
{
    private bool isBurning = false;

    public AnimationClip burningAnimation;
    public AnimationClip firelessAnimation;

    private Animator _torchController;

    private void Awake()
    {
        _torchController = GetComponent<Animator>();
    }

    public void Interact(Player player)
    {
        if(isBurning)
        {
            _torchController.Play(burningAnimation.name, -1, 0f);
        }
        else
        {
            _torchController.Play(firelessAnimation.name, -1, 0f);
        }

        isBurning = !isBurning;

    }
}
