using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public Image[] itemImages = new Image[numItemSlots];
    public InteractableObject[] items = new InteractableObject[numItemSlots];

    public const int numItemSlots = 4;


    public bool ContainsItem(InteractableObject interactable)
    {
        for(int i = 0; i < items.Length; i++)
        {
            if (items[i] == interactable) return true;
        }
        return false;
    }

    public void AddItem(InteractableObject itemToAdd)
    {
        for(int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = itemToAdd;
                itemImages[i].sprite = itemToAdd.sprite;
                itemImages[i].enabled = true;
                return;
            }
        }
    }

    public void RemoveItem(InteractableObject itemToRemove)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == itemToRemove)
            {
                items[i] = null;
                itemImages[i].sprite = null;
                itemImages[i].enabled = false;
                return;
            }
        }
    }
}
