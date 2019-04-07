using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectData
{
    public GameObject RaycastedObj { get; set; }
    public Inventory Inventory { get; set; }
    public GameObject Minimap { get; set; }
    public ScreenManager EndGameScreenManager { get; set; }
    public RaycastHit Hit { get; set; }
    public InteractableObject Interactable { get; set; }
}

public class InteractableObjectHandler : IInteractionHandler<InteractableObjectData>
{
    public void Handle(InteractableObjectData data, Action activateCrosshair, Action deactivateCrosshair)
    {
        if (data.Interactable != null && data.Interactable.dependencies.TrueForAll(d => data.Inventory.ContainsItem(d)))
        {
            activateCrosshair(); // sita funkcija kvieciama, kai paziurim i objekta, turinti InteractableObject tag'a

            if (Input.GetKeyDown("e"))
            {
                Debug.Log("I HAVE INTERACTED WITH AN OBJECT");

                foreach (var d in data.Interactable.dependencies)
                {
                    data.Inventory.RemoveItem(d);
                }

                data.RaycastedObj.SetActive(false);
                data.Inventory.AddItem(data.Interactable);
                //Destroy(raycastedObj);
                if (data.RaycastedObj.name == "map")
                {
                    data.Inventory.RemoveItem(data.Interactable);
                    data.Minimap.SetActive(true);
                }
                if (data.RaycastedObj.name == "padlock")
                {
                    data.Inventory.RemoveItem(data.Interactable);
                    data.EndGameScreenManager.OpenScreen();
                }
            }
        }
        else
        {
            deactivateCrosshair();
        }
    }
}
