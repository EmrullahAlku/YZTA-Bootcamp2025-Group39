using UnityEngine;

public class fireTrigger : MonoBehaviour
{
    [Tooltip("Reference to the level 1 mission manager")]
    public LvL1Missions missionManager;
    [Tooltip("Fire GameObject to activate when wood is collected")]
    public GameObject fireObject;
    // tracks if player is inside trigger
    private bool playerInside = false;

    void Start()
    {
        if (missionManager == null)
            missionManager = FindAnyObjectByType<LvL1Missions>();
        if (fireObject != null)
            fireObject.SetActive(false);
    }

    void Update()
    {
        // if inside trigger and presses F, check wood and light fire
        if (playerInside && Input.GetKeyDown(KeyCode.F) && missionManager != null)
        {   
            Debug.Log("F key pressed, checking wood collection...");
            if (missionManager.isWoodCollected)
            {
                Debug.Log("F key pressed, lighting fire...");
                if (fireObject != null)
                    fireObject.SetActive(true);
                missionManager.LightFire();
                // disable further firing
                enabled = false;
            }
            else
            {
                Debug.Log("You need to collect wood before lighting the fire.");
            }
        } else if (missionManager == null)
        {
            Debug.LogWarning("Mission Manager not found!");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInside = false;
    }
}
