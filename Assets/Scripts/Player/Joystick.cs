using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private RectTransform background;
    [SerializeField] private RectTransform handle;
    [SerializeField] private float handleRange = 1f;
    
    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }
    public Vector2 Direction => new Vector2(Horizontal, Vertical).normalized;
    
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position = RectTransformUtility.WorldToScreenPoint(null, background.position);
        Vector2 radius = background.sizeDelta / 2;
        
        Vector2 input = (eventData.position - position) / (radius * handleRange);
        input = Vector2.ClampMagnitude(input, 1f);
        
        Horizontal = input.x;
        Vertical = input.y;
        
        handle.anchoredPosition = input * radius * handleRange;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        Horizontal = Vertical = 0f;
        handle.anchoredPosition = Vector2.zero;
    }
}