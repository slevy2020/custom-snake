using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour {
  private PersistentData persistentScript;

  public GameObject foodPrefab;

  public GameObject borderScale;
  public float borderOffset;

  public Transform borderTop;
  public Transform borderBottom;
  public Transform borderLeft;
  public Transform borderRight;

  public float spawnStartDelay = 3f;
  public float spawnWait = 4f;

  void Start() {
    persistentScript = GameObject.Find("Persistent Object").GetComponent<PersistentData>();
    spawnStartDelay = persistentScript.upgradeSpawnStartDelay;
    spawnWait = persistentScript.upgradeSpawnWait;
    InvokeRepeating("Spawn", spawnStartDelay, spawnWait);
    borderScale.transform.localScale = persistentScript.upgradeBorderScale;
    borderOffset = persistentScript.upgradeBorderOffset;
    foodPrefab = persistentScript.currentFoodPrefab;
  }

  public void Spawn() {
    float x = Random.Range(borderLeft.position.x + borderOffset, borderRight.position.x - borderOffset);
    float y = Random.Range(borderBottom.position.y + borderOffset, borderTop.position.y - borderOffset);

    Instantiate(foodPrefab, new Vector2(x, y), Quaternion.identity);
  }
}
