using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundComponent : MonoBehaviour {

    GameController GameControllerRef;

    // Use this for initialization
    void Start () {
        // Cache the game controller component
        GameControllerRef = GameObject.Find("GameController").GetComponent<GameController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Ball")
        {
            // Increment the score
            GameControllerRef.IncrementScore(other.transform.localPosition.x < 0 ? "PlayerTwo" : "PlayerOne");

            // Reset the ball position
            other.GetComponentInParent<BallComponent>().resetBall();

            // Reset the paddles position
            GameObject.Find("PlayerOne").GetComponent<PaddleController>().ResetPaddle();
            GameObject.Find("PlayerTwo").GetComponent<PaddleController>().ResetPaddle();
        }
        else if (other.name == "PowerUp")
        {
            other.GetComponentInParent<PowerUpComponent>().Deactivate();
            GameControllerRef.ResetPowerUp();
        }

    }
}
