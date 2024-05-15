using UnityEngine;

public class Jellyfish : MonoBehaviour
{
    public float floatSpeed = 1f; // Speed of vertical floating
    public float floatMagnitude = 0.5f; // Magnitude of vertical floating
    public float moveSpeed = 1f; // Speed of horizontal movement
    public float changeDirectionInterval = 2f; // Interval between changing movement direction

    private Vector3 startPos;
    private float timeSinceLastDirectionChange;
    private Vector2 moveDirection;

    void Start()
    {
        startPos = transform.position;
        timeSinceLastDirectionChange = 0f;
        ChangeDirection();
    }

    void Update()
    {
        // Update time since last direction change
        timeSinceLastDirectionChange += Time.deltaTime;

        // Float vertically using sine wave
        float yOffset = floatMagnitude * Mathf.Sin(Time.time * floatSpeed);
        transform.position = new Vector3(transform.position.x, startPos.y + yOffset, transform.position.z);

        // Move horizontally jellyfish
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // Check if it's time to change direction
        if (timeSinceLastDirectionChange >= changeDirectionInterval)
        {
            ChangeDirection();
            timeSinceLastDirectionChange = 0f;
        }
    }

    void ChangeDirection()
    {
        // Generate a random direction vector
        moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
