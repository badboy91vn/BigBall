using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveObj : MonoBehaviour
{
    private GameManager gm;
    private HoleController holeController;

    void Start()
    {
        GameObject gameManager = GameObject.Find("GameManager");
        gm = gameManager.GetComponent<GameManager>();
        holeController = gameObject.GetComponentInParent<HoleController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            gm.IncreaseScore(holeController.GetName());
        }
    }
}
