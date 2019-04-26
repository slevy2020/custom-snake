using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Play : MonoBehaviour {

  void Start(){
    //get access to the button component
    Button start = GetComponent<Button>();
    //when clicked, call the start game function
    start.onClick.AddListener(StartGame);
  }

  void StartGame() {
    //load the level scene of the game
    SceneManager.LoadScene("Level");
  }
}
