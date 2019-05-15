﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BoatBoardConfig {

    [SerializeField]
    private string task;
    [SerializeField]
    private string answer;
    [SerializeField]
    private List<string> helps;

    public string Task { get { return task; } }
    public string Answer { get { return answer; } }
    public List<string> Helps { get { return helps; } }
}