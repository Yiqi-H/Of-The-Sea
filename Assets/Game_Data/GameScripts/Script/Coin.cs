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
      Destroy(gameObject);
      AudioManager.instance.PlayOneShot(FMODEvents.instance.coinCollected,this.transform.position);
      OnCoinCollected?.Invoke(coinData);

  }
}
