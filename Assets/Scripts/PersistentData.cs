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
    // if (scriptControl) {
    //   if (snake.lose) {
    //     GameOver();
    //   }
    // }

    if ((SceneManager.GetActiveScene().name == "Level") && (!scriptControl)) {
      GetControl();
    }
  }

  public void GameOver() {
    snake.lose = false;
    UpdateScore();
    Debug.Log("PrevScore " + prevScore);
    SceneManager.LoadScene("End");
    scriptControl = false;
  }

  void UpdateScore() {
    GetScore();
    prevScore = GetScore();
    money += prevScore;
  }

  void GetScore() {
    //return the value of the score from the snake script
  }

  void GetControl() {
    //get control of the snake
    snake = GameObject.Find("Head").GetComponent<Snake>();
    //get control of the food spawning
    spawnFood = GameObject.Find("Main Camera").GetComponent<SpawnFood>();

    scriptControl = true;
  }

}
