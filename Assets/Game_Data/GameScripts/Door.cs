using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject MessageBox;
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        // Check if the colliding object is the player and the player does not have the Level1 key
        if (collision.gameObject.CompareTag("Player") && !Inventory.Level1key)
        {
            MessageBox.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the object exiting the trigger is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            MessageBox.SetActive(false);
        }
    }
}
