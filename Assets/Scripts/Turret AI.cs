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
    GameObject targetPlayer = null;
    
	// Use this for initialization
	void Start ()
    {
        isRed = (Random.Range(0, 1) == 0);
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
        }
    }
}
