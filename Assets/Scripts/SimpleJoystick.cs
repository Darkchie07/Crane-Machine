using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SimpleJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image joystickBackground;
    private Image joystickHandle;
    private Vector3 inputDirection;

    private void Start()
    {
        joystickBackground = GetComponent<Image>();
        joystickHandle = transform.GetChild(0).GetComponent<Image>();
        inputDirection = Vector3.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackground.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
        {
            pos.x = (pos.x / joystickBackground.rectTransform.sizeDelta.x);
            pos.y = (pos.y / joystickBackground.rectTransform.sizeDelta.y);

            // Determine the major input direction (up, down, left, or right)
            if (Mathf.Abs(pos.x) > Mathf.Abs(pos.y))
            {
                inputDirection = new Vector3(pos.x, 0, 0);
            }
            else
            {
                inputDirection = new Vector3(0, 0, pos.y);
            }

            // Apply a dead zone
            float deadZone = 0.1f; // Adjust this value to your preference
            if (inputDirection.magnitude < deadZone)
            {
                inputDirection = Vector3.zero;
            }

            joystickHandle.rectTransform.anchoredPosition = new Vector3(inputDirection.x * (joystickBackground.rectTransform.sizeDelta.x / 3), inputDirection.z * (joystickBackground.rectTransform.sizeDelta.y / 3));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputDirection = Vector3.zero;
        joystickHandle.rectTransform.anchoredPosition = Vector3.zero;
    }

    public float Horizontal()
    {
        return inputDirection.x;
    }

    public float Vertical()
    {
        return inputDirection.z;
    }
}