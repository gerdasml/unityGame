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

    public List<string> Questions { get { return questions; } }
    public List<string> Answers { get { return answers; } }
}
