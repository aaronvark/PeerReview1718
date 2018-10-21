using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ufo : MonoBehaviour {

    public bool disabled; //true when currently disable
    public int points;

    [SerializeField]
    private float speed;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Vector2 direction;
    [SerializeField]
    private SpriteRenderer SR;
    [SerializeField]
    private Collider2D col;
    [SerializeField]
    private GameObject laser;
    [SerializeField]
    private float laserSpeed;
    [SerializeField]
    private float shootingDelay; // time between shots in seconds
    [SerializeField]
    private float lastTimeShot = 0;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float timeBeforeRespawn;
    [SerializeField]
    private Transform startPosition;


    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag("Player").transform;

        NewLevel();
    }
	

	// Update is called once per frame
	void Update () {

        if (disabled) {


            return;
        }
		if(Time.time > lastTimeShot + shootingDelay) {
            //shoot
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

            //Make laser bullet
            GameObject newLaser = Instantiate(laser, transform.position, q);

            newLaser.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2 (0f, laserSpeed));
            lastTimeShot = Time.time;

            Destroy(newLaser, 3.5f);
        }
	}


    void FixedUpdate() {

        if (disabled) {
            return;
        }
        //Figure out wich way the player is
        direction = player.position - transform.position;
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }


    public void NewLevel() {

        Disable();
        timeBeforeRespawn = Random.Range(4f, 14f);
        Invoke("Enable", timeBeforeRespawn);
    }


    void Enable() {
        // Move to start psoition
        transform.position = startPosition.position;
        //turn on collider and sprite
        col.enabled = true;
        SR.enabled = true;
        disabled = false;
    }


    public void Disable() {
        //turn off collider and sprite
        col.enabled = false;
        SR.enabled = false;
        disabled = true;

    }


    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Bullet")) {
            //Score points
            player.SendMessage("ScorePoints", points);

            //Destroy Ufo
            Disable();
        }
    }


    void OnCollisionEnter2D(Collision2D col) {
        
        if (col.transform.CompareTag("Player")) {
            //Destroy Ufo
            Disable();
        }
    }
}
