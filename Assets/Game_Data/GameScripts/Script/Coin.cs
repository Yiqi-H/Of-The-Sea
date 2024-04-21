using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Coin : MonoBehaviour,ICollectible
{
  public static event HandleCoinCollected OnCoinCollected;
  public delegate void HandleCoinCollected(ItemData itemData);

  public ItemData coinData;

  public void Collect()
  {
        Destroy(gameObject);
        OnCoinCollected?.Invoke(coinData);

  }
}
