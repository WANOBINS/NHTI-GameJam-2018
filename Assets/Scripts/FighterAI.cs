using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterAI : MonoBehaviour
{
    public GameObject targetPlayer;
    public float MoveSpeed = 10;

    public float frequency = 20f;
    public float magnitude = 5f;

    public bool IsRed = false;
    private Vector3 pos;
	// Use this for initialization
	void Start ()
    {
        IsRed = (Random.Range(0, 1) == 0);
        pos = transform.position;

        //frequency = Random.Range(1.2f, 2.2f);
        //magnitude = Random.Range(20f, 30f);

        transform.rotation = Quaternion.Euler(90f, 0, 90f);

    }
	
	// Update is called once per frame
	void Update ()
    {
        pos += transform.up * Time.deltaTime * MoveSpeed;
        transform.position = pos + transform.right * Mathf.Sin(Time.time * frequency) * magnitude;
	}
}
