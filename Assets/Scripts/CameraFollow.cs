using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float deadZone_x = 0.4f;
    public float deadZone_y = 0.2f;

    public float smoothTime = 0.3f;

    private Vector3 cameraVelocity = Vector3.zero;

    private void FixedUpdate()
    {
        Vector3 playerPosition = GameManager.instance.player.transform.position;
        Vector3 cameraPosition = transform.position;

        Vector3 offset = playerPosition - cameraPosition;
        offset.z = 0;

        if (Mathf.Abs(offset.x) > deadZone_x || Mathf.Abs(offset.y) > deadZone_y)
        {
            Vector3 targetPosition = transform.position;

            if (Mathf.Abs(offset.x) > deadZone_x)
            {
                targetPosition.x = playerPosition.x - Mathf.Sign(offset.x) * deadZone_x;
            }

            if (Mathf.Abs(offset.y) > deadZone_y)
            {
                targetPosition.y = playerPosition.y - Mathf.Sign(offset.y) * deadZone_y;
            }

            transform.position = Vector3.SmoothDamp(cameraPosition, targetPosition, ref cameraVelocity, smoothTime);
        }

    }
}
