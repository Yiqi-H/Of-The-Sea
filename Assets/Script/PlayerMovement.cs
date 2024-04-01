using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
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
    public Text[] StoryPargraphs;
    public int StoryNumber = 0;
    public GameObject Level1heading;
    public GameObject DolphinStory;
    public GameObject Dolphin;
    public bool DialogueActive;
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
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        timeSinceLastShot = shootCooldown;

    }

    void Update()
    {

        if (!DialogueActive)
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
        else
        {
            rb.isKinematic = true;
            if (Input.GetKeyDown(skip))
            {
                if (StoryPargraphs[StoryNumber].GetComponent<TypingStart>().StoryComplete)
                {
                    if (StoryNumber < StoryPargraphs.Length - 1)
                    {
                        StoryNumber += 1;
                        foreach (Text a in StoryPargraphs)
                            a.gameObject.SetActive(false);

                        StoryPargraphs[StoryNumber].gameObject.SetActive(true);

                    }

                    else
                    {
                        if (DolphinStory.activeInHierarchy)
                        {

                            Dolphin.SetActive(false);

                        }
                        this.transform.position = PlayerStartPosition.position;
                        DolphinStory.SetActive(false);
                        Level1heading.SetActive(true);
                        DialogueActive = false;
                        AttackState.SetActive(true);
                        rb.simulated = true;
                        this.GetComponent<SpriteRenderer>().enabled = false;
                        StartCoroutine(ObjectDisable(Level1heading, 4));
                        StartCoroutine(FadeColorOut());
                        CanShoot = true;
                        //BG.GetComponent<SpriteRenderer>().color = new Color(50, 50, 50, 255);
                        LevelEnemiesObject.SetActive(true);
                    }
                }

                else
                {
                    StoryPargraphs[StoryNumber].GetComponent<TypingStart>().StopAllCoroutines();
                    StoryPargraphs[StoryNumber].GetComponent<TypingStart>().StoryComplete = true;
                    StoryPargraphs[StoryNumber].transform.GetChild(0).gameObject.SetActive(true);
                    StoryPargraphs[StoryNumber].GetComponent <Text>().text= StoryPargraphs[StoryNumber].GetComponent<TypingStart>().story;
                    
                }

            }
        }


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

    public IEnumerator ObjectDisable(GameObject Obj,float wait)
    {
        yield return new WaitForSeconds(wait);
        Obj.SetActive(false);

    }
    
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("here");
    //    if (collision.gameObject.CompareTag("Dolphin"))
    //    {
    //        rb.simulated = false;
    //        DolphinStory.SetActive(true);
    //        Dolphin.GetComponent<CapsuleCollider2D>().enabled = false;
    //        DialogueActive = true;

    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Dolphin"))
        {
            StartCoroutine(FadeColor());
            //BG.GetComponent<SpriteRenderer>().color = new Color(50, 50, 50, 255);
            rb.simulated = false;
            DolphinStory.SetActive(true);
            DialogueActive = true;
            Debug.Log("here");
        }
        
        ICollectible collectible = collision.GetComponent<ICollectible>();
        if(collectible != null)
        {
            collectible.Collect();
        }

    


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
