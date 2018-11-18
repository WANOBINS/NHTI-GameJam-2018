using Assets.Scripts.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using WBase.Core.Extensions;

public class ScoreManager : MonoBehaviour
{
    [JsonObject]
    public class HiScore
    {
        [JsonProperty]
        public string Initials { get; private set; }
        [JsonProperty]
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
    public int P1CurrentScore = 0;
    public int P2CurrentScore = 0;
    
    public List<HiScore> hiScores;

    public FileStream FileStream;

    private void Start()
    {
        hiScores = new List<HiScore>();
        Directory.CreateDirectory(SaveDirectory);
        if (!File.Exists(SaveFile))
        {
            hiScores = DefaultHiScores.ToList();
            File.Create(SaveFile).Close();
            SaveScores();
        }
        else
        {
            LoadScores();
        }


    }

    /// <summary>
    /// Adds the submitted score to the hiScores list, wasteful if score is not above the lowest on the leaderboard.
    /// </summary>
    /// <param name="hiScore"></param>
    public void SaveScore(HiScore hiScore)
    {
        hiScores.Add(hiScore);
        SortScores();
        hiScores.Remove(hiScores.Last());
    }

    public void ResetScore(EnumPlayer player)
    {
        switch (player)
        {
            case EnumPlayer.Player1:
                P1CurrentScore = 0;
                break;
            case EnumPlayer.Player2:
                P2CurrentScore = 0;
                break;
            case EnumPlayer.Both:
                P1CurrentScore = 0;
                P2CurrentScore = 0;
                break;
        }
        
        
    }

    public void AddScore(EnumPlayer player, int points)
    {
        switch (player)
        {
            case EnumPlayer.Player1:
                P1CurrentScore += points;
                break;
            case EnumPlayer.Player2:
                P2CurrentScore += points;
                break;
            case EnumPlayer.Both:
                P1CurrentScore += points;
                P2CurrentScore += points;
                break;
        }
    }

    public void LoadScores()
    {
        hiScores = JsonConvert.DeserializeObject<HiScore[]>(File.ReadAllText(SaveFile)).ToList<HiScore>();
        SortScores();
    }

    public void SaveScores()
    {
        SortScores();
        File.WriteAllText(SaveFile, JsonConvert.SerializeObject(hiScores));
    }

    private void SortScores()
    {
        hiScores.Sort((x, y) => {

            int result = -x.Score.CompareTo(y.Score);

            if(result == 0 && x.Initials != null && y.Initials != null)
            {
                return x.Initials.CompareTo(y.Initials); //Sort by Alpha
            }
            else
            {
                return result; //Sort by Score
            }
        });
    }

    public string GetHiScoresString()
    {
        SortScores();
        StringBuilder stringBuilder = new StringBuilder();
        foreach(HiScore hiScore in hiScores)
        {
            stringBuilder.Append(hiScore.Initials);
            stringBuilder.Append("\t");
            stringBuilder.Append(hiScore.Score);
            stringBuilder.Append(Environment.NewLine);
        }
        return stringBuilder.ToString();
    }
}