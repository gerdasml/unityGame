using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


struct ColorManager
{
    public Color originalColor;
    public Action<Color> setColor;
}

public class Raycast : MonoBehaviour {

    private GameObject raycastedObj;
    //private Color prevColor = Color.white;

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
                if (raycastedObj != null && raycastedObj != hit.collider.gameObject)
                {
                    CrossHairNormal();
                }
                raycastedObj = hit.collider.gameObject;
                InteractableObject interactable = raycastedObj.GetComponent<InteractableObject>();
                if (interactable != null && !interactable.dependencies.TrueForAll(d => inventory.ContainsItem(d))) return;
                crosshairActive();
                if (Input.GetKeyDown("e"))
                {
                    if(interactable != null)
                    {
                        List<InteractableObject> dependencies = interactable.dependencies;
                        foreach (var d in dependencies)
                        {
                            inventory.RemoveItem(d);
                        }
                    }
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
                SetColors(raycastedObj, new Color(0, 0, 255, 0.85f));
            }
            else if (raycastedObj.CompareTag("SafePanel"))
            {
                var x = raycastedObj.transform.parent.childCount;
                for (int i = 0; i < x; i++)
                {
                    if (raycastedObj.transform.parent.GetChild(i).CompareTag("SafeButton"))
                    {
                        SetColors(raycastedObj.transform.parent.GetChild(i).gameObject, Color.blue);
                    }
                }
            }
            else
            {
                SetColors(raycastedObj, Color.blue);
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
                        SetColors(raycastedObj.transform.parent.GetChild(i).gameObject);
                    }
                }
            }
            else if (raycastedObj.GetComponent<Image>() != null)
            {
                SetColors(raycastedObj);
            }
            else
            {
                SetColors(raycastedObj);
            }
        }
    }

    
    private Dictionary<UnityEngine.Object, ColorManager> originalColors = new Dictionary<UnityEngine.Object, ColorManager>();

    private Dictionary<UnityEngine.Object, ColorManager> Decompose(GameObject game)
    {
        var result = new Dictionary<UnityEngine.Object, ColorManager>();
        if (game.GetComponent<Renderer>() != null)
        {
            result[game.GetComponent<Renderer>()] = new ColorManager()
            {
                originalColor = game.GetComponent<Renderer>().material.color,
                setColor = color => game.GetComponent<Renderer>().material.color = color
            };
        }
        else if (game.GetComponent<Text>() != null)
        {
            result[game.GetComponent<Text>()] = new ColorManager()
            {
                originalColor = game.GetComponent<Text>().color,
                setColor = color => game.GetComponent<Text>().color = color
            };
        }
        else if (game.GetComponent<Image>() != null)
        {
            result[game.GetComponent<Image>()] = new ColorManager()
            {
                originalColor = game.GetComponent<Image>().color,
                setColor = color => game.GetComponent<Image>().color = color
            };
        }
        else
        {
            foreach (var r in game.GetComponentsInChildren<Renderer>())
            {
                result[r] = new ColorManager()
                {
                    originalColor = r.material.color,
                    setColor = color => r.material.color = color
                };
            }
        }
        return result;
    }

    private void SaveOriginalColors(GameObject obj)
    {
        Decompose(obj)
            .Where(part => !originalColors.ContainsKey(part.Key))
            .ToList()
            .ForEach(part => originalColors[part.Key] = part.Value);
    }

    private void SetColors(GameObject obj, Color? color = null)
    {
        SaveOriginalColors(obj);
        Decompose(obj)
            .ToList()
            .ForEach(part =>
                part.Value.setColor(color.HasValue ? color.Value : originalColors[part.Key].originalColor)
            );
    }
}
