using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualJoystickController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Color normalColor;
    [SerializeField] private Color pressedColor;

    [SerializeField] private float joystickDistance = 100f;
    
    [SerializeField] Image joystickContainer;
    [SerializeField] Image joystickBorder;
    [SerializeField] Image joystickCenter;

    public bool useSimulate = false;

    private Vector2 joystickValue;
    public Vector2 JoystickValue { get { return joystickValue; } }

    public void OnDrag(PointerEventData eventData)
    {
        bool isTouched = RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickContainer.rectTransform,
                                                                                 eventData.position,
                                                                                 eventData.pressEventCamera,
                                                                                 out Vector2 touchPosition);

        if (isTouched)
        {
            touchPosition.x = touchPosition.x / joystickContainer.rectTransform.sizeDelta.x;
            touchPosition.y = touchPosition.y / joystickContainer.rectTransform.sizeDelta.y;

            Vector2 pivot = Vector2.one * 0.5f;
            Vector2 joystickContainerPivot = joystickContainer.rectTransform.pivot;

            touchPosition.x += joystickContainerPivot.x - pivot.x;
            touchPosition.y += joystickContainerPivot.y - pivot.y;

            float x = Mathf.Clamp(touchPosition.x, -1f, 1f);
            float y = Mathf.Clamp(touchPosition.y, -1f, 1f);

            joystickValue = new Vector2(x, y);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        joystickBorder.color = pressedColor;
        joystickCenter.color = pressedColor;

        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        joystickBorder.color = normalColor;
        joystickCenter.color = normalColor;

        joystickValue = Vector2.zero;
    }

    private void Update()
    {
#if UNITY_EDITOR
        EditorMove();
#endif

        joystickCenter.rectTransform.anchoredPosition = new Vector2(joystickValue.x * joystickDistance,
                                                                    joystickValue.y * joystickDistance);
    }

    public void EditorMove()
    {
        if (!useSimulate) return;

        float moveX = 0;
        float moveY = 0;

        if(Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            moveX = 1f;
        }

        if (Input.GetKey(KeyCode.W))
        {
            moveY = 1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveY = -1f;
        }

        joystickValue = new Vector2 (moveX, moveY);
    }
}
