using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Key : MonoBehaviour,ICollectible
{
    public static event HandleKeyCollected OnKeyCollected;
    public delegate void HandleKeyCollected(ItemData itemData);
    public ItemData keyData;


    public void Collect()
    {
        Destroy(gameObject);
        OnKeyCollected?.Invoke(keyData);

    }

    //public static event Action OnKeyCollected;

    //public void Collect()
    //{
       // Debug.Log("Key collected");
        //Destroy(gameObject);
        //OnKeyCollected?.Invoke();
    //}
}
