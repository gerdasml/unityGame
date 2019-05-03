using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordBoardNPC : InteractableNPC {
    
	void Start () {
        helpTexts = Config.Instance.Puzzle.WordBoard.Helps;
	}
	
}
