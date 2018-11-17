using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterAI : MonoBehaviour
{
    public GameObject targetPlayer;
    public float MoveSpeed;

    public float frequency = 20f;
    public float magnitude = 5f;

    public bool IsRed = false;
    private Vector3 pos;
	// Use this for initialization
	void Start ()
    {
        IsRed = (Random.Range(0, 1) == 0);
        pos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = pos + transform.forward * Mathf.Sin(Time.time * frequency) * magnitude;
	}
}
