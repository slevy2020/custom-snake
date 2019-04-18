using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Play : MonoBehaviour {
  void Start(){
    Button start = GetComponent<Button>();
    start.onClick.AddListener(StartGame);
  }
  void StartGame() {
    SceneManager.LoadScene("Level");
  }
}
