using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;

public class ScreenManager : MonoBehaviour {

    public GameObject screenPanel;
    
    public void OpenScreen()
    {
        screenPanel.SetActive(true);
        GameObject.Find("FPSController").GetComponent<FirstPersonController>().enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseScreen()
    {
        screenPanel.SetActive(false);
        GameObject.Find("FPSController").GetComponent<FirstPersonController>().enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
