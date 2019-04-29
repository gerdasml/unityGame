using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerPanelEntryData
{
    public Inventory Inventory { get; set; }
    public InteractableObject Interactable { get; set; }
    public UIEntryPoint UIEntryPoint { get; set; }
}

public class ComputerPanelEntryHandler : IInteractionHandler<ComputerPanelEntryData>
{
    public void Handle(ComputerPanelEntryData data, Action activateCrosshair, Action deactivateCrosshair)
    {
        if (data.Interactable != null && data.Interactable.dependencies.TrueForAll(d => data.Inventory.ContainsItem(d)))
        {
            activateCrosshair(); // sita funkcija kvieciama, kai paziurim i objekta, turinti InteractableObject tag'a
            if (Input.GetKeyDown("e"))
            {
                Debug.Log("I HAVE INTERACTED WITH A COMPUTER");
                data.UIEntryPoint.screenManager.OpenScreen();
            }
        }
        else
        {
            deactivateCrosshair();
        }
    }
}
