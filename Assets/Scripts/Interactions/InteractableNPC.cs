using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class InteractableNPC : MonoBehaviour {

    public GameObject panel;
    private int currentText = 0;
    protected List<string> helpTexts;

    public void NextPanel()
    {
        if(currentText == helpTexts.Count)
        {
            currentText = 0;
            panel.SetActive(false);
        }
        else
        {
            panel.SetActive(true);
            panel.GetComponentInChildren<Text>().text = helpTexts[currentText++];
        }
    }
}
