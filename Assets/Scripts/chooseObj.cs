using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class chooseObj : MonoBehaviour
{
    private objDetails.ObjType objectType;
    [Header("UI Message Display")]
    [Tooltip("TextMeshProUGUI component for displaying object messages")]
    public TextMeshProUGUI messageText;
    public GameObject messagePanel;

    public int goodCount = 0;
    public int badCount = 0;
    public int count = 0;

    void OnTriggerEnter(Collider other)
    {
        messagePanel.SetActive(true);
        // hide panel after delay
        StartCoroutine(HideMessagePanel());

        if (other.CompareTag("Holdable"))
        {
            Debug.Log("Exiting trigger with object: " + other.name);
            objDetails obj = other.GetComponent<objDetails>();
            if (obj != null)
            {
                objectType = obj.objectType;
                // Read back and log the object's ObjType
                Debug.Log($"Triggered object type: {obj.objectType}");
                if (messageText != null)
                {
                    switch (objectType)
                    {
                        case objDetails.ObjType.bardak:
                            messageText.text = "Annemim aldığı bardak";
                            goodCount++;
                            count++;
                            // check end condition on new count
                            break;
                        case objDetails.ObjType.ayi:
                            messageText.text = "Babamın bana verdiği ayıcık";
                            goodCount++;
                            count++;
                            break;
                        case objDetails.ObjType.foto:
                            messageText.text = "Küçüküğümde Köy evinde çekilmiş fotoğrafımız";
                            goodCount++;
                            count++;
                            break;
                        case objDetails.ObjType.Computer:
                            messageText.text = "Her gün çalıştığım bilgisayar";
                            badCount++;
                            count++;
                            break;
                        case objDetails.ObjType.oyuncak:
                            messageText.text = "Babamla küçükken oluşturduğumuz oyuncak";
                            goodCount++;
                            count++;
                            break;
                        case objDetails.ObjType.kupa:
                            messageText.text = "Sıkıcı kupalardan bir tanesi";
                            badCount++;
                            count++;
                            break;
                        default:
                            messageText.text = "";
                            break;
                    }
                }
                
                checkEnd();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Holdable"))
        {
            objDetails obj = other.GetComponent<objDetails>();
            if (obj != null && messageText != null)
            {
                switch (obj.objectType)
                {
                    case objDetails.ObjType.bardak:
                        goodCount--;
                        count--;
                        break;
                    case objDetails.ObjType.ayi:
                        goodCount--;
                        count--;
                        break;
                    case objDetails.ObjType.foto:
                        goodCount--;
                        count--;
                        break;
                    case objDetails.ObjType.oyuncak:
                        goodCount--;
                        count--;
                        break;
                    case objDetails.ObjType.Computer:
                        badCount--;
                        count--;
                        break;
                    case objDetails.ObjType.kupa:
                        badCount--;
                        count--;
                        break;
                    default:
                        break;
                }
            }
        }
        messageText.text = "";
    }

    private IEnumerator HideMessagePanel()
    {
        yield return new WaitForSeconds(4f);
        messagePanel.SetActive(false);
        // Clear the message text after hiding the panel
        if (messageText != null)
            messageText.text = "";
    }

    private void checkEnd()
    {
        Debug.Log($"Good Count: {goodCount}, Bad Count: {badCount}, Total Count: {count}");
        if (count == 4)
        {
            if (goodCount > badCount)
            {
                // load scene with build index 10 when more good objects
                SceneManager.LoadScene(12);
            }
            else
            {

                SceneManager.LoadScene(10);
            }
        }
    }
}


