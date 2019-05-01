using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WordBoardPuzzleConfig {

    [SerializeField]
    private List<string> incorrectWords;
    [SerializeField]
    private List<string> correctWords;

    public List<string> IncorrectWords
    {
        get
        {
            return incorrectWords;
        }
    }

    public List<string> CorrectWords
    {
        get
        {
            return correctWords;
        }
    }
}
