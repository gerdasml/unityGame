using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizNPC : InteractableNPC {

	void Start () {
        helpTexts = Config.Instance.Puzzle.Quiz.Helps;
	}
	
}
