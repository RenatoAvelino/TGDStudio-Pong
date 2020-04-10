using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public float speed = 6.0f; //Speed
    public int testHelper = 0; //Used to assist in testing void methods
    public bool isTest = false;
    // Start is called before the first frame update
    void Start()
    {
        if(!isTest) GetComponent<Rigidbody>().velocity = Vector3.right * speed; //Move the ball when the game starts
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   void OnCollisionEnter(Collision collision) //Checks whether the ball has collided
   {
        GetComponent<AudioSource>().Play();
        if(collision.gameObject.tag == "Player") //Checks whether the ball hit a Player
        {
            float temp = hitPlayer(transform.position, collision.transform.position, collision.collider.bounds.size.z);
            if (isTest)
            {
                if (temp == 0) testHelper = 1;
                else if (temp > 0) testHelper = 2;
                else testHelper = 3;
                if (collision.gameObject.GetComponent<BatonScript>().axis == 0) testHelper *= -1;
                return;
            }
            float newXDir = 1;
            if (collision.gameObject.GetComponent<BatonScript>().axis == 0) newXDir = -1;
            MoveBallWithValues(newXDir, temp);
        }
        else if(collision.gameObject.tag == "TopWall")
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -0.5f));
        }

        else if (collision.gameObject.tag == "BottomWall")
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 0.5f));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "LeftGoal")
        {
            testHelper = 5;
        }
        else
        {
            testHelper = -5;
        }
    }

    public void MoveBallWithValues(float x, float z)
    {
        Vector3 direction = new Vector3(x, 0, z).normalized; //Initiate the speed vector with the speeds in X and Z
        GetComponent<Rigidbody>().velocity = direction * speed; //Move the ball
    }

    public float hitPlayer(Vector3 ball, Vector3 player, float size)
    {
        return (ball.z - player.z) / size;
    }
}
