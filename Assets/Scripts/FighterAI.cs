﻿using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Util;
using UnityEngine;

public class FighterAI : MonoBehaviour
{
    public GameObject targetPlayer;
    public float MoveSpeed = 10;

    public float frequency = 20f;
    public float magnitude = 5f;
    public float health = 3;
    public Color HitFlash;

    public bool isRed = false;
    private Vector3 pos;
    public ScoreManager mySM;

    // Use this for initialization
    void Start ()
    {
        mySM = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreManager>();
        if (GameObject.FindGameObjectWithTag("BluePlayer") != null)
        {
            isRed = (Random.Range(0, 2) == 0);
        }
        else
        {
            isRed = true;
        }

        if (isRed)
        {
            this.GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            this.GetComponent<Renderer>().material.color = Color.blue;
        }

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
        if (this.GetComponent<Renderer>().material.color == Color.white)
        {
            Invoke("ResetMat", 0.05f);
        }
	}

    void ResetMat()
    {
        if (isRed)
        {
            this.GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            this.GetComponent<Renderer>().material.color = Color.blue;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "P1Bullet" && isRed)
        {
            this.GetComponent<Renderer>().material.color = Color.white;
            health--;
            Destroy(other.gameObject);
            if (health <= 0)
            {
                mySM.AddScore(EnumPlayer.Player1, 100);
                Destroy(gameObject);

            }
        }
        else if (other.gameObject.tag == "P2Bullet" && !isRed)
        {
            this.GetComponent<Renderer>().material.color = Color.white;
            health--;
            Destroy(other.gameObject);
            if (health <= 0)
            {
                mySM.AddScore(EnumPlayer.Player2, 100);
                Destroy(gameObject);

            }
        }
        else if (other.gameObject.tag == "P1Bullet" || other.gameObject.tag == "P2Bullet")
        {
            Destroy(other.gameObject);
        }
    }
}
