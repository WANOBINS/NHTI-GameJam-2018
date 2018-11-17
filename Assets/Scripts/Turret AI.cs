using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour
{
    public bool isRed = false;
    [SerializeField] private float range = 1f;
  
	// Use this for initialization
	void Start ()
    {
        isRed = (Random.Range(0, 1) == 0);
	}
	
	// Update is called once per frame
	void Update ()
    {
       if( Vector3.Distance(gameObject.transform.position, GameObject.FindGameObjectWithTag("RedPlayer").transform.position ) <= range)
        {

        }
        else if (Vector3.Distance(gameObject.transform.position, GameObject.FindGameObjectWithTag("BluePlayer").transform.position) <= range)
        {

        }
    }
}
