using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;

public class GameStateManager : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject gameOverScreen;
    public GameObject victoryScreen;

    public GameObject BG;

    public Color startColor; // Initial color
    public Color endColor;
    public Color OriginalColor;
    public float fadeDuration = 1f; // Duration of the fade

    public GameObject LevelEnemiesObject;
    public GameObject InventoryPanel;
    public GameObject Level1heading;
    public static GameStateManager Instance;
    void Start()
    {
        Inventory.Level1key = false;
        Instance = this;
        
        
       

       
        if (Level1heading)
        StartCoroutine(ObjectDisable(Level1heading, 1));
        StartCoroutine(FadeColorOut());
      

        LevelEnemiesObject.SetActive(true);
        InventoryPanel.SetActive(true);
        //

        Time.timeScale = 1;
    }
    

    

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }
    public void SetGameOver(float Delay)
    {
        Invoke("GameOver", Delay);
    }
    public void Victory()
    {
        victoryScreen.SetActive(true);
        Time.timeScale = 0;
    }
    public void PauseGame()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void UnPauseGame()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }
   
    public void OnMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
    public IEnumerator ObjectDisable(GameObject Obj, float wait)
    {
        yield return new WaitForSeconds(wait);
        Obj.SetActive(false);
    }
    IEnumerator FadeColor()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            // Calculate the current color based on the elapsed time and lerp between startColor and endColor
            Color currentColor = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
            BG.GetComponent<SpriteRenderer>().color = currentColor;
            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure the color is set to the endColor when the fade completes
        BG.GetComponent<SpriteRenderer>().color = endColor;
    }

    IEnumerator FadeColorOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            // Calculate the current color based on the elapsed time and lerp between startColor and endColor
            Color currentColor = Color.Lerp(endColor, OriginalColor, elapsedTime / fadeDuration);
            BG.GetComponent<SpriteRenderer>().color = currentColor;

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure the color is set to the endColor when the fade completes
        BG.GetComponent<SpriteRenderer>().color = OriginalColor;
    }
}
