using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class BoardTests
    {
        // A Test behaves as an ordinary method
        GameController script;
        GameObject board;
        [SetUp]
        public void Setup()
        {
            GameObject tmp = (GameObject)Resources.Load("Prefabs/Board");
            board = GameObject.Instantiate(tmp);
            script = board.GetComponent<GameController>();
        }

        [Test]
        public void BoardTestsSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        [Test]
        public void One_Baton_Is_Succesfully_Created() //Test if the Array has been created
        {
            GameObject[] batons = script.CreateBatons();

            Assert.IsNotNull(batons); //Test if the Array exists
        }

        [Test]
        public void Both_Batons_Are_Succesfully_Created() //Test the creation of both batons 
        {
            GameObject[] batons = script.CreateBatons();

            Assert.AreEqual(2, batons.Length); //Test if the Array has 2 batons 
        }

        [Test]
        public void Ball_Is_Succesfully_Created() //Test the creation of the ball
        {
            GameObject ball = script.CreateBall();

            Assert.IsNotNull(ball); //Test if the ball has been created
        }

        [Test]
        public void Ball_Is_Succesfully_Created_And_Unique()
        {
            GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
            Assert.AreEqual(1, balls.Length);
        }

        [Test]
        public void Ball_Is_Sucefully_Destroyed()
        {
            script.DestroyBall();
            GameObject[] ball = GameObject.FindGameObjectsWithTag("Ball");
            Assert.AreEqual(0, ball.Length);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator BoardTestsWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }


        [TearDown]
        public void TearDown()
        {
            script.DestroyBatons();
            GameObject.Destroy(board);
            GameObject ball = GameObject.Find("Ball(Clone)");
            GameObject.Destroy(ball);
        }
    }
}
