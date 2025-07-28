using UnityEngine;

public class AI_Movement : MonoBehaviour
{
    Animator animator;
    Rigidbody rb;

    public float moveSpeed = 0.2f;
    Vector3 stopPosition;

    float walkTime;
    public float walkCounter;
    float waitTime;
    public float waitCounter;

    int WalkDirection;
    public bool isWalking;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        walkTime = Random.Range(3, 6);
        waitTime = Random.Range(5, 7);

        waitCounter = waitTime;
        walkCounter = walkTime;

        ChooseDirection();
    }

    void Update()
    {
        if (isWalking)
        {
            animator.SetBool("isRunning", true);
            walkCounter -= Time.deltaTime;

            Quaternion targetRotation = Quaternion.identity;

            switch (WalkDirection)
            {
                case 0: targetRotation = Quaternion.Euler(0f, 0f, 0f); break;
                case 1: targetRotation = Quaternion.Euler(0f, 90f, 0f); break;
                case 2: targetRotation = Quaternion.Euler(0f, -90f, 0f); break;
                case 3: targetRotation = Quaternion.Euler(0f, 180f, 0f); break;
            }

            transform.localRotation = targetRotation;
            rb.MovePosition(rb.position + transform.forward * moveSpeed * Time.deltaTime);

            if (walkCounter <= 0)
            {
                isWalking = false;
                animator.SetBool("isRunning", false);
                waitCounter = waitTime;
            }
        }
        else
        {
            waitCounter -= Time.deltaTime;
            if (waitCounter <= 0)
            {
                ChooseDirection();
            }
        }
    }

    public void ChooseDirection()
    {
        WalkDirection = Random.Range(0, 4);
        isWalking = true;
        walkCounter = walkTime;
    }
}
