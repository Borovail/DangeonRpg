using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IAttackable
{

    public int Health;
    public int Coins;
    public int Armor;

 

    public bool hasKey = false;

    public event Action OnAttackEnd;


    public event Action OnPlayerBuyNewSword;

    private bool isAttacking = false;

    private SpriteRenderer playerRenderer;
    private Sword sword;

    private void Awake()
    {
        playerRenderer = GetComponent<SpriteRenderer>();
        sword = GetComponentInChildren<Sword>();

        GameManager.Instance.OnPlayerCoinsChanged += (amount) => Coins += amount;
        GameManager.Instance.OnHealthChanged += (amount, maxHeath) =>
        {
            if (Health + amount <= maxHeath)
                Health += amount;
            else
                FloatingTextManager.Instance.Show(new FloatingTextSettings("Health is full", 2f, 16, Color.green, Vector3.up*50, transform.position, FloatingTextType.UIRelativeFloatingText));
        };

        GameManager.Instance.OnArmorChanged += (amount, maxArmor) =>
        {
            if(Armor+amount<maxArmor)
            Armor += amount;
            else
                FloatingTextManager.Instance.Show(new FloatingTextSettings("Armor is full", 2f, 16, Color.cyan, Vector3.up*50, transform.position, FloatingTextType.UIRelativeFloatingText));
        };
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
        {
            sword.Attack();
            isAttacking = true;
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
            GameManager.Instance.PlayerArmorChanged(-1);
            StartCoroutine(GetHitAnimation(Color.cyan));
            return;
        }

        if (Health - damage <= 0)
        {
            Health = 0;
        GameManager.Instance.PlayerDie();
            Destroy(gameObject);
            return;
        }

       GameManager.Instance.PlayerHealthChanged(-damage);

        StartCoroutine(GetHitAnimation(Color.red));
    }

    private IEnumerator GetHitAnimation(Color color)
    {
        playerRenderer.color = color;
        yield return new WaitForSeconds(0.5f);
        playerRenderer.color = Color.white;
    }
}
