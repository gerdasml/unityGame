using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Raycast : MonoBehaviour {

    private GameObject raycastedObj;
    private Color prevColor = Color.white;

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
    private IInteractionHandler<InteractableFunNPCData> interactableFunNpcHandler = new InteractableFunNPCHandler();

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
                prevColor = GetObjectColor(GetTopMostParent(hit.collider.gameObject));
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
                prevColor = GetObjectColor(GetTopMostParent(hit.collider.gameObject));
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
                prevColor = GetObjectColor(hit.collider.gameObject.transform.Find("Text").gameObject);
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
                if (raycastedObj != null && raycastedObj != hit.collider.gameObject)
                {
                    CrossHairNormal();
                }
                prevColor = GetObjectColor(hit.collider.gameObject);
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
                prevColor = GetObjectColor(hit.collider.gameObject);
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
                prevColor = GetObjectColor(GetTopMostParent(hit.collider.gameObject, "HelpParrot"));
                raycastedObj = GetTopMostParent(hit.collider.gameObject, "HelpParrot");
                InteractableNPC interactable = raycastedObj.GetComponent<InteractableNPC>();
                interactableParrotHandler.Handle(new InteractableParrotData
                {
                    NPC = interactable
                }, crosshairActive, CrossHairNormal);
            }
            else if (hit.collider.CompareTag("FunNPC"))
            {
                if (raycastedObj != null && raycastedObj != hit.collider.gameObject)
                {
                    CrossHairNormal();
                }
                prevColor = GetObjectColor(GetTopMostParent(hit.collider.gameObject, "FunNPC"));
                raycastedObj = GetTopMostParent(hit.collider.gameObject, "FunNPC");
                InteractableObject interactable = raycastedObj.GetComponent<InteractableObject>();
                interactableFunNpcHandler.Handle(new InteractableFunNPCData
                {
                    FunNpc = interactable
                }, crosshairActive, CrossHairNormal);
            }
            else if (hit.collider.CompareTag("SafePanel"))
            {
                if (raycastedObj != null && raycastedObj != hit.collider.gameObject)
                {
                    CrossHairNormal();
                }
                prevColor = GetObjectColor(GetTopMostParent(hit.collider.gameObject, "SafePanel"));
                raycastedObj = GetTopMostParent(hit.collider.gameObject, "SafePanel");
                crosshairActive();
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
            else if (raycastedObj.CompareTag("SafePanel"))
            {
                var x = raycastedObj.transform.parent.childCount;
                for (int i = 0; i < x; i++)
                {
                    if (raycastedObj.transform.parent.GetChild(i).CompareTag("SafeButton"))
                    {
                        ChangeObjectColor(Color.blue, raycastedObj.transform.parent.GetChild(i).gameObject);
                    }
                }
            }
            else
            {
                ChangeObjectColor(Color.blue);
            }
        }
    }

    void CrossHairNormal()
    {
        if (raycastedObj != null)
        {
            if (raycastedObj.CompareTag("SafePanel"))
            {
                var x = raycastedObj.transform.parent.childCount;
                for(int i = 0; i < x; i++)
                {
                    if (raycastedObj.transform.parent.GetChild(i).CompareTag("SafeButton"))
                    {
                        ChangeObjectColor(Color.white, raycastedObj.transform.parent.GetChild(i).gameObject);
                    }
                }
            }
            else if (raycastedObj.GetComponent<Image>() != null)
            {
                ChangeObjectColor(new Color(0, 0, 0, 0.85f));
            }
            else
            {
                ChangeObjectColor(prevColor);
            }
        }
    }

    void ChangeObjectColor(Color color, GameObject game = null)
    {
        if (game == null) game = raycastedObj;
        if (game.GetComponent<Renderer>() != null)
        {
            game.GetComponent<Renderer>().material.color = color; 
        }
        else if(game.GetComponent<Text>() != null)
        {
            game.GetComponent<Text>().color = color;
        }
        else if(game.GetComponent<Image>() != null)
        {
            game.GetComponent<Image>().color = color;
        }
        else
        {
            foreach (var r in game.GetComponentsInChildren<Renderer>())
            {
                r.material.color = color;
            }
        }
    }

    Color GetObjectColor(GameObject game = null)
    {
        if (game == raycastedObj) return prevColor;
        if (game == null)
        {
            game = raycastedObj;
                return Color.white;
        }
        if (game.GetComponent<Renderer>() != null)
        {
            return game.GetComponent<Renderer>().material.color;
        }
        else if (game.GetComponent<Text>() != null)
        {
            return game.GetComponent<Text>().color;
        }
        else if (game.GetComponent<Image>() != null)
        {
            return game.GetComponent<Image>().color;
        }
        else
        {
            foreach (var r in game.GetComponentsInChildren<Renderer>())
            {
                return r.material.color;
            }
        }
        throw new Exception("Failed to get color");
    }
}
