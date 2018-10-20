using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //animations
    private Animator anim;
    private UiManager manager;
    private bool dodgeLeft, dodgeRight, atkRight, atkLeft, recovering, gotHit;
    private int Hp = 100;
    private int Stamina = 100;

    public int hp {
        get { return Hp; }
        set { Hp = value; }
    }
    public int stamina {
        get { return Stamina; }
        set { Stamina = value; }
    }
    public bool canMove;


    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();
        manager = GameObject.Find("Canvas").GetComponent<UiManager>();
        canMove = true;
    }


    // Update is called once per frame
    void Update() {

        dodgeLeft = false;
        dodgeRight = false;
        atkRight = false;
        atkLeft = false;

        if(canMove == true){
            if (stamina > 0){
                if (Input.GetKeyDown(KeyCode.A)) {
                    dodgeLeft = true;
                    stamina -= 10;
                }
                if (Input.GetKeyDown(KeyCode.D)) {
                    dodgeRight = true;
                    stamina -= 10;
                }
            }

            if (Input.GetKeyDown(KeyCode.E)) {
                atkRight = true;
            }
            if (Input.GetKeyDown(KeyCode.Q)) {
                atkLeft = true;
            }
        }
        
        if (stamina <= 0 && recovering == false) {
            recovering = true;
            StartCoroutine(RecoverStamina());
        }

        anim.SetBool("dodgeLeft", dodgeLeft);
        anim.SetBool("dodgeRight", dodgeRight);
        anim.SetBool("atkRight", atkRight);
        anim.SetBool("atkLeft", atkLeft);

    }


    public void LoseHP() {
        hp -= 10;
        gotHit = true;
        anim.SetBool("getHit", gotHit);
        StartCoroutine(Idle());
        if (hp <= 0) {
            Die();
        }
    }


    public void Die() {
        manager.winOpponent = true;
        canMove = false;
    }


    private IEnumerator RecoverStamina() {
        yield return new WaitForSeconds(2);
        stamina = 100;
        recovering = false;
    }


    private IEnumerator Idle() {
        yield return new WaitForSeconds(1);
        gotHit = false;
        anim.SetBool("getHit", gotHit);
    }
}
