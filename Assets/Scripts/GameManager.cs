﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public bool GameOver = false;
    public ScoreManager ScoreManager;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        ScoreManager = gameObject.AddComponent<ScoreManager>();
        SceneManager.LoadScene("Colby's Scene", LoadSceneMode.Additive);
        SceneManager.LoadScene("Whatever", LoadSceneMode.Additive);//Allows me to work in my scene and have it be merged into the main scene at runtime
    }

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
	}
}
