using Assets.Scripts.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using WBase.Core.Extensions;

public class ScoreManager : MonoBehaviour
{
    public class HiScore
    {

        public string Initials { get; private set; }
        public int Score { get; private set; }

        public HiScore() { }

        public HiScore(string Initials, int Score)
        {
            this.Initials = Initials.ToUpper();
            this.Score = Score;
        }

    }

    public static readonly HiScore[] DefaultHiScores = new HiScore[MaxHiScores]
    {
        new HiScore("GOD", 50000),
        new HiScore("MOM", 40000),
        new HiScore("DAD", 30000),
        new HiScore("ABC", 20000),
        new HiScore("ABC", 10000),
        new HiScore("ABC", 9000),
        new HiScore("ABC", 8000),
        new HiScore("ABC", 7000),
        new HiScore("ABC", 6000),
        new HiScore("ABC", 5000),
        new HiScore("ABC", 4500),
        new HiScore("ABC", 4250),
        new HiScore("ABC", 3000),
        new HiScore("ABC", 2750),
        new HiScore("ABC", 2500),
        new HiScore("ABC", 2250),
        new HiScore("ABC", 1000),
        new HiScore("ABC", 900),
        new HiScore("ABC", 800),
        new HiScore("ABC", 700),
        new HiScore("ABC", 600),
        new HiScore("ABC", 500),
        new HiScore("ABC", 400),
        new HiScore("ABC", 300),
        new HiScore("BAD", 200)
    };
    public const int MaxHiScores = 25;
    public const string SaveDirectory = "./SaveData/";
    public const string SaveFile = SaveDirectory + "./HiScores.json";
    public int CurrentScore = 0;

    public List<HiScore> hiScores;

    public FileStream FileStream;

    private void Start()
    {
        hiScores = new List<HiScore>();
        Directory.CreateDirectory(SaveDirectory);
        if (!File.Exists(SaveFile))
        {
            hiScores = DefaultHiScores.ToList<HiScore>();

            File.Create(SaveFile).Close();
            SaveScores();
        }
        else
        {
            LoadScores();
        }


    }

    private SortedDictionary<string, int> ConvertToSortedDict(List<HiScore> list)
    {
        SortedDictionary<string, int> output = new SortedDictionary<string, int>();
        foreach(HiScore hiScore in list)
        {
            output.Add(hiScore.Initials, hiScore.Score);
        }
        return output;
    }

    public void SaveScore(HiScore hiScore)
    {
        if(hiScore.Score > hiScores.FindLowest().Score)
        {
            hiScores.Remove(hiScores.FindLowest());
            hiScores.Add(hiScore);
        }
    }

    public void ResetScore()
    {
        CurrentScore = 0;
    }

    public void AddScore(int points)
    {
        CurrentScore += points;
    }

    private void LoadScores()
    {
        hiScores = Newtonsoft.Json.JsonConvert.DeserializeObject<List<HiScore>>(File.ReadAllText(SaveFile));
    }

    private void SaveScores()
    {
        File.WriteAllText(SaveFile, Newtonsoft.Json.JsonConvert.SerializeObject(hiScores));
    }
}