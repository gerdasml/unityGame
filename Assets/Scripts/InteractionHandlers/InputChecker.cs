using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputChecker : MonoBehaviour {

    public InputField input;
    public ScreenManager screenManager;
    public List<GameObject> lockedObjects;
    public GameObject computer;
    public Inventory inventory;
    public Text taskText;
    private string secretPhrase;

    void Start()
    {
        secretPhrase = Config.Instance.Puzzle.Computer.ExpectedAnswer;
        taskText.text = Config.Instance.Puzzle.Computer.Code;
    }

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
                for (int i = 0; i < lockedObjects.Count; i++)
                {
                    lockedObjects[i].SetActive(true);
                }
                Destroy(computer.GetComponent<InteractableObject>());
            }
            input.text = "";
        }
	}
}
