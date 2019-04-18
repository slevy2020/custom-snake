using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PersistentData : MonoBehaviour {

  public int prevScore;
  public int money;

  private Snake snake;
  private SpawnFood spawnFood;

  private bool scriptControl = false;

  void Start() {
    //make this object persistent
    DontDestroyOnLoad(gameObject.transform);
  }

  void Update() {
    if (scriptControl) {
      if (snake.lose) {
        GameOver();
      }
    }

    if ((SceneManager.GetActiveScene().name == "Level") && (!scriptControl)) {
      GetControl();
    }
  }

  void GameOver() {
    GetScore();
    SceneManager.LoadScene("End");
    Text scoreText = GameObject.Find("End UI Score").GetComponent<Text>();
    scoreText.text = "Score: " + prevScore;
    snake.lose = false;
  }

  void GetScore() {
    prevScore = snake.score;
  }

  void GetControl() {
    //get control of the snake
    snake = GameObject.Find("Head").GetComponent<Snake>();
    //get control of the food spawning
    spawnFood = GameObject.Find("Main Camera").GetComponent<SpawnFood>();

    scriptControl = true;
  }

}
