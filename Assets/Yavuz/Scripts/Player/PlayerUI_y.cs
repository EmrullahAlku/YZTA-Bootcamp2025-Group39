using UnityEngine;
using TMPro;

public class PlayerUI_y : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textMeshPro;

    public void UpdateText(string promptMessage)
    {
        textMeshPro.text = promptMessage;
    } 
}
