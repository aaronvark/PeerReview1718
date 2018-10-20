using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haduoken : MonoBehaviour {

    
    private float speed;
    private Animator anim;
    
    public Player player { get; set; }

    public bool towardsPlayer { get; set; }


    private void Start() {

        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("PlayerScriptHolder").GetComponent<Player>();
        towardsPlayer = true;
        speed = Random.Range(6, 10);
    }


    // Update is called once per frame
    void Update() {

        if (!towardsPlayer){
            GetComponent<Rigidbody2D>().velocity = transform.up * speed;
            anim.SetBool("GoingBack", true);
        }
        else{
            GetComponent<Rigidbody2D>().velocity = transform.up * -speed;
        }
    }


    public void OnTriggerEnter2D(Collider2D col) {

        if (col.gameObject.tag == "Player") {
            player.LoseHP();
            Destroy(gameObject);
        }

        if (col.gameObject.tag == "PlayerHit") {
            towardsPlayer = false;
        }

        if (col.gameObject.tag == "Opponent" || col.gameObject.tag == "Stop") {
            Destroy(gameObject);
        }
    }
}
