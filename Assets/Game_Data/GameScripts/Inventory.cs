using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Inventory : MonoBehaviour
{
    public static event Action<List<InventoryItem>> OnInventoryChange;
    public List<InventoryItem> inventory = new List<InventoryItem>();
    private Dictionary<ItemData,InventoryItem>itemDictionary = new Dictionary<ItemData,InventoryItem>();
    public static bool Level1key = false;
    public static bool Level2Key = false;
    public static int coindata=0;
    private void OnEnable()
    {
        DontDestroyOnLoad(this);
        Coin.OnCoinCollected += Add;
        Key.OnKeyCollected += Add;
    }

    private void OnDisable()
    {
        Coin.OnCoinCollected -= Add;
        Key.OnKeyCollected -= Add;
    }

    public void Add(ItemData itemData)
    {
        if(itemDictionary.TryGetValue(itemData,out InventoryItem item))
        {
            item.AddToStack();
            Debug.Log($"{item.itemData.displayName} total stack is now{item.stackSize}");
            if (itemData.displayName == "Coin")
            {
                coindata++;
                Debug.LogError("Coin_Collected");


            }
            OnInventoryChange?.Invoke(inventory);
        }

        else
        {
            InventoryItem newItem = new InventoryItem(itemData);
            inventory.Add(newItem);
            itemDictionary.Add(itemData,newItem);
            if (itemData.displayName == "Key" && SceneManager.GetActiveScene().name=="Level1")
            {
                Level1key = true;
                
            }
            Debug.Log($"Added{itemData.displayName} to  the inventory for the first time.");
            OnInventoryChange?.Invoke(inventory);
        }
    }

    public void Remove(ItemData itemData)
    {
        if(itemDictionary.TryGetValue(itemData,out InventoryItem item))
        {
            item.RemoveFromStack();
            if(item.stackSize == 0)
            {
                inventory.Remove(item);
                itemDictionary.Remove(itemData);
            }
            OnInventoryChange?.Invoke(inventory);
        }
    }
}
