using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // ← ekledik

public class angryBar : MonoBehaviour
{
    public static int brokenCount = 0;
    public Slider slider;

    // yeni bayrak, sonraki sahnenin bir kez yüklenmesini sağlamak için
    private bool hasLoadedNext = false;

    void Update()
    {
        if (slider == null) return;

        slider.value = brokenCount;

        // yalnızca bir kez tetikle
        if (!hasLoadedNext && slider.value >= slider.maxValue)
        {
            hasLoadedNext = true;
            Debug.Log("Level 3 Bitti: Bir sonraki sahneye geçiliyor!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}