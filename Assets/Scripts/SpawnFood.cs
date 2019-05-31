using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour {

  private PersistentData persistentScript; //the persistent data script

  //vars for which food to spawn, set in the inspector
  public GameObject foodPrefab;
  public int superFoodSpawnChance;

  //vars for size of the arena
  public GameObject borderScale;
  public float borderOffset;

  //pointers to the borders, set in the inspector
  public Transform borderTop;
  public Transform borderBottom;
  public Transform borderLeft;
  public Transform borderRight;

  //vars for the timing of food spawning
  public float spawnStartDelay = 3f;
  public float spawnWait = 4f;

  void Start() {
    //pointer to the persistent object
    persistentScript = GameObject.Find("Persistent Object").GetComponent<PersistentData>();

    //update the spawn start and delay based on upgraded vars on the persistent script
    spawnStartDelay = persistentScript.upgradeSpawnStartDelay;
    spawnWait = persistentScript.upgradeSpawnWait;

    //set the chance of spawning a super food to match the value from the persistent script
    superFoodSpawnChance = persistentScript.upgradeSuperFood;

    //start the infinitely repeating spawn function
    if (persistentScript.multiplayer) {
      //if in multiplayer, use these values for the speed at which food spawns
      spawnStartDelay = 1.5f;
      spawnWait = 2f;
    }
    InvokeRepeating("Spawn", spawnStartDelay, spawnWait);

    //update the rest of the vars based on upgraded vars on the persistent script
    borderScale.transform.localScale = persistentScript.upgradeBorderScale;
    borderOffset = persistentScript.upgradeBorderOffset;
    foodPrefab = persistentScript.currentFoodPrefab;

    //if in multiplayer, set the size of the arena and the food prefab to be the proper size for multiplayer
    if (persistentScript.multiplayer) {
      borderScale.transform.localScale = new Vector2(1f, 1f);
      borderOffset = 1;
      foodPrefab = persistentScript.foodPrefabArray[0];
    }
  }

  public void Spawn() {
    //randomly set the position of where to spawn the food based on the positions of the borders
    float x = Random.Range(borderLeft.position.x + borderOffset, borderRight.position.x - borderOffset);
    float y = Random.Range(borderBottom.position.y + borderOffset, borderTop.position.y - borderOffset);

    //if the 8th upgrade (superfood) has been bought then determine whether or not a superfood should spawn
    if ((persistentScript.purchasedItems[7]) && (!persistentScript.multiplayer)) {
      //generate a random value from 0-100 (to determine a percentage)
      float superFoodSpawnRNG = Random.Range(0, 100);
      //test if the generated number is within the range of values needed to spawn a superfood
      if (superFoodSpawnRNG <= superFoodSpawnChance) {
        //within range, spawn a superfood
        foodPrefab = persistentScript.foodPrefabArray[2];
      } else {
        //not within range, spawn a regular food instead
        foodPrefab = persistentScript.currentFoodPrefab;
      }
    }
    //spawn a food using the determined prefab at the randomly generated location
    Instantiate(foodPrefab, new Vector2(x, y), Quaternion.identity);
  }
}
