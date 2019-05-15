using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartNPC : InteractableNPC
{
    void Start()
    {
        helpTexts = Config.Instance.Puzzle.InstructionsParrot.Helps;
    }
}
