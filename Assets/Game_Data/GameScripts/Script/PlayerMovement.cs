using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 200f;
    public float minX = -5f; // Minimum X position
    public float maxX = 5f;  // Maximum X position
    public float minY = -5f; // Minimum Y position
    public float maxY = 5f;  // Maximum Y position

    private Rigidbody2D rb;
    public GameObject PauseScreen,GameOverScreen;

    public GameObject Level1heading;
   
    
    public GameObject AttackState,ShootState;
    public bool CanShoot = false;

    public float shootCooldown = 1f; // Cooldown time between shots

    private float timeSinceLastShot;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform PlayerStartPosition;
    public GameObject BG;

    public Color startColor; // Initial color
    public Color endColor;
    public Color OriginalColor;
    public float fadeDuration = 1f; // Duration of the fade

    public GameObject LevelEnemiesObject;
    public GameObject InventoryPanel;
    public int HitPoints = 3;
    public GameObject[] HitpointsHeart;
   
    public bool canMove = true;
    public static PlayerMovement Instance;
    public static class MyEnumClass
    {
        public const string
            Skip = "space",
            Shoot = "j";
    }

    string skip = MyEnumClass.Skip;
    string shoot = MyEnumClass.Shoot;
    void Start()
    {
        PlayerPrefs.DeleteKey("Level1Key");
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        timeSinceLastShot = shootCooldown;
        for (int i = 0; i < HitPoints; i++)
        {
            HitpointsHeart[i].SetActive(true);

        }


        //

        this.transform.position = PlayerStartPosition.position;
        if(Level1heading)
        StartCoroutine(ObjectDisable(Level1heading, 4));
        StartCoroutine(FadeColorOut());
        CanShoot = true;
       
        LevelEnemiesObject.SetActive(true);
        InventoryPanel.SetActive(true);
        //

        Time.timeScale = 1;
    }

    void Update()
    {
            rb.isKinematic = false;
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
            timeSinceLastShot += Time.deltaTime;

            if (Input.GetKeyDown(shoot) && CanShoot && timeSinceLastShot >= shootCooldown)
            {
                
                timeSinceLastShot = 0f; // Reset the cooldown timer
                AttackState.SetActive(false);
                ShootState.SetActive(true);
                Invoke("Shoot", 0.5f);

            }

                // Rotate the fish based on movement direction
                //if (movement != Vector2.zero)
                //{
                //    float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
                //    rb.rotation = angle;
                //}

    }

    void Shoot()
    {
        // Instantiate the bullet prefab at the fire point position and rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
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
    public void Restart()
    {
        PlayerPrefs.DeleteKey("Level1Key");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    public IEnumerator ObjectDisable(GameObject Obj,float wait)
    {
        yield return new WaitForSeconds(wait);
        Obj.SetActive(false);

    }

    void ApplyHitEffect(Collider2D other)
    {
        // Calculate the direction of the collision force
        Vector2 forceDirection = ((Vector2)transform.position - (Vector2)other.transform.position).normalized;

        // Calculate a random respawn position opposite to the direction of force
        float respawnDistance = 2f; // Adjust this value as needed
        Vector2 randomRespawnDirection = Quaternion.Euler(0, 0, Random.Range(90, 270)) * forceDirection;

        // Calculate the respawn position
        Vector2 respawnPosition = (Vector2)transform.position + randomRespawnDirection * respawnDistance;

        // Smoothly transition to the respawn position
        StartCoroutine(MoveToRespawnPosition(respawnPosition));
    }

    IEnumerator MoveToRespawnPosition(Vector2 targetPosition)
    {
        float duration = 0.1f; // Adjust this value to change the duration of the transition
        float elapsedTime = 0f;
        Vector2 startPosition = transform.position;

        while (elapsedTime < duration)
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition; // Ensure the final position is accurate
    }
    IEnumerator EnableMovementAfterDelay()
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(1f);

        // Allow movement again
        canMove = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.CompareTag("Enemy") && canMove)
        {

            HitpointsHeart[HitPoints - 1].GetComponent<Animator>().enabled = true;
            FindObjectOfType<AudioManager>().Play("Collision");
            HitPoints--;
            if (HitPoints <= 0)
            {
                FindObjectOfType<AudioManager>().Play("PlayerDeath");
                HitPoints = 0;
                Debug.Log("Game_Over");
                Invoke("Gameover", 1.3f);

            }
            // Prevent further movement temporarily
            canMove = false;

            // Apply hit effect immediately
            ApplyHitEffect(collision);

            // Start a coroutine to enable movement after a delay
            StartCoroutine(EnableMovementAfterDelay());
        }
        else if (collision.gameObject.CompareTag("Door"))
        {
            if (PlayerPrefs.GetInt("Level1Key") == 1)
            {
                Time.timeScale = 0;

                FindObjectOfType<AudioManager>().Play("Door");

                SceneManager.LoadScene("Level2");
            }
        }
        else
        {
            ICollectible collectible = collision.GetComponent<ICollectible>();
            if (collectible != null)
            {
                collectible.Collect();
                FindObjectOfType<AudioManager>().Play("Pickup");
            }
        }

    }

    void Gameover()
    {
        GameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }
    public void ResetState()
    {
        ShootState.SetActive(false);
        AttackState.SetActive(true);
    }
    IEnumerator FadeColor()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            // Calculate the current color based on the elapsed time and lerp between startColor and endColor
            Color currentColor = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
            BG.GetComponent<SpriteRenderer>().color = currentColor;

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure the color is set to the endColor when the fade completes
        BG.GetComponent<SpriteRenderer>().color = endColor;
    }

    IEnumerator FadeColorOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            // Calculate the current color based on the elapsed time and lerp between startColor and endColor
            Color currentColor = Color.Lerp(endColor, OriginalColor, elapsedTime / fadeDuration);
            BG.GetComponent<SpriteRenderer>().color = currentColor;

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure the color is set to the endColor when the fade completes
        BG.GetComponent<SpriteRenderer>().color = OriginalColor;
    }

    public void OnMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");

    }

}
