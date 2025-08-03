using System;
using UnityEngine;

public class playerInteraction : MonoBehaviour
{
    // tag to filter interactable objects
    public string interactTag = "Interactable";
    // max distance for raycast interaction
    public float interactRange = 5f;
    // reference to player camera for raycasting
    public Camera playerCamera;
    // toggle state for movement direction
    private bool toggleMove = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // when S key is pressed, raycast from center of screen and move hit object on Z axis
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("B key pressed, checking for interactable objects...");
            // use assigned playerCamera or fallback to main camera
            Camera cam = playerCamera != null ? playerCamera : Camera.main;
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width/2f, Screen.height/2f, 0f));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, interactRange))
            {
                // call the interact method on any IInteractable component
                var interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    interactable.Interact();
                }
            }
        }
    }
}
