using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    public Joystick joystick;
    public TouchMove touchMove;
    public float moveSpeed;

    public Camera holeCam;

    // Properties Hole
    private int curHoleLevel;
    private int holeName;

    // Use this for initialization
    void Start()
    {
        GameObject gameObj = GameObject.Find("Joystick");
        joystick = gameObj.GetComponentInChildren<Joystick>();
        touchMove = gameObj.GetComponentInChildren<TouchMove>();

        holeCam = gameObject.GetComponentInChildren<Camera>();

        ResetGame();

        //Invoke("increaseScale", 2);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(joystick.Horizontal() * moveSpeed * Time.deltaTime, 0f, joystick.Vertical() * moveSpeed * Time.deltaTime);
        transform.Translate(touchMove.Horizontal() * moveSpeed * Time.deltaTime, 0f, touchMove.Vertical() * moveSpeed * Time.deltaTime);
    }

    public void HoleLevelUp()
    {
        if (curHoleLevel == 18) { return; }

        if (curHoleLevel == 1)
        {
            transform.localScale = transform.localScale + new Vector3(.2f, 0, .2f);
        }
        else
        {
            transform.localScale = transform.localScale + new Vector3(.15f, 0, .15f);
        }

        // Tang hole level
        curHoleLevel++;
        print(curHoleLevel);

        // Tang view camera
        CameraController camController = holeCam.GetComponent<CameraController>();
        camController.ChangeOffset(curHoleLevel);
    }

    void ResetGame()
    {
        curHoleLevel = 1;
        transform.position = new Vector3(0, .08f, 0);
        transform.localScale = new Vector3(.2f, 1, .2f);
    }

    public void SetName(int objName)
    {
        holeName = objName;
    }

    public int GetName()
    {
        return holeName;
    }

    public int GetCurHoleLevel()
    {
        return curHoleLevel;
    }
}
