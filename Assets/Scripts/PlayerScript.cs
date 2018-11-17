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
    
    // Use this for initialization
    void Start ()
    {
        myRB = transform.GetComponent<Rigidbody>();
	}

    void FixedUpdate()
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

        myRB.rotation = Quaternion.Euler(myRB.velocity.z * -tilt, 180.0f , 0.0f);
    }
}
