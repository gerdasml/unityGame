using UnityEngine;
using System;

public class InteractableFunNPCData
{
    public InteractableObject FunNpc { get; set; }
}

public class InteractableFunNPCHandler : MonoBehaviour, IInteractionHandler<InteractableFunNPCData>
{
    public AudioSource audioSource;
    public void Handle(InteractableFunNPCData data, Action activateCrosshair, Action deactivateCrosshair)
    {
        if (data.FunNpc != null)
        {
            activateCrosshair();
            if (Input.GetKeyDown("e"))
            {
                audioSource = data.FunNpc.GetComponent<AudioSource>();
                audioSource.Play();
            }
        }
        else
        {
            deactivateCrosshair();
        }
    }
}
