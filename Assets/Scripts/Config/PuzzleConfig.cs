using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PuzzleConfig {

    [SerializeField]
    private ComputerPuzzleConfig computer;
    [SerializeField]
    private WordBoardPuzzleConfig wordBoard;
    [SerializeField]
    private QuizPuzzleConfig quiz;
    [SerializeField]
    private CubesPuzzleConfig cubes;
    [SerializeField]
    private BoatBoardConfig boatBoard;
    [SerializeField]
    private InstructionsParrotConfig instructionsParrot;

    public ComputerPuzzleConfig Computer
    {
        get
        {
            return computer;
        }
    }

    public WordBoardPuzzleConfig WordBoard
    {
        get
        {
            return wordBoard;
        }
    }

    public QuizPuzzleConfig Quiz { get { return quiz; } }

    public CubesPuzzleConfig Cubes {
        get
        {
            return cubes;
        }
    }

    public BoatBoardConfig BoatBoard { get { return boatBoard; } }

    public InstructionsParrotConfig InstructionsParrot { get { return instructionsParrot; } }
}
