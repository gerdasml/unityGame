using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Raycast : MonoBehaviour {

    private GameObject raycastedObj;

    [SerializeField] private int rayLengt = 10;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private Image uiCrosshair;
    [SerializeField] private Inventory inventory;

    void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if(Physics.Raycast(transform.position, fwd, out hit, rayLengt, layerMaskInteract.value))
        //if (Physics.SphereCast(transform.position, 50, fwd, out hit, 0, layerMaskInteract.value))
        {
            if (hit.collider.CompareTag("InteractableObject")) 
            {
                raycastedObj = hit.collider.gameObject;
                crosshairActive(); // sita funkcija kvieciama, kai paziurim i objekta, turinti InteractableObject tag'a

                if (Input.GetKeyDown("e"))
                {
                    Debug.Log("I HAVE INTERACTED WITH AN OBJECT");
                    raycastedObj.SetActive(false);
                    var shit = raycastedObj.GetComponent<InteractableObject>();
                    var item = new Item();
                    item.sprite = shit.sprite;
                    inventory.AddItem(item);
                    //Destroy(raycastedObj);
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
            raycastedObj.GetComponent<Renderer>().material.color = Color.cyan; // kai ziurim, pakeiciu objekto spalva i raudona
        }
    }

    void CrossHairNormal()
    {
        uiCrosshair.color = Color.white;
        if (raycastedObj != null)
        {
            raycastedObj.GetComponent<Renderer>().material.color = Color.white; // kitu atveju pakeiciu i balta (pasirodo tai tas pats kaip neturet spalvos kazkodel)
        }
    }
}
