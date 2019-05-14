using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BoatBoardConfig {

    [SerializeField]
    private string task;
    [SerializeField]
    private string answer;

    public string Task { get { return task; } }
    public string Answer { get { return answer; } }
}
