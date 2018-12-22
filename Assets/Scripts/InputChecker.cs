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
    public Inventory inventory;

    void Update () {
        if (Input.GetKeyDown(KeyCode.Return))
        {
	        if (input.text == secretPhrase)
            {
                List<InteractableObject> dependencies = computer.GetComponent<InteractableObject>().dependencies;
                foreach (var d in dependencies)
                {
                    inventory.RemoveItem(d);
                }

                screenManager.CloseScreen();
                lockedObject.SetActive(true);
                Destroy(computer.GetComponent<InteractableObject>());
            }
            input.text = "";
        }
	}
}
