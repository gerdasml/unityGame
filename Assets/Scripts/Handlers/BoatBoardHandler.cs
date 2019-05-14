using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoatBoardHandler : MonoBehaviour {

    private string task;
    private string answer;
    public TextMeshPro boardText;
    public GameObject safeCode;

    void Start()
    {
        task = Config.Instance.Puzzle.BoatBoard.Task;
        answer = Config.Instance.Puzzle.BoatBoard.Answer;
        boardText.text = task;
    }
}
