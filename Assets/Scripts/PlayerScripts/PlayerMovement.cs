using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   
        public float speed = 2f;
        private float originalXScale;

        private void Awake()
        {
            originalXScale = transform.localScale.x;
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

            transform.position += speed * Time.deltaTime * direction;
        }
    

}
