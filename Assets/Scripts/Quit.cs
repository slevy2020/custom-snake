using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Quit : MonoBehaviour {
  
  void Start(){
    //get access to the button component
    Button quit = GetComponent<Button>();
    //when clicked, call the quit game function
    quit.onClick.AddListener(QuitGame);
  }

  void QuitGame() {
    //quit the game
    Application.Quit();
  }
}
