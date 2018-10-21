using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PacMan : MonoBehaviour {

    private Rigidbody2D MyRigidbody;
    private Collider2D MyCollider;
    private Animator MyAnimator;
    private float Speed;
    private float OtherSpeed;
    private bool isLeft;
    private bool isRight;
    private bool isUp;
    private bool isDown;
    private int points = 0;

    public Text ScoreText;


	void Start () {
        MyRigidbody = GetComponent<Rigidbody2D>();
        MyCollider = GetComponent<Collider2D>();
        MyAnimator = GetComponent<Animator>();

        MyAnimator.SetBool("idle-State", false);

        Speed = 5f;
        OtherSpeed = 0f;
	}
	

	void Update () {
        Movement();
        ShowText();
    }


    private void ShowText() {

        ScoreText.text = "Score: " + points.ToString();
    }


    private void OnTriggerEnter2D(Collider2D collision) {

        //Collecting coins
        if (collision.gameObject.tag == "Point") {
            points = points + 1;
            Destroy(collision.gameObject);
        }

        //Going trouh Right portal 
        if (collision.gameObject.tag == "Portal1") {
            transform.position = new Vector3(-92f, transform.position.y, transform.position.z);
        }

        //Going trouh Left portal 2
        if (collision.gameObject.tag == "Portal2")
        {
            transform.position = new Vector3(92f, transform.position.y, transform.position.z);
        }
    }


    private void Movement() {

        if (Input.GetKeyDown(KeyCode.A)) { //LEFT
            MyRigidbody.velocity = new Vector2(-1 * Speed, OtherSpeed);
            MyAnimator.SetBool("Move_Left", true);
            MyAnimator.SetBool("Move_Right", false);
            MyAnimator.SetBool("Move_Up", false);
            MyAnimator.SetBool("Move_Down", false);

        }


        if (Input.GetKeyDown(KeyCode.D)) { //RIGHT
            MyRigidbody.velocity = new Vector2(1 * Speed, OtherSpeed);
            MyAnimator.SetBool("Move_Right", true);
            MyAnimator.SetBool("Move_Left", false);
            MyAnimator.SetBool("Move_Up", false);
            MyAnimator.SetBool("Move_Down", false);
        }
        

        if (Input.GetKeyDown(KeyCode.W)) {  //UP
            MyRigidbody.velocity = new Vector2(OtherSpeed, 1 * Speed);
            MyAnimator.SetBool("Move_Up", true);
            MyAnimator.SetBool("Move_Right", false);
            MyAnimator.SetBool("Move_Left", false);
            MyAnimator.SetBool("Move_Down", false);
        }
        

        if (Input.GetKeyDown(KeyCode.S)) { //DOWN
            MyRigidbody.velocity = new Vector2(OtherSpeed, -1 * Speed);
            MyAnimator.SetBool("Move_Down", true);
            MyAnimator.SetBool("Move_Right", false);
            MyAnimator.SetBool("Move_Left", false);
            MyAnimator.SetBool("Move_Up", false);
        }
        
    }

}
