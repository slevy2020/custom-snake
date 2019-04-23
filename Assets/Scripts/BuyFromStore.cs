using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyFromStore : MonoBehaviour {
  private PersistentData persistentScript;
  public int cost;
  public string item;
  public int storeIndex;
  public bool bought = false;
  public Image soldStamp;
  public bool soldStampInstance = false;

  void Start() {
    persistentScript = GameObject.Find("Persistent Object").GetComponent<PersistentData>();
    Button buy = GetComponent<Button>();
    buy.onClick.AddListener(Purchase);
  }

  void Update() {
    if ((persistentScript.purchasedItems[storeIndex]) && (!soldStampInstance)) {
      var soldStampChild = Instantiate(soldStamp, transform.position, Quaternion.identity);
      soldStampChild.transform.parent = gameObject.transform;
      soldStampInstance = true;
      Debug.Log(soldStampInstance);
    }
  }

  void Purchase() {
    if (!bought) {
      persistentScript.SendMessage("GetStoreIndex", storeIndex);
      persistentScript.SendMessage("PriceCheck", cost);
      if (cost <= persistentScript.money) {
        bought = true;
      }
      persistentScript.SendMessage("BuyItem", item);
    }
  }
}
