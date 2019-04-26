using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

  private PersistentData persistentScript; //the persistent data script

  public Text scoreDisplayText; //the text object where the player's score will be displayed

  void Start() {
    //get access to the persistent data script
    persistentScript = GameObject.Find("Persistent Object").GetComponent<PersistentData>();
  }

  void Update() {
    //display the player's current score
    scoreDisplayText.text = "Score: " + persistentScript.score;
  }
}
