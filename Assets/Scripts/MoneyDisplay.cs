using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplay : MonoBehaviour {

  private PersistentData persistentScript; //the persistent data script

  public Text moneyDisplayText; //the text to display the player's current total

  void Start() {
    //get access to the persistent data script
    persistentScript = GameObject.Find("Persistent Object").GetComponent<PersistentData>();
  }

  void Update() {
    //update the text on the money display based on the player's current total
    moneyDisplayText.text = "You have $" + persistentScript.money;
  }
}
