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
    public int i = 0;
  
    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    // Start is called before the first frame update
    public void LoadScene(string _scenename)
    {
        SceneName = _scenename;
        //Loading.GetComponent<Loading>().LoadLevel(SceneName);
       // StartCoroutine(PercentageCalculator());
        LoadSceneInvoke();
        //Invoke("LoadSceneInvoke", 2);
        //Invoke("LoadSceneInvoke", 4);
        //SceneManager.LoadScene(scenename);
    }

    public void LoadSceneInvoke()
    {

        SceneManager.LoadScene(SceneName);

    }
    
}
