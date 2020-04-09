using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] batons = CreateBatons();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        GameObject ball = GameObject.FindGameObjectWithTag("Ball"); //Find Ball
        GameObject.DestroyImmediate(ball, true); //Destroy the Ball (I tried to use only Destroy but it didn't work)
    }

    public void DestroyBatons()
    {
        GameObject[] batons = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject baton in batons)
        {
            Destroy(baton);
        }
    }
}
