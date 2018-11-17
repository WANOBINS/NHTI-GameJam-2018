using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    
    public float bulletSpeed = 1f;

    // Update is called once per frame
    void Update ()
    {
        transform.position += transform.position * (bulletSpeed * Time.deltaTime);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RedPlayer" || other.tag == "BluePlayer")
        {
            Destroy(other);
            Destroy(gameObject);
        }
        else if (other.tag == "boundry")
        {
            Destroy(gameObject);
        }
    }
}
