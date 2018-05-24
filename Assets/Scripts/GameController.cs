using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    // Game state
    public enum GameState { PAUSED, ACTIVE, COUNT_DOWN, GAME_ENDED }

    // Entities
    public GameObject PlayerOne;
    public GameObject PlayerTwo;
    public GameObject Ball;
    public GameObject PowerUp;
    public Text PlayerOneScoreText;
    public Text PlayerTwoScoreText;

    // Current Players scroe
    private System.UInt16 PlayerOneScore = 0;
    private System.UInt16 PlayerTwoScore = 0;

    // Time to wait for next powerup spawn
    float NextPowerUpSpawnTime;
    
    // Initial countdown
    float InitialCountDown = 3f;

    // Current game state
    GameState CurrentGameState { get; set; }

    // Use this for initialization
    void Start () {
        // Get the game objects if not detected
        if (!PlayerOne)
            PlayerOne = GameObject.Find("PlayerOne");
        if (!PlayerTwo)
            PlayerTwo = GameObject.Find("PlayerTwo");
        if (!Ball)
            Ball = GameObject.Find("Ball");
        if (!PowerUp)
            PowerUp = GameObject.Find("PowerUp");

        // Fix the player aera bound
        FixBounds();

        // When the first power up will spawn
        NextPowerUpSpawnTime = Random.Range(2f, 10f);
        print(NextPowerUpSpawnTime);

        CurrentGameState = GameState.COUNT_DOWN;
    }

    public void SetGameState(GameState NewGameState)
    {
        CurrentGameState = NewGameState;
    }

    public GameState GetGameState()
    {
        return CurrentGameState;
    }

    void FixBounds()
    {
        // Get the viewport size
        Vector2 ViewPortSize = Camera.main.pixelRect.size;

        // Fix the dimension and position of the vertical bounds
        BoxCollider2D[] VerticalBounds = GameObject.Find("VerticalBounds").GetComponents<BoxCollider2D>();
        VerticalBounds[0].offset = new Vector2(0, ViewPortSize.y / 2 + 25);
        VerticalBounds[0].size = new Vector2(ViewPortSize.x, 50);
        VerticalBounds[1].offset = new Vector2(0, -ViewPortSize.y / 2 - 25);
        VerticalBounds[1].size = new Vector2(ViewPortSize.x, 50);
        
        // Fix the dimension and position of the horizontal bounds
        BoxCollider2D[] HorizontalBounds = GameObject.Find("HorizontalBounds").GetComponents<BoxCollider2D>();
        HorizontalBounds[0].offset = new Vector2(ViewPortSize.x / 2 + 25, 0);
        HorizontalBounds[0].size = new Vector2(50, ViewPortSize.y);
        HorizontalBounds[1].offset = new Vector2(-ViewPortSize.x / 2 - 25, 0);
        HorizontalBounds[1].size = new Vector2(50, ViewPortSize.y);

        // Fix the divider size
        SpriteRenderer Divider = GameObject.Find("Divider").GetComponent<SpriteRenderer>();
        Divider.size = new Vector2(Divider.size.x, (ViewPortSize.y / 53.5f - 2) / 2);
    }

    // Increment the score based on the passed parameter
    public void IncrementScore(string Player)
    {
        // Increment the score of the selected text and update the visualized score
        if (Player == "PlayerOne")
             PlayerOneScoreText.text = System.Convert.ToString(++PlayerOneScore);
        else if (Player == "PlayerTwo")
            PlayerTwoScoreText.text = System.Convert.ToString(++PlayerTwoScore);
    }

    // Reset the power up state
    public void ResetPowerUp()
    {
        NextPowerUpSpawnTime = Random.Range(2f, 10f);
    }
	
	// Update is called once per frame
	void Update ()
    {
        //
        switch (CurrentGameState)
        {
            // If we have paused the game
            case GameState.PAUSED:
                if (Input.GetKey(KeyCode.P) == true)
                    CurrentGameState = GameState.ACTIVE;
                break;

            // In the initial countdown phase
            case GameState.COUNT_DOWN:
                // Decrease the count time
                InitialCountDown -= Time.deltaTime;

                //
                if (InitialCountDown < 0f)
                {
                    // Remove the black overlay and the countdown text
                    GameObject.Find("BlackOverlay").GetComponent<Image>().enabled = false;
                    GameObject.Find("CountDown").GetComponent<Text>().enabled = false;
                    CurrentGameState = GameState.ACTIVE;
                }
                else
                {
                    GameObject.Find("CountDown").GetComponent<Text>().text = System.Convert.ToString(InitialCountDown);
                }
                break;

            // If the game ended
            case GameState.GAME_ENDED:
                GameObject.Find("BlackOverlay").GetComponent<Image>().enabled = true;
                GameObject.Find("CountDown").GetComponent<Text>().enabled = true;
                GameObject.Find("CountDown").GetComponent<Text>().fontSize = 36;
                GameObject.Find("CountDown").GetComponent<RectTransform>().sizeDelta = new Vector2(500f, 100f);

                if (PlayerOneScore == 10)
                    GameObject.Find("CountDown").GetComponent<Text>().text = "Player One Won!";
                else
                    GameObject.Find("CountDown").GetComponent<Text>().text = "Player Two Won!";

                // Decrease the count time
                InitialCountDown -= Time.deltaTime;

                // If we display the end game for at least 3 seconds
                if (InitialCountDown < 0f)
                    SceneManager.LoadScene("Initial Screen");

                break;

            // If the game is active
            case GameState.ACTIVE:
                // If we press P pause the game
                if (Input.GetKey(KeyCode.P) == true)
                    CurrentGameState = GameState.PAUSED;

                // If one player get to 10 he wins and we go back to the main menu
                if (PlayerOneScore == 10 || PlayerTwoScore == 10)
                {
                    InitialCountDown = 3f;
                    CurrentGameState = GameState.GAME_ENDED;
                }

                if (GameObject.Find("PowerUp"))
                {
                    // If we are waiting for a power up to spawn we reduce the spawing time
                    if (NextPowerUpSpawnTime > 0f)
                        NextPowerUpSpawnTime -= Time.deltaTime;
                    // If the power up is not active, activates it
                    else if (!PowerUp.GetComponent<SpriteRenderer>().isVisible)
                    {
                        PowerUp.GetComponent<PowerUpComponent>().Activate();
                        print("Activated!");
                    }
                }
                break;
        }

    }
}
