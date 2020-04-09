using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class BatonTests
    {
        GameController script;
        GameObject board;

        [SetUp]
        public void Setup()
        {
            GameObject tmp = (GameObject)Resources.Load("Prefabs/Board");
            board = GameObject.Instantiate(tmp);
            script = board.GetComponent<GameController>();
        }

        // A Test behaves as an ordinary method
        [Test]
        public void BatonTestsSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator BatonTestsWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }

        [UnityTest]
        public IEnumerator Right_Baton_Stays_In_Upper_Camera_Bounds() //Test if the right baton can escape in the top
        {
            Time.timeScale = 20.0f; //Speeds the test
            GameObject[] batons = script.CreateBatons(); //Create Batons for the test

            float time = 0; 
            while(time < 5){
                batons[0].GetComponent<BatonScript>().MoveBatonUp();
                time += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            Time.timeScale = 1.0f; //Reload the original time speed

            Assert.LessOrEqual(batons[0].transform.position.z, (Camera.main.sensorSize.y - batons[1].transform.localScale.z /2) + 0.15f); //Check if the baton escaped
        }

        [UnityTest]
        public IEnumerator Right_Baton_Stays_In_Lower_Camera_Bounds() //Test if the right baton can escape in the bottom
        {
            Time.timeScale = 20.0f; //Speeds the test
            GameObject[] batons = script.CreateBatons(); //Create Batons for the test

            float time = 0;
            while (time < 5)
            {
                batons[0].GetComponent<BatonScript>().MoveBatonDown();
                time += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            Time.timeScale = 1.0f; //Reload the original time speed

            Assert.GreaterOrEqual(batons[0].transform.position.z, (-Camera.main.sensorSize.y + batons[1].transform.localScale.z / 2) + 0.15f); //Check if the baton escaped
        }

        [UnityTest]
        public IEnumerator Left_Baton_Stays_In_Upper_Camera_Bounds() //Test if the left baton can escape in the top
        {
            Time.timeScale = 20.0f; //Speeds the test
            GameObject[] batons = script.CreateBatons(); //Create Batons for the test

            float time = 0;
            while (time < 5)
            {
                batons[1].GetComponent<BatonScript>().MoveBatonUp();
                time += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            Time.timeScale = 1.0f; //Reload the original time speed

            Assert.LessOrEqual(batons[1].transform.position.z, (Camera.main.sensorSize.y - batons[0].transform.localScale.z / 2) + 0.15f); //Check if the baton escaped
        }

        [UnityTest]
        public IEnumerator Left_Baton_Stays_In_Lower_Camera_Bounds() //Test if the left baton can escape in the bottom
        {
            Time.timeScale = 20.0f; //Speeds the test
            GameObject[] batons = script.CreateBatons(); //Create Batons for the test

            float time = 0;
            while (time < 5)
            {
                batons[1].GetComponent<BatonScript>().MoveBatonDown();
                time += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            Time.timeScale = 1.0f; //Reload the original time speed

            Assert.GreaterOrEqual(batons[1].transform.position.z, (-Camera.main.sensorSize.y + batons[0].transform.localScale.z / 2) + 0.15f); //Check if the baton escaped
        }

        [TearDown]
        public void TearDown()
        {
            GameObject.Destroy(board);
            GameObject[] batons = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject baton in batons)
            {
                GameObject.Destroy(baton);
            }
            GameObject ball = GameObject.Find("Ball(Clone)");
            GameObject.Destroy(ball);
        }
    }
}
