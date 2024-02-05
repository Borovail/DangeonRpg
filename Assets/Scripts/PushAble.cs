//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PushAble : MonoBehaviour
//{
//    [Range(0f, 1f)]
//    [Tooltip("Resistance to push effect. Range from 0 (no resistance force) to 1 (full resistance).")]
//    public float pushResistance = 0f;
//    public float pushDuration = 0f;

//    private float pushTime = 0f;
//    private Vector3 targetPosition = Vector3.zero;

//    private Rigidbody2D rb;

//    private void Awake()
//    {
//        rb = GetComponent<Rigidbody2D>();
//    }

//    public void Push(Vector2 direction, float pushForce)
//    {
//        pushTime = 0f;
//        targetPosition = (Vector2)transform.position + direction * (pushForce * (1 - pushResistance));
//        StartCoroutine(PushRoutine(direction));
//    }

//    private IEnumerator PushRoutine(Vector2 direction)
//    {
//        while (pushTime < pushDuration)
//        {
//            Vector2 rayStart = rb.position + direction * 0.15f;
//            RaycastHit2D hit = Physics2D.Raycast(rayStart, direction, 0.1f);
//            Debug.Log(hit.collider);
//            if (hit.collider != null) break;

//            pushTime += Time.deltaTime;
//            Vector2 newPosition = Vector2.Lerp(rb.position, targetPosition, pushTime / pushDuration);
//            rb.MovePosition(newPosition);
//            yield return null;
//        }
//    }


//}




using System.Collections;
using UnityEngine;

public class PushAble : MonoBehaviour
{
    [Range(0f, 1f)]
    [Tooltip("Resistance to push effect. Range from 0 (no resistance force) to 1 (full resistance).")]
    public float pushResistance = 0f;
    public float pushDuration = 0f;
    public LayerMask collisionLayer; // Укажите слой препятствий, который нужно учитывать при отталкивании

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Push(Vector2 direction, float pushForce)
    {
        float pushDistance = pushForce * (1 - pushResistance); // Расчёт предполагаемого расстояния отталкивания
        Vector2 end = (Vector2)transform.position + direction * pushDistance;

        // Пускаем луч для определения фактической конечной точки отталкивания
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, pushDistance, collisionLayer);

        if (hit.collider != null)
        {
            // Если есть препятствие, корректируем конечную точку до места столкновения
            end = hit.point;
        }

        // Запускаем корутину для плавного перемещения объекта
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
