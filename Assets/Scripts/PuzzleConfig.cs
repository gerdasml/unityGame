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
}
