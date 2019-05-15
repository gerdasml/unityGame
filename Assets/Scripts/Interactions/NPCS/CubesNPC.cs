using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesNPC : InteractableNPC
{
    void Start()
    {
        helpTexts = Config.Instance.Puzzle.Cubes.Helps;
    }
}
