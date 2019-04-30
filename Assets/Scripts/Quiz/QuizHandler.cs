using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizHandler : MonoBehaviour {
    
    public Text questionText;
    public Text pointsText;
    public List<Button> choiceButtons;
    public ScreenManager screenManager;
    public GameObject tv;
    public GameObject lockedObject;

    private int requiredAnswers;
    private List<QuizQuestion> questions;
    private int index = 0;
    private string Answer;

    // Use this for initialization
    void Start () {
        pointsText.text = "0";
        questions = Config.Instance.Puzzle.Quiz.Questions;
        questions.Shuffle();
        FillButtonValues();
        requiredAnswers = Config.Instance.Puzzle.Quiz.NumberOfAnswersNeeded;
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
        index++;
    }
    
    public void HandleClick(Button button)
    {
        if(button.GetComponentInChildren<Text>().text == Answer)
        {
            pointsText.text = (int.Parse(pointsText.text) + 1).ToString();
            if (requiredAnswers == index)
            {
                screenManager.CloseScreen();
                Destroy(tv.GetComponent<InteractableObject>());
            }
            FillButtonValues();
        }
        else
        {
            pointsText.text = "0";
            index = 0;
            questions.Shuffle();
        }
    }
}
