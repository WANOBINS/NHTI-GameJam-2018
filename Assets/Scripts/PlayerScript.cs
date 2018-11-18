using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    [Range(1,2)]
    public int Player = 1;
    public Rigidbody myRB;
    public Vector4 bounds;
    public float speed;
    public float tilt;
    public GameObject MyBullet;
    public Transform Gun1;
    public Transform Gun2;

    public float ShotSpeed = 10f;


    // Use this for initialization
    void Start ()
    {
        myRB = transform.GetComponent<Rigidbody>();
	}

    void FixedUpdate()
    {
        if (Player == 1)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            myRB.velocity = movement * speed;

            myRB.position = new Vector3
            (
                Mathf.Clamp(myRB.position.x, bounds.x, bounds.y),
                0.0f,
                Mathf.Clamp(myRB.position.z, bounds.z, bounds.w)
            );

            myRB.rotation = Quaternion.Euler(myRB.velocity.z * -tilt, 180.0f, 0.0f);

            if(Input.GetButtonDown("Fire1"))
            {
                GameObject NewBullet1 = Instantiate(MyBullet, Gun1.position, Gun1.rotation);
                GameObject NewBullet2 = Instantiate(MyBullet, Gun2.position, Gun2.rotation);

                NewBullet1.GetComponent<Rigidbody>().AddForce(Vector3.right * ShotSpeed);
                NewBullet2.GetComponent<Rigidbody>().AddForce(Vector3.right * ShotSpeed);
            }
        }
        else
        {
            float moveHorizontal = Input.GetAxis("Horizontal2");
            float moveVertical = Input.GetAxis("Vertical2");

            if(moveHorizontal > 0)
            {
                moveHorizontal *= 1.1f;
            }

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            
            myRB.velocity = movement * speed;

            myRB.position = new Vector3
            (
                Mathf.Clamp(myRB.position.x, bounds.x, bounds.y),
                0.0f,
                Mathf.Clamp(myRB.position.z, bounds.z, bounds.w)
            );

            myRB.rotation = Quaternion.Euler(myRB.velocity.z * -tilt, 180.0f, 0.0f);

            if (Input.GetButtonDown("Fire2"))
            {
                GameObject NewBullet1 = Instantiate(MyBullet, Gun1.position, Gun1.rotation);
                GameObject NewBullet2 = Instantiate(MyBullet, Gun2.position, Gun2.rotation);

                NewBullet1.GetComponent<Rigidbody>().AddForce(Vector3.right * ShotSpeed);
                NewBullet2.GetComponent<Rigidbody>().AddForce(Vector3.right * ShotSpeed);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fighter")
        {

        }
    }
}
