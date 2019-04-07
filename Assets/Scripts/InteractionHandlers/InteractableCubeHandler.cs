using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCubeData
{
    public InteractableObject Interactable { get; set; }
    public Inventory Inventory { get; set; }
    public PickupableObject Pickupable { get; set; }
}

public class InteractableCubeHandler : IInteractionHandler<InteractableCubeData>
{
    public void Handle(InteractableCubeData data, Action activateCrosshair, Action deactivateCrosshair)
    {
        if (data.Interactable != null && data.Pickupable != null)
        {
            activateCrosshair(); // sita funkcija kvieciama, kai paziurim i objekta, turinti InteractableObject tag'a

            if (Input.GetKeyDown("e"))
            {
                data.Pickupable.isBeingCarried = !data.Pickupable.isBeingCarried;
            }
        }
        else
        {
            deactivateCrosshair();
        }
    }
}
