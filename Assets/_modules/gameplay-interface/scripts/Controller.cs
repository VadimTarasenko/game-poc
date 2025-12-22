// 12/9/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;
using UnityEngine.EventSystems;

public class Controller : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public RectTransform joystickBg; // Reference to the joystick background
    public float maxBgShift = 50f; // Maximum distance the background can move from its initial position
    public float smoothSpeed = 15f; // Speed of smooth interpolation (higher = faster)

    private RectTransform rectTransform; // Reference to the RectTransform of the controller
    private Vector2 initialBgPosition; // Initial position of the joystick background
    private Vector2 targetBgPosition; // Target position for smooth interpolation
    private Vector2 centerPosition; // Center position of the controller
    public Vector2 InputDirection { get; private set; } // Direction of input

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        // Calculate the center position of the controller
        centerPosition = rectTransform.position;

        // Store the initial position of the joystick background
        if (joystickBg != null)
        {
            initialBgPosition = joystickBg.anchoredPosition;
            targetBgPosition = initialBgPosition;
        }
    }

    private void Update()
    {
        // Smoothly interpolate the joystick background to the target position
        if (joystickBg != null)
        {
            joystickBg.anchoredPosition = Vector2.Lerp(
                joystickBg.anchoredPosition,
                targetBgPosition,
                Time.deltaTime * smoothSpeed
            );
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
        OnDrag(eventData); // Start tracking input when the finger touches the screen
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 touchPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out touchPosition);

        // Calculate the offset from the center
        Vector2 offset = touchPosition - (Vector2)rectTransform.rect.center;

        // Calculate the direction relative to the center
        InputDirection = offset.normalized;

        // Set the target position for the joystick background (will be smoothly interpolated in Update)
        if (joystickBg != null)
        {
            // Calculate how much the background should move
            Vector2 bgOffset = Vector2.ClampMagnitude(offset, maxBgShift);
            targetBgPosition = initialBgPosition + bgOffset;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InputDirection = Vector2.zero; // Reset direction when the finger is lifted

        // Set the target to return the joystick background to its initial position
        if (joystickBg != null)
        {
            targetBgPosition = initialBgPosition;
        }
    }
}