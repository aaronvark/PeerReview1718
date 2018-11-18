using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public Rigidbody Body;
    private Animator Anim;

    public int walkSpeed;
    public int runSpeed;

    public int playerHealth;

    //Public Vars
    public Camera _camera;
    public float speed;

    public GameObject deathParticle;
    public GameObject weapon;


    public delegate void PlayerDeath();
    public static event PlayerDeath playerDied;

    private void Awake() {
        OnHitPlayer.SendHit += TakeDamage;
    }

    void Start() {
        Body = GetComponent<Rigidbody>();
        Anim = GetComponentInChildren<Animator>();
    }


    void FixedUpdate() {
        if (playerHealth > 0 ) {
            Movement();
            Animation();
        }
    }

    // Update is called once per frame
    void Update () {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float distance;
        if ( plane.Raycast(ray, out distance) ) {
            Vector3 target = ray.GetPoint(distance);
            Vector3 direction = target - weapon.transform.position;
            float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            weapon.transform.rotation = Quaternion.Euler(0, rotation, 0);
        }

        if ( playerHealth < 0 ) {
            Death();
        }
    }


    private void TakeDamage(int hitDamage) {
        playerHealth -= hitDamage;
    }


    void Movement() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        Body.AddForce(movement * walkSpeed);

        if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) {
            Body.AddForce(movement * (walkSpeed + runSpeed));
        }
    }


    void Animation() {

        //Animations triggers for walking directions
        if (Input.GetKeyDown(KeyCode.W)) Anim.SetBool("walkFront", true);
        if (Input.GetKeyUp(KeyCode.W)) Anim.SetBool("walkFront", false);

        if (Input.GetKeyDown(KeyCode.S)) Anim.SetBool("walkBack", true);
        if (Input.GetKeyUp(KeyCode.S)) Anim.SetBool("walkBack", false);

        if (Input.GetKeyDown(KeyCode.A)) Anim.SetBool("walkLeft", true);
        if (Input.GetKeyUp(KeyCode.A)) Anim.SetBool("walkLeft", false);

        if (Input.GetKeyDown(KeyCode.D)) Anim.SetBool("walkRight", true);
        if (Input.GetKeyUp(KeyCode.D)) Anim.SetBool("walkRight", false);

        //Play deth animation

    }


    void Death() {
        Debug.Log("dies");
        Instantiate(deathParticle, gameObject.transform.position, Quaternion.identity, deathParticle.transform.parent = null);
        OnHitPlayer.SendHit -= TakeDamage;
        playerDied();
    }
}

