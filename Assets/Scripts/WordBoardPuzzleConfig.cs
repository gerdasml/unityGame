using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WordBoardPuzzleConfig {

    [SerializeField]
    private List<string> words;
    [SerializeField]
    private List<string> correctWords;

    public List<string> Words
    {
        get
        {
            return words;
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
