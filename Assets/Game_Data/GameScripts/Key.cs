using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Key : MonoBehaviour,ICollectible
{
    //Define a delegate type for the key collected event
    public static event HandleKeyCollected OnKeyCollected;
    //Define a static event based on the HandleKeyCollected delegate.
    public delegate void HandleKeyCollected(ItemData itemData);
    public ItemData keyData;

    public void Collect()
    {
        Destroy(gameObject);
        //Invole the OnKeyCollected event,passing in the keyData.
        OnKeyCollected?.Invoke(keyData);

    }

}
