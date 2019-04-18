using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour {

  public int money;

  private Snake snake;
  private SpawnFood spawnFood;

  void Start() {
    //make this object persistent
    DontDestroyOnLoad(gameObject.transform);
    //get control of the snake
    snake = GameObject.Find("Head").GetComponent<Snake>();
    //get control of the food spawning
    spawnFood = GameObject.Find("Main Camera").GetComponent<SpawnFood>();
  }

}
