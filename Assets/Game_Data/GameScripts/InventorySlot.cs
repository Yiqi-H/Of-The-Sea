using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI labelText;
    public TextMeshProUGUI stackSizeText;

    //Clear the slot,hiding all UI elements 
    public void ClearSlot()
    {
        icon.enabled = false;
        labelText.enabled = false;
        stackSizeText.enabled = false;
    }
    
    //Draws the slot with the given InventoryItem data.
    public void DrawSlot(InventoryItem item)
    {
        Debug.Log("Hereee.....");
        if(item == null)
        {
            ClearSlot();
            return;
        }
        
        //Enable UI elements for displaying the item.
        icon.enabled = true;
        labelText.enabled = true;
        stackSizeText.enabled = true;

        //Set the UI elements with the item's data.
        icon.sprite = item.itemData.icon;
        labelText.text = item.itemData.displayName;
        stackSizeText.text = item.stackSize.ToString();

    }

}
