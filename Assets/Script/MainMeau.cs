using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMeau : MonoBehaviour
{
    string SceneName;
    public GameObject Loading;
    public Text PercentageText;
    public int i = 0;
    //public void PlayGame()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //}

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
    
    //public void AboutGame()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    //}

    public void AboutGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    
    // Start is called before the first frame update
    public void LoadScene(string scenename)
    {
        SceneName = scenename;
        //Loading.GetComponent<Loading>().LoadLevel(SceneName);
        StartCoroutine(PercentageCalculator());
        Invoke("LoadSceneInvoke", 4);
        //SceneManager.LoadScene(scenename);

    }

    public void LoadSceneInvoke()
    {

        SceneManager.LoadScene(SceneName);

    }
    public IEnumerator PercentageCalculator()
    {
        
        yield return new WaitForSeconds(0.022f);
        i += 1;
        if (i >= 100)
        {
            i = 100;

            StopAllCoroutines();
        }
        else
        {

            StartCoroutine(PercentageCalculator());

        }
        PercentageText.text = "" + i + " %";


    }
}
