using UnityEngine;

public class UnderwaterBullet : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 20;

    void Start()
    {
        // Move the bullet in the desired direction
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        Invoke("OnDestroy", 1.2f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Here_It_Is"+other.gameObject.name);
        // Handle collisions with other objects
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage); // Deal damage to the enemy
               
            }
            // Do something when the bullet hits an enemy
            Destroy(gameObject); // Destroy the bullet on impact
        }
    }

    private void OnDestroy()
    {
        Destroy(this.gameObject);
    }
}
