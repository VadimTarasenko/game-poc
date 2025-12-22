using UnityEngine;
using System.Collections.Generic;

public class HeroInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private float interactionRadius = 3f;
    [SerializeField] private LayerMask interactableLayer;

    private HashSet<IInteractable> interactablesInRange = new HashSet<IInteractable>();
    private SphereCollider interactionTrigger;

    private void Start()
    {
        SetupInteractionTrigger();
    }

    private void SetupInteractionTrigger()
    {
        // Create a trigger collider for interaction detection
        interactionTrigger = gameObject.AddComponent<SphereCollider>();
        interactionTrigger.radius = interactionRadius;
        interactionTrigger.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null && !interactablesInRange.Contains(interactable))
        {
            interactablesInRange.Add(interactable);
            interactable.OnHeroEnterRange(gameObject);
            Debug.Log($"Hero entered range of {other.gameObject.name}");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null && interactablesInRange.Contains(interactable))
        {
            interactablesInRange.Remove(interactable);
            interactable.OnHeroExitRange(gameObject);
            Debug.Log($"Hero exited range of {other.gameObject.name}");
        }
    }

    public IInteractable GetClosestInteractable()
    {
        IInteractable closest = null;
        float closestDistance = float.MaxValue;

        foreach (IInteractable interactable in interactablesInRange)
        {
            if (interactable != null)
            {
                float distance = Vector3.Distance(transform.position, interactable.GetTransform().position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = interactable;
                }
            }
        }

        return closest;
    }

    public HashSet<IInteractable> GetInteractablesInRange()
    {
        return new HashSet<IInteractable>(interactablesInRange);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }

    private void OnValidate()
    {
        // Update trigger radius when changed in inspector
        if (interactionTrigger != null)
        {
            interactionTrigger.radius = interactionRadius;
        }
    }
}
