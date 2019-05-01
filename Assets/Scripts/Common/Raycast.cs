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
    [SerializeField] private GameObject board;
    [SerializeField] private Text boardUnlockText;
    public GameObject minimap;
    public ScreenManager endGameScreenManager;

    private IInteractionHandler<InteractableObjectData> interactableObjectHandler = new InteractableObjectHandler();
    private IInteractionHandler<InteractableParrotData> interactableParrotHandler = new InteractableParrotHandler();
    private IInteractionHandler<ComputerPanelEntryData> computerPanelEntryHandler = new ComputerPanelEntryHandler();
    private IInteractionHandler<UIColliderData> uiColliderHandler = new UIColliderHandler();
    private IInteractionHandler<InteractableCubeData> interactableCubeHandler = new InteractableCubeHandler();

    void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, rayLengt, layerMaskInteract.value))
        {
            if (hit.collider.CompareTag("InteractableObject"))
            {
                if (raycastedObj != null && raycastedObj != hit.collider.gameObject)
                {
                    CrossHairNormal();
                }
                raycastedObj = GetTopMostParent(hit.collider.gameObject);
                InteractableObject interactable = raycastedObj.GetComponent<InteractableObject>();
                interactableObjectHandler.Handle(new InteractableObjectData
                {
                    RaycastedObj = raycastedObj,
                    EndGameScreenManager = endGameScreenManager,
                    Hit = hit,
                    Interactable = interactable,
                    Inventory = inventory,
                    Minimap = minimap
                }, crosshairActive, CrossHairNormal);
            }
            else if (hit.collider.CompareTag("ComputerPanelEntry"))
            {
                if (raycastedObj != null && raycastedObj != hit.collider.gameObject)
                {
                    CrossHairNormal();
                }
                raycastedObj = GetTopMostParent(hit.collider.gameObject);
                InteractableObject interactable = raycastedObj.GetComponent<InteractableObject>();
                UIEntryPoint uiEntryPoint = raycastedObj.GetComponent<UIEntryPoint>();
                computerPanelEntryHandler.Handle(new ComputerPanelEntryData
                {
                    Interactable = interactable,
                    Inventory = inventory,
                    UIEntryPoint = uiEntryPoint
                }, crosshairActive, CrossHairNormal);
            }
            else if (hit.collider.CompareTag("UICollider"))
            {
                if (raycastedObj != null && raycastedObj != hit.collider.gameObject.transform.Find("Text").gameObject)
                {
                    CrossHairNormal();
                }
                raycastedObj = hit.collider.gameObject.transform.Find("Text").gameObject;
                var board = GetTopMostParent(raycastedObj);
                var boardHandler = board.GetComponent<BoardHandler>();
                uiColliderHandler.Handle(new UIColliderData
                {
                    Board = board,
                    BoardUnlockText = boardUnlockText,
                    Hit = hit,
                    Inventory = inventory,
                    BoardHandler = boardHandler
                }, crosshairActive, CrossHairNormal);
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
            else if (hit.collider.CompareTag("InteractableCube"))
            {
                if (raycastedObj != null && raycastedObj != hit.collider.gameObject)
                {
                    CrossHairNormal();
                }
                raycastedObj = hit.collider.gameObject;
                InteractableObject interactable = raycastedObj.GetComponent<InteractableObject>();
                PickupableObject pickupable = raycastedObj.GetComponent<PickupableObject>();
                interactableCubeHandler.Handle(new InteractableCubeData
                {
                    Interactable = interactable,
                    Inventory = inventory,
                    Pickupable = pickupable
                }, crosshairActive, CrossHairNormal);
            }
            else if (hit.collider.CompareTag("HelpParrot"))
            {
                if (raycastedObj != null && raycastedObj != hit.collider.gameObject)
                {
                    CrossHairNormal();
                }
                raycastedObj = GetTopMostParent(hit.collider.gameObject, "HelpParrot");
                InteractableObject interactable = raycastedObj.GetComponent<InteractableObject>();
                interactableParrotHandler.Handle(new InteractableParrotData
                {
                    Interactable = interactable
                }, crosshairActive, CrossHairNormal);
            }
        }
        else
        {
            CrossHairNormal(); // sita funkcija kvieciama kai neziurim
        }
    }

    GameObject GetTopMostParent(GameObject obj, string tag = null)
    {
        while (obj.transform.parent != null && (tag == null || obj.transform.parent.CompareTag(tag)))
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
                ChangeObjectColor(Color.blue);
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
