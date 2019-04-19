using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

  private PersistentData persistentScript;
  public Text scoreDisplayText;

  void Start() {
    persistentScript = GameObject.Find("Persistent Object").GetComponent<PersistentData>();
    scoreDisplayText.text = "Score: " + persistentScript.prevScore;
  }
}
