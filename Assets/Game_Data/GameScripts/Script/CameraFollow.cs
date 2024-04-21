using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Player's transform
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float minX = -5f; // Minimum X position
    public float maxX = 5f;  // Maximum X position
    public float minY = -5f; // Minimum Y position
    public float maxY = 5f;  // Maximum Y position

    void FixedUpdate()
    {
        if (target != null)
        {
            // Calculate target position with offset
            Vector3 desiredPosition = target.position + offset;

            // Clamp target position within limits
            float clampedX = Mathf.Clamp(desiredPosition.x, minX, maxX);
            float clampedY = Mathf.Clamp(desiredPosition.y, minY, maxY);
            desiredPosition = new Vector3(clampedX, clampedY, desiredPosition.z);

            // Smoothly move the camera towards the target position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
