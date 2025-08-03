using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{

    public static SelectionManager Instance { get; set; }

    public bool onTarget;

    public GameObject selectedObject;


    public GameObject interaction_Info_UI;
    Text interaction_text;

    private void Start()
    {
        onTarget = false;
        interaction_text = interaction_Info_UI.GetComponent<Text>();
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        else
        {
            Instance = this;
        }
    }

    // last outline we enabled
    private Outline lastOutline;
    [Tooltip("Max distance for selection and outline raycast")]
    public float interactRange = 30f;
    void Update()
    {
        // unified raycast for outline and selection
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit, interactRange);
        // outline toggle on Interactable layer
        Outline currentOutline = null;
        if (hasHit && hit.collider.gameObject.layer == LayerMask.NameToLayer("Interactable"))
            currentOutline = hit.collider.GetComponent<Outline>();
        if (lastOutline != currentOutline)
        {
            if (lastOutline != null) lastOutline.enabled = false;
            if (currentOutline != null) currentOutline.enabled = true;
            lastOutline = currentOutline;
        }
        // selection and info UI
        if (hasHit)
        {
            var selectionTransform = hit.transform;

            InteractableObject interactable = selectionTransform.GetComponent<InteractableObject>();

            if (interactable && interactable.playerInRange)
            {
                onTarget = true;
                selectedObject = interactable.gameObject;
                interaction_text.text = selectionTransform.GetComponent<InteractableObject>().GetItemName();
                interaction_Info_UI.SetActive(true);
            }
            else
            {
                onTarget = false;
                interaction_Info_UI.SetActive(false);
            }

        }
        else
        {
            onTarget = false;
            interaction_Info_UI.SetActive(false);
        }
        
    }
}