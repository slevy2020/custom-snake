using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyFromStore : MonoBehaviour {

  private PersistentData persistentScript; //the persistent data script

  public int cost; //how much the item costs, set in inspector
  public string item; //what the name of the item is, set in inspector
  public int storeIndex; //the index of the item, to be used on the persistent script, set in inspector

  public int[] upgradeCosts; //the price of each tiered upgrade, set in inspector
  public Image currentStoreIcon; //the current image used in the store for the upgrade icon
  public Sprite[] storeIcons; //the list of all of the store icons

  public bool bought = false; //the item has not been purchased yet
  public Image soldStamp; //image to put over button when purchased
  public bool soldStampInstance = false; //the sold object has not been placed yet

  void Start() {
    //get access to the persistent data script
    persistentScript = GameObject.Find("Persistent Object").GetComponent<PersistentData>();

    //find how many tiered upgrades have been purchased and update the cost and image to reflect it
    if (!persistentScript.purchasedUpgrades[storeIndex]) {
      cost = upgradeCosts[(persistentScript.upgradeTiers[storeIndex])];
      currentStoreIcon.sprite = storeIcons[(persistentScript.upgradeTiers[storeIndex])];
    } else if (persistentScript.purchasedUpgrades[storeIndex]) {
      cost = upgradeCosts[upgradeCosts.Length - 1];
      currentStoreIcon.sprite = storeIcons[storeIcons.Length - 1];
    }

    //get access to the button component
    Button buy = GetComponent<Button>();
    //when clicked, call the purchase function
    buy.onClick.AddListener(Purchase);
  }

  void Update() {
    //if the item has been purchased and the sold object has been placed, then:
    if ((persistentScript.purchasedUpgrades[storeIndex]) && (!soldStampInstance)) {
      //instantiate the sold object and assign it to a variable
      var soldStampChild = Instantiate(soldStamp, transform.position, Quaternion.identity);
      //move the sold object to the canvas so it actually appears
      soldStampChild.transform.parent = gameObject.transform;
      //indicate that the sold object has been placed
      soldStampInstance = true;
    }
  }

  void Purchase() {
    //if the item has not yet been bought, run the functions to buy it on the persistent script
    if (!persistentScript.purchasedUpgrades[storeIndex]) {
      persistentScript.SendMessage("GetStoreIndex", storeIndex);
      persistentScript.SendMessage("UpgradeCheck", bought);
      //if the player can afford it, then:
      if (cost <= persistentScript.money) {
        //if the player has purchased all of the upgrades, set bought to true
        if (persistentScript.upgradeTiers[storeIndex] >= (persistentScript.MAX_UPGRADES[storeIndex] - 1)) {
          bought = true;
        }
      }
      persistentScript.SendMessage("PriceCheck", cost);
      persistentScript.SendMessage("UpgradeCheck", bought);
      persistentScript.SendMessage("BuyItem", item);

      //if the player purchases the tiered upgrade, update the cost and icon of the item
      if ((persistentScript.upgradeTiers[storeIndex] + 1) <= (upgradeCosts.Length)) {
        cost = upgradeCosts[persistentScript.upgradeTiers[storeIndex]];
        currentStoreIcon.sprite = storeIcons[persistentScript.upgradeTiers[storeIndex]];
      }
    }
  }
}
