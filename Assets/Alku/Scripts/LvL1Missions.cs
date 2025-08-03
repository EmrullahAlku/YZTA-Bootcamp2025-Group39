using UnityEngine;
using TMPro; 
public class LvL1Missions : MonoBehaviour
{
    [Header("Mission Parameters")]
    public bool isWoodCollected = false;
    public bool isFireLit = false;
    public bool isGoSwim = false;
    public bool isGoTavsan = false;
    public bool isPaintingFound = false;
    public bool IsLevelComplete => isWoodCollected && isFireLit && isGoSwim && isGoTavsan && isPaintingFound;

    [Header("Mission Scripts")]
    [Tooltip("Camera used for raycasting to collect wood")]
    public Camera playerCamera;
    [Tooltip("Max distance to collect wood")]
    public float collectRange = 3f;
    
    public int requiredWood = 8;
    public int woodCollected = 0;

    [Header("UI Panels")]
    [Tooltip("Panel GameObject displaying the level 1 missions")]
    //public GameObject missionsUIPanel;
    public GameObject level1ContentUI;

    void Start()
    {
        UpdateMissionUI();
        if (playerCamera == null)
            playerCamera = Camera.main;
        // hide missions UI at start
        if (level1ContentUI != null)
            level1ContentUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // toggle missions UI
        if (Input.GetKeyDown(KeyCode.M) && level1ContentUI != null)
        {
            level1ContentUI.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && level1ContentUI != null)
        {
            level1ContentUI.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.F) && playerCamera != null)
        {
            Debug.Log("F key pressed, checking for wood collection...");
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
            if (Physics.Raycast(ray, out RaycastHit hit, collectRange))
            {
                Debug.Log($"Hit object: {hit.collider.gameObject.name}");
                if (hit.collider.CompareTag("wood"))
                {
                    CollectWood();
                    Destroy(hit.collider.gameObject);
                }
                if (hit.collider.gameObject.name == "Foto")
                {
                    FindPainting();
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }

 [Header("Mission UI Texts")]
    public TextMeshProUGUI woodStatusText;
    public TextMeshProUGUI fireStatusText;
    public TextMeshProUGUI swimStatusText;
    public TextMeshProUGUI tavsanStatusText;
    public TextMeshProUGUI paintingStatusText;
    private void UpdateMissionUI()
    {
        if (woodStatusText != null)
            woodStatusText.text = $"Odun: {woodCollected}/{requiredWood}";
        if (fireStatusText != null)
            fireStatusText.text = $"Ateş: {(isFireLit ? "✓" : "✗")}";
        if (swimStatusText != null)
            swimStatusText.text = $"Yüzme: {(isGoSwim ? "✓" : "✗")}";
        if (tavsanStatusText != null)
            tavsanStatusText.text = $"Tavşan: {(isGoTavsan ? "✓" : "✗")}";
        if (paintingStatusText != null)
            paintingStatusText.text = $"Resim: {(isPaintingFound ? "✓" : "✗")}";
    }

    public void CollectWood(int amount = 1)
    {
        woodCollected += amount;
        Debug.Log($"Wood collected: {woodCollected}/{requiredWood}");
        if (woodCollected >= requiredWood)
        {
            Debug.Log("All required wood collected!");
            isWoodCollected = true;
        }
        UpdateMissionUI();
        CheckCompletion();
    }

    /// <summary>
    /// Call this when the fire is lit.
    /// </summary>
    public void LightFire()
    {
        isFireLit = true;
        Debug.Log("Fire has been lit.");
        UpdateMissionUI();
        CheckCompletion();
    }

    /// <summary>
    /// Call this when the painting is found.
    /// </summary>
    public void FindPainting()
    {
        isPaintingFound = true;
        Debug.Log("Painting has been found.");
        UpdateMissionUI();
        CheckCompletion();
    }

    public void GoSwim()
    {
        isGoSwim = true;
        Debug.Log("Player has gone swimming.");
        UpdateMissionUI();
        CheckCompletion();
    }

    public void GoTavsan()
    {
        isGoTavsan = true;
        Debug.Log("Player has gone to the tavsan.");
        UpdateMissionUI();
        CheckCompletion();
    }

    private void CheckCompletion()
    {
        if (IsLevelComplete)
        {
            Debug.Log("Level 1 Complete!");
        }
    }

    
}
