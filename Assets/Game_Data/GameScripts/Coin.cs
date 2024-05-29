using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using FMODUnity;

public class Coin : MonoBehaviour,ICollectible
{

  //Define a delegate type for the coin collected event
  public static event HandleCoinCollected OnCoinCollected;

  //Define a static event based on the HandleCoinCollected delegate
  public delegate void HandleCoinCollected(ItemData itemData);

  public ItemData coinData;

  public void Collect()  
  {
        //Check if the AudioManage instanace exists and play the coin collected sound at the coin's position
        if (AudioManager.instance)
            AudioManager.instance.PlayOneShot(FMODEvents.instance.coinCollected, this.transform.position);
        
        //Destroy the coin GameObject
        Destroy(gameObject);
      
      //Invoke the OnCoinCollected event,passing in the CoinData
      OnCoinCollected?.Invoke(coinData);

  }
}
