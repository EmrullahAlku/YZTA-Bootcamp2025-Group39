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
    // last outline component we enabled
    private Outline lastOutline;

    [SerializeField]
    private LayerMask highlightLayers;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // no Start needed for outline; outlines are on target objects

    // Update is called once per frame
    void Update()
    {
        // raycast each frame from screen center for outline highlighting
        Camera cam = playerCamera != null ? playerCamera : Camera.main;
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width/2f, Screen.height/2f, 0f));
        RaycastHit hitInfo;
        Outline currentOutline = null;
        if (Physics.Raycast(ray, out hitInfo, interactRange))
        {
            int layer = hitInfo.collider.gameObject.layer;
            if ((highlightLayers.value & (1 << layer)) != 0)
                currentOutline = hitInfo.collider.GetComponent<Outline>();
        }
        if (lastOutline != currentOutline)
        {
            if (lastOutline != null)
                lastOutline.enabled = false;
            if (currentOutline != null)
                currentOutline.enabled = true;
            lastOutline = currentOutline;
        }
        // interaction on B key
        if (Input.GetKeyDown(KeyCode.B) && lastOutline != null)
        {
            var interactable = lastOutline.GetComponent<Interactable>();
            if (interactable != null)
                interactable.Interact();
        }
    }
}
