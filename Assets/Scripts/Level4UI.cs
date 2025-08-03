using UnityEngine;

public class Level4UI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject level4ContentUI;

    void Start()
    {
        if (level4ContentUI != null)
            level4ContentUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // toggle missions UI
        if (Input.GetKeyDown(KeyCode.M) && level4ContentUI != null)
        {
            level4ContentUI.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && level4ContentUI != null)
        {
            level4ContentUI.SetActive(false);
        }
    }
}
