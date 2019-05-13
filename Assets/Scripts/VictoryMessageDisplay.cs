using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryMessageDisplay : MonoBehaviour {

  private PersistentData persistentScript; //the persistent data script

  public Text victoryMessageDisplayText; //the text object

  void Start() {
    //get access to the persistent data script
    persistentScript = GameObject.Find("Persistent Object").GetComponent<PersistentData>();
  }

  void Update() {
    //display the player's current high score
    victoryMessageDisplayText.text = persistentScript.currentVictoryMessage;
  }
}
