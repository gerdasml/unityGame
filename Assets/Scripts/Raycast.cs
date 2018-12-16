using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Raycast : MonoBehaviour {

    private GameObject raycastedObj;

    [SerializeField] private int rayLengt = 10;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private Image uiCrosshair;

    void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if(Physics.Raycast(transform.position, fwd, out hit, rayLengt, layerMaskInteract.value))
        {
            if (hit.collider.CompareTag("Object"))
            {
                raycastedObj = hit.collider.gameObject;
                crosshairActive();

                if (Input.GetKeyDown("e"))
                {
                    Debug.Log("I HAVE INTERACTED WITH AN OBJECT");
                    raycastedObj.SetActive(false);
                }
            }
        }
        else
        {
            CrossHairNormal();
        }
    }

    void crosshairActive()
    {
        uiCrosshair.color = Color.red;
    }

    void CrossHairNormal()
    {
        uiCrosshair.color = Color.white;
    }
}
