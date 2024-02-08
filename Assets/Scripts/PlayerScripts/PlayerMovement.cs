using Assets.Scripts.Class;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 2f;
    private float originalXScale;
    private Rigidbody2D rb;
    public float stepInterval = 0.3f;
    private float stepTimer;

    private void Awake()
    {
        originalXScale = transform.localScale.x;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        int x = (int)Input.GetAxisRaw("Horizontal");
        int y = (int)Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(x, y, 0);

        if (x != 0)
        {
            transform.localScale = new Vector3(originalXScale * Mathf.Sign(x), transform.localScale.y, transform.localScale.z);
        }

        rb.velocity = speed * direction.normalized;

        if (direction != Vector3.zero)
        {
            if (stepTimer <= 0f)
            {
                AudioManager.Instance.PlaySound(SoundType.PlayerStep, 1);
                stepTimer = stepInterval;
            }
            else
                stepTimer -= Time.deltaTime;
        }
        else
            stepTimer = 0f;





    }


}
