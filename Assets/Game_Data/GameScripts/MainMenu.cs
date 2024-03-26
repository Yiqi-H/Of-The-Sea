using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    string SceneName;
    public GameObject Loading;
    public Text PercentageText;
    int LoadingValue = 0;
    private void Awake()
    {
        Time.timeScale = 1;
    }
    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    // Start is called before the first frame update
    public void LoadScene(string scenename)
    {
        SceneName = scenename;
        //Loading.GetComponent<Loading>().LoadLevel(SceneName);
        StartCoroutine(PercentageCalculator());
        //Invoke("LoadSceneInvoke", 2);
        //Invoke("LoadSceneInvoke", 4);
        //SceneManager.LoadScene(scenename);
    }

    public IEnumerator PercentageCalculator()
    {
        
        yield return new WaitForSeconds(0.022f);
        LoadingValue += 1;
        if (LoadingValue >= 101)
        {
            LoadingValue = 100;
            PercentageText.text = "" + LoadingValue + " %";
            StopAllCoroutines();
            SceneManager.LoadScene(SceneName);
        }
        else
        {

            StartCoroutine(PercentageCalculator());

        }
        PercentageText.text = "" + LoadingValue + " %";
    }
}
