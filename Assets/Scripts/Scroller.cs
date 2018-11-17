using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour {

    public float scrollSpeed;
    public float tileSize;

    private Vector3 startPos;

    // Use this for initialization
    void Start ()
    {
        startPos = transform.position;
        tileSize = transform.lossyScale.y;

    }
	
	// Update is called once per frame
	void Update ()
    {
        float newPos = Mathf.Repeat(Time.time * scrollSpeed, tileSize);
        transform.position = startPos + Vector3.forward * newPos;
    }
}
