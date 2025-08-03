using UnityEngine;

public class SwimArea : MonoBehaviour
{
    // reference to level 1 missions manager
    private LvL1Missions lvl1Missions;

    private void Start()
    {
        // find the missions manager in scene
        lvl1Missions = Object.FindAnyObjectByType<LvL1Missions>();
        if (lvl1Missions == null)
            Debug.LogWarning("LvL1Missions not found in scene for SwimArea");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().isSwimming = true;
            // notify missions that player started swimming
            if (lvl1Missions != null)
                lvl1Missions.GoSwim();
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
