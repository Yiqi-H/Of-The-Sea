using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Create the instance of ScriptableObject
[CreateAssetMenu(fileName = "New item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string displayName;
    public Sprite icon;
}
