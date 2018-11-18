using Assets.Scripts.Util;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScreenManager : MonoBehaviour {
    ScoreManager ScoreManager;
    EnumPlayer player = EnumPlayer.Neither;
    Text HiScoresList;
    Text Prompt;
    GameObject Canvas;
    private const string P1Prompt = "Player 1, enter your initials:";
    private const string P2Prompt = "Player 2, enter your initials:";

    public void HideUI()
    {
        Canvas.SetActive(false);
    }

    public void ShowUI()
    {
        Canvas.SetActive(true);
        HiScoresList.text = ScoreManager.hiScores.ToString();
    }

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        Canvas = GameObject.Find("HiScoresCanvas");
        Prompt = GameObject.Find("Prompt").GetComponent<Text>();
        HiScoresList = GameObject.Find("HiScores").GetComponent<Text>();
    }

    // Use this for initialization
    void Start () {
        ScoreManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreManager>();
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

    public void SaveScore(string Initials)
    {
        switch (player)
        {
            case EnumPlayer.Player1:
                ScoreManager.SaveScore(new ScoreManager.HiScore(Initials, ScoreManager.P1CurrentScore));
                break;
            case EnumPlayer.Player2:
                ScoreManager.SaveScore(new ScoreManager.HiScore(Initials, ScoreManager.P2CurrentScore));
                break;
            default:
                break;
        }
    }
}
