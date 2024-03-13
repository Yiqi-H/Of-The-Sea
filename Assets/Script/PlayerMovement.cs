using UnityEngine;
using UnityEngine.UI;
using System.Collections;
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
    public GameObject AttackState;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            if (Input.GetKeyDown("space"))
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
                        DolphinStory.SetActive(false);
                        Level1heading.SetActive(true);
                        DialogueActive = false;
                        AttackState.SetActive(true);
                        rb.simulated = true;
                        this.GetComponent<SpriteRenderer>().enabled = false;
                        StartCoroutine(ObjectDisable(Level1heading, 4));


                    }
                }

            }
        }


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
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("here");
        if (collision.gameObject.CompareTag("Dolphin"))
        {
            rb.simulated = false;
            DolphinStory.SetActive(true);
            Dolphin.GetComponent<CapsuleCollider2D>().enabled = false;
            DialogueActive = true;

        }
    }
    

  
}
