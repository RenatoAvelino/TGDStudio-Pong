using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatonScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int axis;
    public float speed = 5.0f;
    public int points = 0;
    private KeyCode Up;
    private KeyCode Down;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(Up))
        {
            MoveBatonUp();
        }
        if (Input.GetKey(Down))
        {
            MoveBatonDown();
        }
    }

    public void SetAxis(int newAxis)
    {
        this.axis = newAxis;
        if(this.axis == 1){//Controls for Right baton
            this.Up = KeyCode.W;
            this.Down = KeyCode.S;
        }else {//Controls for the Left baton
            this.Up = KeyCode.UpArrow;
            this.Down = KeyCode.DownArrow;
        }
    }

    public void MoveBatonUp()
    {
        this.transform.Translate(new Vector3(0, 0, 1) * speed * Time.deltaTime);
    }

    public void MoveBatonDown()
    {
        this.transform.Translate(new Vector3(0, 0, -1) * speed * Time.deltaTime);
    }
}
