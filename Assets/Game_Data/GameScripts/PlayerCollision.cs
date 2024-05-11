using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class PlayerCollision : MonoBehaviour
{
    public int HitPoints = 3;
    public GameObject[] HitpointsHeart;

    public bool canMove = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy") && canMove)
        {

            HitpointsHeart[HitPoints - 1].GetComponent<Animator>().enabled = true;
            if(AudioManager.instance)
            AudioManager.instance.PlayOneShot(FMODEvents.instance.enemyHit, this.transform.position);
            HitPoints--;
            if (HitPoints <= 0)
            {
                HitPoints = 0;
                if (AudioManager.instance)
                    AudioManager.instance.PlayOneShot(FMODEvents.instance.playerDeath, this.transform.position);
                Debug.Log("Game_Over");
                GameStateManager.Instance.SetGameOver(1.3f);
                print("StopMusic");
                if (AudioManager.instance)
                    AudioManager.instance.StopMusic();
            }
            // Prevent further movement temporarily
            canMove = false;

            // Apply hit effect immediately
            ApplyHitEffect(collision);

            // Start a coroutine to enable movement after a delay
            StartCoroutine(EnableMovementAfterDelay());
        }
        else if (collision.gameObject.CompareTag("Door"))
        {
            if (Inventory.Level1key)
            {
                Time.timeScale = 0;
                if (AudioManager.instance)
                AudioManager.instance.PlayOneShot(FMODEvents.instance.doorOpen, this.transform.position);
                SceneManager.LoadScene("Level2");
            }

        }

        else if (collision.gameObject.CompareTag("Box"))
        {
            if (AudioManager.instance)
                AudioManager.instance.PlayOneShot(FMODEvents.instance.boxOpen, this.transform.position);
            GameStateManager.Instance.Victory();
            //Invoke("Victory", 1f);
            Debug.Log("box");
        }


        else if (collision.gameObject.CompareTag("Obstacle") && canMove)
        {
            Debug.Log("we collide it");
            canMove = false;
            ApplyHitEffect(collision);
            StartCoroutine(EnableMovementAfterDelay());
        }

        else
        {
            ICollectible collectible = collision.GetComponent<ICollectible>();
            if (collectible != null)
            {
                collectible.Collect();
            }
        }
    }

    void ApplyHitEffect(Collider2D other)
    {
        // Calculate the direction of the collision force
        Vector2 forceDirection = ((Vector2)transform.position - (Vector2)other.transform.position).normalized;

        // Calculate a random respawn position opposite to the direction of force
        float respawnDistance = 2f; // Adjust this value as needed
        Vector2 randomRespawnDirection = Quaternion.Euler(0, 0, Random.Range(90, 270)) * forceDirection;

        // Calculate the respawn position
        Vector2 respawnPosition = (Vector2)transform.position + randomRespawnDirection * respawnDistance;

        // Smoothly transition to the respawn position
        StartCoroutine(MoveToRespawnPosition(respawnPosition));
    }

    IEnumerator MoveToRespawnPosition(Vector2 targetPosition)
    {
        float duration = 0.1f; // Adjust this value to change the duration of the transition
        float elapsedTime = 0f;
        Vector2 startPosition = transform.position;

        while (elapsedTime < duration)
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition; // Ensure the final position is accurate
    }
    IEnumerator EnableMovementAfterDelay()
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(1f);

        // Allow movement again
        canMove = true;
    }

}
