using UnityEngine;

public class SwimArea : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().isSwimming = true;
        }
        if (other.CompareTag("MainCamera"))
        {
            other.GetComponentInParent<PlayerMovement>().isUnderwater = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().isSwimming = false;
        }
        if (other.CompareTag("MainCamera"))
        {
            other.GetComponentInParent<PlayerMovement>().isUnderwater = false;
        }
    }

}
