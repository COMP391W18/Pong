using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpComponent : MonoBehaviour {

    // Power UP type
    public enum Types : int { NULL, PADDLE_LARGER, PADDLE_SMALLER, SIZE }

    // This power up type
    public Types PowerUpType;

	// Use this for initialization
	void Start () {
        // PowerUp Is Not Assigned
        PowerUpType = Types.NULL;
        GetComponentInParent<CircleCollider2D>().isTrigger = false;
    }

    void AssignPowerUp()
    { 
        // Select a PowerUp
        int SelectedPowerUp = Random.Range(1, (int)Types.SIZE);

        // Select the right power up
        PowerUpType = (Types)SelectedPowerUp;
    }

    public Types GetPowerUpType()
    {
        return PowerUpType;
    }

    public void Activate()
    {
        //
        AssignPowerUp();

        // Show the power up
        GetComponentInParent<SpriteRenderer>().enabled = true;

        // Make the circle collider trigger touch events
        GetComponentInParent<CircleCollider2D>().isTrigger = true;

        // Reset the power up position
        GetComponentInParent<BallComponent>().resetBall();
    }

    public void Deactivate()
    {
        // Show the power up
        GetComponentInParent<SpriteRenderer>().enabled = false;

        // Make the circle collider trigger touch events
        GetComponentInParent<CircleCollider2D>().isTrigger = false;

        // Reset the power up position
        GetComponentInParent<BallComponent>().resetBall();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}    
}
