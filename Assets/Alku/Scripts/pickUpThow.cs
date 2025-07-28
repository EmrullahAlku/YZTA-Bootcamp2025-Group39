using UnityEngine;

public class pickUpThow : MonoBehaviour
{
    public Camera cam;
    public float pickUpRange = 3f;
    public Transform holdParent;
    public float throwForce = 500f;
    public float spring = 800f;
    public float damper = 5f;
    // angular damping saved when held
    private float originalAngularDamping;

    // replace holdableObject with Rigidbody
    private Rigidbody heldRb;
    private SpringJoint springJoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (cam == null)
            cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (heldRb == null)
                TryPickUp();
            else
                Throw();
        }

        if (springJoint != null)
            springJoint.connectedAnchor = holdParent.position;
    }

    void TryPickUp()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, pickUpRange))
        {
            var go = hit.collider.gameObject;
            var rb = hit.collider.attachedRigidbody;
            // check Holdable tag and Rigidbody
            if (go.CompareTag("Holdable") && rb != null)
            {
                heldRb = rb;
                // save original damping
                originalAngularDamping = rb.angularDamping;
                // apply damping
                rb.angularDamping = damper;
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;

                springJoint = go.AddComponent<SpringJoint>();
                springJoint.autoConfigureConnectedAnchor = false;
                springJoint.connectedAnchor = holdParent.position;
                springJoint.spring = spring;
                springJoint.damper = damper;
                springJoint.maxDistance = 0f;
            }
        }
    }

    void Throw()
    {
        var rb = heldRb;
        if (rb == null) return;
        // restore original damping
        rb.angularDamping = originalAngularDamping;

        Destroy(springJoint);
        springJoint = null;
        heldRb = null;
        rb.AddForce(cam.transform.forward * throwForce);
    }
}
