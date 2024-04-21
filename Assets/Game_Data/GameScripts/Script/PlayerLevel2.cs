using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel2 : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 200f;
    public float minX = -5f; // Minimum X position
    public float maxX = 5f;  // Maximum X position
    public float minY = -5f; // Minimum Y position
    public float maxY = 5f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

            // Move the fish
         Vector2 movement = new Vector2(horizontalInput, verticalInput);
        rb.velocity = movement * speed;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ICollectible collectible = collision.GetComponent<ICollectible>();
        if(collectible != null)
        {
            collectible.Collect();
        }
        
    }
}
