using UnityEngine;

public class Constants: MonoBehaviour
{
    public static Constants Instance { get; private set; }


    // Luu local
    public string HIGHSCORE = "HIGHSCORE";
    public string CURRENTBALL = "CURRENTBALL";  // Ball user hien tai
    public string COIN = "COIN";

    // Use this for initialization
    void Awake()
    {
        // Init GameManager
        if (Instance == null)
        {
            print("Create Constants");
            Instance = this;
        }
        else
        {
            print("Destroy Constants");
            Destroy(gameObject); // dont allow 2 GameManager
        }

    }
}