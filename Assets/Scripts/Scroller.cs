using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour {

    public float scrollSpeed;    

    private Vector2 offset;

    // Use this for initialization
    void Start ()
    {
        offset = GetComponent<Renderer>().sharedMaterial.GetTextureOffset("TestBG");

    }
	
	// Update is called once per frame
	void Update ()
    {
       
    }
}
