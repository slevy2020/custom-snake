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

  public Color snakeColor;

  private bool canBuy;

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

  public void PriceCheck(int cost, string item) {
    canBuy = false;
    if (cost <= money) {
      money -= cost;
      canBuy = true;
    }
    if (canBuy) {
      switch (item) {
        case "green skin":
          snakeColor = new Color(22f/255f, 186f/255f, 101f/255f);
          canBuy = false;
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
