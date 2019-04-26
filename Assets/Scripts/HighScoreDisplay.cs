using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreDisplay : MonoBehaviour {

  private PersistentData persistentScript; //the persistent data script

  public Text highScoreDisplayText; //the text object where the player's high score will be displayed

  void Start() {
    //get access to the persistent data script
    persistentScript = GameObject.Find("Persistent Object").GetComponent<PersistentData>();
  }

  void Update() {
    //display the player's current high score
    highScoreDisplayText.text = "HIGH SCORE: " + persistentScript.highScore;
  }
}
