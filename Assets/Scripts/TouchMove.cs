using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchMove : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    //private Image bgIMG;
    private Vector3 inputVector;
    //private float width;
    //private float height;

    private Vector2 startPoint;

    private void Start()
    {
        //width = 150f; //Screen.width;
        //height = 150f; //Screen.height;

        //bgIMG = GetComponent<Image>();
    }

    public void OnDrag(PointerEventData ped)
    {
        Vector2 pos = ped.position-startPoint;
        Debug.DrawLine(startPoint, ped.position, Color.white, 1f);
        inputVector = new Vector3(pos.x, 0, pos.y);
        inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;
    }

    public void OnPointerDown(PointerEventData ped)
    {
        startPoint = ped.position;
    }

    public void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector3.zero;
        startPoint = Vector2.zero;
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
