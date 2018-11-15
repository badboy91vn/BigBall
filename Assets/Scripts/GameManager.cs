using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Transform listSpawnPoint;
    public TextMeshProUGUI txtCountDown;
    public int countDown = 3;
    //public TextMeshProUGUI txtScore;

    [SerializeField]
    public GameObject[] ballPrefab;

    private List<GameObject> listBall = new List<GameObject>();

    private int score;
    public bool gameRun;

    // Use this for initialization
    void Awake()
    {
        // Init GameManager
        if (Instance == null)
        {
            print("Create GameManager");
            Instance = this;
        }
        else
        {
            print("Destroy GameManager");
            Destroy(gameObject); // dont allow 2 GameManager
        }

        gameRun = false;
        score = 0;

        // Get one Random Spawn Point (Chon 1 trong 4 point trong scene)
        List<Transform> listPoint = SuffleList(listSpawnPoint);
        Transform pointSpawn = listPoint[Random.Range(0, listPoint.Count)];
        //print("Root Point: " + pointSpawn.position);

        // Get List point trong point o tren
        List<Transform> listPosition = SuffleList(pointSpawn);
        //foreach (Transform obj in listPosition)
        //{
        //    print("Child Point: " + obj.position);
        //}
        //print("Point Spawn: " + pointSpawn);

        // Spawn Ball
        int curBall = PlayerPrefs.GetInt(Constants.Instance.CURRENTBALL);
        for (int i = 0; i < 1; i++)
        {
            int ranBall = Random.Range(0, ballPrefab.Length);
            GameObject gameobj;

            // Add Tag name to attach camera
            Vector3 posSpawn;
            if (i == 0)
            {
                gameobj = Instantiate(ballPrefab[curBall], Vector3.zero, Quaternion.identity) as GameObject;
                gameobj.tag = "MainPlayer";
                posSpawn = new Vector3(pointSpawn.position.x, ballPrefab[curBall].transform.position.y, pointSpawn.position.z);
            }
            else
            {
                gameobj = Instantiate(ballPrefab[ranBall], Vector3.zero, Quaternion.identity) as GameObject;
                posSpawn = new Vector3(listPosition[i - 1].position.x, ballPrefab[ranBall].transform.position.y, listPosition[i - 1].position.z);
                Destroy(gameobj.GetComponent<BallController>());
            }
            gameobj.GetComponent<BallController>().SetName(i);
            gameobj.transform.position = posSpawn;

            listBall.Add(gameobj);
        }

        //listBall = GameObject.FindGameObjectsWithTag("Player");
        //for (int i = 0; i < listBall.Length; i++)
        //{
        //    listBall[i].GetComponent<HoleController>().SetName(i);
        //    //Debug.Log("Player Number " + i + " is named " + listBall[i].name);
        //}

        // Add gravity for Obj in City
        foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject)))
        {
            Rigidbody objRigi = obj.GetComponent<Rigidbody>();
            if (objRigi)
            {
                //objRigi.Sleep();
                objRigi.useGravity = true;
            }
        }

        //StartCoroutine(AddScore(0));
    }

    void Start()
    {
        txtCountDown.text = countDown.ToString();
        StartCoroutine(ChangeTextCountDown());

        //PlayerPrefs.DeleteKey(Constants.Instance.HIGHSCORE);
        //PlayerPrefs.DeleteKey(Constants.Instance.CURRENTBALL);
        //PlayerPrefs.DeleteKey(Constants.Instance.COIN);

        int highScore = PlayerPrefs.GetInt(Constants.Instance.HIGHSCORE);
        print("Highscore: " + highScore);

        int curBall = PlayerPrefs.GetInt(Constants.Instance.CURRENTBALL);
        print("CurBall: " + curBall);

        int coin = PlayerPrefs.GetInt(Constants.Instance.COIN);
        print("Coin: " + coin);

        //PlayerPrefs.SetInt("HighScore", 5);
        //PlayerPrefs.DeleteKey("HighScore");
    }

    public void IncreaseScore(int index)
    {
        score++;
        //txtScore.text = "Score: " + score;

        BallController hole = listBall[index].GetComponentInChildren<BallController>();

        if (score >= (10 * hole.GetCurHoleLevel() + 10))
        {
            print("LevelUp");
            hole.HoleLevelUp();
        }
    }

    List<Transform> SuffleList(Transform list)
    {
        List<Transform> listPosition = new List<Transform>();
        foreach (Transform obj in list)
        {
            listPosition.Add(obj);
        }

        for (int i = 0; i < listPosition.Count; i++)
        {
            Transform temp = listPosition[i];
            int randomIndex = Random.Range(i, listPosition.Count);
            listPosition[i] = listPosition[randomIndex];
            listPosition[randomIndex] = temp;
        }

        return listPosition;
    }

    IEnumerator ChangeTextCountDown()
    {
        yield return new WaitForSeconds(1f);

        if (countDown <= 0)
        {
            txtCountDown.text = "Start Game !!!";
            txtCountDown.fontSize = 40;
            Invoke("StartGame", 0.5f);
        }
        else
        {
            countDown--;
            txtCountDown.text = countDown.ToString();
            StartCoroutine(ChangeTextCountDown());
        }
    }

    void StartGame()
    {
        print("Start Game");
        gameRun = true;
        txtCountDown.gameObject.SetActive(false);
        gameObject.GetComponent<CountdownTimer>().StartTimer();
    }

    public void GameOver()
    {
        gameRun = false;
        foreach (GameObject obj in listBall)
        {
            Rigidbody ballRigi = obj.GetComponent<Rigidbody>();
            ballRigi.velocity = Vector3.zero;
            ballRigi.angularVelocity = Vector3.zero;
            ballRigi.Sleep();
        }

        print("Gameover!!!");

        if (PlayerPrefs.GetInt(Constants.Instance.HIGHSCORE) > score)
        {
            PlayerPrefs.SetInt(Constants.Instance.HIGHSCORE, score);
        }
    }
}