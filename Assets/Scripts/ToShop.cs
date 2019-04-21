using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToShop : MonoBehaviour {

  void Start() {
    Button start = GetComponent<Button>();
    start.onClick.AddListener(GoToShop);
  }

  void GoToShop() {
    SceneManager.LoadScene("Shop");
  }
}
