using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public List<GameObject> Enemies;

    public float Speed;

    public float SpawnCountDown;
    public float SpawnTimer = 5f;

    public GameObject[] EnemiesOnScreen;
    public GameManager myGM;

    public bool IsTurret;


    // Use this for initialization
    void Start()
    {
        myGM = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Sets our SpawnTimer to count down correctly
        SpawnTimer -= Time.deltaTime;

        //Update our ObstaclesOnScreen List 
        if(IsTurret)
        {
            EnemiesOnScreen = GameObject.FindGameObjectsWithTag("Turret");
        }
        else
        {
            EnemiesOnScreen = GameObject.FindGameObjectsWithTag("Fighter");
        }
        

        if (myGM.P1GameOver == false && myGM.P2GameOver == false)
        {
            //When SpawnTimer is zero we spawn an Obstacle
            if (SpawnTimer <= 0)
            {
                //Check to see if we have enough obstacles on screen already

                //Spawning of obstacle and adding force so it moves towards our player
                    GameObject NewEnemy = Instantiate(Enemies[Random.Range(0, Enemies.Count)], transform.position, transform.rotation);
                    NewEnemy.GetComponent<Rigidbody>().AddForce(-Vector3.right * Speed, ForceMode.Impulse);
                    //Set the spawn timer back to the CountDown time
                    SpawnTimer = SpawnCountDown;

            }
        }
    }
}
