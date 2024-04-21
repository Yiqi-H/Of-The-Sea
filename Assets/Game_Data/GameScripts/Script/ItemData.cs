using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string displayName;
    public Sprite icon;
}
