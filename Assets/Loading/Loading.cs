using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Loading : MonoBehaviour
{
    public Image loadingBar;
    public Text LoadingCounter;

    AsyncOperation a;


    public void LoadLevel(string level, float delay = 0.8f)
    {
        
            gameObject.SetActive(true);
            StartCoroutine(loadLevel(level, delay));
        
    }

    public void LoadLevel(int level, float delay = 0.8f)
    {

        //  a = SceneManager.LoadSceneAsync(level);

        
            gameObject.SetActive(true);
            StartCoroutine(loadLevel(level, delay));
        
    }



    IEnumerator loadLevel(int level, float delay)
    {
		yield return new WaitForSeconds(delay);
		
        a = SceneManager.LoadSceneAsync(level, LoadSceneMode.Single);
    }

    IEnumerator loadLevel(string level, float delay)
    {

        yield return new WaitForSeconds(delay);
        a = SceneManager.LoadSceneAsync(level, LoadSceneMode.Single);
        
       
    }

    private void OnEnable()
    {
        loadingBar.fillAmount = 0;
        LoadingCounter.text = "0 %";
    }

    void Update()
    {
        if (a != null)
        {
            if (loadingBar)
                loadingBar.fillAmount = a.progress;

            if (LoadingCounter)
            if (a.progress.ToString().Length > 4) LoadingCounter.text =( (a.progress)*100).ToString().Remove(4) + " %";
            else LoadingCounter.text = ((a.progress) * 100).ToString() + " %";
            if (a.isDone)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
