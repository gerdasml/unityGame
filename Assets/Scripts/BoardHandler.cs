using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHandler : MonoBehaviour {
    private List<string> incorrectWords = Config.Instance.Puzzle.WordBoard.IncorrectWords;
    private List<string> correctWords = Config.Instance.Puzzle.WordBoard.CorrectWords;
    public List<InteractableText> choices;
    public string correctAnswer;
    

    public void Reshuffle()
    {
        correctAnswer = correctWords[0];
        incorrectWords.Shuffle();
        correctWords.Shuffle();
        List<string> boardWords = incorrectWords.GetRange(0, 8);
        boardWords.Add(correctWords[0]);
        boardWords.Shuffle();
        for(int i = 0; i<9; i++)
        {
            choices[i].text.text = boardWords[i];
        }
    }
}
