using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PaddleController : MonoBehaviour {
        
    // Define a struct with user inputs
    public struct UserInputs
    {
        public KeyCode Up { get; set; }
        public KeyCode Down { get; set; }
    }

    // How fast the paddle moves
    public float MovementSpeed = 10f;

    // The user input
    public UserInputs PaddleCommands;

    GameController GameControllerRef;

    BoxCollider2D[] VerticalBounds;

    // Reset the paddle positions
    public void ResetPaddle()
    {
        // Get the paddle position
        Vector3 PaddleTrans = transform.localPosition;
        PaddleTrans.y = 0f;
        transform.localPosition = PaddleTrans;
    }
    
    // Use this for initialization
    void Start ()
    {
        // Set default key
        if (name == "PlayerOne")
        {
            PaddleCommands.Up = KeyCode.W;
            PaddleCommands.Down = KeyCode.S;
        }
        else if (name == "PlayerTwo")
        {
            PaddleCommands.Up = KeyCode.UpArrow;
            PaddleCommands.Down = KeyCode.DownArrow;
        }

        // Reset the paddle position
        ResetPaddle();

        // Cache the game controller component
        GameControllerRef = GameObject.Find("GameController").GetComponent<GameController>();

        // Cache the bounds components
        VerticalBounds = GameObject.Find("VerticalBounds").GetComponents<BoxCollider2D>();
    }

    // Function to update the paddle position
    void UpdatePaddlePosition()
    {
        // Get the paddle position
        Vector3 PaddleTrans = transform.localPosition;

        // Update the paddle position based on the pressed key while keeping the paddle in the playable area
        if (Input.GetKey(PaddleCommands.Up) && !GetComponent<BoxCollider2D>().IsTouching(VerticalBounds[0]))
            PaddleTrans.y += MovementSpeed;
        else if (Input.GetKey(PaddleCommands.Down) && !GetComponent<BoxCollider2D>().IsTouching(VerticalBounds[1]))
            PaddleTrans.y -= MovementSpeed;

        // Update the paddle position
        gameObject.transform.localPosition = PaddleTrans;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (GameControllerRef.GetGameState() == GameController.GameState.ACTIVE)
            // Update paddle position
            UpdatePaddlePosition();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Different response based on what we touch
        switch (other.name)
        {           
            case "PowerUp":
                switch (other.GetComponentInParent<PowerUpComponent>().GetPowerUpType())
                {
                    case PowerUpComponent.Types.PADDLE_LARGER:
                        //transform.localScale = new Vector3(107f, 150f, 53.5f);
                        gameObject.transform.localScale = new Vector3(107f, 150f, 53.5f);

                        other.GetComponentInParent<PowerUpComponent>().Deactivate();
                        GameControllerRef.ResetPowerUp();

                        break;
                    case PowerUpComponent.Types.PADDLE_SMALLER:
                        //transform.localScale = new Vector3(107f, 53.5f, 53.5f);
                        gameObject.transform.localScale = new Vector3(107f, 53.5f, 53.5f);

                        other.GetComponentInParent<PowerUpComponent>().Deactivate();
                        GameControllerRef.ResetPowerUp();

                        break;
                }
                break;
        }
    }
}
