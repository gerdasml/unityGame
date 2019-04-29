using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizHandler : MonoBehaviour {
    private string Answer = "02-21";
    public Text questionText;
    public Text pointsText;
    public List<Button> choiceButtons;
    public int requiredAnswers;

	// Use this for initialization
	void Start () {
        questionText.text = "Kada Vytoto gimtadienis?";
        pointsText.text = "0";
        choiceButtons[0].GetComponentInChildren<Text>().text = "02-12";
        choiceButtons[1].GetComponentInChildren<Text>().text = "04-17";
        choiceButtons[2].GetComponentInChildren<Text>().text = "04-04";
        choiceButtons[3].GetComponentInChildren<Text>().text = "02-21";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void HandleClick(Button button)
    {
        if(button.GetComponentInChildren<Text>().text == Answer)
        {
            pointsText.text = (int.Parse(pointsText.text) + 1).ToString();
            Debug.Log("Guf");
        } else
        {
            pointsText.text = "0";
            Debug.Log("WA on test 1");
        }
    }
}
