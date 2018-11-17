using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public bool DebugQuit = true;
    public bool GameOver = false;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(transform);
        SceneManager.LoadScene("Colby's Scene", LoadSceneMode.Additive);
        SceneManager.LoadScene("Whatever", LoadSceneMode.Additive);//Allows me to work in my scene and have it be merged into the main scene at runtime
    }
	
	// Update is called once per frame
	void Update () {
        if (DebugQuit && Input.GetKey(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
                EditorApplication.OpenURL("https://google.com");
#else
            Application.Quit();
#endif
        }
	}
}
