using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatBoardNPC : InteractableNPC
{
    void Start()
    {
        helpTexts = Config.Instance.Puzzle.BoatBoard.Helps;
    }

}
