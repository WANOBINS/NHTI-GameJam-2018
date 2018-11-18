using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Util;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public bool DebugQuit = true;
    public bool GameOver = false;

    public int P1Lives = 3;
    public int P2Lives = 3;

    public ScoreManager ScoreManager;
    private ScoreScreenManager scoreScreenManager;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        ScoreManager = gameObject.GetComponent<ScoreManager>();
        SceneManager.LoadScene("HighScores", LoadSceneMode.Additive);
        SceneManager.LoadScene("Whatever", LoadSceneMode.Additive);//Allows me to work in my scene and have it be merged into the main scene at runtime\
    }

    // Use this for initialization
    void Start () {
        scoreScreenManager = GameObject.Find("HiScoresCanvas").GetComponent<ScoreScreenManager>();
        scoreScreenManager.HideUI();
    }
	
	// Update is called once per frame
	void Update () {
        if (DebugQuit && Input.GetKey(KeyCode.Escape))
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
                EditorApplication.OpenURL("https://google.com");
#else
                Application.Quit();
#endif
        }

        if(P1Lives <= 0 && P2Lives <= 0)
        {
            GameOver = true;
            if(ScoreManager.P1CurrentScore + ScoreManager.P2CurrentScore >= ScoreManager.hiScores.Last().Score)
            {
                ScoreManager.AddScore(EnumPlayer.Player1, ScoreManager.P2CurrentScore);
                scoreScreenManager.SaveP1Score();
            }
        }
    }
}
