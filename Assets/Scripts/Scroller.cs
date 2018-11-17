using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour {

    public float scrollSpeed;
    public float tileSize;

    private Vector2 savedOffset;

    // Use this for initialization
    void Start ()
    {
        savedOffset = GetComponent<Renderer>().sharedMaterial.GetTextureOffset("_MainTex");
        tileSize = transform.lossyScale.y;
    }
	
	// Update is called once per frame
	void Update ()
    {
        float scroll_Y = Mathf.Repeat(Time.time * scrollSpeed, tileSize);
        Vector2 offset = new Vector2(savedOffset.x, scroll_Y);
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}