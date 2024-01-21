using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public AnimationClip attackAnimation;

    private Animator animator;
    private Collider2D _collider;

    private IAttackable attackableObject;


    private void Awake()
    {
       animator =  gameObject.GetComponentInParent<Animator>();
        _collider =  gameObject.GetComponent<Collider2D>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        attackableObject = collision.gameObject.GetComponent<IAttackable>();

        if (attackableObject == null) return;


        attackableObject.GetHit(GameManager.instance.swords[GameManager.instance.currentSwordId].damage);
        _collider.enabled = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        attackableObject = null;
    }

    public void Attack()
    {
        animator.Play(attackAnimation.name, -1, 0f);
    }

 
}
