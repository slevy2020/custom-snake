using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Snake : MonoBehaviour {
    Vector2 currentDir = Vector2.right;
    private float moveTimer = 0f;
    private bool canTurn = true;

    public bool lose;
//    public int score;

    bool ate = false;
    public GameObject tailPrefab;
    public SpriteRenderer snakeSkin;

    public AudioSource crunchSound;

    public bool ghostModeOn = false;
    private float ghostModeTimer = 3f;
    private float ghostModeCooldown = 10f;
    private bool ghostModeReady = true;
    public Slider ghostSlider;
    public GameObject sliderFill;
    public Color sliderColor = new Color(239f/255f, 72f/255f, 100f/255f, 1f);

    private PersistentData persistentScript;

    List<Transform> tail = new List<Transform>();

    void Start() {
      InvokeRepeating("Move", 0.3f, 0.05f);
      lose = false;
      persistentScript = GameObject.Find("Persistent Object").GetComponent<PersistentData>();
      persistentScript.score = 0;
      snakeSkin.color = persistentScript.snakeColor;
      tailPrefab.GetComponent<SpriteRenderer>().color = persistentScript.bodyColor;
      sliderFill.GetComponent<Image>().color = sliderColor;
    }

    void Update() {
      if (Input.GetKeyDown("w") && (currentDir != Vector2.up) && (currentDir != Vector2.down) && (canTurn)) {
        currentDir = Vector2.up;
        canTurn = false;
      } else if (Input.GetKeyDown("s") && (currentDir != Vector2.up) && (currentDir != Vector2.down) && (canTurn)) {
        currentDir = -Vector2.up;
        canTurn = false;
      } else if (Input.GetKeyDown("d") && (currentDir != Vector2.right) && (currentDir != Vector2.left) && (canTurn)) {
        currentDir = Vector2.right;
        canTurn = false;
      } else if (Input.GetKeyDown("a") && (currentDir != Vector2.right) && (currentDir != Vector2.left) && (canTurn)) {
        currentDir = -Vector2.right;
        canTurn = false;
      }

      if (!canTurn) {
        moveTimer += Time.deltaTime;
      }
      if (moveTimer > .1) {
        canTurn = true;
        moveTimer = 0f;
      }

      if (persistentScript.purchasedItems[4]) {
        ghostSlider.value = ghostModeTimer;
        if (Input.GetKeyDown("space") && (ghostModeReady)) {
          ghostModeOn = true;
          ghostModeReady = false;
        }
        if (ghostModeOn) {
          ghostModeTimer -= Time.deltaTime;
          if (ghostModeTimer < 0) {
            ghostModeOn = false;
            sliderColor = new Color(0f, 0f, 0f, 1f);
            sliderFill.GetComponent<Image>().color = sliderColor;
          }
        }
        if (!ghostModeReady) {
          ghostModeCooldown -= Time.deltaTime;
          if (ghostModeCooldown < 0) {
            sliderColor = new Color(239f/255f, 72f/255f, 100f/255f, 1f);
            sliderFill.GetComponent<Image>().color = sliderColor;
            ghostModeTimer = 3f;
            ghostModeCooldown = 10f;
            ghostModeReady = true;
          }
        }
      }

    }

    void OnTriggerEnter2D(Collider2D collision) {
      if (collision.tag == "food") {
        ate = true;
        persistentScript.FoodCollected();
        if (persistentScript.purchasedItems[5]) {
          crunchSound.Play();
        }
        Destroy(collision.gameObject);
      } if (((collision.tag == "wall") || ((collision.tag == "body") && (!ghostModeOn))) && (!lose)) {
        //wall or snake, go to game end screen
        lose = true;
        persistentScript.GameOver();
      }
    }

    void Move() {
      Vector2 gapPos = transform.position;
      transform.Translate(currentDir/2);

      if (ate) {
        for (int i=0; i<3; i++) {
          GameObject tailObj = Instantiate(tailPrefab, gapPos, Quaternion.identity);
          tail.Insert(0, tailObj.transform);
          //prevent collision with first piece of the tail
        }

        ate = false;
      }
      else if (tail.Count > 0) {
        tail.Last().position = gapPos;
        tail.Insert(0, tail.Last());
        //prevent collision with first piece of the tail
        tail.First().tag = "Untagged";
        tail.ElementAt(1).tag = "body";
        tail.ElementAt(2).tag = "body";

        tail.RemoveAt(tail.Count-1);
      }
    }

    // public void GetScore(int val) {
    //   val = score;
    //   Debug.Log("Val: " + val);
    // }
}
