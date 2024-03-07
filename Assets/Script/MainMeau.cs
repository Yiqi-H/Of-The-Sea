using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMeau : MonoBehaviour
{
    string SceneName;
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
        Invoke("LoadSceneInvoke", 4);
        //SceneManager.LoadScene(scenename);

    }

    public void LoadSceneInvoke()
    {

        SceneManager.LoadScene(SceneName);

    }
}
