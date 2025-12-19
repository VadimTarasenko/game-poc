using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    public Controller circularController;
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    private Animator animator;
    private bool isRunning = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector2 inputDirection = circularController.InputDirection;

        if (inputDirection != Vector2.zero)
        {
            Vector3 movement = new Vector3(inputDirection.x, 0, inputDirection.y);

            transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);

            Quaternion targetRotation = Quaternion.LookRotation(movement);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (!isRunning)
            {
                animator.CrossFade("Run", 0f);
                isRunning = true;
            }
        }
        else if (isRunning)
        {
            animator.CrossFade("Idle2", 0f);
            isRunning = false;
        }
    }
}