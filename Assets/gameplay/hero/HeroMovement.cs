// 12/2/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;
using UnityEngine.AI;

public class HeroMovement : MonoBehaviour
{
    Camera mainCamera;
    NavMeshAgent agent;
    Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component is missing on the GameObject.");
        }

        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("No camera tagged as 'MainCamera' found in the scene.");
        }

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component is missing on the GameObject.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            
            if(Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                agent.SetDestination(hit.point);
            }
        }

        // Check if the agent is moving
        if (agent.velocity.magnitude > 0.1f && agent.remainingDistance > agent.stoppingDistance)
        {
            animator.SetBool("isWalking", true); // Trigger walking animation
        }
        else
        {
            animator.SetBool("isWalking", false); // Stop walking animation
        }
    }
}