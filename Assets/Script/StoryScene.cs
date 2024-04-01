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

                    foreach (Text a in StoryPargraphs)
                        a.gameObject.SetActive(false);
                    FinalText.gameObject.SetActive(true);
                    StartGameButton.SetActive(true);



                }
            }

            else
            {
                StoryPargraphs[StoryNumber].GetComponent<TypingStart>().StopAllCoroutines();
                StoryPargraphs[StoryNumber].GetComponent<TypingStart>().StoryComplete = true;
                StoryPargraphs[StoryNumber].transform.GetChild(0).gameObject.SetActive(true);
                StoryPargraphs[StoryNumber].GetComponent<Text>().text = StoryPargraphs[StoryNumber].GetComponent<TypingStart>().story;

            }

        }

    }

    public void OnStartBtn() {

        SceneManager.LoadScene("Level1");
        

    }
}
