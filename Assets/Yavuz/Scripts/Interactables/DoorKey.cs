using UnityEngine;

public class DoorKey : Interactable_y
{
    [SerializeField] PlayerInventory_y inventory;

    protected override void Interact()
    {
        inventory.items.Add(gameObject.name);
        Debug.Log("Key Taken");
        GameObject.Destroy(gameObject);
    }

}
