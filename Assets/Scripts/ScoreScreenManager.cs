using Assets.Scripts.Util;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreScreenManager : MonoBehaviour {
    ScoreManager scoreManager;
    EnumPlayer player = EnumPlayer.Neither;
    Text HiScoresList;
    Text Prompt;
    GameObject Canvas;
    private const string P1Prompt = "Enter your team initials:";
    private const string P2Prompt = "Player 2, enter your initials:";

    public void HideUI()
    {
        Canvas.SetActive(false);
    }

    public void ShowUI()
    {
        Canvas.SetActive(true);
        HiScoresList.text = scoreManager.GetHiScoresString();
    }

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        Canvas = GameObject.Find("HiScoresCanvas");
        Prompt = GameObject.Find("Prompt").GetComponent<Text>();
        HiScoresList = GameObject.Find("HiScores").GetComponent<Text>();
        scoreManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreManager>();
    }

    // Use this for initialization
    void Start () {
        
    }



    public void SaveP1Score()
    {
        player = EnumPlayer.Player1;
        Prompt.text = P1Prompt;
        ShowUI();
    }

    public void SaveP2Score()
    {
        player = EnumPlayer.Player2;
        Prompt.text = P2Prompt;
        ShowUI();
    }

    public void SaveScore()
    {
        scoreManager.SaveScore(new ScoreManager.HiScore(Canvas.transform.Find("Initials").Find("Text").GetComponent<Text>().text, scoreManager.P1CurrentScore));
    }
}
