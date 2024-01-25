using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAble : MonoBehaviour
{
    [Range(0f, 1f)]
    [Tooltip("Resistance to push effect. Range from 0 (no resistance force) to 1 (full resistance).")]
    public float pushResistance = 0f;
    public float pushDuration = 0f;
    

    private float pushTime = 0f;
    private Vector3 targetPosition = Vector3.zero;

    public void Push(Vector2 direction,float pushForce)
    {
        pushTime = 0f;
        targetPosition = (Vector2)transform.position + direction * (pushForce * (1-pushResistance));
        StartCoroutine(PushRoutine());
    }

    private IEnumerator PushRoutine()
    {
        while (pushTime < pushDuration)
        {
            pushTime += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, targetPosition, pushTime/pushDuration);
            yield return null;
        }
    }
}
