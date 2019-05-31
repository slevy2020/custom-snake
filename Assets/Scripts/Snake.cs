using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Snake : MonoBehaviour {

    //vars for basic movement
    Vector2 currentDir = Vector2.right; //start by moving right
    private float moveTimer = 0f; //cooldown for when the player can turn again
    private bool canTurn = true; //the player is allowed to turn

    //vars for multiplayer, set in inspector
    public string upButton;
    public string downButton;
    public string rightButton;
    public string leftButton;
    public int playerIndex;

    public bool lose; //starts false, the player has not lost yet

    //vars for increasing the tail length
    bool ate = false; //the player has not eaten anything yet
    public GameObject tailPrefab; //the object to use as the snake tail
    public SpriteRenderer snakeSkin; //color of the snake
    List<Transform> tail = new List<Transform>(); //list of tail objects

    public AudioSource crunchSound; //sound effect for when the snake eats something

    //ghost mode vars
    public bool ghostModeOn = false; //ghost mode starts off
    private float ghostModeTimer = 3f; //ghost mode remains active for 3 seconds
    private float ghostModeCooldown = 10f; //cooldown before ghost mode can be used again
    private bool ghostModeReady = true; //whether or not the player is able to use ghost mode
    public Slider ghostSlider; //pointer to the slider object used to show time remaining for ghost mode
    public GameObject sliderFill; //pointer to part of the slider object
    public Color sliderColor = new Color(239f/255f, 72f/255f, 100f/255f, 1f); //color of the ghost mode slider

    private PersistentData persistentScript; //pointer to the persistent data script

    void Start() {
      //start basic movement
      InvokeRepeating("Move", 0.3f, 0.05f);
      //the player has not yet lost
      lose = false;
      //get access to the persistent data script
      persistentScript = GameObject.Find("Persistent Object").GetComponent<PersistentData>();
      //set the score to 0 on the persistent script
      persistentScript.score = 0;
      //update vars based on upgrades on the persistent script
      if (!persistentScript.multiplayer) {
        snakeSkin.color = persistentScript.snakeColor;
      }
      tailPrefab.GetComponent<SpriteRenderer>().color = persistentScript.bodyColor;
      if (!persistentScript.multiplayer) {
        sliderFill.GetComponent<Image>().color = sliderColor;
      }

      //set the ghost mode timer to reflect the upgraded value on the persistent data
      ghostModeTimer = persistentScript.upgradeGhostTimer;
      ghostSlider.maxValue = ghostModeTimer;
    }

    void Update() {
      //when key pressed and not already going that direction or the opposite direction and the player is able to turn,
      //turn in that direction and prevent the player from being able to turn temporarily
      if (Input.GetKeyDown(upButton) && (currentDir != Vector2.up) && (currentDir != Vector2.down) && (canTurn)) {
        currentDir = Vector2.up;
        canTurn = false;
      } else if (Input.GetKeyDown(downButton) && (currentDir != Vector2.up) && (currentDir != Vector2.down) && (canTurn)) {
        currentDir = -Vector2.up;
        canTurn = false;
      } else if (Input.GetKeyDown(rightButton) && (currentDir != Vector2.right) && (currentDir != Vector2.left) && (canTurn)) {
        currentDir = Vector2.right;
        canTurn = false;
      } else if (Input.GetKeyDown(leftButton) && (currentDir != Vector2.right) && (currentDir != Vector2.left) && (canTurn)) {
        currentDir = -Vector2.right;
        canTurn = false;
      }

      //if the player cannot turn, start adding to the move timer
      if (!canTurn) {
        moveTimer += Time.deltaTime;
      }
      //if .1 second had passed, allow the player to turn again and reset the timer
      if (moveTimer > .1) {
        canTurn = true;
        moveTimer = 0f;
      }

      //if the player has purchased the 5th upgrade (ghost mode), then:
      if (persistentScript.purchasedItems[4] && !persistentScript.multiplayer) {
        //reset the value of the ghost mode timer
        ghostSlider.value = ghostModeTimer;
        //when the space bar is pressed and if the player is able to use ghost mode,
        //then activate ghost mode and prevent the player from using it again
        if (Input.GetKeyDown("space") && (ghostModeReady)) {
          ghostModeOn = true;
          ghostModeReady = false;
        }
        //if ghost mode is on, then start counting down the timer
        if (ghostModeOn) {
          ghostModeTimer -= Time.deltaTime;
          //if the timer runs out, then deactivate ghost mode, and hide the slider by changing the color
          if (ghostModeTimer < 0) {
            ghostModeOn = false;
            sliderColor = new Color(0f, 0f, 0f, 1f);
            sliderFill.GetComponent<Image>().color = sliderColor;
          }
        }
        //if the player is not able to use ghost mode, start the cooldown timer
        if (!ghostModeReady) {
          ghostModeCooldown -= Time.deltaTime;
          //if the cooldown timer runs out, then:
          if (ghostModeCooldown < 0) {
            //show the slider by changing the color
            sliderColor = new Color(239f/255f, 72f/255f, 100f/255f, 1f);
            sliderFill.GetComponent<Image>().color = sliderColor;
            //reset the timers and allow the player to use ghost mode again
            ghostModeTimer = persistentScript.upgradeGhostTimer;
            ghostModeCooldown = 10f;
            ghostModeReady = true;
          }
        }
      }
    }

    void OnTriggerEnter2D(Collider2D collision) {
      //if the player collides with regular food
      if (collision.tag == "food") {
        //indicate that the player ate the food
        ate = true;
        //update how many points the player should get for this piece of food
        persistentScript.currentPointsFromFood = persistentScript.standardPointsFromFood;
        //increase the score on the persistent script
        persistentScript.FoodCollected();
        //if the player has purchased the 6th upgrade (audio), then play the sound effect
        if (persistentScript.purchasedItems[5]) {
          crunchSound.Play();
        }
        //destroy the instance of the food object
        Destroy(collision.gameObject);
      } if (collision.tag == "super food") { //if the player collides with superfood
        //indicate that the player ate the food
        ate = true;
        //update how many points the player should get for this piece of food
        persistentScript.currentPointsFromFood = 5;
        //increase the score on the persistent script
        persistentScript.FoodCollected();
        //if the player has purchased the 6th upgrade (audio), then play the sound effect
        if (persistentScript.purchasedItems[5]) {
          crunchSound.Play();
        }
        //destroy the instance of the food object
        Destroy(collision.gameObject);
      } if (((collision.tag == "wall") || ((collision.tag == "head") && (!ghostModeOn)) || ((collision.tag == "body") && (!ghostModeOn))) && (!lose)) {
        //if the snake collides with a wall (or itself when ghost mode is not active) and they have not yet lost, then:
        //indicate that the player has lost
        lose = true;
        if (!persistentScript.multiplayer) {
          //run the game over function on the persistent data script
          persistentScript.GameOver();
        } else if (persistentScript.multiplayer) {
          if (collision.tag == "head") {
            int zero = 0;
            persistentScript.SendMessage("GetMultiplayerWinner", zero);
          } else {
            persistentScript.SendMessage("GetMultiplayerWinner", playerIndex);
          }
          persistentScript.MultiplayerGameOver();
        }
      }
    }

    void Move() {
      //assign the head's previous position to a variable
      Vector2 gapPos = transform.position;
      //move the head in the direction it's going in
      transform.Translate(currentDir/2);

      //if the player has eaten food, then instantiate a piece of the tail where the head previously was
      //do this four times to make the tail longer
      if (ate) {
        for (int i=0; i<4; i++) {
          GameObject tailObj = Instantiate(tailPrefab, gapPos, Quaternion.identity);
          tail.Insert(0, tailObj.transform);
        }

        //reset the ate boolean
        ate = false;
      }
      else if (tail.Count > 0) { //if there is a tail, then:
        //move the last piece of the tail where the head previously was
        tail.Last().position = gapPos;
        tail.Insert(0, tail.Last());
        //prevent collision with first piece of the tail
        tail.First().tag = "Untagged";
        //allow collision at all of the other positions
        tail.ElementAt(1).tag = "body";
        tail.ElementAt(2).tag = "body";
        tail.ElementAt(3).tag = "body";

        //remove the last piece of the tail, as it was moved to the front
        tail.RemoveAt(tail.Count-1);
      }
    }
}
