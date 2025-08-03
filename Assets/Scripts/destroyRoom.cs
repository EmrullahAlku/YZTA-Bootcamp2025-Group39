using UnityEngine;

public class destroyRoom : MonoBehaviour
{
private void OnTriggerExit(Collider other)
    {
        // Eğer çıkış yapan collider Player tag’ine sahipse bu GameObject’i yok et
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited the room, destroying this GameObject.");
            Destroy(gameObject);
        }
    }    
}
