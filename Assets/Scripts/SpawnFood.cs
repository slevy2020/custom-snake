using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour {
  public GameObject foodPrefab;

  public Transform borderTop;
  public Transform borderBottom;
  public Transform borderLeft;
  public Transform borderRight;

  void Start() {
    InvokeRepeating("Spawn", 3f, 4f);
  }

  public void Spawn() {
    float x = Random.Range(borderLeft.position.x + .5f, borderRight.position.x - .5f);
    float y = Random.Range(borderBottom.position.y + .5f, borderTop.position.y - .5f);

    Instantiate(foodPrefab, new Vector2(x, y), Quaternion.identity);
  }
}
