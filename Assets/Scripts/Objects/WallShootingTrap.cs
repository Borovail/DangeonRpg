using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallShootingTrap : MonoBehaviour,IAttackable
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float shotCooldown = 1f;

    public int hp=1;

    private void Start()
    {
        InvokeRepeating("Shoot", 0f, shotCooldown);
    }



    private void Shoot()
    {
     Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    }

    public void GetHit(int damage, float pushForce, Vector3 attackerPosition)
    {
        hp -= damage;

        if(hp<=0)
        {
            CancelInvoke("Shoot");
            Destroy(gameObject);
        }
    }
}
