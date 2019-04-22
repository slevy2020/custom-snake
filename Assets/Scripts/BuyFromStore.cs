using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyFromStore : MonoBehaviour {
  private PersistentData persistentScript;
  public int cost;
  public string item;

  void Start() {
    persistentScript = GameObject.Find("Persistent Object").GetComponent<PersistentData>();
    Button buy = GetComponent<Button>();
    buy.onClick.AddListener(Purchase);
  }

  void Purchase() {
    persistentScript.SendMessage("PriceCheck", cost);
    persistentScript.SendMessage("PriceCheck", item);
  }
}
