using UnityEngine;
using UnityEngine.UI;

public class angryBar : MonoBehaviour
{
    // Tracks number of broken objects
    public static int brokenCount = 0;
    // UI slider to display anger
    public Slider slider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (slider != null)
            slider.value = brokenCount;
    }
}
