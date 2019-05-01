using System;
using UnityEngine;

public class InteractableParrotData
{
    public InteractableObject Interactable { get; set; }
}

public class InteractableParrotHandler : IInteractionHandler<InteractableParrotData>
{
    public void Handle(InteractableParrotData data, Action activateCrosshair, Action deactivateCrosshair)
    {
        if (data.Interactable != null)
        {
            activateCrosshair();
            if (Input.GetKeyDown("e"))
            {
                Debug.Log("I HAVE INTERACTED WITH A PARROT");
            }
        }
        else
        {
            deactivateCrosshair();
        }
    }
}
