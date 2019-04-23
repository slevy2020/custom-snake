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

  public Vector2 upgradeBorderScale = new Vector2(.5f, .5f);
  public float upgradeBorderOffset = .5f;

  public Color snakeColor = new Color(255f/255f, 255f/255f, 255f/255f, 255f/255f);
  public Color bodyColor = new Color(255f/255f, 255f/255f, 255f/255f, 255f/255f);

  private bool canBuy;
  public bool[] purchasedItems;
  public int currentStoreIndex;

  private bool scriptControl = false;

  void Start() {
    //make this object persistent
    DontDestroyOnLoad(gameObject.transform);
  }

  void Update() {
    if ((SceneManager.GetActiveScene().name == "Level") && (!scriptControl)) {
      GetControl();
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
    score += 1;
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
      }
    }
  }

  void GetControl() {
    //get control of the snake
    snake = GameObject.Find("Head").GetComponent<Snake>();
    //get control of the food spawning
    spawnFood = GameObject.Find("Main Camera").GetComponent<SpawnFood>();

    scriptControl = true;
  }

}
