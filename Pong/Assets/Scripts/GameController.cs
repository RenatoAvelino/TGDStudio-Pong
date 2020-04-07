using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject batonPrefab;
    void Start()
    {
        batonPrefab = Resources.Load<GameObject>("Prefabs/Baton");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject[] CreateBatons()
    {
        GameObject[] batons = new GameObject[2]; //Create the Array
        for(int i = 0; i < 2; i++){
            GameObject temp = GameObject.Instantiate(batonPrefab);
            temp.transform.position = new Vector3((7 - (14 * i)), 0.5f, 0);//Set the initial position of the batons
            batons[i] = temp;
        }
        return batons; //Return the Array
    }
}
