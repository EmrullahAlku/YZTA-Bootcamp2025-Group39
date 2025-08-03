using UnityEngine;

public class rabbitTrigger : MonoBehaviour
{
    private LvL1Missions lvl1Missions;

    void Start()
    {
        if (lvl1Missions == null)
            lvl1Missions = FindAnyObjectByType<LvL1Missions>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (lvl1Missions != null)
                lvl1Missions.GoTavsan();
            else
                Debug.LogWarning("LvL1Missions instance not found in rabbitTrigger.");
        }
    }
}   