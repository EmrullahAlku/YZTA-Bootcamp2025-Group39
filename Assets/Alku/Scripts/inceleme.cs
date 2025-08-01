using UnityEngine;

public class inceleme : MonoBehaviour
{
    [Tooltip("Player’ın baktığı merkez noktası")]
    public Camera playerCamera;
    [Tooltip("İnceleme mesafesi")]
    public float interactRange = 3f;
    [Tooltip("Sadece bu layer’daki nesneler incelenebilir")]
    public LayerMask examineLayer;
    [Tooltip("Objeyi tutmak için kamera önünde oluşturulmuş boş Transform")]
    public Transform holdPoint;
    [Tooltip("Max world-space size (x,y,z) the examined object should fit into")]
    public Vector3 examineMaxSize = new Vector3(1f, 1f, 1f);
    [Tooltip("Mouse ile döndürme hızı")]
    public float rotationSpeed = 100f;

    private GameObject currentObject;
    private bool isExamining = false;
    private MonoBehaviour playerMovementScript;

    public int toplananMektupParcalari = 0; // Toplam etkileşim sayısı
    // saved transform for returning object
    private Transform originalParent;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    // saved original scale for examined object
    private Vector3 originalScale;

    void Start()
    {
        if (playerCamera == null) 
            playerCamera = Camera.main;
        // Örnek: PlayerHealth, CharacterController ya da kendi movement script’inizi atayın
        playerMovementScript = GetComponent<FirstPersonController>();
    }

    void Update()
    {
        // F tuşuna basıldığında incelemeyi başlat / bitir
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F key pressed, toggling examine mode...");
            if (!isExamining)
                TryStartExamine();
            else
                EndExamine();
        }

        // İnceleme modunda, fare hareketiyle objeyi dönder
        if (isExamining && currentObject != null)
        {
            float mx = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float my = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
            // Y ekseninde global, X ekseninde yerel dönüş
            currentObject.transform.Rotate(Vector3.up, -mx, Space.World);
            currentObject.transform.Rotate(Vector3.right, -my, Space.Self);
        }
    }

    private void TryStartExamine()
    {
        Debug.Log("Attempting to start examine mode...");
        // Ekran merkezinden raycast
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width/2f, Screen.height/2f));
        if (Physics.Raycast(ray, out RaycastHit hit, interactRange, examineLayer))
        {
            GameObject obj = hit.collider.gameObject;
            // save original world transform
            originalParent = obj.transform.parent;
            originalPosition = obj.transform.position;
            originalRotation = obj.transform.rotation;
            currentObject = obj;
            // stop player movement
            if (playerMovementScript != null) playerMovementScript.enabled = false;
            // move object to holdPoint
            obj.transform.SetParent(holdPoint);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            // save original scale and fit within max size
            originalScale = obj.transform.localScale;
            ScaleToFit(obj);
            // Fareyi aktif et
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isExamining = true;
        }
    }

    private void EndExamine()
    {
        if (currentObject == null) return;
        // restore scale
        currentObject.transform.localScale = originalScale;
        // release or destroy based on tag
        if (currentObject.CompareTag("Mektup"))
        {
            toplananMektupParcalari++;
            Destroy(currentObject);
            if (toplananMektupParcalari == 5)
            {
                Debug.Log("Level Ended - All letter pieces collected!");
            }
        }
        else
        {
            currentObject.transform.SetParent(originalParent);
            currentObject.transform.position = originalPosition;
            currentObject.transform.rotation = originalRotation;
        }
        currentObject = null;
        // Hareketi aç
        if (playerMovementScript != null) playerMovementScript.enabled = true;
        // Fareyi kilitle
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isExamining = false;
    }

    /// <summary>
    /// Scales obj uniformly so its renderer bounds fit within examineMaxSize
    /// </summary>
    private void ScaleToFit(GameObject obj)
    {
        var rend = obj.GetComponentInChildren<Renderer>();
        if (rend == null) return;
        Vector3 size = rend.bounds.size*2;
        float factor = Mathf.Min(
            examineMaxSize.x / size.x,
            examineMaxSize.y / size.y,
            examineMaxSize.z / size.z);
        // do not scale up objects smaller than target size
        factor = Mathf.Min(1f, factor);
        obj.transform.localScale = originalScale * factor;
    }
}