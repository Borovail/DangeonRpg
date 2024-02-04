using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public Player player;

    public float triggerRange;
    public float chaseRange;
    public float chaseSpeed;
    public float returningSpeed;

    public LayerMask playerLayer;

    public event Action<Collider2D> OnPlayerReached;


    private Vector3 triggerPoint;
    private Vector3 chaseDirection;
    private Vector3 playerPosition;

    private Collider2D monsterCollider;
    private ContactFilter2D contactFilter;
    private Collider2D[] collidersResult;

    private bool isTriggered = false;

    private void Awake()
    {
        triggerPoint = transform.position;
        monsterCollider = GetComponent<Collider2D>();
        contactFilter.SetLayerMask(playerLayer);
        contactFilter.useLayerMask = true;
        collidersResult = new Collider2D[1];

    }

    public void Start()
    {
        GameManager.Instance.OnPlayerDie += () => enabled = false;
    }



    private void FixedUpdate()
    {
        playerPosition = player.transform.position;
        float sqrDistanceToPlayer = (triggerPoint - playerPosition).sqrMagnitude;

        if (sqrDistanceToPlayer < triggerRange * triggerRange)
        {
            isTriggered = true;
        }

        if (isTriggered)
        {
            if (sqrDistanceToPlayer < chaseRange * chaseRange)
                ChasePlayer();
            else
                ReturnToTriggerPoint();
        }
    }

    private void ChasePlayer()
    {
        int collidersCount = Physics2D.OverlapCollider(monsterCollider, contactFilter, collidersResult);
        if (collidersCount != 0)
        {
            OnPlayerReached?.Invoke(collidersResult[0]);
            return;
        }

        chaseDirection = (playerPosition - transform.position).normalized;
        transform.position += chaseSpeed * Time.deltaTime * chaseDirection;

    }

    private void ReturnToTriggerPoint()
    {
        chaseDirection = (triggerPoint - transform.position).normalized;
        transform.position += returningSpeed * Time.deltaTime * chaseDirection;

        if (Vector3.Distance(transform.position, triggerPoint) < 0.1f)
            isTriggered = false;
    }
}
