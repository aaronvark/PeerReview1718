using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour {

    public Rigidbody Body;
    private Animator Anim;

    public GameObject weapon;
    public GameObject Enemy;
    

    private void Awake() {
        // add common components
        Body = gameObject.AddComponent<Rigidbody>();
        Anim = GetComponent<Animator>();
        // Set common sprite

    }
}
