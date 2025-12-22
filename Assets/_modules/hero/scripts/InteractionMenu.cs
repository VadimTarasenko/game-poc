using UnityEngine;
using UnityEngine.UI;

public class InteractionMenu : MonoBehaviour
{
    [Header("Menu Settings")]
    [SerializeField] private bool lookAtCamera = true;
    [SerializeField] private bool autoAssignButtons = true;

    [Header("Button Names (optional - for auto-assignment)")]
    [SerializeField] private string openButtonName = "Open";
    [SerializeField] private string closeButtonName = "CloseButton";

    private Camera mainCamera;
    private ChestInteraction parentChest;

    private void Start()
    {
        mainCamera = Camera.main;

        if (autoAssignButtons)
        {
            AutoAssignButtonListeners();
        }
    }

    private void Update()
    {
        if (lookAtCamera && mainCamera != null)
        {
            transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
                           mainCamera.transform.rotation * Vector3.up);
        }
    }

    private void AutoAssignButtonListeners()
    {
        // Find all buttons in children
        Button[] buttons = GetComponentsInChildren<Button>();

        foreach (Button button in buttons)
        {
            // Check by name
            if (button.gameObject.name.Equals(openButtonName, System.StringComparison.OrdinalIgnoreCase))
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(OnOpenChest);
                Debug.Log($"Auto-assigned OnOpenChest to button: {button.gameObject.name}");
            }
            if (button.gameObject.name.Equals(closeButtonName, System.StringComparison.OrdinalIgnoreCase))
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(OnCloseMenu);
                Debug.Log($"Auto-assigned OnCloseMenu to button: {button.gameObject.name}");
            }
            // Also check for common variations
            if (button.gameObject.name.Contains("Open", System.StringComparison.OrdinalIgnoreCase))
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(OnOpenChest);
                Debug.Log($"Auto-assigned OnOpenChest to button: {button.gameObject.name}");
            }
            if (button.gameObject.name.Contains("Close", System.StringComparison.OrdinalIgnoreCase) ||
                     button.gameObject.name.Contains("Cancel", System.StringComparison.OrdinalIgnoreCase))
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(OnCloseMenu);
                Debug.Log($"Auto-assigned OnCloseMenu to button: {button.gameObject.name}");
            }
        }
    }

    public void SetParentChest(ChestInteraction chest)
    {
        parentChest = chest;
    }

    public void OnOpenChest()
    {
        if (parentChest != null)
        {
            parentChest.OpenChest();
        }
        else
        {
            Debug.LogWarning("InteractionMenu: No parent chest assigned!");
        }
    }

    public void OnCloseMenu()
    {
        Destroy(gameObject);
    }
}
