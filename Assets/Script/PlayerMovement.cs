using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 200f;
    public float minX = -5f; // Minimum X position
    public float maxX = 5f;  // Maximum X position
    public float minY = -5f; // Minimum Y position
    public float maxY = 5f;  // Maximum Y position

    private Rigidbody2D rb;
    public GameObject PauseScreen;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input for movement and rotation
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Move the fish
        Vector2 movement = new Vector2(horizontalInput, verticalInput);
        rb.velocity = movement * speed;

        // Clamp the position
        float clampedX = Mathf.Clamp(rb.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(rb.position.y, minY, maxY);
        rb.position = new Vector2(clampedX, clampedY);

        // Rotate the fish based on movement direction
        //if (movement != Vector2.zero)
        //{
        //    float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
        //    rb.rotation = angle;
        //}
    }

    public void PauseGame()
    {
        PauseScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void UnPauseGame()
    {
        PauseScreen.SetActive(false);
        Time.timeScale = 1;



    }
}
