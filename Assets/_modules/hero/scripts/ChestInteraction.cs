using UnityEngine;
using System.Collections.Generic;

public class ChestInteraction : MonoBehaviour, IInteractable
{
    [Header("Menu Settings")]
    [SerializeField] private GameObject menuPrefab;
    [SerializeField] private Vector3 menuOffset = new Vector3(0, 2, 0);

    [Header("Animation Settings")]
    [SerializeField] private string openAnimationName = "Open";
    [SerializeField] private bool disableInteractionAfterOpen = true;

    private GameObject menuInstance;
    private HashSet<GameObject> heroesInRange = new HashSet<GameObject>();
    private Animator animator;
    private bool isOpened = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogWarning($"ChestInteraction: No Animator found on {gameObject.name}");
        }
    }

    public void OnHeroEnterRange(GameObject hero)
    {
        // Don't show menu if chest is already opened
        if (isOpened && disableInteractionAfterOpen)
        {
            return;
        }

        heroesInRange.Add(hero);
        ShowMenu();
    }

    public void OnHeroExitRange(GameObject hero)
    {
        heroesInRange.Remove(hero);

        // Hide menu only if no heroes are in range
        if (heroesInRange.Count == 0)
        {
            HideMenu();
        }
    }

    public Transform GetTransform()
    {
        return transform;
    }

    private void Update()
    {
        // Keep menu positioned above chest while visible
        if (menuInstance != null)
        {
            menuInstance.transform.position = transform.position + menuOffset;
        }
    }

    private void ShowMenu()
    {
        if (menuPrefab != null && menuInstance == null)
        {
            menuInstance = Instantiate(menuPrefab, transform.position + menuOffset, Quaternion.identity);

            // Set the parent chest reference in the menu
            InteractionMenu menu = menuInstance.GetComponent<InteractionMenu>();
            if (menu != null)
            {
                menu.SetParentChest(this);
            }

            Debug.Log($"Chest menu displayed for {gameObject.name}");
        }
    }

    private void HideMenu()
    {
        if (menuInstance != null)
        {
            Destroy(menuInstance);
            menuInstance = null;
            Debug.Log($"Chest menu hidden for {gameObject.name}");
        }
    }

    private void OnDestroy()
    {
        HideMenu();
    }

    public void OpenChest()
    {
        if (isOpened)
        {
            Debug.Log($"Chest {gameObject.name} is already opened");
            return;
        }

        Debug.Log($"Opening chest: {gameObject.name}");

        // Play open animation
        if (animator != null)
        {
            animator.CrossFade(openAnimationName, 0.1f);
            Debug.Log($"Playing animation: {openAnimationName}");
        }

        isOpened = true;
        HideMenu();

        // Add reward logic here
        // For example: GiveRewards();
    }
}

