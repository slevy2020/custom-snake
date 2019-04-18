using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Snake : MonoBehaviour {
    Vector2 currentDir = Vector2.right;

    bool ate = false;
    public GameObject tailPrefab;

    List<Transform> tail = new List<Transform>();

    void Start() {
      InvokeRepeating("Move", 0.3f, 0.05f);
    }

    void Update() {
      if (Input.GetKeyDown("w") && (currentDir != Vector2.up) && (currentDir != Vector2.down)) {
        currentDir = Vector2.up;
      } else if (Input.GetKeyDown("s") && (currentDir != Vector2.up) && (currentDir != Vector2.down)) {
        currentDir = -Vector2.up;
      } else if (Input.GetKeyDown("d") && (currentDir != Vector2.right) && (currentDir != Vector2.left)) {
        currentDir = Vector2.right;
      } else if (Input.GetKeyDown("a") && (currentDir != Vector2.right) && (currentDir != Vector2.left)) {
        currentDir = -Vector2.right;
      }
    }

    void OnTriggerEnter2D(Collider2D collision) {
      if (collision.tag == "food") {
        ate = true;
        Destroy(collision.gameObject);
      } else {
        //wall or snake, go to game end screen
      }
    }

    void Move() {
      Vector2 gapPos = transform.position;
      transform.Translate(currentDir/2);

      if (ate) {
        for (int i=0; i<2; i++) {
          GameObject tailObj = Instantiate(tailPrefab, gapPos, Quaternion.identity);
          tail.Insert(0, tailObj.transform);
        }
        ate = false;
      }
      else if (tail.Count > 0) {
        tail.Last().position = gapPos;
        tail.Insert(0, tail.Last());
        tail.RemoveAt(tail.Count-1);
      }
    }
}
