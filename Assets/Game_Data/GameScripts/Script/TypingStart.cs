using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TypingStart : MonoBehaviour {

    Text txt;
    public string story;
    public float textTimer;
    public bool StoryComplete = false;
  

    void Start()
    {
        
        Time.timeScale = 1f;
        txt = GetComponent<Text>();
        txt.text = " ";

        // TODO: add optional delay when to start
        StartCoroutine("PlayText");
    }

    IEnumerator PlayText()
    {
        foreach (char c in story)
        {
            txt.text += c;
            yield return new WaitForSeconds(textTimer);
        }
        StoryComplete = true;
        transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(1);

    }
}

 
