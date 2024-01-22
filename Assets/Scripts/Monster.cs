using UnityEngine;

public class Monster : MonoBehaviour, IAttackable
{
    public float triggerRange;
    public float chaseRange;
    public float chaseSpeed;
    public float returningSpeed;

    public LayerMask playerLayer;

    private Vector3 triggerPoint;
    private Vector3 chaseDirection;
    private Vector3 playerPosition;


    private bool isTriggered = false;

    private Collider2D monsterCollider;
    private ContactFilter2D contactFilter;
    private Collider2D[] collidersResult;
    private void Awake()
    {
        triggerPoint = transform.position;

        monsterCollider = GetComponent<Collider2D>();
        contactFilter.SetLayerMask(playerLayer);
        contactFilter.useLayerMask = true;
        collidersResult = new Collider2D[1];
    }

    private void FixedUpdate()
    {
        playerPosition = GameManager.instance.player.transform.position;
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
       int  collidersCount= Physics2D.OverlapCollider(monsterCollider, contactFilter, collidersResult);
        if (collidersCount != 0) return;

           chaseDirection = (playerPosition - transform.position).normalized;
            transform.position += chaseDirection * chaseSpeed * Time.deltaTime;

    }

    private void ReturnToTriggerPoint()
    {
        chaseDirection = (triggerPoint - transform.position).normalized;
        transform.position += chaseDirection * returningSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, triggerPoint) < 0.1f)
            isTriggered = false;

    }


   public void GetHit(float damage,float pushForce)
    {
        Debug.Log(gameObject.name + " get hit with: " + damage+ " damage");
        GetComponent<PushAble>().Push(Vector2.left, pushForce);
    }
}
