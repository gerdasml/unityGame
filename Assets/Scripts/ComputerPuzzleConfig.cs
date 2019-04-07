using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ComputerPuzzleConfig {

    [SerializeField]
    private string code;
    [SerializeField]
    private string expectedAnswer;

    public string Code
    {
        get
        {
            return code;
        }
    }

    public string ExpectedAnswer
    {
        get
        {
            return expectedAnswer;
        }
    }
}
