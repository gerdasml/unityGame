using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizHandler : MonoBehaviour {
    
    public TextMeshProUGUI questionText;
    public List<Button> choiceButtons;
    public ScreenManager screenManager;
    public GameObject tv;
    public GameObject lightbulbsPanel;
    public Image lightbulbTemplate;
    public List<GameObject> lockedObjects;
    public Inventory inventory;

    private int requiredAnswers;
    private List<QuizQuestion> questions;
    private int index = 0;
    private string Answer;
    private List<Image> lightbulbs = new List<Image>();

    // Use this for initialization
    void Start () {
        questions = Config.Instance.Puzzle.Quiz.Questions;
        requiredAnswers = Config.Instance.Puzzle.Quiz.NumberOfAnswersNeeded;

        questions.Shuffle();
        FillButtonValues();

        for(int i = 0; i < requiredAnswers; i++)
        {
            var lightbulb = Object.Instantiate(lightbulbTemplate);
            lightbulb.gameObject.SetActive(true);
            lightbulbs.Add(lightbulb);
            lightbulb.transform.parent = lightbulbsPanel.transform;
        }
    }

    private void FillButtonValues()
    {
        questionText.text = Config.Instance.Puzzle.Quiz.Questions[index].Text;
        for(int i = 0; i < 4; i++)
        {
            var choice = Config.Instance.Puzzle.Quiz.Questions[index].Choices[i];
            choiceButtons[i].GetComponentInChildren<Text>().text = choice.Text;
            if (choice.IsCorrect) Answer = choice.Text;
        }
    }
    
    public void HandleClick(Button button)
    {
        if(button.GetComponentInChildren<Text>().text == Answer)
        {
            lightbulbs[index].color = Color.green;
            index++;
            if (requiredAnswers == index)
            {
                List<InteractableObject> dependencies = tv.GetComponent<InteractableObject>().dependencies;
                foreach (var d in dependencies)
                {
                    inventory.RemoveItem(d);
                }
                screenManager.CloseScreen();
                for (int i = 0; i < lockedObjects.Count; i++)
                {
                    lockedObjects[i].SetActive(true);
                }
                Destroy(tv.GetComponent<InteractableObject>());
                return;
            }

            FillButtonValues();
        }
        else
        {
            index = 0;
            questions.Shuffle();
            FillButtonValues();
            for(int i = 0; i < requiredAnswers; i++)
            {
                lightbulbs[i].color = Color.white;
            }
        }
    }
}
