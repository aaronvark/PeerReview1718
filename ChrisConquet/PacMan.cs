using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMan : MonoBehaviour {


    private Rigidbody2D MyRigidbody;
    private Collider2D MyCollider;

    private float Speed;
    private float OtherSpeed;
   
    


	// Use this for initialization
	void Start () {
        MyRigidbody = GetComponent<Rigidbody2D>();
        MyCollider = GetComponent<Collider2D>();
        Speed = 2f;
        OtherSpeed = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
	}


    void Movement() {

        if (Input.GetKeyDown(KeyCode.A)) { //LEFT
            MyRigidbody.velocity = new Vector2(-1* Speed, OtherSpeed);
        }
        if (Input.GetKeyDown(KeyCode.D)) { //RIGHT
            MyRigidbody.velocity = new Vector2(1* Speed, OtherSpeed);
        }
        if (Input.GetKeyDown(KeyCode.W)) {  //UP
            MyRigidbody.velocity = new Vector2(OtherSpeed, 1* Speed);
        }
        if (Input.GetKeyDown(KeyCode.S)) { //DOWN
            MyRigidbody.velocity = new Vector2(OtherSpeed,  -1* Speed);
        }
    }

}
