using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToShop : MonoBehaviour {

  void Start() {
    //get access to the button component
    Button start = GetComponent<Button>();
    //when clicked, call the go to shop function
    start.onClick.AddListener(GoToShop);
  }

  void GoToShop() {
    //go to the shop scene
    SceneManager.LoadScene("Shop");
  }
}
