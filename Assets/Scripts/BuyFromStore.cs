using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyFromStore : MonoBehaviour {
  private PersistentData persistentScript;

  void Start() {
    persistentScript = GameObject.Find("Persistent Object").GetComponent<PersistentData>();
  }
}
