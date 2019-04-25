using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PersistentData : MonoBehaviour {

  public int score;
  public int money;

  private Snake snake;
  private SpawnFood spawnFood;

  private GameObject ghostUI;

  public Vector2 upgradeBorderScale = new Vector2(.5f, .5f);
  public float upgradeBorderOffset = .5f;

  public float upgradeSpawnStartDelay = 3f;
  public float upgradeSpawnWait = 4f;

  public int pointsFromFood = 1;
  public GameObject[] foodPrefabArray;
  public GameObject currentFoodPrefab;

  public bool ghostModePurchased = false;

  public Color snakeColor = new Color(255f/255f, 255f/255f, 255f/255f, 255f/255f);
  public Color bodyColor = new Color(255f/255f, 255f/255f, 255f/255f, 255f/255f);

  private bool canBuy;
  public bool[] purchasedItems;
  public int currentStoreIndex;

  private bool scriptControl = false;

  void Start() {
    //make this object persistent
    DontDestroyOnLoad(gameObject.transform);
    currentFoodPrefab = foodPrefabArray[0];
  }

  void Update() {
    if ((SceneManager.GetActiveScene().name == "Level") && (!scriptControl)) {
      GetControl();
      ghostUI.SetActive(false);
      if (ghostModePurchased) {
        ghostUI.SetActive(true);
      }
    }
  }

  public void GameOver() {
    money += score;
    Debug.Log("Score: " + score);
    Debug.Log("Money: " + money);
  //  snake.lose = false;
    SceneManager.LoadScene("End");
    scriptControl = false;
  }

  public void FoodCollected() {
    score += pointsFromFood;
  }

  public void GetStoreIndex(int storeIndex) {
    currentStoreIndex = storeIndex;
  }

  public void PriceCheck(int cost) {
    canBuy = false;
    Debug.Log("Cost: " + cost);
    if ((cost <= money) && (purchasedItems[currentStoreIndex] == false)) {
      money -= cost;
      canBuy = true;
    }
  }

  public void BuyItem(string item) {
    if (canBuy) {
      switch (item) {
        case "green skin":
          snakeColor = new Color(22f/255f, 186f/255f, 101f/255f, 255f/255f);
          bodyColor = new Color(22f/255f, 186f/255f, 101f/255f, 255f/255f);
          canBuy = false;
          purchasedItems[0] = true;
          break;
        case "upgrade arena":
          upgradeBorderScale = new Vector2(1f, 1f);
          upgradeBorderOffset = 1;
          canBuy = false;
          purchasedItems[1] = true;
          break;
        case "upgrade food spawns":
          upgradeSpawnStartDelay = 1.5f;
          upgradeSpawnWait = 2f;
          canBuy = false;
          purchasedItems[2] = true;
          break;
        case "healthy diet":
          pointsFromFood = 2;
          currentFoodPrefab = foodPrefabArray[1];
          canBuy = false;
          purchasedItems[3] = true;
          break;
        case "ghost mode":
          ghostModePurchased = true;
          canBuy = false;
          purchasedItems[4] = true;
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

    scriptControl = true;
  }

}
