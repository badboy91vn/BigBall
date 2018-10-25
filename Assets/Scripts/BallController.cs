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
        ballRigi = GetComponent<Rigidbody>();

        GameObject gameObj = GameObject.Find("Main Camera");
        ballCamera = gameObj.GetComponentInChildren<Camera>();
        camController = ballCamera.GetComponent<CameraController>();

        GameObject gameManager = GameObject.Find("GameManager");
        gm = gameManager.GetComponent<GameManager>();

        ResetGame();

        //StartCoroutine(TestChangeBallSize());
    }

    IEnumerator TestChangeBallSize()
    {
        yield return new WaitForSeconds(.5f);

        ChangeBallSize();

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
            //FixedJoint fixedJoint = col.gameObject.AddComponent<FixedJoint>();
            //fixedJoint.connectedBody = ballRigi;

            //ContactPoint contact = col.contacts[0];
            //Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            //Vector3 pos = contact.point;
            //Instantiate(explosionPrefab, pos, rot);


            Vector3 colObjSize = Vector3.Scale(colObj.transform.localScale, colObj.gameObject.GetComponent<MeshFilter>().mesh.bounds.size);
            print(colObj.gameObject.name+" : "+ colObjSize);

            print("TEst: "+ GetComponent<MeshFilter>().mesh.bounds.size * transform.localScale.x);

            Vector3 ball = Vector3.Scale(transform.localScale, GetComponent<MeshFilter>().mesh.bounds.size);
            print("Ball : " + ball);

            // Change status collider
            colObj.rigidbody.useGravity = false;
            colObj.rigidbody.isKinematic = true;
            Destroy(colObj.collider);

            // Add obj collider to ball
            colObj.transform.parent = transform;

            // Change Score
            gm.IncreaseScore(GetName());

            // Change Ball Size
            ChangeBallSize();
        }
    }

    void ChangeBallSize()
    {
        int ran = Random.Range(0, 1);
        print("Change Ball: "+ ran);
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
        camController.ChangeOffset(curHoleLevel);
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
}
