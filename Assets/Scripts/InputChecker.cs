using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputChecker : MonoBehaviour {

    public string secretPhrase;
    public InputField input;
    public ScreenManager screenManager;
    public GameObject lockedObject;
    public GameObject computer;
    
	void Update () {
        if(Input.GetKeyDown(KeyCode.Return))
        {
	        if (input.text == secretPhrase)
            {
                screenManager.CloseScreen();
                lockedObject.SetActive(true);
                Destroy(computer.GetComponent<InteractableObject>());
            }
            input.text = "";
        }
	}
}
