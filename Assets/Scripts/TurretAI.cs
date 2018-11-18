using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Util;
using UnityEngine;

public class TurretAI : MonoBehaviour
{
    public bool isRed = false;
    public float speed = 1f;
    public float health = 10;
    public int rand;
    
    [SerializeField] private float range = 1f;

    Quaternion aimRotation;
    Vector3 lastTargetPos = Vector3.zero;
    public GameObject targetPlayer = null;
    public GameObject Bullet = null;
    public GameObject BulletSpawn;
   
    public ScoreManager mySM;
    
	// Use this for initialization
	void Start ()
    {
       
        mySM = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreManager>();

        if (GameObject.FindGameObjectWithTag("BluePlayer") != null)
        {
            rand = Random.Range(0, 2);
            isRed = (rand == 0);
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
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(gameObject.transform.position, GameObject.FindGameObjectWithTag("RedPlayer").transform.position) <= range)
        {
            targetPlayer = GameObject.FindGameObjectWithTag("RedPlayer");
        }
        else if (Vector3.Distance(gameObject.transform.position, GameObject.FindGameObjectWithTag("BluePlayer").transform.position) <= range)
        {
            targetPlayer = GameObject.FindGameObjectWithTag("BluePlayer");
        }

        if (targetPlayer)
        {
            if (lastTargetPos != targetPlayer.transform.position)
            {
                lastTargetPos = targetPlayer.transform.position;
                aimRotation = Quaternion.LookRotation(lastTargetPos - transform.position);
            }

            if (transform.rotation != aimRotation)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, aimRotation, speed * Time.deltaTime);
            }

            if (!Bullet)
            {
                Fire();
            }    
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "P1Bullet" && isRed)
        {
            health--;
            Destroy(other.gameObject);
            if (health <= 0)
            {
                mySM.AddScore(EnumPlayer.Player1, 300);
                Destroy(gameObject);
                
            }
        }
        else if (other.gameObject.tag == "P2Bullet" && !isRed)
        {
            health--;
            Destroy(other.gameObject);
            if (health <= 0)
            {
                mySM.AddScore(EnumPlayer.Player2, 300);
                Destroy(gameObject);
                
            }
        }
        else if(other.gameObject.tag == "P1Bullet" || other.gameObject.tag == "P2Bullet")
        {
            Destroy(other.gameObject);
        }
    }
    private void Fire()
    {
        Bullet = Instantiate(Bullet, BulletSpawn.transform.position, BulletSpawn.transform.rotation);
    }    
}
