using UnityEngine;

public class movingObj : MonoBehaviour
{
    // reference to the object to follow
    public Transform targetObject;
    // initial offset from target
    private Vector3 offset;
    // last recorded target position to detect movement
    private Vector3 lastTargetPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // calculate offset if target assigned
        if (targetObject != null)
        {
            offset = transform.position - targetObject.position;
            lastTargetPosition = targetObject.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // only update if target has moved
        if (targetObject != null)
        {
            Vector3 currentPos = targetObject.position;
            if (currentPos != lastTargetPosition)
            {
                transform.position = currentPos + offset;
                lastTargetPosition = currentPos;
            }
        }
    }
}
