using System;
using UnityEngine;

public class Monster : MonoBehaviour, IAttackable
{
    public int CurrentHp;
    public int maxHp;

    public int attackDamage =1;
    public float attackPushForce = 0.3f;

    public float attackCooldown =1f;

    public event Action<int,int> OnHealthChanged;

    private void Start()
    {
        var ai = GetComponent<MonsterAI>();
        ai.OnPlayerReached += Attack;
    }

    private void Attack(Collider2D player)
    {

        if (attackCooldown <= 0)
        {
           player.gameObject.GetComponent<IAttackable>().GetHit(attackDamage,attackPushForce,transform.position);

            attackCooldown = 0.5f;
        }
        else
        {
            attackCooldown -= Time.deltaTime;
        }
    }


    public void GetHit(int damage,float pushForce, Vector3 attackerPosition)
    {
        if(CurrentHp-damage<=0)
        {
            Destroy(gameObject); 
            return;
        }

        CurrentHp -= damage;
        OnHealthChanged?.Invoke(CurrentHp, maxHp); 
        GetComponent<PushAble>().Push((transform.position-attackerPosition).normalized, pushForce);
    }


    private void OnDestroy()
    {
        var ai = GetComponent<MonsterAI>();
        if (ai != null)
        {
            ai.OnPlayerReached -= Attack;
        }
    }
}
