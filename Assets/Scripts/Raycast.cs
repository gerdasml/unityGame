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
    [SerializeField] private ScreenManager screenManager;
    [SerializeField] private GameObject board;
    [SerializeField] private Text boardUnlockText;
    public GameObject minimap;

    void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, rayLengt, layerMaskInteract.value))
        //if (Physics.SphereCast(transform.position, 50, fwd, out hit, 0, layerMaskInteract.value))
        {
            if (hit.collider.CompareTag("InteractableObject"))
            {
                if (raycastedObj != null && raycastedObj != hit.collider.gameObject)
                {
                    CrossHairNormal();
                }
                raycastedObj = GetTopMostParent(hit.collider.gameObject);
                InteractableObject interactable = raycastedObj.GetComponent<InteractableObject>();
                if (interactable != null && interactable.dependencies.TrueForAll(d => inventory.ContainsItem(d)))
                {
                    crosshairActive(); // sita funkcija kvieciama, kai paziurim i objekta, turinti InteractableObject tag'a

                    if (Input.GetKeyDown("e"))
                    {
                        Debug.Log("I HAVE INTERACTED WITH AN OBJECT");

                        foreach (var d in interactable.dependencies)
                        {
                            inventory.RemoveItem(d);
                        }

                        raycastedObj.SetActive(false);
                        inventory.AddItem(interactable);
                        //Destroy(raycastedObj);
                        if (raycastedObj.name == "map")
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
            else if (hit.collider.CompareTag("ComputerPanelEntry"))
            {
                if (raycastedObj != null && raycastedObj != hit.collider.gameObject)
                {
                    CrossHairNormal();
                }
                raycastedObj = GetTopMostParent(hit.collider.gameObject);
                InteractableObject interactable = raycastedObj.GetComponent<InteractableObject>();
                if (interactable != null && interactable.dependencies.TrueForAll(d => inventory.ContainsItem(d)))
                {
                    crosshairActive(); // sita funkcija kvieciama, kai paziurim i objekta, turinti InteractableObject tag'a

                    if (Input.GetKeyDown("e"))
                    {
                        Debug.Log("I HAVE INTERACTED WITH A COMPUTER");
                        screenManager.OpenScreen();
                    }
                }
                else
                {
                    CrossHairNormal();
                }
            }
            else if (hit.collider.CompareTag("UICollider"))
            {
                if (raycastedObj != null && raycastedObj != hit.collider.gameObject.transform.Find("Text").gameObject)
                {
                    CrossHairNormal();
                }
                raycastedObj = hit.collider.gameObject.transform.Find("Text").gameObject;
                var deps = hit.collider.gameObject.GetComponent<InteractableText>().dependencies;
                if(deps.TrueForAll(d => inventory.ContainsItem(d)))
                {
                    crosshairActive();
                    if (Input.GetKeyDown("e"))
                    {
                        if (hit.collider.gameObject.GetComponent<InteractableText>().text == boardUnlockText)
                        {
                            foreach (var d in deps)
                            {
                                inventory.RemoveItem(d);
                            }
                            Destroy(board.GetComponent<FixedJoint>());
                        }
                    }
                }
                else
                {
                    CrossHairNormal();
                }
            }
            else if (hit.collider.CompareTag("InteractableInstruction"))
            {
                raycastedObj = hit.collider.gameObject;
                crosshairActive();
                if (Input.GetKeyDown("e"))
                {
                    raycastedObj.SetActive(false);
                }
            }
        }
        else
        {
            CrossHairNormal(); // sita funkcija kvieciama kai neziurim
        }
    }

    GameObject GetTopMostParent(GameObject obj)
    {
        while (obj.transform.parent != null)
        {
            obj = obj.transform.parent.gameObject;
        }
        return obj;
    }

    void crosshairActive()
    {
        uiCrosshair.color = Color.red;
        if(raycastedObj != null)
        {
            if (raycastedObj.GetComponent<Image>() != null)
            {
                ChangeObjectColor(new Color(0, 255, 255, 0.85f));
            }
            else
            {
                ChangeObjectColor(Color.cyan);
            }
        }
    }

    void CrossHairNormal()
    {
        uiCrosshair.color = Color.white;
        if (raycastedObj != null)
        {
            if (raycastedObj.GetComponent<Image>() != null)
            {
                ChangeObjectColor(new Color(0, 0, 0, 0.85f));
            }
            else
            {
                ChangeObjectColor(Color.white);
            }
        }
    }

    void ChangeObjectColor(Color color)
    {
        if (raycastedObj.GetComponent<Renderer>() != null)
        {
            raycastedObj.GetComponent<Renderer>().material.color = color; // kai ziurim, pakeiciu objekto spalva i raudona
        }
        else if(raycastedObj.GetComponent<Text>() != null)
        {
            raycastedObj.GetComponent<Text>().color = color;
        }
        else if(raycastedObj.GetComponent<Image>() != null)
        {
            raycastedObj.GetComponent<Image>().color = color;
        }
        else
        {
            foreach (var r in raycastedObj.GetComponentsInChildren<Renderer>())
            {
                r.material.color = color;
            }
        }
    }
}
