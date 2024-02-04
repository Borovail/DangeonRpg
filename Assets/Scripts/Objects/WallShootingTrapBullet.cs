using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallShootingTrapBullet : MonoBehaviour
{
    public int damage = 1;
    public float pushForce = 1;

    public float speed = 1f;

    private void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        IAttackable attackable = collision.gameObject.GetComponent<IAttackable>();

        if (attackable != null)
        {
            attackable.GetHit(damage, pushForce, transform.position);
        }
        Destroy(gameObject);
    }
}
