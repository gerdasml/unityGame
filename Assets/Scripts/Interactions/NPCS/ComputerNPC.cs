using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerNPC : InteractableNPC {

    void Start()
    {
        helpTexts = Config.Instance.Puzzle.Computer.Helps;
    }
}
