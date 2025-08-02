using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class videoEndSceneLoader : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
