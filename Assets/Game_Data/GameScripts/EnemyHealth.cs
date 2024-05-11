using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health of the enemy
    private int currentHealth; // Current health of the enemy

    void Start()
    {
        currentHealth = maxHealth; // Set current health to max health when enemy spawns
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; // Reduce current health by damage amount
        if (currentHealth <= 0)
        {
            Die(); // If health drops to or below zero, call the Die function
        }
    }

    void Die()
    {
        // Perform any death-related actions here (e.g., play death animation, spawn particles, etc.)
        Destroy(gameObject); // Destroy the enemy GameObject
    }
}
