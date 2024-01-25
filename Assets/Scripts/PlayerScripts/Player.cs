using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IAttackable
{

    public int Health;
    public int Coins;
    public int Armor;

    public event Action OnAttackEnd;


    public event Action<int> OnHealthChanged;
    public event Action<int> OnArmorChanged;
    public event Action OnPlayerBuyNewSword;
    public event Action OnPlayerDie;

    private bool isAttacking = false;
    private SpriteRenderer playerRenderer;
    private Sword sword;

    private void Awake()
    {
        playerRenderer = GetComponent<SpriteRenderer>();
        sword = GetComponentInChildren<Sword>();
        GameManager.Instance.OnPlayerCoinsChanged += (amount) => Coins += amount;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
        {
            sword.Attack();
            isAttacking = true;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            FloatingTextManager.Instance.Show(new FloatingTextSettings("Pidor Up", 5f, 12, Color.red, Vector3.up, transform.position, FloatingTextType.UIRelativeFloatingText));
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            FloatingTextManager.Instance.Show(new FloatingTextSettings("Pidor Down", 5f, 12, Color.red, Vector3.down, transform.position, FloatingTextType.WorldSpaceFloatingText));
        }
    }


    public void BuySword()
    {
        GameManager.Instance.PlayerCoinsChanged(-GameManager.Instance.swords[GameManager.Instance.currentSwordId].price);
        OnPlayerBuyNewSword?.Invoke();
    }


    public void OnAttackAnimationEnd()
    {
        isAttacking = false;
        OnAttackEnd?.Invoke();
    }

    public void GetHit(int damage, float pushForce, Vector3 attackerPosition)
    {
        GetComponent<PushAble>().Push((transform.position-attackerPosition).normalized, pushForce);

        if (Armor > 0)
        {
            Armor -= 1;
            OnArmorChanged?.Invoke(Armor);
            StartCoroutine(GetHitAnimation(Color.cyan));
            return;
        }

        if (Health - damage <= 0)
        {
            Health = 0;
            OnPlayerDie?.Invoke();
            Destroy(gameObject);
            return;
        }

        Health -= damage;
        OnHealthChanged?.Invoke(Health);

        StartCoroutine(GetHitAnimation(Color.red));
    }

    private IEnumerator GetHitAnimation(Color color)
    {
        playerRenderer.color = color;
        yield return new WaitForSeconds(0.5f);
        playerRenderer.color = Color.white;
    }
}
