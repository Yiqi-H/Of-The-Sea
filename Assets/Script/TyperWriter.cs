using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogeStory
{
    public string name;
    public string[] story;

}

public class TyperWriter : MonoBehaviour
{

    [SerializeField] Text txt;
    [SerializeField] DialogeStory[] storyStrings;
    [SerializeField] GameObject[] choiceToEnable;
    [SerializeField] GameObject[] timeLine;

    [SerializeField] GameObject TextPrinterObj;
    [SerializeField] GameObject Fade;
    [SerializeField] GameObject ActItem;

    [SerializeField] float textTimer;

    public bool isEnd;
    void OnEnable()
    {
        isEnd = false;
        timeLine[0].SetActive(true);
        StartCoroutine(EnableChoice(0, 30, true)); 

    }

    IEnumerator PrintText(string[] storyText, GameObject qOoption, int timelineIndex, float startWaitTime = 0f)
    {

        txt.text = " ";
        yield return new WaitForSeconds(startWaitTime);
         TextPrinterObj.SetActive(true);
        for (int i = 0; i < storyText.Length; i++)
        {
            txt.text = " ";

            foreach (char c in storyText[i])
            {
                txt.text += c;
                yield return new WaitForSeconds(textTimer);
            }
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(0.5f);

        TextPrinterObj.SetActive(false);
        StartCoroutine(AfterStory(qOoption,timelineIndex));
    }

    
    public void SelectChoice(string ChoiceType)
    {
        for (int i = 0; i < choiceToEnable.Length; i++)
        {
            choiceToEnable[i].SetActive(false);
        }


        switch (ChoiceType)
        {
            case "A":
                //1st option .. 
                 isEnd = true;
                 DisableAllTimeLine();
                 timeLine[5].SetActive(true);
                
                 StartCoroutine(PrintText(storyStrings[2].story, choiceToEnable[1],3,1));
                
                break;
            
            case "B":
                //2nd option? 
                
                DisableAllTimeLine();
                timeLine[2].SetActive(true);
               
                StartCoroutine(PrintText(storyStrings[0].story, choiceToEnable[1],3,4f));
                break;
            case "BA":
                //2nd option? 
                
                DisableAllTimeLine();
                timeLine[7].SetActive(true);
               
                StartCoroutine(PrintText(storyStrings[1].story, choiceToEnable[2],4,4f));
                break;                              
        }
    }


    void DisableAllTimeLine()
    {
        for (int i = 0; i < timeLine.Length; i++)
        {
            timeLine[i].SetActive(false);
        }
    }


    IEnumerator EnableChoice(int index, int waitTime,bool status)
    {
        yield return new WaitForSeconds(waitTime);
        choiceToEnable[index].SetActive(status);
        timeLine[0].SetActive(false);
        timeLine[1].SetActive(true);
    }
    IEnumerator AfterStory(GameObject option, int timeLineIndex)
    {
        yield return new WaitForSeconds(0.5f);

        DisableAllTimeLine();
        if (isEnd)
        {
            StartCoroutine(GameComplete(0));
        }
        else
        {
            timeLine[timeLineIndex].SetActive(true);
            option.SetActive(true);
        }
       
    }
    IEnumerator GameComplete(int wait)
    {
        yield return new WaitForSeconds(wait);

        DisableAllTimeLine();
        timeLine[6].SetActive(true);
        yield return new WaitForSeconds(25); 
        DisableAllTimeLine();
        timeLine[8].SetActive(true);
        yield return new WaitForSeconds(46f);
       
    }
}
 
 
 
