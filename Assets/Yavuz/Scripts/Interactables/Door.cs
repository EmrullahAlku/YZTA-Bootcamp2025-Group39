using Unity.VisualScripting;
using UnityEngine;

public class Door : Interactable_y
{
    [SerializeField] PlayerInventory_y inventory;

    protected override void Interact()
    {
        Debug.Log(inventory.items.BinarySearch("DoorKey"));
        if (inventory.SearchItem("DoorKey") != -1)
        {
            GameObject.Destroy(gameObject);
        }
    }
}