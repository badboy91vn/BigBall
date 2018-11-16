using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class BallController : MonoBehaviour
{
    public float moveSpeed;
    public Camera ballCamera;

    // Properties Ball
    private GameManager gm;
    private int curHoleLevel;
    private int holeName;
    private Rigidbody ballRigi;

    private CameraController camController;

    // Use this for initialization
    void Start()
    {
        GameObject gameObj = GameObject.Find("Main Camera");
        ballCamera = gameObj.GetComponentInChildren<Camera>();
        camController = ballCamera.GetComponent<CameraController>();

        GameObject gameManager = GameObject.Find("GameManager");
        gm = gameManager.GetComponent<GameManager>();

        ResetGame();

        //Mesh mesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
        //float volume = VolumeOfMesh(mesh);
        //Debug.Log("The volume of 'Ball' is " + volume + " cube units.");

        ballRigi = gameObject.GetComponent<Rigidbody>();

        //StartCoroutine(TestChangeBallSize());
    }

    IEnumerator TestChangeBallSize()
    {
        yield return new WaitForSeconds(.5f);

        //ChangeBallSize();

        StartCoroutine(TestChangeBallSize());
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.gameRun) return;

        float hoz = CrossPlatformInputManager.GetAxis("Horizontal");
        float ver = CrossPlatformInputManager.GetAxis("Vertical");

        if (hoz == 0 && ver == 0)
        {
            ballRigi.velocity = Vector3.zero;
            ballRigi.angularVelocity = Vector3.zero;
            ballRigi.Sleep();
        }

        ballRigi.velocity = new Vector3(hoz * moveSpeed * Time.deltaTime, ballRigi.velocity.y, ver * moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision colObj)
    {
        if (colObj.gameObject.tag == "Enemy")
        {
            //Mesh meshB = gameObject.GetComponent<MeshFilter>().sharedMesh;
            //float volumeB = VolumeOfMesh(meshB);
            //Debug.Log("The volume of 'Ball' is " + volumeB + " cube units.");

            //Mesh mesh = colObj.gameObject.GetComponent<MeshFilter>().sharedMesh;
            //float volume = VolumeOfMesh(mesh);
            //Debug.Log("The volume of '" + colObj.gameObject.name + "' is " + volume + " units.");


            // get the volume from the bounds
            Vector3 colObjSize = colObj.gameObject.GetComponent<Collider>().bounds.size;
            var addVolume = colObjSize.x * colObjSize.y * colObjSize.z;
            // get the current radius
            var radius = transform.localScale.y;
            // now figure volume of the sphere
            var ballVol = 4.19f * radius * radius * radius;
            // now add the mass of the cube
            ballVol += addVolume;
            // now reverse the calculation for the radius from the volume
            radius = Mathf.Sqrt(ballVol / (4 / 3 * Mathf.PI));

            //print("Radius: " + radius + " | Add: " + Vector3.one * radius);
            //transform.localScale = Vector3.one * radius;


            float ballSize = gameObject.GetComponent<Renderer>().bounds.size.y;
            float vBall = 4.19f * ballSize * ballSize * ballSize;

            float objSize = colObj.gameObject.GetComponent<Renderer>().bounds.size.y;
            float vObj = 4.19f * objSize * objSize * objSize; //objSize.x * objSize.y * objSize.z;

            float ratio = (vBall * 100) / vObj;
            print("BallY: " + ballSize + " | ObjY: " + objSize);
            //print("Ball: " + vBall + " | " + colObj.gameObject.name + ": " + vObj + " | Ratio:" + ratio);

            // Check ty le phai > 50%
            if (ratio <= 50) { return; }

            string parentName = colObj.transform.parent.name;
            if (parentName == "Cars")
            {
                print("Car: " + colObj.transform.name);
            }
            else if (parentName == "Props")
            {
                print("Props: " + colObj.transform.name);
            }
            else
            {
                print("Building: " + colObj.transform.parent.name);
            }

            Vector3 sizeIncrease = Vector3.zero;
            Vector3 camPosIncrease = Vector3.zero;
            if (ratio > 50 && ratio <= 60)
            {
                //print("51 -> 60: " + colObj.gameObject.name);
                sizeIncrease = new Vector3(.2f, .2f, .2f);
                camPosIncrease = new Vector3(.2f, .2f, .2f);
            }
            else if (ratio > 60 && ratio <= 80)
            {
                //print("61 -> 80: " + colObj.gameObject.name);
                sizeIncrease = new Vector3(.5f, .5f, .5f);
                camPosIncrease = new Vector3(.2f, .2f, .2f);
            }
            else if (ratio > 81 && ratio <= 100)
            {
                //print("81 -> 100: " + colObj.gameObject.name);
                sizeIncrease = new Vector3(.7f, .7f, .7f);
                camPosIncrease = new Vector3(.2f, .2f, .2f);
            }
            else if (ratio > 100)
            {
                //print("> 100: " + colObj.gameObject.name);
                sizeIncrease = new Vector3(.9f, .9f, .9f);
                camPosIncrease = new Vector3(.2f, .2f, .2f);
            }

            //// Change Ball Size
            //ChangeBallSize(sizeIncrease, camPosIncrease);

            //// Change Score
            //gm.IncreaseScore(GetName());

            //// Change status collider
            //colObj.rigidbody.useGravity = false;
            //colObj.rigidbody.isKinematic = true;
            //Destroy(colObj.collider);

            //// Add obj collider to ball
            //colObj.transform.parent = transform;
        }
    }

    void ChangeBallSize(Vector3 sizeIncrease, Vector3 camPosIncrease)
    {
        int ran = Random.Range(0, 1);
        //print("Change Ball: " + ran);
        transform.localScale = transform.localScale + sizeIncrease;
        camController.ChangeOffset(camPosIncrease);
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

        //// Tang view camera
        //camController.ChangeOffset(curHoleLevel);
    }

    void ResetGame()
    {
        curHoleLevel = 1;
        //transform.position = new Vector3(0, .08f, 0);
        //transform.localScale = new Vector3(.2f, 1, .2f);

        //camController.ResetCamera();
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

    // Calcutate Mesh Volume Obj
    float SignedVolumeOfTriangle(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float v321 = p3.x * p2.y * p1.z;
        float v231 = p2.x * p3.y * p1.z;
        float v312 = p3.x * p1.y * p2.z;
        float v132 = p1.x * p3.y * p2.z;
        float v213 = p2.x * p1.y * p3.z;
        float v123 = p1.x * p2.y * p3.z;
        return (1.0f / 6.0f) * (-v321 + v231 + v312 - v132 - v213 + v123);
    }
    float VolumeOfMesh(Mesh mesh)
    {
        float volume = 0;
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;
        for (int i = 0; i < mesh.triangles.Length; i += 3)
        {
            Vector3 p1 = vertices[triangles[i + 0]];
            Vector3 p2 = vertices[triangles[i + 1]];
            Vector3 p3 = vertices[triangles[i + 2]];
            volume += SignedVolumeOfTriangle(p1, p2, p3);
        }
        return Mathf.Abs(volume);
    }
}
