using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StoryScene : MonoBehaviour
{

    public Text[] StoryPargraphs;
    public Text FinalText;
    public int StoryNumber = 0;
    public GameObject StartGameButton;
    string Skip = "space";
    
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Skip))
        {
            // if the current story text is marked as complete
            if (StoryPargraphs[StoryNumber].GetComponent<TypingStart>().StoryComplete)
            {
                // Check if there are more text to show
                if (StoryNumber < StoryPargraphs.Length - 1)
                {
                    // Move to the next text
                    StoryNumber += 1;
                    // deactive all the next paragrapghs
                    foreach (Text a in StoryPargraphs)
                        a.gameObject.SetActive(false);
                    // Activate the next text
                    StoryPargraphs[StoryNumber].gameObject.SetActive(true);
                }

                else
                {
                    // If there are no more text deactivate all and show the final text
                    foreach (Text a in StoryPargraphs)
                        a.gameObject.SetActive(false);
                    FinalText.gameObject.SetActive(true);
                    StartGameButton.SetActive(true);
                }
            }

            else
            {
                StoryPargraphs[StoryNumber].GetComponent<TypingStart>().StopAllCoroutines();
                // Mark the current story as completely shown.
                StoryPargraphs[StoryNumber].GetComponent<TypingStart>().StoryComplete = true;
                StoryPargraphs[StoryNumber].transform.GetChild(0).gameObject.SetActive(true);
                // Display all the text of the current story immediately, without typing effect.
                StoryPargraphs[StoryNumber].GetComponent<Text>().text = StoryPargraphs[StoryNumber].GetComponent<TypingStart>().story;
            }
        }
    }

    public void OnStartBtn() 
    {
        SceneManager.LoadScene("StoryLevel1");
    }
}
