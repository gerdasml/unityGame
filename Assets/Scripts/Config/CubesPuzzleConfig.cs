using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CubesPuzzleConfig {

    [SerializeField]
    private List<string> questions;
    [SerializeField]
    private List<string> answers;
    [SerializeField]
    private string wordsBoardTask;
    [SerializeField]
    private List<string> helps;

    public List<string> Questions { get { return questions; } }
    public List<string> Answers { get { return answers; } }
    public string WordsBoardTask { get { return wordsBoardTask; } }
    public List<string> Helps { get { return helps; } }
}
