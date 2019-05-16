using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHandler : MonoBehaviour {
    private List<string> incorrectWords;
    private List<string> correctWords;
    public List<InteractableText> choices;
    public string correctAnswer;

    void Start()
    {
        incorrectWords = Config.Instance.Puzzle.WordBoard.IncorrectWords;
        correctWords = Config.Instance.Puzzle.WordBoard.CorrectWords;
        Reshuffle();
    }

    public void Reshuffle()
    {
        incorrectWords.Shuffle();
        correctWords.Shuffle();
        List<string> boardWords = incorrectWords.GetRange(0, 8);
        correctAnswer = correctWords[0];
        boardWords.Add(correctAnswer);
        boardWords.Shuffle();
        for(int i = 0; i<9; i++)
        {
            choices[i].text.text = boardWords[i];
        }
    }
}
