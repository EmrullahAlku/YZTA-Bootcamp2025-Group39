using UnityEngine;

public abstract class Interactable_y : MonoBehaviour
{
    public string promptMessage;

    public void BaseInteract()
    {
        Interact();
    }

    protected virtual void Interact()
    {
    }
}
