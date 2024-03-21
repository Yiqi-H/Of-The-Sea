using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StoryScene : MonoBehaviour
{

    public Text[] StoryPargraphs;
    public int StoryNumber = 0;
    public GameObject StartGameButton;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
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
                    StartGameButton.SetActive(true);



                }
            }

        }

    }

    public void OnStartBtn() {

        SceneManager.LoadScene("Level1");
        

    }
}
