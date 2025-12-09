// 12/9/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;
using UnityEngine.EventSystems;

public class Controller : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private RectTransform rectTransform; // Reference to the RectTransform of the controller
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
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData); // Start tracking input when the finger touches the screen
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 touchPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out touchPosition);

        // Calculate the direction relative to the center
        InputDirection = (touchPosition - (Vector2)rectTransform.rect.center).normalized;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InputDirection = Vector2.zero; // Reset direction when the finger is lifted
    }
}