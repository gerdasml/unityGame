using System;
using UnityEngine;

public class InteractableParrotData
{
    public InteractableNPC NPC { get; set; }
}

public class InteractableParrotHandler : IInteractionHandler<InteractableParrotData>
{
    public void Handle(InteractableParrotData data, Action activateCrosshair, Action deactivateCrosshair)
    {
        if (data.NPC != null)
        {
            activateCrosshair();
            if (Input.GetKeyDown("e"))
            {
                data.NPC.NextPanel();
            }
        }
        else
        {
            deactivateCrosshair();
        }
    }
}
