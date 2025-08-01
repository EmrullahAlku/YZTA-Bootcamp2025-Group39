using UnityEngine;

public class Hammer : Interactable_y
{
    [SerializeField] PlayerInventory_y inventory;

    protected override void Interact()
    {
        GameObject.Destroy(this.gameObject);
        inventory.items.Add(this.gameObject.name);
    }
}
