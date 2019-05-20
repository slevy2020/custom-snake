using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PersistentData : MonoBehaviour {

  //vars for scorekeeping
  public int score;
  public int money;
  public int highScore = 0;

  public bool multiplayer = false;
  private Snake playerOne;
  private Snake playerTwo;
  public string[] multiplayerVictoryMessages;
  public string currentVictoryMessage;
  public Color playerOneColor = new Color(255f/255f, 255f/255f, 255f/255f, 255f/255f);
  public Color playerTwoColor = new Color(255f/255f, 0f/255f, 255f/255f, 255f/255f);

  public bool upgradeComplete = false;

  //pointers to the snake and spawn food scripts
  private Snake snake;
  private SpawnFood spawnFood;

  //pointers to the UI objects for ghost mode and background upgrades
  private GameObject ghostUI;
  private GameObject backgroundImage;

  //vars for border scale and offset (offset prevents food from spawning in walls)
  public Vector2 upgradeBorderScale = new Vector2(.5f, .5f);
  public float upgradeBorderOffset = .5f;

  //vars for food spawining speed
  public float upgradeSpawnStartDelay = 3f;
  public float upgradeSpawnWait = 4f;

  //vars for how many points the player gets per food
  public int standardPointsFromFood = 1;
  public int currentPointsFromFood = 1;
  //pointers to the various food prefabs used
  public GameObject[] foodPrefabArray;
  public GameObject currentFoodPrefab;
  public GameObject standardFoodPrefab;

  public AudioSource backgroundMusic; //pointer to the background music audio

  //vars for snake color
  public Color snakeColor = new Color(255f/255f, 255f/255f, 255f/255f, 255f/255f);
  public Color bodyColor = new Color(255f/255f, 255f/255f, 255f/255f, 255f/255f);

  //vars to determine whether or not a player can buy / has bought an item
  private bool canBuy;
  public bool[] purchasedItems;
  public int currentStoreIndex;

  //whether or not the persistent script has control of the other objects in the level scene
  public bool scriptControl = false;

  void Start() {
    //make this object persistent
    DontDestroyOnLoad(gameObject.transform);

    //reset the food to spawn to be the default
    currentFoodPrefab = foodPrefabArray[0];
    standardFoodPrefab = foodPrefabArray[0];

    currentVictoryMessage = multiplayerVictoryMessages[0];

    SceneManager.LoadScene("Start");
  }

  void Update() {
    //if in the level scene and does not have control of the scripts, then get control
    if ((SceneManager.GetActiveScene().name == "Level") && (!scriptControl)) {
      GetControl();
    }
    if ((SceneManager.GetActiveScene().name == "Multiplayer") && (!scriptControl)) {
      GetControlMultiplayer();
    }
  }

  public void GameOver() {
    //add the round's score to the persistent money when the player loses
    //called from the snake script
    money += score;

    //if the score was greater than the previous high score, update the high score
    if (score > highScore) {
      highScore = score;
    }

    //load the end scene and remove control of the scripts (to prevent null reference exceptions)
    SceneManager.LoadScene("End");
//    scriptControl = false;
  }

  public void MultiplayerGameOver() {
    SceneManager.LoadScene("MultiplayerEnd");
//    scriptControl = false;
  }

  public void FoodCollected() {
    //called from the snake scritpt, add the proper amount of points to the score when food collected
    score += currentPointsFromFood;
  }

  public void GetMultiplayerWinner(int playerIndex) {
    //set the current store index to that of the item the player is attempting to buy
    currentVictoryMessage = multiplayerVictoryMessages[playerIndex];
  }

  public void GetStoreIndex(int storeIndex) {
    //set the current store index to that of the item the player is attempting to buy
    currentStoreIndex = storeIndex;
  }

  public void UpgradeCheck(bool bought) {
    upgradeComplete = bought;
  }

  public void PriceCheck(int cost) {
    //assume that the player cannot afford the item
    canBuy = false;
    //if the player can afford it and has not already bought the item with this store index, then:
    if ((cost <= money) && (!upgradeComplete)) {
      //subtract the cost from the total money
      money -= cost;
      //allow the player to buy the object
      canBuy = true;
    }
  }

  public void BuyItem(string item) {
    //if the player can buy the item, determine which item it is with the switch statement
    if (canBuy) {
      switch (item) {
        case "green skin":
          //if the item is the green skin, update the snake's color
          snakeColor = new Color(22f/255f, 186f/255f, 101f/255f, 255f/255f);
          bodyColor = new Color(22f/255f, 186f/255f, 101f/255f, 255f/255f);
          //prevent the player from buying this item again
          canBuy = false;
          purchasedItems[0] = true;
          break;
        case "upgrade arena":
          //increase the scale of the arena walls to make a bigger arena
          upgradeBorderScale = new Vector2(1f, 1f);
          //prevent the food from spawning inside the walls
          upgradeBorderOffset = 1;
          //prevent the player from buying this item again
          canBuy = false;
          purchasedItems[1] = true;
          break;
        case "upgrade food spawns":
          //decrease how long it takes to spawn food
          upgradeSpawnStartDelay = 1.5f;
          upgradeSpawnWait = 2f;
          //prevent the player from buying this item again
          canBuy = false;
          purchasedItems[2] = true;
          break;
        case "healthy diet":
          //increase how many points the player gets for each food eaten
          standardPointsFromFood = 2;
          currentPointsFromFood = 2;
          //set the new prefab to instantiate for food
          standardFoodPrefab = foodPrefabArray[1];
          currentFoodPrefab = foodPrefabArray[1];
          //prevent the player from buying this item again
          canBuy = false;
          purchasedItems[3] = true;
          break;
        case "ghost mode":
          //prevent the player from buying this item again
          canBuy = false;
          purchasedItems[4] = true;
          break;
        case "buy audio":
          //start playing the background music
          backgroundMusic.Play();
          //prevent the player from buying this item again
          canBuy = false;
          purchasedItems[5] = true;
          break;
        case "buy background":
          //prevent the player from buying this item again
          canBuy = false;
          purchasedItems[6] = true;
          break;
        case "super food":
          //prevent the player from buying this item again
          canBuy = false;
          purchasedItems[7] = true;
          break;
      }
    }
  }

  void GetControl() {
    //get control of the snake
    snake = GameObject.Find("Head").GetComponent<Snake>();
    //get control of the food spawning
    spawnFood = GameObject.Find("Main Camera").GetComponent<SpawnFood>();
    //get control of the ghost mode ui
    ghostUI = GameObject.Find("Ghost Mode UI");
    ghostUI.SetActive(false);
    if (purchasedItems[4]) {
      ghostUI.SetActive(true);
    }
    //get control of the background image
    backgroundImage = GameObject.Find("Background Texture");
    backgroundImage.SetActive(false);
    if (purchasedItems[6]) {
      backgroundImage.SetActive(true);
    }

    //indicate that the script has control of the other objects in the level scene
    scriptControl = true;
  }

  void GetControlMultiplayer() {
    //get control of the snakes
    playerOne = GameObject.Find("P1").GetComponent<Snake>();
    playerOne.GetComponent<SpriteRenderer>().color = playerOneColor;
    playerOne.tailPrefab.GetComponent<SpriteRenderer>().color = playerOneColor;

    playerTwo = GameObject.Find("P2").GetComponent<Snake>();
    playerTwo.GetComponent<SpriteRenderer>().color = playerTwoColor;
    playerTwo.tailPrefab.GetComponent<SpriteRenderer>().color = playerTwoColor;
    //get control of the food spawning
    spawnFood = GameObject.Find("Main Camera").GetComponent<SpawnFood>();
    //indicate that the script has control of the other objects in the level scene
    scriptControl = true;
  }
}
