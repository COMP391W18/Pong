using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallComponent : MonoBehaviour {

    // The direction the paddle is moving
    private float VerticalDirection;
    private float HorizontalDirection;

    // How fast the paddle moves
    public float MovementSpeed = 10f;
    
    GameController GameControllerRef;

    public void resetBall()
    {
        // Reset the ball position
        transform.localPosition = new Vector3(0f, 0f);

        // Selected a random direction
        VerticalDirection = Random.value < 0.5 ? MovementSpeed : -MovementSpeed;
        HorizontalDirection = Random.value < 0.5 ? MovementSpeed : -MovementSpeed;
    }

    // Use this for initialization
    void Start ()
    {
        // Cache the game controller component
        GameControllerRef = GameObject.Find("GameController").GetComponent<GameController>();

        // Reset the ball position
        resetBall();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (GameControllerRef.GetGameState() == GameController.GameState.ACTIVE)
            // Move the ball based on the current direction
            transform.localPosition += new Vector3(HorizontalDirection, VerticalDirection, 0);
    }

    // If we touch a paddle or the bounds
    void OnTriggerEnter2D(Collider2D other)
    {
        // Different response based on what we touch
        switch (other.name)
        {
            case "VerticalBounds":
                VerticalDirection *= -1f;   // Invert the vertical direction
                break;
            case "HorizontalBounds":
                HorizontalDirection *= -1f; // Invert the horizontal direction
                break;
            case "PlayerOne":
            case "PlayerTwo":
                HorizontalDirection *= -1f; // Invert the horizontal direction
                break;
        }
        
    }
}
