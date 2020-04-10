using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameObject[] batons;
    private GameObject ball;
    public bool istest = false;
    // Start is called before the first frame update
    void Start()
    {
        if (!istest)//Just not to interfere with testing
        {
            ball = CreateBall();
            batons = CreateBatons();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape)) Application.Quit(); //Ends the Game
        if (!istest)//Just not to interfere with testing
        {
            if (ball.GetComponent<BallScript>().testHelper == 5) //Test if the ball enters in left goal
            {
                batons[0].GetComponent<BatonScript>().points++;
                DestroyBall(); // Destroy the ball
                ball = CreateBall(); //Recreate the ball
            }
            else if (ball.GetComponent<BallScript>().testHelper == -5) //Test if the ball enters in right goal
            {
                batons[1].GetComponent<BatonScript>().points++;
                DestroyBall(); // Destroy the ball
                ball = CreateBall(); //Recreate the ball
            }
        }
    }

    public GameObject[] CreateBatons() //Create and return a Array with 2 batons
    {
        GameObject batonPrefab = Resources.Load<GameObject>("Prefabs/Baton");
        GameObject[] batons = new GameObject[2]; //Create the Array
        for(int i = 0; i < 2; i++){
            GameObject temp = GameObject.Instantiate(batonPrefab);
            temp.transform.position = new Vector3((7 - (14 * i)), 0.5f, 0);//Set the initial position of the batons
            temp.GetComponent<BatonScript>().SetAxis(i); //Set the controls for the baton
            batons[i] = temp;
        }

        return batons; //Return the Array
    }

    public GameObject CreateBall() //Create the Game Ball
    {
        GameObject ballPrefab = Resources.Load<GameObject>("Prefabs/Ball");
        GameObject ball = GameObject.Instantiate(ballPrefab);
        ball.transform.position = new Vector3(0, 0.5f, 0);

        return ball; //Return the Ball
    }

    public void DestroyBall()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls)
        {
            GameObject.DestroyImmediate(ball); //Destroy the Ball (I tried to use only Destroy but it didn't work)
        }
        
    }

    public void DestroyBatons()
    {
        foreach (GameObject baton in batons)
        {
            Destroy(baton); //Destroy the Baton
        }
    }

    public void OnGUI()
    {
        if (!istest) //Just not to interfere with testing
        {
            int centerX = Screen.width / 2;
            int offset = 30;
            GUI.Label(new Rect(new Vector2(centerX - offset, 20), new Vector2(100, 100)), batons[1].GetComponent<BatonScript>().points.ToString());
            GUI.Label(new Rect(new Vector2(centerX + offset, 20), new Vector2(100, 100)), batons[0].GetComponent<BatonScript>().points.ToString());
        }
    }
}
