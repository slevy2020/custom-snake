using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Quit : MonoBehaviour {
  void Start(){
    Button quit = GetComponent<Button>();
    quit.onClick.AddListener(QuitGame);
  }

  void QuitGame() {
    Debug.Log("quit");
    Application.Quit();
  }
}
