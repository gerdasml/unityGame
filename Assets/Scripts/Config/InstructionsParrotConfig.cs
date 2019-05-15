using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InstructionsParrotConfig {

    [SerializeField]
    private List<string> helps;

    public List<string> Helps { get { return helps; } }
}
