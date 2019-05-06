using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ToMenu : MonoBehaviour {

    void Start() {
      //get access to the button component
      Button start = GetComponent<Button>();
      //when clicked, call the start game function
      start.onClick.AddListener(ToMenuScreen);
    }

    void ToMenuScreen() {
      //load the level scene of the game
      SceneManager.LoadScene("Start");
    }
}
