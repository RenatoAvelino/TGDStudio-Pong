using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class BallTests
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
        public void BallTestsSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        [Test]
        public void Hit_Player_Result_Zero() //Test if the method hitPlayer return zero
        {
            GameObject ball = script.CreateBall();
            Vector3 player = new Vector3(0, 0, 5);
            Vector3 vBall = new Vector3(0, 0, 5);
            float size = 3;
            float test = ball.GetComponent<BallScript>().hitPlayer(vBall, player, size);

            Assert.AreEqual(0, test);
        }

        [Test]
        public void Hit_Player_Result_Positive() //Test if the method hitPlayer return positive 
        {
            GameObject ball = script.CreateBall();
            Vector3 player = new Vector3(0, 0, 3);
            Vector3 vBall = new Vector3(0, 0, 5);
            float size = 3;
            float test = ball.GetComponent<BallScript>().hitPlayer(vBall, player, size);

            Assert.Greater(test, 0);
        }

        [Test]
        public void Hit_Player_Result_Negative() //Test if the method hitPlayer return negative
        {
            GameObject ball = script.CreateBall();
            Vector3 player = new Vector3(0, 0, 5);
            Vector3 vBall = new Vector3(0, 0, 3);
            float size = 3;
            float test = ball.GetComponent<BallScript>().hitPlayer(vBall, player, size);

            Assert.Less(test, 0);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator BallTestsWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }

        [UnityTest]
        public IEnumerator Ball_Stays_In_Upper_Camera_Bounds() //Test if the ball stays on screen
        {
            //Time.timeScale = 20.0f; //Speeds the test
            GameObject ball = script.CreateBall();

            //Arbitrary values
            float linSpeed = 0f; //For x
            float angSpeed = 1.0f; // For z
            ball.GetComponent<BallScript>().speed = 4;

            float time = 0;
            while (time < 5)
            {
                ball.GetComponent<BallScript>().MoveBallWithValues(linSpeed, angSpeed);
                time += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
            Time.timeScale = 1.0f; //Reload the original time speed

            Assert.LessOrEqual(ball.transform.position.z, (Camera.main.sensorSize.y - ball.transform.localScale.z / 2));//Check if the ball escaped
        }

        [UnityTest]
        public IEnumerator Ball_Stays_In_Lower_Camera_Bounds() //Test if the ball stays on screen
        {
            Time.timeScale = 20.0f; //Speeds the test
            GameObject ball = script.CreateBall();

            //Arbitrary values
            float linSpeed = 0f; //For x
            float angSpeed = -1.0f; // For z
            ball.GetComponent<BallScript>().speed = 4;

            float time = 0;
            while (time < 5)
            {
                ball.GetComponent<BallScript>().MoveBallWithValues(linSpeed, angSpeed);
                time += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
            Time.timeScale = 1.0f; //Reload the original time speed

            Assert.GreaterOrEqual(ball.transform.position.z, (-Camera.main.sensorSize.y + ball.transform.localScale.z / 2));//Check if the ball escaped
        }

        [UnityTest]
        public IEnumerator Ball_Collides_In_Middle_Of_Left_Baton()
        {
            Time.timeScale = 20.0f; //Speeds the test
            GameObject[] batons = script.CreateBatons();
            GameObject ball = script.CreateBall();

            //Arbitrary values
            float linSpeed = 1.0f; //For x
            float angSpeed = 0f; // For z
            ball.GetComponent<BallScript>().speed = 4;
            ball.GetComponent<BallScript>().isTest = true;

            ball.GetComponent<BallScript>().MoveBallWithValues(linSpeed, angSpeed);

            float time = 0;
            while(time < 5)
            {
                time += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            Assert.AreEqual(1, ball.GetComponent<BallScript>().testHelper);
        }

        [UnityTest]
        public IEnumerator Ball_Collides_In_Top_Of_Left_Baton()
        {
            Time.timeScale = 20.0f; //Speeds the test
            GameObject[] batons = script.CreateBatons();
            GameObject ball = script.CreateBall();
            ball.transform.position = new Vector3(0, 0.5f, 1.2f);

            //Arbitrary values
            float linSpeed = 1.0f; //For x
            float angSpeed = 0f; // For z
            ball.GetComponent<BallScript>().speed = 4;
            ball.GetComponent<BallScript>().isTest = true;

            ball.GetComponent<BallScript>().MoveBallWithValues(linSpeed, angSpeed);

            float time = 0;
            while (time < 5)
            {
                time += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            Assert.AreEqual(2, ball.GetComponent<BallScript>().testHelper);
        }

        [UnityTest]
        public IEnumerator Ball_Collides_In_Bottom_Of_Left_Baton()
        {
            Time.timeScale = 20.0f; //Speeds the test
            GameObject[] batons = script.CreateBatons();
            GameObject ball = script.CreateBall();
            ball.transform.position = new Vector3(0, 0.5f, -1.2f);

            //Arbitrary values
            float linSpeed = 1.0f; //For x
            float angSpeed = 0f; // For z
            ball.GetComponent<BallScript>().speed = 4;
            ball.GetComponent<BallScript>().isTest = true;

            ball.GetComponent<BallScript>().MoveBallWithValues(linSpeed, angSpeed);

            float time = 0;
            while (time < 5)
            {
                time += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            Assert.AreEqual(3, ball.GetComponent<BallScript>().testHelper);
        }

        [UnityTest]
        public IEnumerator Ball_Collides_In_Middle_Of_Right_Baton()
        {
            Time.timeScale = 20.0f; //Speeds the test
            GameObject[] batons = script.CreateBatons();
            GameObject ball = script.CreateBall();

            //Arbitrary values
            float linSpeed = -1.0f; //For x
            float angSpeed = 0f; // For z
            ball.GetComponent<BallScript>().speed = 4;
            ball.GetComponent<BallScript>().isTest = true;

            ball.GetComponent<BallScript>().MoveBallWithValues(linSpeed, angSpeed);

            float time = 0;
            while (time < 5)
            {
                time += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            Assert.AreEqual(-1, ball.GetComponent<BallScript>().testHelper);
        }

        [UnityTest]
        public IEnumerator Ball_Collides_In_Top_Of_Right_Baton()
        {
            Time.timeScale = 20.0f; //Speeds the test
            GameObject[] batons = script.CreateBatons();
            GameObject ball = script.CreateBall();
            ball.transform.position = new Vector3(0, 0.5f, 1.2f);

            //Arbitrary values
            float linSpeed = -1.0f; //For x
            float angSpeed = 0f; // For z
            ball.GetComponent<BallScript>().speed = 4;
            ball.GetComponent<BallScript>().isTest = true;

            ball.GetComponent<BallScript>().MoveBallWithValues(linSpeed, angSpeed);

            float time = 0;
            while (time < 5)
            {
                time += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            Assert.AreEqual(-2, ball.GetComponent<BallScript>().testHelper);
        }

        [UnityTest]
        public IEnumerator Ball_Collides_In_Bottom_Of_Right_Baton()
        {
            Time.timeScale = 20.0f; //Speeds the test
            GameObject[] batons = script.CreateBatons();
            GameObject ball = script.CreateBall();
            ball.transform.position = new Vector3(0, 0.5f, -1.2f);

            //Arbitrary values
            float linSpeed = -1.0f; //For x
            float angSpeed = 0f; // For z
            ball.GetComponent<BallScript>().speed = 4;
            ball.GetComponent<BallScript>().isTest = true;

            ball.GetComponent<BallScript>().MoveBallWithValues(linSpeed, angSpeed);

            float time = 0;
            while (time < 5)
            {
                time += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            Assert.AreEqual(-3, ball.GetComponent<BallScript>().testHelper);
        }

        [TearDown]
        public void TearDown()
        {
            GameObject.Destroy(board);
            script.DestroyBatons();
            GameObject ball = GameObject.Find("Ball(Clone)");
            GameObject.Destroy(ball);
        }
    }
}
