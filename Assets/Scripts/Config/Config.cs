using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Config {

    private static Config _instance;
    public static Config Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = LoadData();
            }
            return _instance;
        }
    }

    [SerializeField]
    private PuzzleConfig puzzles;

    public PuzzleConfig Puzzle
    {
        get
        {
            return puzzles;
        }
    }

    private static Config LoadData()
    {
        var path = Application.dataPath + "/Resources/data.json";
        var json = File.ReadAllText(path);
        return JsonUtility.FromJson<Config>(json);
    }
}
