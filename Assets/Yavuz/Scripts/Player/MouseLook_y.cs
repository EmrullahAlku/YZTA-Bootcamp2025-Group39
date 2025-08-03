using UnityEngine;

public class MouseLook_y : MonoBehaviour
{
    [SerializeField]
    float mouseSensitivity = 100f;
    [SerializeField]
    Transform playerBody;

    float rotateX = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotateX -= mouseY;
        rotateX = Mathf.Clamp(rotateX, -90f, 90f);

        transform.localRotation = Quaternion.Euler(rotateX, transform.localRotation.eulerAngles.y, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
