using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour
{
    public bool isRed = false;
    public float speed = 1f;
   
    
    [SerializeField] private float range = 1f;

    Quaternion aimRotation;
    Vector3 lastTargetPos = Vector3.zero;
    public GameObject targetPlayer = null;
    public GameObject Bullet = null;
    public GameObject BulletSpawn;
    public GameObject tBase;
    public GameObject tBarrel;
    
	// Use this for initialization
	void Start ()
    {
        tBase = this.gameObject.transform.Find("Mount").gameObject;
        tBarrel = this.gameObject.transform.Find("Turret").gameObject;

        if (GameObject.FindGameObjectsWithTag("BluePlayer") != null)
        {
            isRed = (Random.Range(0, 1) == 0);
        }
        else isRed = true;
        

        if (isRed)
        {
            tBarrel.GetComponent<Renderer>().material.color = Color.red;
        }
        else tBarrel.GetComponent<Renderer>().material.color = Color.blue;
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

    private void Fire()
    {
        Bullet = Instantiate(Bullet, BulletSpawn.transform.position, BulletSpawn.transform.rotation);
    }    
}
