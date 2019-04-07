using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupableObject : MonoBehaviour {
    public bool isBeingCarried;
    GameObject mainCamera;
    float distance = 1;

    // Use this for initialization
    void Start () {
        mainCamera = GameObject.FindWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
        if (isBeingCarried)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.transform.position = mainCamera.transform.position + mainCamera.transform.forward * distance;
        } else
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
	}
}
