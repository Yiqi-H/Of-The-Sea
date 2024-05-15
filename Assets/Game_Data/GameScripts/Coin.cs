using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using FMODUnity;

public class Coin : MonoBehaviour,ICollectible
{
  public static event HandleCoinCollected OnCoinCollected;
  public delegate void HandleCoinCollected(ItemData itemData);

  public ItemData coinData;

  public void Collect()
  {
        if (AudioManager.instance)
            AudioManager.instance.PlayOneShot(FMODEvents.instance.coinCollected, this.transform.position);

        Destroy(gameObject);
        
      OnCoinCollected?.Invoke(coinData);

  }
}
