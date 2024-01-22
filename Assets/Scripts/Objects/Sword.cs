using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public AnimationClip attackAnimation;
    public float pushForce = 1f;

    private Animator animator;
    private Collider2D _collider;
    private IAttackable attackableObject;

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
        _collider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        GameManager.instance.player.OnAttackEnd += HandleAttackEnd;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _collider.isTrigger = true;

        attackableObject = collision.gameObject.GetComponent<IAttackable>();
        if (attackableObject == null) return;

        attackableObject.GetHit(GameManager.instance.swords[GameManager.instance.currentSwordId].damage, pushForce);
    }

    public void Attack()
    {
        animator.Play(attackAnimation.name, -1, 0f);
    }

    private void HandleAttackEnd()
    {
        // Коллайдер остаётся выключенным после удара до начала следующей атаки
        attackableObject = null;
        _collider.isTrigger = false;


    }
}
