using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    /// <summary>
    /// Loads the next scene in Build Settings order.
    /// </summary>
    /// 
    void Awake() {
    Debug.Log("NextScene.Awake()");
}

void OnEnable() {
    Debug.Log("NextScene enabled");
}
    public void LoadNextScene()
    {
        Debug.Log("Loading next scene...");
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }
}
