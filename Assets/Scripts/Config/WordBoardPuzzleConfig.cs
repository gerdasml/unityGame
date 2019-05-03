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
    [SerializeField]
    private List<string> helps;

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

    public List<string> Helps
    {
        get
        {
            return helps;
        }
    }
}
