using UnityEngine;

public class Door1 : Interactable_y
{
    [SerializeField] PlayerInventory_y inventory;

    protected override void Interact()
    {
        if (inventory.items.BinarySearch("DoorKey") != -1)
        {
            GameObject.Destroy(gameObject);
        }
    }
}