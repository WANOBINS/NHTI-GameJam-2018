using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Util;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public bool DebugQuit = true;
    public bool GameOver = false;

    public GameObject Player1;
    public GameObject Player2;
    public Vector3 P1Spawn;
    public Vector3 P2Spawn;

    public Text P1LivesText;
    public Text P2LivesText;



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
        ScoreManager.LoadScores();
        P1Spawn = GameObject.FindGameObjectWithTag("RedPlayer").transform.position;
        P2Spawn = GameObject.FindGameObjectWithTag("BluePlayer").transform.position;
        scoreScreenManager = GameObject.Find("HiScoresCanvas").GetComponent<ScoreScreenManager>();
        scoreScreenManager.HideUI();
    }

    public void PlayerDied(GameObject dedplayer)
    {
        int temp = dedplayer.GetComponent<PlayerScript>().Player;
        Destroy(dedplayer);
        StartCoroutine(SpawnPlayer(temp, 1f));
    }
    IEnumerator SpawnPlayer(int player, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        if (player == 1)
        {
            P1Lives--;
            if (P1Lives > 0)
            {
                Instantiate(Player1, P1Spawn, Quaternion.Euler(0, 0, 0));
            }
        }
        if (player == 2)
        {
            P2Lives--;
            if (P2Lives > 0)
            {
                Instantiate(Player2, P2Spawn, Quaternion.Euler(0, 0, 0));
            }
        }

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

        

        P1LivesText.text = "x" + P1Lives.ToString();
        P2LivesText.text = "x" + P2Lives.ToString();

        if (P1Lives <= 0 && P2Lives <= 0)
        {
            GameOver = true;
            if(ScoreManager.P1CurrentScore + ScoreManager.P2CurrentScore >= ScoreManager.hiScores.Last().Score)
            {
                ScoreManager.AddScore(EnumPlayer.Player1, ScoreManager.P2CurrentScore);
                scoreScreenManager.SaveP1Score();
            }
        }
    }

    [Obsolete("NEVER CALL")]
    public static void RestartGame()
    {
        List<AsyncOperation> asyncOps = new List<AsyncOperation>();
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            if (SceneManager.GetSceneByBuildIndex(i).isLoaded)
            {
                asyncOps.Add(SceneManager.UnloadSceneAsync(i));
            }
        }
        while (asyncOps.Any((AsyncOperation op) => !op.isDone)) { };
        SceneManager.LoadScene("MainScene");
    }
}
