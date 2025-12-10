// 12/2/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;
using UnityEngine.AI;

public class HeroMovement : MonoBehaviour
{
    public Controller circularController; // Reference to the CircularController script
    public float moveSpeed = 5f; // Speed of the player
    public float rotationSpeed = 10f;

    private Animator animator;

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

            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}