// 12/2/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;
using UnityEngine.AI;

public class HeroMovement : MonoBehaviour
{
    public Controller circularController; // Reference to the CircularController script
    public float moveSpeed = 5f; // Speed of the player
    public float rotationSpeed = 10f;

    private void Update()
    {
        // Get the input direction from the circular controller
        Vector2 inputDirection = circularController.InputDirection;

        // Check if there is any input
        if (inputDirection != Vector2.zero)
        {
            // Convert the 2D input direction to 3D movement
            Vector3 movement = new Vector3(inputDirection.x, 0, inputDirection.y);

            // Move the player
            transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);

            // Calculate the rotation direction
            Quaternion targetRotation = Quaternion.LookRotation(movement);

            // Smoothly rotate the player towards the movement direction
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}