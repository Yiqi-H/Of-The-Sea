using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryLevelPlayer : MonoBehaviour
{

    public float speed = 5f;
    public float rotationSpeed = 200f;
    public float minX = -5f; // Minimum X position
    public float maxX = 5f;  // Maximum X position
    public float minY = -5f; // Minimum Y position
    public float maxY = 5f;  // Maximum Y position

    // Array of Text components that hold the story to be displayed
    public Text[] StoryPargraphs;
    // Index of the current story
    public int StoryNumber = 0;
    public GameObject Level1heading;
    public GameObject DolphinStory;
    public GameObject Dolphin;
    public bool DialogueActive;
    public Transform PlayerStartPosition;
    public GameObject BG;
    private Rigidbody2D rb;
    public Color startColor; // Initial color
    public Color endColor;
    public Color OriginalColor;
    public float fadeDuration = 1f; // Duration of the fade
    public GameObject PauseScreen;
    public GameObject AttackState;
    public static class MyEnumClass
    {
        public const string
            Skip = "space";
    }

    string skip = MyEnumClass.Skip;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if there is no active dialogue in the game
        if (!DialogueActive)
        {
            rb.isKinematic = false;
            // Get input for movement and rotation
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Move the fish
            Vector2 movement = new Vector2(horizontalInput, verticalInput);
            rb.velocity = movement * speed;

            // Clamp the position, limit the player x and y position to stay within predefined boundaries
            float clampedX = Mathf.Clamp(rb.position.x, minX, maxX);
            float clampedY = Mathf.Clamp(rb.position.y, minY, maxY);
            rb.position = new Vector2(clampedX, clampedY);
            
        }

        if (DialogueActive)
        {
            if (Input.GetKeyDown(skip))
            {
                if (StoryPargraphs[StoryNumber].GetComponent<TypingStart>().StoryComplete)
                {
                    // Check if the current story text is fully displayed
                    if (StoryNumber < StoryPargraphs.Length - 1)
                    {
                        // Check if there are more texts left
                        StoryNumber += 1;
                        // Disable all texts
                        foreach (Text a in StoryPargraphs)
                            a.gameObject.SetActive(false);
                        // Enable the next text
                        StoryPargraphs[StoryNumber].gameObject.SetActive(true);
                    }

                    else
                    {
                        // Check if the DolphinStory is currently active
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
                        SceneManager.LoadScene("Level1");
                        //StartCoroutine(ObjectDisable(Level1heading, 4));
                        StartCoroutine(FadeColorOut());
                        //SceneManager.LoadScene("Level1");
                        //BG.GetComponent<SpriteRenderer>().color = new Color(50, 50, 50, 255);
                    }
                }

                else
                {
                    // If the story is not complete mark it as complete and update the text
                    StoryPargraphs[StoryNumber].GetComponent<TypingStart>().StopAllCoroutines();
                    StoryPargraphs[StoryNumber].GetComponent<TypingStart>().StoryComplete = true;
                    StoryPargraphs[StoryNumber].transform.GetChild(0).gameObject.SetActive(true);
                    // Update the text of the current story paragraph to show the complete story. 
                    StoryPargraphs[StoryNumber].GetComponent<Text>().text = StoryPargraphs[StoryNumber].GetComponent<TypingStart>().story;
                }
            }
        }
    }

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
    public void OnMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");

    }
    IEnumerator FadeColor()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            // Calculate the current color based on the elapsed time and lerp between startColor and endColor
            Color currentColor = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);

            BG.GetComponent<SpriteRenderer>().color = new Color(currentColor.r, currentColor.g, currentColor.b, 255);
            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure the color is set to the endColor when the fade completes
        //BG.GetComponent<SpriteRenderer>().color = endColor;
        BG.GetComponent<SpriteRenderer>().color = new Color(endColor.r, endColor.g, endColor.b, 255);
    }
    IEnumerator FadeColorOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            // Calculate the current color based on the elapsed time and lerp between startColor and endColor
            Color currentColor = Color.Lerp(endColor, OriginalColor, elapsedTime / fadeDuration);
            BG.GetComponent<SpriteRenderer>().color = new Color(currentColor.r, currentColor.g, currentColor.b, 255);

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure the color is set to the endColor when the fade completes
        BG.GetComponent<SpriteRenderer>().color = new Color(OriginalColor.r, OriginalColor.g, OriginalColor.b, 255);
    }
}
