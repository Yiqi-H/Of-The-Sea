using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class KeyText : MonoBehaviour
{
    public TextMeshProUGUI keyText;
    int keyCount;

    private void OnEnable()
    {
        Key.OnKeyCollected += IncrementKeyCount;
    }

    private void OnDisable()
    {
        Key.OnKeyCollected -= IncrementKeyCount;
    }

    public void IncrementKeyCount(ItemData itemData)
    {
        keyCount++;
        keyText.text = $"Keys:{keyCount}";
    }
}
