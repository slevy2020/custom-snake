using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreDisplay : MonoBehaviour {

    private PersistentData persistentScript;
    public Text highScoreDisplayText;

    void Start() {
      persistentScript = GameObject.Find("Persistent Object").GetComponent<PersistentData>();
    }

    void Update() {
      highScoreDisplayText.text = "HIGH SCORE: " + persistentScript.highScore;
    }
}
