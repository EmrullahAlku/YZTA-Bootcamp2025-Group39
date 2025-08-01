using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering.Universal;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInventory_y : MonoBehaviour
{
    public List<string> items;

    public int SearchItem(string itemName)
    {
        int n = items.Count;
        for (int i = 0; i < n; i++)
        {
            if (itemName == items[i])
            {
                return i;
            }
        }
        return -1;
    }
}
