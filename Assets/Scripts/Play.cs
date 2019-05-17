using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Play : MonoBehaviour {

  private PersistentData persistentScript;

  void Start(){
    //get access to the persistent data
    persistentScript = GameObject.Find("Persistent Object").GetComponent<PersistentData>();
    //get access to the button component
    Button start = GetComponent<Button>();
    //when clicked, call the start game function
    start.onClick.AddListener(StartGame);
  }

  void StartGame() {
    //set multiplayer to false
    persistentScript.multiplayer = false;
    //set script control to false
    persistentScript.scriptControl = false;
    //load the level scene of the game
    SceneManager.LoadScene("Level");
  }
}
