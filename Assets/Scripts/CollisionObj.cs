using UnityEngine;
using System.Collections;

public class CollisionObj : MonoBehaviour {

    //public int LayerOnEnter; // ObjInHole
    //public int LayerOnExit;  // ObjOnTable
	
    void OnTriggerEnter(Collider other)
    {
        print("OnTriggerEnter");
        if (other.gameObject.tag == "Enemy")
        {
            print("Collider with Enemy");
            //other.gameObject.layer = LayerOnEnter;
        }
    }

    //void OnTriggerExit(Collider other)
    //{
    //    if(other.gameObject.tag == "Enemy")
    //    {
    //        other.gameObject.layer = LayerOnExit;
    //    }
    //}
}