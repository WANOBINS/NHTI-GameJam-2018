using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour {
    private ScoreManager scoreManager;
    private Text P1Score;
    private Text P2Score;
    private const string P1ScorePrefix = "Player 1: ";
    private const string P2ScorePrefix = "Player 2: ";
    // Use this for initialization
    void Start () {
        scoreManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreManager>();
        P1Score = transform.Find("P1 Score").GetComponent<Text>();
        P2Score = transform.Find("P2 Score").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        P1Score.text = P1ScorePrefix + scoreManager.P1CurrentScore;
        P2Score.text = P2ScorePrefix + scoreManager.P2CurrentScore;
	}
}
