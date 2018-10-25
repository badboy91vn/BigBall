using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController1 : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;
    public bool useOffsetValue;
    public float rotationSpeed;
    public Transform pivot;

    // Use this for initialization
    void Start()
    {
        if (!useOffsetValue)
        {
            offset = target.position - transform.position;
        }

        pivot.transform.position = target.transform.position;
        pivot.transform.parent = null;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        pivot.transform.position = target.transform.position;

        // Get x position of the mouse and rotate the target
        float horizontal = Input.GetAxis("Mouse X") * rotationSpeed;
        pivot.Rotate(0, horizontal, 0);

        // Get x position of the mouse and rotate the pivot
        float vertical = Input.GetAxis("Mouse Y") * rotationSpeed;
        pivot.Rotate(-vertical, 0, 0);

        // Limnit up/down pivot
        if (pivot.rotation.eulerAngles.x > 45f && pivot.rotation.eulerAngles.x < 100f)
        {
            pivot.rotation = Quaternion.Euler(45f, 0, 0);
        }
        if (pivot.rotation.eulerAngles.x > 100f && pivot.rotation.eulerAngles.x < 315f)
        {
            pivot.rotation = Quaternion.Euler(315f, 0, 0);
        }

        // Move the camera based on the target and offet
        float desiredYAngle = pivot.eulerAngles.y;
        float desiredXAngle = pivot.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = target.position - (rotation * offset);

        if (transform.position.y < target.position.y)
        {
            transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
        }

        //transform.position = target.position - offset;
        transform.LookAt(target);
    }
}
