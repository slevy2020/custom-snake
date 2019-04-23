using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyFromStore : MonoBehaviour {
  private PersistentData persistentScript;
  public int cost;
  public string item;
  public bool bought = false;

  void Start() {
    persistentScript = GameObject.Find("Persistent Object").GetComponent<PersistentData>();
    Button buy = GetComponent<Button>();
    buy.onClick.AddListener(Purchase);
  }

  void Purchase() {
    if (!bought) {
      persistentScript.SendMessage("PriceCheck", cost);
      if (cost <= persistentScript.money) {
        bought = true;
      }
      persistentScript.SendMessage("BuyItem", item);
    }
  }
}
