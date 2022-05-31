using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform crosshair;
    public Transform player;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    

    void FixedUpdate()
    {
        Vector3 playerPosition = player.position + offset;
        Vector3 desiredPosition = (crosshair.position + playerPosition) / 2f;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
