using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{

    private Image bgIMG;
    private Image joystickIMG;
    private Vector3 inputVector;

    private float bgImgW;
    private float bgImgH;

    private void Start()
    {
        bgIMG = GetComponent<Image>();
        joystickIMG = transform.GetChild(0).GetComponent<Image>();

        bgImgW = bgIMG.rectTransform.sizeDelta.x;
        bgImgH = bgIMG.rectTransform.sizeDelta.y;
    }

    public void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgIMG.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            //print("POS1: " + pos.x + " " + pos.y);
            pos.x = pos.x / bgImgW;
            pos.y = pos.y / bgImgH;
            //print("POS2: " + pos.x + " " + pos.y);

            inputVector = new Vector3(pos.x * 2 + 1, 0, pos.y * 2 - 1);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            // Move Joystick IMG
            joystickIMG.rectTransform.anchoredPosition = new Vector3(inputVector.x * (bgImgW / 3), inputVector.z * (bgImgH / 3));
        }
    }

    public void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector3.zero;
        joystickIMG.rectTransform.anchoredPosition = Vector3.zero;
    }

    public float Horizontal()
    {
        if (inputVector.x != 0)
            return inputVector.x;
        else
            return Input.GetAxis("Horizontal");
    }

    public float Vertical()
    {
        if (inputVector.z != 0)
            return inputVector.z;
        else
            return Input.GetAxis("Vertical");
    }

}
