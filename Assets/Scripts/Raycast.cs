using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Raycast : MonoBehaviour {

    private GameObject raycastedObj;

    [SerializeField] private int rayLengt = 10;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private Image uiCrosshair;
    [SerializeField] private Inventory inventory;
    public GameObject minimap;

    void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if(Physics.Raycast(transform.position, fwd, out hit, rayLengt, layerMaskInteract.value))
        //if (Physics.SphereCast(transform.position, 50, fwd, out hit, 0, layerMaskInteract.value))
        {
            if (hit.collider.CompareTag("InteractableObject")) 
            {
                GameObject.Find("FPSController").GetComponent<FirstPersonController>().enabled = false;
                if(raycastedObj != null && raycastedObj != hit.collider.gameObject)
                {
                    CrossHairNormal();
                }
                raycastedObj = hit.collider.gameObject;
                while(raycastedObj.transform.parent != null)
                {
                    raycastedObj = raycastedObj.transform.parent.gameObject;
                }
                InteractableObject interactable = raycastedObj.GetComponent<InteractableObject>();
                if(interactable.dependencies.TrueForAll(d => inventory.ContainsItem(d)))
                {
                    crosshairActive(); // sita funkcija kvieciama, kai paziurim i objekta, turinti InteractableObject tag'a

                    if (Input.GetKeyDown("e"))
                    {
                        Debug.Log("I HAVE INTERACTED WITH AN OBJECT");

                        foreach(var d in interactable.dependencies)
                        {
                            inventory.RemoveItem(d);
                        }

                        raycastedObj.SetActive(false);
                        inventory.AddItem(interactable);
                        //Destroy(raycastedObj);
                        if(raycastedObj.name == "map")
                        {
                            inventory.RemoveItem(interactable);
                            minimap.SetActive(true);
                        }
                    }
                }
                else
                {
                    CrossHairNormal();
                }
            }
        }
        else
        {
            CrossHairNormal(); // sita funkcija kvieciama kai neziurim
        }
    }

    void crosshairActive()
    {
        uiCrosshair.color = Color.red;
        if(raycastedObj != null)
        {
            ChangeObjectColor(Color.cyan);
        }
    }

    void CrossHairNormal()
    {
        uiCrosshair.color = Color.white;
        if (raycastedObj != null)
        {
            ChangeObjectColor(Color.white);
        }
    }

    void ChangeObjectColor(Color color)
    {
        if (raycastedObj.GetComponent<Renderer>() == null)
        {
            foreach (var r in raycastedObj.GetComponentsInChildren<Renderer>())
            {
                r.material.color = color;
            }
        }
        else
        {
            raycastedObj.GetComponent<Renderer>().material.color = color; // kai ziurim, pakeiciu objekto spalva i raudona
        }
    }
}
