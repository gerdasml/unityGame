using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIColliderData
{
    public Inventory Inventory { get; set; }
    public RaycastHit Hit { get; set; }
    public Text BoardUnlockText { get; set; }
    public GameObject Board { get; set; }
    public BoardHandler BoardHandler { get; set; }
}

public class UIColliderHandler : IInteractionHandler<UIColliderData>
{
    public void Handle(UIColliderData data, Action activateCrosshair, Action deactivateCrosshair)
    {
        var deps = data.Hit.collider.gameObject.GetComponent<InteractableText>().dependencies;
        if (deps.TrueForAll(d => data.Inventory.ContainsItem(d)))
        {
            activateCrosshair();
            if (Input.GetKeyDown("e"))
            {
                if (data.Hit.collider.gameObject.GetComponent<InteractableText>().text.text == data.BoardHandler.correctAnswer)
                {
                    foreach (var d in deps)
                    {
                        data.Inventory.RemoveItem(d);
                    }
                    UnityEngine.Object.Destroy(data.Board.GetComponent<FixedJoint>());
                }
                else
                {
                    data.BoardHandler.Reshuffle();
                }
            }
        }
        else
        {
            deactivateCrosshair();
        }
    }
}
