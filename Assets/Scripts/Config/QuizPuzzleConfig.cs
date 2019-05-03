using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuizPuzzleConfig {

    [SerializeField]
    private int numberOfAnswersNeeded;
    [SerializeField]
    private List<QuizQuestion> questions;
    [SerializeField]
    private List<string> helps;

    public int NumberOfAnswersNeeded { get { return numberOfAnswersNeeded; } }
    public List<QuizQuestion> Questions { get { return questions; } }
    public List<string> Helps { get { return helps; } }
}

[Serializable]
public class QuizQuestion
{
    [SerializeField]
    private string text;
    [SerializeField]
    private List<QuizChoice> choices;

    public string Text { get { return text; } }
    public List<QuizChoice> Choices { get { return choices; } }
}

[Serializable]
public class QuizChoice
{
    [SerializeField]
    private string text;
    [SerializeField]
    private bool isCorrect;

    public string Text { get { return text; } }
    public bool IsCorrect { get { return isCorrect; } }
}
