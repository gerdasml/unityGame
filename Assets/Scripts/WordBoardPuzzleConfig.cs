using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WordBoardPuzzleConfig {

    [SerializeField]
    private List<string> incorectWords;
    [SerializeField]
    private List<string> correctWords;

    public List<string> IncorrectWords
    {
        get
        {
            return incorectWords;
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
