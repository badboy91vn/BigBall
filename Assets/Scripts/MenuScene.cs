using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
        // Add gravity for Obj in City
        foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject)))
        {
            Rigidbody objRigi = obj.GetComponent<Rigidbody>();
            if (objRigi)
            {
                //objRigi.Sleep();
                objRigi.useGravity = false;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BtnPlay()
    {
        print("Game Scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
