using UnityEngine;
using UnityEngine.SceneManagement;

public class TekrarOyna : MonoBehaviour
{

    public void AgainGame()
    {

        SceneManager.LoadScene("Level 1");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}



//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class TekrarOyna : MonoBehaviour
//{

//    void AgainGame()
//    {
//        SceneManager.LoadScene("Level 1");
//    }

//}
