using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InventoryItem
{
    public ItemData itemData;
    public int stackSize;

    public InventoryItem(ItemData item)
    {
        itemData = item;
        //Initializes stackSize to 1 when a new InventoryItem is created
        AddToStack();
    }

    public void AddToStack()
    {
        stackSize++;
    }

    public void RemoveFromStack()
    {
        stackSize--;
    }

}
