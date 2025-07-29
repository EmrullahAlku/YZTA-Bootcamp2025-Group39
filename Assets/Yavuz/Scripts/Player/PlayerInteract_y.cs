using UnityEngine;

public class PlayerInteract_y : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private float distance = 4f;
    [SerializeField]
    private LayerMask mask;
    
    private PlayerUI_y playerUI;

    void Start()
    {
        playerUI = GetComponent<PlayerUI_y>();   
    }    

    void Update()
    {
        playerUI.UpdateText(string.Empty);
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(cam.transform.position, ray.direction * distance, Color.red);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            Interactable_y interactObj = hitInfo.collider.GetComponent<Interactable_y>();
            if (interactObj != null)
            {
                playerUI.UpdateText(interactObj.promptMessage);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactObj.BaseInteract();
                }
            }
        }
    }
}
