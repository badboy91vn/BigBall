using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 offset;

    private Transform target;

    // Use this for initialization
    void Start()
    {
        //target = GameObject.FindGameObjectWithTag("MainPlayer").GetComponent<Transform>().GetChild(0).transform;
        target = GameObject.FindGameObjectWithTag("MainPlayer").GetComponent<Transform>().transform;
        ResetCamera();
        //offset = target.position - transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!target) { return; }

        // Move the camera based on the target and offet
        if (transform.position.y < target.position.y)
        {
            transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
        }

        transform.position = target.position - offset;
    }

    public void ChangeOffset(int curholeLevel)
    {
        if (curholeLevel == 2)
        {
            offset = offset + new Vector3(0, -1f, .5f);
        }
        else
        {
            offset = offset + new Vector3(0, -.65f, .3f);
        }
    }

    public void ResetCamera()
    {
        transform.position = target.position;
        transform.position = transform.position - new Vector3(0f, -4f, 5.0f);
        transform.rotation = Quaternion.Euler(new Vector3(40f, 0f, 0f));

        offset = target.position - transform.position;
    }
}
