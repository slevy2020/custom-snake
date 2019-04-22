using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplay : MonoBehaviour {

  private PersistentData persistentScript;
  public Text moneyDisplayText;

  void Start() {
    persistentScript = GameObject.Find("Persistent Object").GetComponent<PersistentData>();
  }

  void Update() {
    moneyDisplayText.text = "You have $" + persistentScript.money;
  }
}
