using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoAnimation : MonoBehaviour {

    public GameObject dustEff;

	void InitDust()
    {
        Instantiate(dustEff, new Vector3(27.57f, 0.065f, -8.4f), Quaternion.Euler(-90f, 90f, 0f));
    }
}
