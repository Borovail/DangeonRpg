using System.Collections;
using UnityEngine;

public class PushAble : MonoBehaviour
{
    [Range(0f, 1f)]
    [Tooltip("Resistance to push effect. Range from 0 (no resistance force) to 1 (full resistance).")]
    public float pushResistance = 0f;
    public float pushDuration = 0f;
    public LayerMask collisionLayer; 

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
      
    }

    public void Push(Vector2 direction, float pushForce)
    {
        float pushDistance = pushForce * (1 - pushResistance); 
        Vector2 end = (Vector2)transform.position + direction * pushDistance;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, pushDistance, collisionLayer);

        if (hit.collider != null)
        {
            end = hit.point;
        }

        StartCoroutine(PushRoutine(end));
    }

    private IEnumerator PushRoutine(Vector2 targetPosition)
    {
        float pushTime = 0f;

        while (pushTime < pushDuration)
        {
            pushTime += Time.deltaTime;
            float fraction = pushTime / pushDuration;
            Vector2 newPosition = Vector2.Lerp(transform.position, targetPosition, fraction);
            rb.MovePosition(newPosition);
            yield return null;
        }
    }
}
