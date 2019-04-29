using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PuzzleConfig {

    [SerializeField]
    private ComputerPuzzleConfig computer;
    [SerializeField]
    private WordBoardPuzzleConfig wordBoard;
    [SerializeField]
    private QuizPuzzleConfig quiz;

    public ComputerPuzzleConfig Computer
    {
        get
        {
            return computer;
        }
    }

    public WordBoardPuzzleConfig WordBoard
    {
        get
        {
            return wordBoard;
        }
    }

    public QuizPuzzleConfig Quiz { get { return quiz; } }
}
