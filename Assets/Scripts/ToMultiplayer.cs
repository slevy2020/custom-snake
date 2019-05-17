using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToMultiplayer : MonoBehaviour {

    private PersistentData persistentScript;

    void Start() {
      //get access to the persistent data
      persistentScript = GameObject.Find("Persistent Object").GetComponent<PersistentData>();
      //get access to the button component
      Button start = GetComponent<Button>();
      //when clicked, call the start game function
      start.onClick.AddListener(StartMultiplayer);
    }

    void StartMultiplayer() {
      //set multiplayer to true
      persistentScript.multiplayer = true;
      //set script control to false
      persistentScript.scriptControl = false;
      //load the level scene of the game
      SceneManager.LoadScene("Multiplayer");
    }
}
