using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public AnimationClip attackAnimation;
    public float pushForce = 1f;

    private Animator _animator;
    private Collider2D _collider;
    private SpriteRenderer _swordRenderer;
    private IAttackable _attackableObject;


    private void Start()
    {
        _animator = GetComponentInParent<Animator>();
        _collider = GetComponent<Collider2D>();
        _swordRenderer = GetComponent<SpriteRenderer>();

        var player = GetComponentInParent<Player>();
        player.OnAttackEnd += HandleAttackEnd;
        player.OnPlayerBuyNewSword += HandleSkinChange;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _collider.isTrigger = true;

        _attackableObject = collision.gameObject.GetComponent<IAttackable>();
        if (_attackableObject == null) return;

        _attackableObject.GetHit(GameManager.Instance.swords[GameManager.Instance.currentSwordId].damage, GameManager.Instance.swords[GameManager.Instance.currentSwordId].pushForce, transform.position);
    }

    public void Attack()
    {
        _animator.Play(attackAnimation.name, -1, 0f);
    }

    private void HandleAttackEnd()
    {
        // Коллайдер остаётся выключенным после удара до начала следующей атаки
        _attackableObject = null;
        _collider.isTrigger = false;
    }

    private void HandleSkinChange()
    {
        _swordRenderer.sprite = GameManager.Instance.swords[GameManager.Instance.currentSwordId].skin;
    }
}
